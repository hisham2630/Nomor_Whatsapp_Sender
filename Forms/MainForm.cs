using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;
using Nomor_Whatsapp_Sender.Models;
using Nomor_Whatsapp_Sender.Services;
using Nomor_Whatsapp_Sender.Theme;

namespace Nomor_Whatsapp_Sender
{
    public partial class MainForm : Form
    {
        private DataTable dataTable = new DataTable();
        private CancellationTokenSource? _cancellationTokenSource;
        private System.Windows.Forms.Timer schedulerTimer = null!;
        private List<DayOfWeek> selectedDays = new List<DayOfWeek>();
        private bool isSchedulerRunning = false;

        private readonly SasService _sasService = new SasService();
        private readonly WhatsAppService _whatsAppService = new WhatsAppService();

        // Day mapping for CheckedListBox (matches Items order)
        private static readonly DayOfWeek[] DayMapping = new[]
        {
            DayOfWeek.Saturday,
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday
        };

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            ThemeManager.ApplyTheme(this);

            // Style specific buttons
            ThemeManager.StyleButton(buttonStartScheduler, isPrimary: true);
            ThemeManager.StyleButton(buttonStopScheduler, isDanger: true);

            CredentialManager.Load();
            comboBoxExpirationStatus.SelectedIndex = Properties.Settings.Default.ExpirationStatus;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            schedulerTimer = new System.Windows.Forms.Timer();
            schedulerTimer.Tick += SchedulerTimer_Tick;

            LoadSelectedDaysFromSettings();
            LoadSchedulerStateFromSettings();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            dataGridViewResults.DataError += dataGridViewResults_DataError;

            // Load API settings
            textBoxApiUrl.Text = Properties.Settings.Default.WhatsAppApiUrl;
            textBoxLocationCoords.Text = Properties.Settings.Default.LocationCoords;
            checkBoxSendLocation.Checked = Properties.Settings.Default.SendLocation;
        }

        private async void buttonFetchData_Click(object? sender, EventArgs e)
        {
            string selectedStatus = string.Empty;
            this.Invoke(new MethodInvoker(delegate {
                if (comboBoxExpirationStatus.SelectedItem != null)
                {
                    selectedStatus = comboBoxExpirationStatus.SelectedItem?.ToString() ?? string.Empty;
                }
                else
                {
                    MessageBox.Show("Please select a status from the combo box.");
                    return;
                }
            }));

            // Show loading state
            this.Invoke((Action)(() =>
            {
                toolStripStatusLabelSuccessFail.Text = "⏳ Fetching data...";
                toolStripProgressBar.Style = ProgressBarStyle.Marquee;
                toolStripMain.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
            }));

            try
            {
                var users = await _sasService.GetExpiringUsersAsync(selectedStatus);
                if (users == null || !users.Any())
                {
                    MessageBox.Show("No data received or data is null.");
                    return;
                }

                if (dataTable != null)
                {
                    dataTable.Clear();
                }
                else
                {
                    dataTable = new DataTable();
                }

                InitializeDataTable();

                foreach (var user in users)
                {
                    dataTable.Rows.Add(
                        user.Id,
                        user.Username ?? string.Empty,
                        user.Firstname ?? string.Empty,
                        user.Lastname ?? string.Empty,
                        user.Phone ?? string.Empty,
                        user.Expiration ?? string.Empty,
                        user.Last_Online ?? string.Empty,
                        user.Notes ?? string.Empty,
                        user.Remaining_days,
                        user.Parent_Username ?? string.Empty,
                        user.Profile_Details?.Name ?? string.Empty,
                        string.Empty
                    );
                }

                dataGridViewResults.Invoke((Action)(() =>
                {
                    dataGridViewResults.DataSource = dataTable;

                    // Auto-fit columns to content first
                    dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridViewResults.AutoResizeColumns();

                    // Then switch to manual so user can resize
                    dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    foreach (DataGridViewColumn column in dataGridViewResults.Columns)
                    {
                        column.Resizable = DataGridViewTriState.True;
                        column.SortMode = DataGridViewColumnSortMode.Programmatic;
                        if (column.Width < 60) column.Width = 60;
                    }

                    // Give wider columns to Phone and Notes
                    if (dataGridViewResults.Columns.Contains("Phone"))
                        dataGridViewResults.Columns["Phone"]!.MinimumWidth = 200;
                    if (dataGridViewResults.Columns.Contains("Notes"))
                        dataGridViewResults.Columns["Notes"]!.MinimumWidth = 120;
                }));

                UpdateUserCountLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                // Restore normal state
                this.Invoke((Action)(() =>
                {
                    toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                    toolStripProgressBar.Value = 0;
                    toolStripStatusLabelSuccessFail.Text = "Ready";
                    toolStripMain.Enabled = true;
                    this.Cursor = Cursors.Default;
                }));
            }
        }

        private void InitializeDataTable()
        {
            if (dataTable.Columns.Count == 0)
            {
                dataTable.Columns.Add("Id", typeof(int));
                dataTable.Columns.Add("Username", typeof(string));
                dataTable.Columns.Add("Firstname", typeof(string));
                dataTable.Columns.Add("Lastname", typeof(string));
                dataTable.Columns.Add("Phone", typeof(string));
                dataTable.Columns.Add("Expiration", typeof(string));
                dataTable.Columns.Add("Last_Online", typeof(string));
                dataTable.Columns.Add("Notes", typeof(string));
                dataTable.Columns.Add("Remaining_days", typeof(int));
                dataTable.Columns.Add("Parent_Username", typeof(string));
                dataTable.Columns.Add("ProfileName", typeof(string));
                dataTable.Columns.Add("SentStatus", typeof(string));
            }
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            buttonFetchData_Click(sender, e);
            dataGridViewResults.AllowUserToResizeColumns = true;
            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;

            foreach (DataGridViewColumn column in dataGridViewResults.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            dataGridViewResults.ColumnHeaderMouseClick += dataGridViewResults_ColumnHeaderMouseClick;
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IsSchedulerRunning = isSchedulerRunning;
            Properties.Settings.Default.NextScheduledTime = CalculateNextScheduledTime();
            Properties.Settings.Default.WhatsAppApiUrl = textBoxApiUrl.Text;
            Properties.Settings.Default.LocationCoords = textBoxLocationCoords.Text;
            Properties.Settings.Default.SendLocation = checkBoxSendLocation.Checked;
            Properties.Settings.Default.Save();
        }

        private void textBoxSearch_TextChanged(object? sender, EventArgs e)
        {
            string searchQuery = textBoxSearch.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    row.DefaultCellStyle.BackColor = ThemeManager.Surface;
                }
                else
                {
                    bool matchFound = false;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString()?.ToLower().Contains(searchQuery) == true)
                        {
                            matchFound = true;
                            break;
                        }
                    }

                    row.DefaultCellStyle.BackColor = matchFound ? ThemeManager.RowHighlight : ThemeManager.Surface;
                }
            }
        }

        private void dataGridViewResults_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            var column = dataGridViewResults.Columns[e.ColumnIndex];
            if (column.SortMode != DataGridViewColumnSortMode.Programmatic)
                return;

            var sortGlyph = column.HeaderCell.SortGlyphDirection;
            switch (sortGlyph)
            {
                case SortOrder.None:
                case SortOrder.Ascending:
                    dataGridViewResults.Sort(column, ListSortDirection.Descending);
                    column.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    break;
                case SortOrder.Descending:
                    dataGridViewResults.Sort(column, ListSortDirection.Ascending);
                    column.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    break;
            }
        }

        private void buttonOpenSenderForm_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل تريد ارسال رسالة لارقام الاكسباير؟", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            List<string> phoneNumbers = new List<string>();

            if (result == DialogResult.Yes)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string phone = row["Phone"]?.ToString() ?? string.Empty;
                    phoneNumbers.AddRange(phone.Split(new[] { '-', '/', ' ', '\\', '_' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            else
            {
                phoneNumbers = new List<string>();
            }

            PhoneSenderForm senderForm = new PhoneSenderForm(phoneNumbers);
            if (result == DialogResult.No)
            {
                senderForm.checkBoxCustomNumbers.Checked = true;
            }
            senderForm.ShowDialog();
        }

        private List<string> GetPhoneNumbersFromDataTable()
        {
            List<string> phoneNumbers = new List<string>();
            DataTable snapshot = dataTable.Copy();

            foreach (DataRow row in snapshot.Rows)
            {
                if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached)
                {
                    continue;
                }

                string phone = row["Phone"]?.ToString() ?? string.Empty;
                List<string> numbers = Regex.Split(phone, @"[ .\/\\-_?]")
                                            .SelectMany(part => Regex.Split(part, @"-"))
                                            .Where(n => !string.IsNullOrWhiteSpace(n))
                                            .Select(n => n.TrimStart('+'))
                                            .ToList();

                Debug.WriteLine($"Row {row["Id"]}: Original Phone: {phone}, Split Numbers: {string.Join(", ", numbers)}");
                phoneNumbers.AddRange(numbers);
            }

            return phoneNumbers;
        }

        private async void buttonSendMessage_Click(object? sender, EventArgs e, bool autoSend = false)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;

            if (!autoSend)
            {
                using (MessageForm messageForm = new MessageForm())
                {
                    if (messageForm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            string templateActive = Properties.Settings.Default.TemplateActive;
            string templateExpired = Properties.Settings.Default.TemplateExpired;
            List<string> phoneNumbers = GetPhoneNumbersFromDataTable();

            if (!dataTable.Columns.Contains("SentStatus"))
            {
                dataTable.Columns.Add("SentStatus", typeof(string));
            }

            foreach (DataRow row in dataTable.Rows)
            {
                row["SentStatus"] = string.Empty;
                row["Phone"] = (row["Phone"]?.ToString() ?? string.Empty).Replace("✔️", "").Replace("❌", "").Trim();
            }

            if (dataGridViewResults != null)
            {
                dataGridViewResults.Invoke((Action)(() =>
                {
                    dataGridViewResults.DataSource = null;
                    dataGridViewResults.DataSource = dataTable;
                }));
            }

            int totalNumbers = phoneNumbers.Count;
            int currentNumber = 0;
            int successCount = 0;
            int failureCount = 0;

            try
            {
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    DataRow row = dataTable.Rows[rowIndex];

                    if (token.IsCancellationRequested)
                    {
                        MessageBox.Show("Sending canceled.");
                        break;
                    }

                    HighlightAndScrollToRow(rowIndex, ThemeManager.RowHighlight);

                    string phone = row["Phone"]?.ToString() ?? string.Empty;
                    List<string> numbers = Regex.Split(phone, @"[ .\/\\-_?]")
                        .SelectMany(part => Regex.Split(part, @"-"))
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Select(n => n.TrimStart('+'))
                        .ToList();

                    if (numbers.Count == 0)
                    {
                        row["SentStatus"] = "X";
                        failureCount++;
                    }
                    else
                    {
                        bool sent = false;
                        List<string> statusList = new List<string>();

                        foreach (string number in numbers)
                        {
                            if (token.IsCancellationRequested)
                            {
                                MessageBox.Show("Sending canceled.");
                                break;
                            }

                            if (number.Contains('?'))
                            {
                                statusList.Add("❌");
                                failureCount++;
                                continue;
                            }

                            if (number.Count(c => c == '0') > 1)
                            {
                                statusList.Add("❌");
                                failureCount++;
                                continue;
                            }

                            int remainingDays = row["Remaining_days"] is int rd ? rd : 0;
                            string selectedTemplate = remainingDays > 0 ? templateActive : templateExpired;
                            string message = ReplacePlaceholders(selectedTemplate, row);
                            string? location = null;
                            if (checkBoxSendLocation.Checked && !string.IsNullOrWhiteSpace(textBoxLocationCoords.Text))
                            {
                                location = textBoxLocationCoords.Text.Trim();
                            }
                            bool success = await SendWhatsAppMessage(number, message, location);
                            sent |= success;

                            if (success)
                            {
                                successCount++;
                                statusList.Add("✔️");
                            }
                            else
                            {
                                failureCount++;
                                statusList.Add("❌");
                            }

                            await Task.Delay(1000, token);

                            currentNumber++;
                            UpdateProgressAndLabels(currentNumber, totalNumbers, successCount, failureCount);
                        }

                        row["SentStatus"] = sent ? "✔️" : statusList.All(status => status == "❌") ? "invalid_phone_number" : "✔️";
                        row["Phone"] = string.Join(" ", numbers.Zip(statusList, (number, status) => $"{status} {number}"));
                    }

                    HighlightAndScrollToRow(rowIndex, ThemeManager.Surface);
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Sending canceled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                ResetProgressAndLabels();
                UpdateUserCountLabel();
            }
        }

        private void dataGridViewResults_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
            MessageBox.Show($"Data error: {e.Exception?.Message ?? "Unknown error"}");
        }

        private void buttonSendMessage_Click(object? sender, EventArgs e)
        {
            buttonSendMessage_Click(sender, e, false);
        }

        private string ReplacePlaceholders(string messageTemplate, DataRow row)
        {
            string customerName = row["Firstname"]?.ToString() ?? string.Empty;
            string expirationRaw = row["Expiration"]?.ToString() ?? string.Empty;
            string bundleName = row["ProfileName"]?.ToString() ?? string.Empty;

            DateTime expirationDate = DateTime.TryParse(expirationRaw, out var parsed) ? parsed : DateTime.Now;
            string expiration = "\u200E" + expirationDate.ToString("yyyy-MM-dd h:mm tt") + "\u200E";
            TimeSpan remainingTime = expirationDate - DateTime.Now;

            int days = Math.Abs(remainingTime.Days);
            int hours = Math.Abs(remainingTime.Hours);
            int minutes = Math.Abs(remainingTime.Minutes);

            string message = messageTemplate
                .Replace("%CustomerName%", customerName)
                .Replace("%Expiration%", expiration)
                .Replace("%BundleName%", bundleName)
                .Replace("%يوم%", days.ToString())
                .Replace("%ساعة%", hours.ToString())
                .Replace("%دقيقة%", minutes.ToString());

            return message;
        }

        private async Task<bool> SendWhatsAppMessage(string phoneNumber, string message, string? location = null)
        {
            return await _whatsAppService.SendMessageAsync(phoneNumber, message, location);
        }

        private void textBoxApiUrl_TextChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.WhatsAppApiUrl = textBoxApiUrl.Text;
            Properties.Settings.Default.Save();
        }

        private void textBoxLocationCoords_TextChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.LocationCoords = textBoxLocationCoords.Text;
            Properties.Settings.Default.Save();
        }

        private void checkBoxSendLocation_CheckedChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.SendLocation = checkBoxSendLocation.Checked;
            Properties.Settings.Default.Save();
        }

        private async void SchedulerTimer_Tick(object? sender, EventArgs e)
        {
            if (selectedDays.Contains(DateTime.Now.DayOfWeek))
            {
                await Task.Run(() =>
                {
                    buttonFetchData_Click(sender, e);
                    buttonSendMessage_Click(sender, e, true);
                });
            }

            DateTime nextScheduledTime = CalculateNextScheduledTime();
            TimeSpan timeUntilScheduled = nextScheduledTime - DateTime.Now;

            schedulerTimer.Interval = (int)timeUntilScheduled.TotalMilliseconds;
            schedulerTimer.Start();

            labelSchedulerStatus.Invoke((Action)(() =>
            {
                labelSchedulerStatus.Text = $"Next: {nextScheduledTime:g}";
            }));
        }

        private void buttonStartScheduler_Click(object? sender, EventArgs e)
        {
            if (isSchedulerRunning)
            {
                MessageBox.Show("Scheduler is already running.");
                return;
            }

            DateTime nextScheduledTime = CalculateNextScheduledTime();

            if (nextScheduledTime != DateTime.MinValue)
            {
                TimeSpan timeUntilScheduled = nextScheduledTime - DateTime.Now;
                schedulerTimer.Interval = (int)timeUntilScheduled.TotalMilliseconds;
                schedulerTimer.Start();
                labelSchedulerStatus.Text = $"Next: {nextScheduledTime:g}";
                MessageBox.Show("Scheduler started.");
                isSchedulerRunning = true;
            }
            else
            {
                MessageBox.Show("No valid scheduled days selected.");
            }
        }

        private DateTime CalculateNextScheduledTime()
        {
            DateTime now = DateTime.Now;
            DateTime nextScheduledTime = now.Date + timePicker.Value.TimeOfDay;

            if (nextScheduledTime <= now)
            {
                nextScheduledTime = nextScheduledTime.AddDays(1);
            }

            for (int i = 0; i < 7; i++)
            {
                if (selectedDays.Contains(nextScheduledTime.DayOfWeek))
                {
                    return nextScheduledTime;
                }
                nextScheduledTime = nextScheduledTime.AddDays(1);
            }

            return DateTime.MinValue;
        }

        private void buttonStopScheduler_Click(object? sender, EventArgs e)
        {
            schedulerTimer.Stop();
            labelSchedulerStatus.Text = "Stopped";
            MessageBox.Show("Scheduler stopped.");
            isSchedulerRunning = false;
        }

        // ── CheckedListBox Days ─────────────────────────────────

        private void checkedListBoxDays_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            DayOfWeek day = DayMapping[e.Index];

            if (e.NewValue == CheckState.Checked)
            {
                if (!selectedDays.Contains(day))
                {
                    selectedDays.Add(day);
                }
            }
            else
            {
                selectedDays.Remove(day);
            }

            // Defer save to after the check state is applied
            this.BeginInvoke(new Action(SaveSelectedDaysToSettings));
        }

        private void SaveSelectedDaysToSettings()
        {
            string selectedDaysString = string.Join(",", selectedDays.Select(d => d.ToString()));
            Properties.Settings.Default.SelectedDays = selectedDaysString;
            Properties.Settings.Default.Save();
        }

        private void LoadSelectedDaysFromSettings()
        {
            string selectedDaysString = Properties.Settings.Default.SelectedDays;
            if (!string.IsNullOrEmpty(selectedDaysString))
            {
                selectedDays = selectedDaysString.Split(',').Select(d => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), d)).ToList();
                UpdateCheckedListBox();
            }
        }

        private void UpdateCheckedListBox()
        {
            for (int i = 0; i < DayMapping.Length; i++)
            {
                checkedListBoxDays.SetItemChecked(i, selectedDays.Contains(DayMapping[i]));
            }
        }

        private void buttonCancelSending_Click(object? sender, EventArgs e)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private void buttonExportToExcel_Click(object? sender, EventArgs e)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");
                            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true, TableStyles.Medium9);
                            FileInfo excelFile = new FileInfo(filePath);
                            package.SaveAs(excelFile);
                            MessageBox.Show("Data exported successfully.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting data: {ex.Message}");
                    }
                }
            }
        }

        private void LoadSchedulerStateFromSettings()
        {
            isSchedulerRunning = Properties.Settings.Default.IsSchedulerRunning;
            DateTime nextScheduledTime = Properties.Settings.Default.NextScheduledTime;

            if (isSchedulerRunning && nextScheduledTime > DateTime.Now)
            {
                TimeSpan timeUntilScheduled = nextScheduledTime - DateTime.Now;
                schedulerTimer.Interval = (int)timeUntilScheduled.TotalMilliseconds;
                schedulerTimer.Start();
                labelSchedulerStatus.Text = $"Next: {nextScheduledTime:g}";
            }
            else
            {
                labelSchedulerStatus.Text = "Stopped";
            }
        }

        private void comboBoxExpirationStatus_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.ExpirationStatus = comboBoxExpirationStatus.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void UpdateUserCountLabel()
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelUserCount.Text = $"Users: {dataTable?.Rows.Count ?? 0}";
            });
        }

        private void HighlightAndScrollToRow(int rowIndex, Color color)
        {
            dataGridViewResults.Invoke((Action)(() =>
            {
                if (rowIndex >= 0 && rowIndex < dataGridViewResults.Rows.Count)
                {
                    dataGridViewResults.Rows[rowIndex].DefaultCellStyle.BackColor = color;
                    dataGridViewResults.FirstDisplayedScrollingRowIndex = rowIndex;
                    dataGridViewResults.Refresh();
                }
            }));
        }

        private void UpdateProgressAndLabels(int currentNumber, int totalNumbers, int successCount, int failureCount)
        {
            int progressValue = (int)Math.Min(((double)currentNumber / totalNumbers * 100), 100);

            this.Invoke((Action)(() =>
            {
                try
                {
                    toolStripProgressBar.Value = progressValue;
                }
                catch (ArgumentOutOfRangeException)
                {
                    toolStripProgressBar.Value = toolStripProgressBar.Maximum;
                }

                toolStripStatusLabelProgress.Text = $"Sending... {currentNumber}/{totalNumbers}";
                toolStripStatusLabelSuccessFail.Text = $"✔ {successCount}  ✕ {failureCount}";
            }));
        }

        private void ResetProgressAndLabels()
        {
            this.Invoke((Action)(() =>
            {
                toolStripProgressBar.Value = 0;
                toolStripStatusLabelProgress.Text = "";
                toolStripStatusLabelSuccessFail.Text = "Ready";
            }));
        }

        private void buttonRemoveRows_Click(object? sender, EventArgs e)
        {
            if (dataGridViewResults.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to remove {dataGridViewResults.SelectedRows.Count} selected row(s)?",
                    "Confirm Removal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    RemoveSelectedRows();
                }
            }
            else
            {
                MessageBox.Show("Please select at least one row to remove.", "No Rows Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoveSelectedRows()
        {
            var selectedRowIndices = new List<int>();
            foreach (DataGridViewRow row in dataGridViewResults.SelectedRows)
            {
                selectedRowIndices.Add(row.Index);
            }
            selectedRowIndices.Sort((a, b) => b.CompareTo(a));

            foreach (int index in selectedRowIndices)
            {
                dataTable.Rows.RemoveAt(index);
            }

            dataGridViewResults.DataSource = null;
            dataGridViewResults.DataSource = dataTable;

            UpdateUserCountLabel();

            MessageBox.Show($"{selectedRowIndices.Count} row(s) removed successfully.", "Rows Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonCredentials_Click(object? sender, EventArgs e)
        {
            using (var form = new CredentialsForm())
            {
                form.ShowDialog(this);
            }
        }
    }
}
