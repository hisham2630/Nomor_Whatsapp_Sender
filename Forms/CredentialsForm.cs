using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nomor_Whatsapp_Sender.Models;
using Nomor_Whatsapp_Sender.Services;
using Nomor_Whatsapp_Sender.Theme;

namespace Nomor_Whatsapp_Sender
{
    public partial class CredentialsForm : Form
    {
        public CredentialsForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleButton(buttonAdd, isPrimary: true);
            ThemeManager.StyleButton(buttonDelete, isDanger: true);
            ThemeManager.StyleButton(buttonSave, isPrimary: true);

            LoadCredentials();
        }

        private void LoadCredentials()
        {
            dataGridViewCredentials.Rows.Clear();

            var credentials = CredentialManager.GetAll();
            foreach (var cred in credentials)
            {
                dataGridViewCredentials.Rows.Add(
                    cred.Name,
                    cred.Host,
                    cred.Username,
                    cred.Password,
                    cred.Enabled
                );
            }
        }

        private void buttonAdd_Click(object? sender, EventArgs e)
        {
            dataGridViewCredentials.Rows.Add("", "admin.halasat-ftth.iq", "", "", true);

            int newRowIndex = dataGridViewCredentials.Rows.Count - 1;
            dataGridViewCredentials.CurrentCell = dataGridViewCredentials.Rows[newRowIndex].Cells[0];
            dataGridViewCredentials.BeginEdit(true);
        }

        private void buttonDelete_Click(object? sender, EventArgs e)
        {
            if (dataGridViewCredentials.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Delete {dataGridViewCredentials.SelectedRows.Count} selected credential(s)?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridViewCredentials.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dataGridViewCredentials.Rows.Remove(row);
                }
            }
        }

        private void buttonSave_Click(object? sender, EventArgs e)
        {
            dataGridViewCredentials.EndEdit();

            var credentials = new List<SasCredential>();

            foreach (DataGridViewRow row in dataGridViewCredentials.Rows)
            {
                if (row.IsNewRow) continue;

                string name = row.Cells["colName"].Value?.ToString() ?? "";
                string host = row.Cells["colHost"].Value?.ToString() ?? "";
                string username = row.Cells["colUsername"].Value?.ToString() ?? "";
                string password = row.Cells["colPassword"].Value?.ToString() ?? "";
                bool enabled = row.Cells["colEnabled"].Value is bool b ? b : true;

                if (string.IsNullOrWhiteSpace(host) && string.IsNullOrWhiteSpace(username))
                    continue;

                credentials.Add(new SasCredential
                {
                    Name = name,
                    Host = host,
                    Username = username,
                    Password = password,
                    Enabled = enabled
                });
            }

            CredentialManager.SetAll(credentials);

            MessageBox.Show("Credentials saved successfully.", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
