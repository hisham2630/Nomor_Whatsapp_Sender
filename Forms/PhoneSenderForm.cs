using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nomor_Whatsapp_Sender.Properties;
using Nomor_Whatsapp_Sender.Services;
using Nomor_Whatsapp_Sender.Theme;

namespace Nomor_Whatsapp_Sender
{
    public partial class PhoneSenderForm : Form
    {
        private List<string> phoneNumbers;
        private readonly WhatsAppService _whatsAppService = new WhatsAppService();

        public PhoneSenderForm(List<string> phoneNumbers)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            ThemeManager.ApplyTheme(this);

            // Style action buttons
            ThemeManager.StyleButton(buttonSend, isPrimary: true);
            ThemeManager.StyleButton(buttonRemove, isDanger: true);
            ThemeManager.StyleButton(buttonClear, isDanger: true);

            this.phoneNumbers = phoneNumbers.Select(number => number.Replace("+", "")).ToList();
            listBoxPhoneNumbers.DataSource = this.phoneNumbers;
            LoadMessage();
            UpdatePhoneNumberCount();
        }

        private async void buttonSend_Click(object? sender, EventArgs e)
        {
            string message = textBoxMessage.Text;
            List<string> selectedPhoneNumbers;

            if (checkBoxCustomNumbers.Checked)
            {
                selectedPhoneNumbers = GetCustomNumbers();
            }
            else
            {
                selectedPhoneNumbers = listBoxPhoneNumbers.SelectedItems.Cast<string>().ToList();
            }

            if (selectedPhoneNumbers.Count == 0)
            {
                MessageBox.Show("Please select phone numbers to send the message.");
                return;
            }

            progressBarSending.Maximum = selectedPhoneNumbers.Count;
            progressBarSending.Value = 0;
            progressBarSending.Step = 1;

            string? location = null;
            if (Properties.Settings.Default.SendLocation && !string.IsNullOrWhiteSpace(Properties.Settings.Default.LocationCoords))
            {
                location = Properties.Settings.Default.LocationCoords.Trim();
            }

            await SendMessagesAsync(selectedPhoneNumbers, message, location);
        }

        private async Task SendMessagesAsync(List<string> phoneNumbers, string message, string? location = null)
        {
            for (int i = 0; i < phoneNumbers.Count; i++)
            {
                string phoneNumber = phoneNumbers[i];

                try
                {
                    bool success = await _whatsAppService.SendMessageAsync(phoneNumber, message, location);
                    labelStatus.Text = $"Sending {i + 1}/{phoneNumbers.Count}" + (success ? " ✔️" : " ❌");
                    progressBarSending.PerformStep();
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending message to {phoneNumber}: {ex.Message}");
                }
            }

            MessageBox.Show("Messages sent successfully.");
        }

        private void buttonInject_Click(object? sender, EventArgs e)
        {
            string newNumbers = textBoxInjectNumbers.Text;
            List<string> injectedNumbers = GetCustomNumbers(newNumbers);
            phoneNumbers.AddRange(injectedNumbers);
            listBoxPhoneNumbers.DataSource = null;
            listBoxPhoneNumbers.DataSource = phoneNumbers;
            UpdatePhoneNumberCount();
        }

        private void buttonRemove_Click(object? sender, EventArgs e)
        {
            if (listBoxPhoneNumbers.SelectedItems.Count > 0)
            {
                List<string> selectedNumbers = listBoxPhoneNumbers.SelectedItems.Cast<string>().ToList();
                phoneNumbers.RemoveAll(number => selectedNumbers.Contains(number));
                listBoxPhoneNumbers.DataSource = null;
                listBoxPhoneNumbers.DataSource = phoneNumbers;
                UpdatePhoneNumberCount();
            }
            else
            {
                MessageBox.Show("Please select numbers to remove.");
            }
        }

        private void buttonClear_Click(object? sender, EventArgs e)
        {
            phoneNumbers.Clear();
            listBoxPhoneNumbers.DataSource = null;
            listBoxPhoneNumbers.DataSource = phoneNumbers;
            UpdatePhoneNumberCount();
        }

        private void UpdatePhoneNumberCount()
        {
            labelPhoneNumberCount.Text = $"Count: {phoneNumbers.Count}";
            if (checkBoxCustomNumbers.Checked)
            {
                List<string> customNumbers = GetCustomNumbers();
                labelCustomNumberCount.Text = $"Count: {customNumbers.Count}";
            }
            else
            {
                labelCustomNumberCount.Text = "Count: 0";
            }
        }

        private void checkBoxCustomNumbers_CheckedChanged(object? sender, EventArgs e)
        {
            UpdatePhoneNumberCount();
        }

        private void textBoxCustomNumbers_TextChanged(object? sender, EventArgs e)
        {
            UpdatePhoneNumberCount();
        }

        private List<string> GetCustomNumbers(string? input = null)
        {
            string text = input ?? textBoxCustomNumbers.Text;
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> numbers = new List<string>();

            foreach (var line in lines)
            {
                numbers.AddRange(line.Split(new[] { ',', ';', ' ', '-', '/', '\\', '_' }, StringSplitOptions.RemoveEmptyEntries));
            }

            return numbers;
        }

        private void LoadMessage()
        {
            textBoxMessage.Text = Settings.Default.SavedMessage;
        }

        private void SaveMessage()
        {
            Settings.Default.SavedMessage = textBoxMessage.Text;
            Settings.Default.Save();
        }

        private void textBoxMessage_TextChanged(object? sender, EventArgs e)
        {
            SaveMessage();
        }

        private void PhoneSenderForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveMessage();
        }
    }
}
