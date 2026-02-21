namespace Nomor_Whatsapp_Sender
{
    partial class MessageForm
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
            tabControlTemplates = new TabControl();
            tabPageActive = new TabPage();
            textBoxTemplateActive = new TextBox();
            tabPageExpired = new TabPage();
            textBoxTemplateExpired = new TextBox();
            panelPlaceholders = new Panel();
            labelPlaceholderTitle = new Label();
            labelPlaceholderList = new Label();
            panelPreview = new GroupBox();
            textBoxPreview = new TextBox();
            panelButtons = new Panel();
            buttonPreview = new Button();
            buttonReset = new Button();
            buttonSend = new Button();
            tabControlTemplates.SuspendLayout();
            tabPageActive.SuspendLayout();
            tabPageExpired.SuspendLayout();
            panelPlaceholders.SuspendLayout();
            panelPreview.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // tabControlTemplates
            //
            tabControlTemplates.Dock = DockStyle.Fill;
            tabControlTemplates.Controls.Add(tabPageActive);
            tabControlTemplates.Controls.Add(tabPageExpired);
            tabControlTemplates.Location = new Point(0, 0);
            tabControlTemplates.Name = "tabControlTemplates";
            tabControlTemplates.SelectedIndex = 0;
            tabControlTemplates.Size = new Size(560, 280);
            tabControlTemplates.TabIndex = 0;
            tabControlTemplates.SelectedIndexChanged += tabControlTemplates_SelectedIndexChanged;
            //
            // tabPageActive
            //
            tabPageActive.Controls.Add(textBoxTemplateActive);
            tabPageActive.Location = new Point(4, 26);
            tabPageActive.Name = "tabPageActive";
            tabPageActive.Padding = new Padding(8);
            tabPageActive.Size = new Size(552, 250);
            tabPageActive.TabIndex = 0;
            tabPageActive.Text = "✅ Active Template (سينتهي قريبًا)";
            tabPageActive.UseVisualStyleBackColor = true;
            //
            // textBoxTemplateActive
            //
            textBoxTemplateActive.Dock = DockStyle.Fill;
            textBoxTemplateActive.Multiline = true;
            textBoxTemplateActive.Name = "textBoxTemplateActive";
            textBoxTemplateActive.ScrollBars = ScrollBars.Vertical;
            textBoxTemplateActive.RightToLeft = RightToLeft.Yes;
            textBoxTemplateActive.Size = new Size(536, 234);
            textBoxTemplateActive.TabIndex = 0;
            textBoxTemplateActive.AcceptsReturn = true;
            textBoxTemplateActive.TextChanged += textBoxTemplate_TextChanged;
            //
            // tabPageExpired
            //
            tabPageExpired.Controls.Add(textBoxTemplateExpired);
            tabPageExpired.Location = new Point(4, 26);
            tabPageExpired.Name = "tabPageExpired";
            tabPageExpired.Padding = new Padding(8);
            tabPageExpired.Size = new Size(552, 250);
            tabPageExpired.TabIndex = 1;
            tabPageExpired.Text = "⛔ Expired Template (منتهي الصلاحية)";
            tabPageExpired.UseVisualStyleBackColor = true;
            //
            // textBoxTemplateExpired
            //
            textBoxTemplateExpired.Dock = DockStyle.Fill;
            textBoxTemplateExpired.Multiline = true;
            textBoxTemplateExpired.Name = "textBoxTemplateExpired";
            textBoxTemplateExpired.ScrollBars = ScrollBars.Vertical;
            textBoxTemplateExpired.RightToLeft = RightToLeft.Yes;
            textBoxTemplateExpired.Size = new Size(536, 234);
            textBoxTemplateExpired.TabIndex = 0;
            textBoxTemplateExpired.AcceptsReturn = true;
            textBoxTemplateExpired.TextChanged += textBoxTemplate_TextChanged;
            //
            // panelPlaceholders
            //
            panelPlaceholders.Dock = DockStyle.Right;
            panelPlaceholders.Controls.Add(labelPlaceholderList);
            panelPlaceholders.Controls.Add(labelPlaceholderTitle);
            panelPlaceholders.Location = new Point(560, 0);
            panelPlaceholders.Name = "panelPlaceholders";
            panelPlaceholders.Padding = new Padding(10, 8, 10, 8);
            panelPlaceholders.Size = new Size(220, 280);
            panelPlaceholders.TabIndex = 1;
            //
            // labelPlaceholderTitle
            //
            labelPlaceholderTitle.Dock = DockStyle.Top;
            labelPlaceholderTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPlaceholderTitle.Location = new Point(10, 8);
            labelPlaceholderTitle.Name = "labelPlaceholderTitle";
            labelPlaceholderTitle.Size = new Size(200, 28);
            labelPlaceholderTitle.TabIndex = 0;
            labelPlaceholderTitle.Text = "📋 Available Placeholders";
            //
            // labelPlaceholderList
            //
            labelPlaceholderList.Dock = DockStyle.Fill;
            labelPlaceholderList.Location = new Point(10, 36);
            labelPlaceholderList.Name = "labelPlaceholderList";
            labelPlaceholderList.Size = new Size(200, 236);
            labelPlaceholderList.TabIndex = 1;
            labelPlaceholderList.Text = "%CustomerName%\n  → اسم المشترك (Firstname)\n\n" +
                "%Expiration%\n  → تاريخ انتهاء الاشتراك\n\n" +
                "%BundleName%\n  → نوع الاشتراك (Profile)\n\n" +
                "%يوم%\n  → عدد الأيام المتبقية\n\n" +
                "%ساعة%\n  → عدد الساعات المتبقية\n\n" +
                "%دقيقة%\n  → عدد الدقائق المتبقية";
            //
            // panelPreview
            //
            panelPreview.Dock = DockStyle.Bottom;
            panelPreview.Controls.Add(textBoxPreview);
            panelPreview.Location = new Point(0, 280);
            panelPreview.Name = "panelPreview";
            panelPreview.Padding = new Padding(10, 8, 10, 8);
            panelPreview.Size = new Size(780, 130);
            panelPreview.TabIndex = 2;
            panelPreview.Text = "👁 Preview";
            //
            // textBoxPreview
            //
            textBoxPreview.Dock = DockStyle.Fill;
            textBoxPreview.Multiline = true;
            textBoxPreview.ReadOnly = true;
            textBoxPreview.Name = "textBoxPreview";
            textBoxPreview.ScrollBars = ScrollBars.Vertical;
            textBoxPreview.RightToLeft = RightToLeft.Yes;
            textBoxPreview.Size = new Size(760, 94);
            textBoxPreview.TabIndex = 0;
            textBoxPreview.TabStop = false;
            //
            // panelButtons
            //
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Controls.Add(buttonSend);
            panelButtons.Controls.Add(buttonReset);
            panelButtons.Controls.Add(buttonPreview);
            panelButtons.Location = new Point(0, 410);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(12, 8, 12, 8);
            panelButtons.Size = new Size(780, 50);
            panelButtons.TabIndex = 3;
            //
            // buttonPreview
            //
            buttonPreview.Location = new Point(12, 8);
            buttonPreview.Name = "buttonPreview";
            buttonPreview.Size = new Size(120, 32);
            buttonPreview.TabIndex = 0;
            buttonPreview.Text = "👁 Preview";
            buttonPreview.Click += buttonPreview_Click;
            //
            // buttonReset
            //
            buttonReset.Location = new Point(145, 8);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(130, 32);
            buttonReset.TabIndex = 1;
            buttonReset.Text = "🔄 Reset Defaults";
            buttonReset.Click += buttonReset_Click;
            //
            // buttonSend
            //
            buttonSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSend.Location = new Point(648, 8);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(120, 32);
            buttonSend.TabIndex = 2;
            buttonSend.Text = "📨 Confirm & Send";
            buttonSend.Click += buttonSend_Click;
            //
            // MessageForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 460);
            Controls.Add(tabControlTemplates);
            Controls.Add(panelPlaceholders);
            Controls.Add(panelPreview);
            Controls.Add(panelButtons);
            MinimumSize = new Size(700, 450);
            Name = "MessageForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "📝 Message Template Editor";
            FormClosing += MessageForm_FormClosing;
            tabControlTemplates.ResumeLayout(false);
            tabPageActive.ResumeLayout(false);
            tabPageActive.PerformLayout();
            tabPageExpired.ResumeLayout(false);
            tabPageExpired.PerformLayout();
            panelPlaceholders.ResumeLayout(false);
            panelPreview.ResumeLayout(false);
            panelPreview.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlTemplates;
        private TabPage tabPageActive;
        private TextBox textBoxTemplateActive;
        private TabPage tabPageExpired;
        private TextBox textBoxTemplateExpired;
        private Panel panelPlaceholders;
        private Label labelPlaceholderTitle;
        private Label labelPlaceholderList;
        private GroupBox panelPreview;
        private TextBox textBoxPreview;
        private Panel panelButtons;
        private Button buttonPreview;
        private Button buttonReset;
        private Button buttonSend;
    }
}
