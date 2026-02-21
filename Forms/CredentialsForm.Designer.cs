namespace Nomor_Whatsapp_Sender
{
    partial class CredentialsForm
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
            dataGridViewCredentials = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colHost = new DataGridViewTextBoxColumn();
            colUsername = new DataGridViewTextBoxColumn();
            colPassword = new DataGridViewTextBoxColumn();
            colEnabled = new DataGridViewCheckBoxColumn();
            panelButtons = new Panel();
            buttonAdd = new Button();
            buttonDelete = new Button();
            buttonSave = new Button();
            buttonCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCredentials).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // dataGridViewCredentials
            //
            dataGridViewCredentials.AllowUserToAddRows = false;
            dataGridViewCredentials.AllowUserToDeleteRows = false;
            dataGridViewCredentials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCredentials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCredentials.Columns.AddRange(new DataGridViewColumn[] {
                colName,
                colHost,
                colUsername,
                colPassword,
                colEnabled
            });
            dataGridViewCredentials.Dock = DockStyle.Fill;
            dataGridViewCredentials.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridViewCredentials.Location = new Point(0, 0);
            dataGridViewCredentials.Name = "dataGridViewCredentials";
            dataGridViewCredentials.RowHeadersWidth = 30;
            dataGridViewCredentials.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCredentials.Size = new Size(684, 326);
            dataGridViewCredentials.TabIndex = 0;
            //
            // colName
            //
            colName.HeaderText = "Name";
            colName.Name = "colName";
            colName.FillWeight = 80;
            //
            // colHost
            //
            colHost.HeaderText = "Host";
            colHost.Name = "colHost";
            colHost.FillWeight = 120;
            //
            // colUsername
            //
            colUsername.HeaderText = "Username";
            colUsername.Name = "colUsername";
            colUsername.FillWeight = 120;
            //
            // colPassword
            //
            colPassword.HeaderText = "Password";
            colPassword.Name = "colPassword";
            colPassword.FillWeight = 100;
            //
            // colEnabled
            //
            colEnabled.HeaderText = "Enabled";
            colEnabled.Name = "colEnabled";
            colEnabled.FillWeight = 50;
            //
            // panelButtons
            //
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Controls.Add(buttonCancel);
            panelButtons.Controls.Add(buttonSave);
            panelButtons.Controls.Add(buttonDelete);
            panelButtons.Controls.Add(buttonAdd);
            panelButtons.Location = new Point(0, 326);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(12, 8, 12, 8);
            panelButtons.Size = new Size(684, 50);
            panelButtons.TabIndex = 1;
            //
            // buttonAdd
            //
            buttonAdd.Location = new Point(12, 8);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(90, 32);
            buttonAdd.TabIndex = 0;
            buttonAdd.Text = "➕ Add";
            buttonAdd.Click += buttonAdd_Click;
            //
            // buttonDelete
            //
            buttonDelete.Location = new Point(112, 8);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(90, 32);
            buttonDelete.TabIndex = 1;
            buttonDelete.Text = "🗑 Delete";
            buttonDelete.Click += buttonDelete_Click;
            //
            // buttonSave
            //
            buttonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSave.Location = new Point(482, 8);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(90, 32);
            buttonSave.TabIndex = 2;
            buttonSave.Text = "💾 Save";
            buttonSave.Click += buttonSave_Click;
            //
            // buttonCancel
            //
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.Location = new Point(582, 8);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 32);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Cancel";
            buttonCancel.Click += buttonCancel_Click;
            //
            // CredentialsForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 376);
            Controls.Add(dataGridViewCredentials);
            Controls.Add(panelButtons);
            MinimumSize = new Size(500, 300);
            Name = "CredentialsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "🔑 SAS Credentials Manager";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCredentials).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewCredentials;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colHost;
        private DataGridViewTextBoxColumn colUsername;
        private DataGridViewTextBoxColumn colPassword;
        private DataGridViewCheckBoxColumn colEnabled;
        private Panel panelButtons;
        private Button buttonAdd;
        private Button buttonDelete;
        private Button buttonSave;
        private Button buttonCancel;
    }
}
