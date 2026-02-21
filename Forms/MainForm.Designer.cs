namespace Nomor_Whatsapp_Sender
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            toolStripMain = new ToolStrip();
            toolStripButtonFetch = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButtonSendMessage = new ToolStripButton();
            toolStripButtonOpenSender = new ToolStripButton();
            toolStripButtonCancelSending = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButtonExport = new ToolStripButton();
            toolStripButtonRemoveRows = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripButtonCredentials = new ToolStripButton();
            panelTopBar = new Panel();
            comboBoxExpirationStatus = new ComboBox();
            textBoxSearch = new TextBox();
            labelUserCount = new Label();
            panelApiSettings = new Panel();
            labelApiUrl = new Label();
            textBoxApiUrl = new TextBox();
            labelLocation = new Label();
            textBoxLocationCoords = new TextBox();
            checkBoxSendLocation = new CheckBox();
            groupBoxScheduler = new GroupBox();
            panelSchedulerLeft = new Panel();
            timePicker = new DateTimePicker();
            dateTimePicker = new DateTimePicker();
            buttonStartScheduler = new Button();
            buttonStopScheduler = new Button();
            labelSchedulerStatus = new Label();
            panelSchedulerRight = new Panel();
            checkedListBoxDays = new CheckedListBox();
            dataGridViewResults = new DataGridView();
            statusStripBottom = new StatusStrip();
            toolStripStatusLabelSuccessFail = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            toolStripStatusLabelProgress = new ToolStripStatusLabel();
            toolStripMain.SuspendLayout();
            panelTopBar.SuspendLayout();
            panelApiSettings.SuspendLayout();
            groupBoxScheduler.SuspendLayout();
            panelSchedulerLeft.SuspendLayout();
            panelSchedulerRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            statusStripBottom.SuspendLayout();
            SuspendLayout();
            //
            // toolStripMain
            //
            toolStripMain.Dock = DockStyle.Top;
            toolStripMain.Items.AddRange(new ToolStripItem[] {
                toolStripButtonFetch,
                toolStripSeparator1,
                toolStripButtonSendMessage,
                toolStripButtonOpenSender,
                toolStripButtonCancelSending,
                toolStripSeparator2,
                toolStripButtonExport,
                toolStripButtonRemoveRows,
                toolStripSeparator3,
                toolStripButtonCredentials
            });
            toolStripMain.Location = new Point(0, 0);
            toolStripMain.Name = "toolStripMain";
            toolStripMain.Size = new Size(1468, 38);
            toolStripMain.TabIndex = 0;
            //
            // toolStripButtonFetch
            //
            toolStripButtonFetch.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonFetch.Name = "toolStripButtonFetch";
            toolStripButtonFetch.Size = new Size(70, 35);
            toolStripButtonFetch.Text = "⟳ Fetch";
            toolStripButtonFetch.ToolTipText = "Fetch expiring users";
            toolStripButtonFetch.Click += buttonFetchData_Click;
            //
            // toolStripSeparator1
            //
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 38);
            //
            // toolStripButtonSendMessage
            //
            toolStripButtonSendMessage.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonSendMessage.Name = "toolStripButtonSendMessage";
            toolStripButtonSendMessage.Size = new Size(105, 35);
            toolStripButtonSendMessage.Text = "📨 Send Message";
            toolStripButtonSendMessage.ToolTipText = "Send message to all";
            toolStripButtonSendMessage.Click += buttonSendMessage_Click;
            //
            // toolStripButtonOpenSender
            //
            toolStripButtonOpenSender.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonOpenSender.Name = "toolStripButtonOpenSender";
            toolStripButtonOpenSender.Size = new Size(105, 35);
            toolStripButtonOpenSender.Text = "📱 Sender Form";
            toolStripButtonOpenSender.ToolTipText = "Open phone sender form";
            toolStripButtonOpenSender.Click += buttonOpenSenderForm_Click;
            //
            // toolStripButtonCancelSending
            //
            toolStripButtonCancelSending.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonCancelSending.Name = "toolStripButtonCancelSending";
            toolStripButtonCancelSending.Size = new Size(60, 35);
            toolStripButtonCancelSending.Text = "✕ Cancel";
            toolStripButtonCancelSending.ToolTipText = "Cancel sending";
            toolStripButtonCancelSending.Click += buttonCancelSending_Click;
            //
            // toolStripSeparator2
            //
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 38);
            //
            // toolStripButtonExport
            //
            toolStripButtonExport.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonExport.Name = "toolStripButtonExport";
            toolStripButtonExport.Size = new Size(80, 35);
            toolStripButtonExport.Text = "📊 Export";
            toolStripButtonExport.ToolTipText = "Export to Excel";
            toolStripButtonExport.Click += buttonExportToExcel_Click;
            //
            // toolStripButtonRemoveRows
            //
            toolStripButtonRemoveRows.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonRemoveRows.Name = "toolStripButtonRemoveRows";
            toolStripButtonRemoveRows.Size = new Size(100, 35);
            toolStripButtonRemoveRows.Text = "🗑 Remove Rows";
            toolStripButtonRemoveRows.ToolTipText = "Remove selected rows";
            toolStripButtonRemoveRows.Click += buttonRemoveRows_Click;
            //
            // toolStripSeparator3
            //
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 38);
            //
            // toolStripButtonCredentials
            //
            toolStripButtonCredentials.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonCredentials.Name = "toolStripButtonCredentials";
            toolStripButtonCredentials.Size = new Size(90, 35);
            toolStripButtonCredentials.Text = "🔑 Credentials";
            toolStripButtonCredentials.ToolTipText = "Manage SAS credentials";
            toolStripButtonCredentials.Click += buttonCredentials_Click;
            //
            // panelTopBar
            //
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Controls.Add(labelUserCount);
            panelTopBar.Controls.Add(textBoxSearch);
            panelTopBar.Controls.Add(comboBoxExpirationStatus);
            panelTopBar.Location = new Point(0, 38);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Padding = new Padding(12, 8, 12, 8);
            panelTopBar.Size = new Size(1468, 48);
            panelTopBar.TabIndex = 1;
            //
            // comboBoxExpirationStatus
            //
            comboBoxExpirationStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxExpirationStatus.FormattingEnabled = true;
            comboBoxExpirationStatus.Items.AddRange(new object[] { "expiring_today", "expiring_soon" });
            comboBoxExpirationStatus.Location = new Point(12, 12);
            comboBoxExpirationStatus.Name = "comboBoxExpirationStatus";
            comboBoxExpirationStatus.Size = new Size(180, 25);
            comboBoxExpirationStatus.TabIndex = 0;
            comboBoxExpirationStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            comboBoxExpirationStatus.SelectedIndexChanged += comboBoxExpirationStatus_SelectedIndexChanged;
            //
            // textBoxSearch
            //
            textBoxSearch.Location = new Point(210, 12);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(300, 25);
            textBoxSearch.TabIndex = 1;
            textBoxSearch.PlaceholderText = "🔍 Search...";
            textBoxSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            //
            // labelUserCount
            //
            labelUserCount.AutoSize = true;
            labelUserCount.Location = new Point(530, 15);
            labelUserCount.Name = "labelUserCount";
            labelUserCount.Size = new Size(90, 17);
            labelUserCount.TabIndex = 2;
            labelUserCount.Text = "Users: 0";
            labelUserCount.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            //
            // panelApiSettings
            //
            panelApiSettings.Dock = DockStyle.Top;
            panelApiSettings.Controls.Add(checkBoxSendLocation);
            panelApiSettings.Controls.Add(textBoxLocationCoords);
            panelApiSettings.Controls.Add(labelLocation);
            panelApiSettings.Controls.Add(textBoxApiUrl);
            panelApiSettings.Controls.Add(labelApiUrl);
            panelApiSettings.Location = new Point(0, 86);
            panelApiSettings.Name = "panelApiSettings";
            panelApiSettings.Padding = new Padding(12, 6, 12, 6);
            panelApiSettings.Size = new Size(1468, 40);
            panelApiSettings.TabIndex = 10;
            //
            // labelApiUrl
            //
            labelApiUrl.AutoSize = true;
            labelApiUrl.Location = new Point(12, 11);
            labelApiUrl.Name = "labelApiUrl";
            labelApiUrl.Size = new Size(55, 17);
            labelApiUrl.TabIndex = 0;
            labelApiUrl.Text = "API URL:";
            //
            // textBoxApiUrl
            //
            textBoxApiUrl.Location = new Point(75, 8);
            textBoxApiUrl.Name = "textBoxApiUrl";
            textBoxApiUrl.Size = new Size(500, 25);
            textBoxApiUrl.TabIndex = 1;
            textBoxApiUrl.PlaceholderText = "http://localhost:3111/send?number={phone}&message={message}";
            textBoxApiUrl.TextChanged += textBoxApiUrl_TextChanged;
            //
            // labelLocation
            //
            labelLocation.AutoSize = true;
            labelLocation.Location = new Point(590, 11);
            labelLocation.Name = "labelLocation";
            labelLocation.Size = new Size(60, 17);
            labelLocation.TabIndex = 2;
            labelLocation.Text = "📍 Coords:";
            //
            // textBoxLocationCoords
            //
            textBoxLocationCoords.Location = new Point(660, 8);
            textBoxLocationCoords.Name = "textBoxLocationCoords";
            textBoxLocationCoords.Size = new Size(300, 25);
            textBoxLocationCoords.TabIndex = 3;
            textBoxLocationCoords.PlaceholderText = "33.353209, 44.412526";
            textBoxLocationCoords.TextChanged += textBoxLocationCoords_TextChanged;
            //
            // checkBoxSendLocation
            //
            checkBoxSendLocation.AutoSize = true;
            checkBoxSendLocation.Checked = true;
            checkBoxSendLocation.CheckState = CheckState.Checked;
            checkBoxSendLocation.Location = new Point(975, 10);
            checkBoxSendLocation.Name = "checkBoxSendLocation";
            checkBoxSendLocation.Size = new Size(120, 19);
            checkBoxSendLocation.TabIndex = 4;
            checkBoxSendLocation.Text = "Send Location";
            checkBoxSendLocation.CheckedChanged += checkBoxSendLocation_CheckedChanged;
            //
            // groupBoxScheduler
            //
            groupBoxScheduler.Dock = DockStyle.Top;
            groupBoxScheduler.Controls.Add(panelSchedulerRight);
            groupBoxScheduler.Controls.Add(panelSchedulerLeft);
            groupBoxScheduler.Location = new Point(0, 126);
            groupBoxScheduler.Name = "groupBoxScheduler";
            groupBoxScheduler.Padding = new Padding(8, 4, 8, 4);
            groupBoxScheduler.Size = new Size(1468, 100);
            groupBoxScheduler.TabIndex = 2;
            groupBoxScheduler.TabStop = false;
            groupBoxScheduler.Text = "📅 Scheduler";
            //
            // panelSchedulerLeft
            //
            panelSchedulerLeft.Dock = DockStyle.Left;
            panelSchedulerLeft.Controls.Add(labelSchedulerStatus);
            panelSchedulerLeft.Controls.Add(buttonStopScheduler);
            panelSchedulerLeft.Controls.Add(buttonStartScheduler);
            panelSchedulerLeft.Controls.Add(dateTimePicker);
            panelSchedulerLeft.Controls.Add(timePicker);
            panelSchedulerLeft.Location = new Point(8, 20);
            panelSchedulerLeft.Name = "panelSchedulerLeft";
            panelSchedulerLeft.Size = new Size(760, 72);
            panelSchedulerLeft.TabIndex = 0;
            //
            // timePicker
            //
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.Location = new Point(4, 16);
            timePicker.Name = "timePicker";
            timePicker.ShowUpDown = true;
            timePicker.Size = new Size(110, 25);
            timePicker.TabIndex = 0;
            //
            // dateTimePicker
            //
            dateTimePicker.Location = new Point(124, 16);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 25);
            dateTimePicker.TabIndex = 1;
            //
            // buttonStartScheduler
            //
            buttonStartScheduler.Location = new Point(340, 12);
            buttonStartScheduler.Name = "buttonStartScheduler";
            buttonStartScheduler.Size = new Size(100, 30);
            buttonStartScheduler.TabIndex = 2;
            buttonStartScheduler.Text = "▶ Start";
            buttonStartScheduler.Click += buttonStartScheduler_Click;
            //
            // buttonStopScheduler
            //
            buttonStopScheduler.Location = new Point(450, 12);
            buttonStopScheduler.Name = "buttonStopScheduler";
            buttonStopScheduler.Size = new Size(100, 30);
            buttonStopScheduler.TabIndex = 3;
            buttonStopScheduler.Text = "■ Stop";
            buttonStopScheduler.Click += buttonStopScheduler_Click;
            //
            // labelSchedulerStatus
            //
            labelSchedulerStatus.AutoSize = true;
            labelSchedulerStatus.Location = new Point(560, 18);
            labelSchedulerStatus.Name = "labelSchedulerStatus";
            labelSchedulerStatus.Size = new Size(100, 17);
            labelSchedulerStatus.TabIndex = 4;
            labelSchedulerStatus.Text = "";
            //
            // panelSchedulerRight
            //
            panelSchedulerRight.Dock = DockStyle.Fill;
            panelSchedulerRight.Controls.Add(checkedListBoxDays);
            panelSchedulerRight.Location = new Point(768, 20);
            panelSchedulerRight.Name = "panelSchedulerRight";
            panelSchedulerRight.Size = new Size(692, 72);
            panelSchedulerRight.TabIndex = 1;
            //
            // checkedListBoxDays
            //
            checkedListBoxDays.CheckOnClick = true;
            checkedListBoxDays.ColumnWidth = 100;
            checkedListBoxDays.Dock = DockStyle.Fill;
            checkedListBoxDays.FormattingEnabled = true;
            checkedListBoxDays.Items.AddRange(new object[] { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" });
            checkedListBoxDays.ItemHeight = 22;
            checkedListBoxDays.Location = new Point(0, 0);
            checkedListBoxDays.MultiColumn = true;
            checkedListBoxDays.Name = "checkedListBoxDays";
            checkedListBoxDays.Size = new Size(692, 72);
            checkedListBoxDays.TabIndex = 0;
            checkedListBoxDays.ItemCheck += checkedListBoxDays_ItemCheck;
            //
            // dataGridViewResults
            //
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AllowUserToDeleteRows = false;
            dataGridViewResults.BorderStyle = BorderStyle.None;
            dataGridViewResults.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResults.Dock = DockStyle.Fill;
            dataGridViewResults.Location = new Point(0, 206);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.RowHeadersWidth = 40;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.Size = new Size(1468, 556);
            dataGridViewResults.TabIndex = 3;
            //
            // statusStripBottom
            //
            statusStripBottom.Dock = DockStyle.Bottom;
            statusStripBottom.Items.AddRange(new ToolStripItem[] {
                toolStripStatusLabelSuccessFail,
                toolStripProgressBar,
                toolStripStatusLabelProgress
            });
            statusStripBottom.Location = new Point(0, 762);
            statusStripBottom.Name = "statusStripBottom";
            statusStripBottom.Size = new Size(1468, 32);
            statusStripBottom.TabIndex = 4;
            //
            // toolStripStatusLabelSuccessFail
            //
            toolStripStatusLabelSuccessFail.Name = "toolStripStatusLabelSuccessFail";
            toolStripStatusLabelSuccessFail.Size = new Size(200, 35);
            toolStripStatusLabelSuccessFail.Text = "Ready";
            toolStripStatusLabelSuccessFail.TextAlign = ContentAlignment.MiddleLeft;
            //
            // toolStripProgressBar
            //
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new Size(400, 35);
            //
            // toolStripStatusLabelProgress
            //
            toolStripStatusLabelProgress.Name = "toolStripStatusLabelProgress";
            toolStripStatusLabelProgress.Size = new Size(200, 35);
            toolStripStatusLabelProgress.Text = "";
            toolStripStatusLabelProgress.Spring = true;
            toolStripStatusLabelProgress.TextAlign = ContentAlignment.MiddleRight;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1468, 802);
            Controls.Add(dataGridViewResults);
            Controls.Add(groupBoxScheduler);
            Controls.Add(panelApiSettings);
            Controls.Add(panelTopBar);
            Controls.Add(toolStripMain);
            Controls.Add(statusStripBottom);
            MinimumSize = new Size(900, 500);
            Name = "MainForm";
            Text = "Nomor WhatsApp Sender";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            toolStripMain.ResumeLayout(false);
            toolStripMain.PerformLayout();
            panelTopBar.ResumeLayout(false);
            panelTopBar.PerformLayout();
            panelApiSettings.ResumeLayout(false);
            panelApiSettings.PerformLayout();
            groupBoxScheduler.ResumeLayout(false);
            panelSchedulerLeft.ResumeLayout(false);
            panelSchedulerLeft.PerformLayout();
            panelSchedulerRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            statusStripBottom.ResumeLayout(false);
            statusStripBottom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStripMain;
        private ToolStripButton toolStripButtonFetch;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonSendMessage;
        private ToolStripButton toolStripButtonOpenSender;
        private ToolStripButton toolStripButtonCancelSending;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonExport;
        private ToolStripButton toolStripButtonRemoveRows;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButtonCredentials;
        private Panel panelTopBar;
        private ComboBox comboBoxExpirationStatus;
        private TextBox textBoxSearch;
        private Label labelUserCount;
        private Panel panelApiSettings;
        private Label labelApiUrl;
        private TextBox textBoxApiUrl;
        private Label labelLocation;
        private TextBox textBoxLocationCoords;
        private CheckBox checkBoxSendLocation;
        private GroupBox groupBoxScheduler;
        private Panel panelSchedulerLeft;
        private DateTimePicker timePicker;
        private DateTimePicker dateTimePicker;
        private Button buttonStartScheduler;
        private Button buttonStopScheduler;
        private Label labelSchedulerStatus;
        private Panel panelSchedulerRight;
        private CheckedListBox checkedListBoxDays;
        private DataGridView dataGridViewResults;
        private StatusStrip statusStripBottom;
        private ToolStripStatusLabel toolStripStatusLabelSuccessFail;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripStatusLabelProgress;
    }
}
