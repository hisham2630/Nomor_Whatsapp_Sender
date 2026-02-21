namespace Nomor_Whatsapp_Sender
{
    partial class PhoneSenderForm
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
            panelPhoneList = new Panel();
            listBoxPhoneNumbers = new ListBox();
            panelPhoneHeader = new Panel();
            labelPhoneNumbers = new Label();
            labelPhoneNumberCount = new Label();
            buttonClear = new Button();
            groupBoxMessage = new GroupBox();
            textBoxMessage = new TextBox();
            panelActions = new Panel();
            buttonSend = new Button();
            buttonInject = new Button();
            buttonRemove = new Button();
            groupBoxCustomNumbers = new GroupBox();
            checkBoxCustomNumbers = new CheckBox();
            labelCustomNumberCount = new Label();
            textBoxCustomNumbers = new TextBox();
            groupBoxInjectNumbers = new GroupBox();
            textBoxInjectNumbers = new TextBox();
            panelBottom = new Panel();
            progressBarSending = new ProgressBar();
            labelStatus = new Label();
            panelPhoneList.SuspendLayout();
            panelPhoneHeader.SuspendLayout();
            groupBoxMessage.SuspendLayout();
            panelActions.SuspendLayout();
            groupBoxCustomNumbers.SuspendLayout();
            groupBoxInjectNumbers.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            //
            // panelPhoneList
            //
            panelPhoneList.Dock = DockStyle.Top;
            panelPhoneList.Controls.Add(listBoxPhoneNumbers);
            panelPhoneList.Controls.Add(panelPhoneHeader);
            panelPhoneList.Location = new Point(0, 0);
            panelPhoneList.Name = "panelPhoneList";
            panelPhoneList.Padding = new Padding(12, 8, 12, 4);
            panelPhoneList.Size = new Size(380, 210);
            panelPhoneList.TabIndex = 0;
            //
            // panelPhoneHeader
            //
            panelPhoneHeader.Dock = DockStyle.Top;
            panelPhoneHeader.Controls.Add(buttonClear);
            panelPhoneHeader.Controls.Add(labelPhoneNumberCount);
            panelPhoneHeader.Controls.Add(labelPhoneNumbers);
            panelPhoneHeader.Location = new Point(12, 8);
            panelPhoneHeader.Name = "panelPhoneHeader";
            panelPhoneHeader.Size = new Size(356, 30);
            panelPhoneHeader.TabIndex = 0;
            //
            // labelPhoneNumbers
            //
            labelPhoneNumbers.AutoSize = true;
            labelPhoneNumbers.Location = new Point(0, 7);
            labelPhoneNumbers.Name = "labelPhoneNumbers";
            labelPhoneNumbers.Size = new Size(95, 17);
            labelPhoneNumbers.TabIndex = 0;
            labelPhoneNumbers.Text = "📞 Phone Numbers";
            //
            // labelPhoneNumberCount
            //
            labelPhoneNumberCount.AutoSize = true;
            labelPhoneNumberCount.Location = new Point(150, 7);
            labelPhoneNumberCount.Name = "labelPhoneNumberCount";
            labelPhoneNumberCount.Size = new Size(80, 17);
            labelPhoneNumberCount.TabIndex = 1;
            labelPhoneNumberCount.Text = "Count: 0";
            //
            // buttonClear
            //
            buttonClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClear.Location = new Point(280, 2);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(75, 26);
            buttonClear.TabIndex = 2;
            buttonClear.Text = "Clear";
            buttonClear.Click += buttonClear_Click;
            //
            // listBoxPhoneNumbers
            //
            listBoxPhoneNumbers.Dock = DockStyle.Fill;
            listBoxPhoneNumbers.FormattingEnabled = true;
            listBoxPhoneNumbers.ItemHeight = 17;
            listBoxPhoneNumbers.Location = new Point(12, 38);
            listBoxPhoneNumbers.Name = "listBoxPhoneNumbers";
            listBoxPhoneNumbers.SelectionMode = SelectionMode.MultiExtended;
            listBoxPhoneNumbers.Size = new Size(356, 168);
            listBoxPhoneNumbers.TabIndex = 1;
            //
            // groupBoxMessage
            //
            groupBoxMessage.Dock = DockStyle.Top;
            groupBoxMessage.Controls.Add(textBoxMessage);
            groupBoxMessage.Location = new Point(0, 210);
            groupBoxMessage.Name = "groupBoxMessage";
            groupBoxMessage.Padding = new Padding(12, 8, 12, 8);
            groupBoxMessage.Size = new Size(380, 110);
            groupBoxMessage.TabIndex = 1;
            groupBoxMessage.TabStop = false;
            groupBoxMessage.Text = "💬 Message";
            //
            // textBoxMessage
            //
            textBoxMessage.Dock = DockStyle.Fill;
            textBoxMessage.Multiline = true;
            textBoxMessage.Name = "textBoxMessage";
            textBoxMessage.ScrollBars = ScrollBars.Vertical;
            textBoxMessage.Size = new Size(356, 82);
            textBoxMessage.TabIndex = 0;
            textBoxMessage.TextChanged += textBoxMessage_TextChanged;
            //
            // panelActions
            //
            panelActions.Dock = DockStyle.Top;
            panelActions.Controls.Add(buttonRemove);
            panelActions.Controls.Add(buttonInject);
            panelActions.Controls.Add(buttonSend);
            panelActions.Location = new Point(0, 320);
            panelActions.Name = "panelActions";
            panelActions.Padding = new Padding(12, 6, 12, 6);
            panelActions.Size = new Size(380, 44);
            panelActions.TabIndex = 2;
            //
            // buttonSend
            //
            buttonSend.Location = new Point(12, 6);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(110, 30);
            buttonSend.TabIndex = 0;
            buttonSend.Text = "📨 Send";
            buttonSend.Click += buttonSend_Click;
            //
            // buttonInject
            //
            buttonInject.Location = new Point(135, 6);
            buttonInject.Name = "buttonInject";
            buttonInject.Size = new Size(110, 30);
            buttonInject.TabIndex = 1;
            buttonInject.Text = "💉 Inject";
            buttonInject.Click += buttonInject_Click;
            //
            // buttonRemove
            //
            buttonRemove.Location = new Point(258, 6);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(110, 30);
            buttonRemove.TabIndex = 2;
            buttonRemove.Text = "🗑 Remove";
            buttonRemove.Click += buttonRemove_Click;
            //
            // groupBoxCustomNumbers
            //
            groupBoxCustomNumbers.Dock = DockStyle.Top;
            groupBoxCustomNumbers.Controls.Add(textBoxCustomNumbers);
            groupBoxCustomNumbers.Controls.Add(labelCustomNumberCount);
            groupBoxCustomNumbers.Controls.Add(checkBoxCustomNumbers);
            groupBoxCustomNumbers.Location = new Point(0, 364);
            groupBoxCustomNumbers.Name = "groupBoxCustomNumbers";
            groupBoxCustomNumbers.Padding = new Padding(12, 4, 12, 8);
            groupBoxCustomNumbers.Size = new Size(380, 120);
            groupBoxCustomNumbers.TabIndex = 3;
            groupBoxCustomNumbers.TabStop = false;
            groupBoxCustomNumbers.Text = "📋 Custom Numbers";
            //
            // checkBoxCustomNumbers
            //
            checkBoxCustomNumbers.AutoSize = true;
            checkBoxCustomNumbers.Dock = DockStyle.Top;
            checkBoxCustomNumbers.Location = new Point(12, 20);
            checkBoxCustomNumbers.Name = "checkBoxCustomNumbers";
            checkBoxCustomNumbers.Size = new Size(356, 19);
            checkBoxCustomNumbers.TabIndex = 0;
            checkBoxCustomNumbers.Text = "Use Custom Numbers";
            checkBoxCustomNumbers.CheckedChanged += checkBoxCustomNumbers_CheckedChanged;
            //
            // labelCustomNumberCount
            //
            labelCustomNumberCount.AutoSize = true;
            labelCustomNumberCount.Dock = DockStyle.Top;
            labelCustomNumberCount.Location = new Point(12, 39);
            labelCustomNumberCount.Name = "labelCustomNumberCount";
            labelCustomNumberCount.Padding = new Padding(0, 2, 0, 2);
            labelCustomNumberCount.Size = new Size(80, 21);
            labelCustomNumberCount.TabIndex = 1;
            labelCustomNumberCount.Text = "Count: 0";
            //
            // textBoxCustomNumbers
            //
            textBoxCustomNumbers.Dock = DockStyle.Fill;
            textBoxCustomNumbers.Multiline = true;
            textBoxCustomNumbers.Name = "textBoxCustomNumbers";
            textBoxCustomNumbers.ScrollBars = ScrollBars.Vertical;
            textBoxCustomNumbers.Size = new Size(356, 52);
            textBoxCustomNumbers.TabIndex = 2;
            textBoxCustomNumbers.TextChanged += textBoxCustomNumbers_TextChanged;
            //
            // groupBoxInjectNumbers
            //
            groupBoxInjectNumbers.Dock = DockStyle.Top;
            groupBoxInjectNumbers.Controls.Add(textBoxInjectNumbers);
            groupBoxInjectNumbers.Location = new Point(0, 484);
            groupBoxInjectNumbers.Name = "groupBoxInjectNumbers";
            groupBoxInjectNumbers.Padding = new Padding(12, 8, 12, 8);
            groupBoxInjectNumbers.Size = new Size(380, 100);
            groupBoxInjectNumbers.TabIndex = 4;
            groupBoxInjectNumbers.TabStop = false;
            groupBoxInjectNumbers.Text = "💉 Inject Numbers";
            //
            // textBoxInjectNumbers
            //
            textBoxInjectNumbers.Dock = DockStyle.Fill;
            textBoxInjectNumbers.Multiline = true;
            textBoxInjectNumbers.Name = "textBoxInjectNumbers";
            textBoxInjectNumbers.ScrollBars = ScrollBars.Vertical;
            textBoxInjectNumbers.Size = new Size(356, 72);
            textBoxInjectNumbers.TabIndex = 0;
            //
            // panelBottom
            //
            panelBottom.Dock = DockStyle.Top;
            panelBottom.Controls.Add(progressBarSending);
            panelBottom.Controls.Add(labelStatus);
            panelBottom.Location = new Point(0, 584);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(12, 8, 12, 8);
            panelBottom.Size = new Size(380, 68);
            panelBottom.TabIndex = 5;
            //
            // labelStatus
            //
            labelStatus.Dock = DockStyle.Top;
            labelStatus.Location = new Point(12, 8);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(356, 24);
            labelStatus.TabIndex = 0;
            labelStatus.Text = "Ready";
            labelStatus.TextAlign = ContentAlignment.MiddleCenter;
            //
            // progressBarSending
            //
            progressBarSending.Dock = DockStyle.Top;
            progressBarSending.Location = new Point(12, 32);
            progressBarSending.Name = "progressBarSending";
            progressBarSending.Size = new Size(356, 22);
            progressBarSending.TabIndex = 1;
            //
            // PhoneSenderForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(380, 660);
            Controls.Add(panelBottom);
            Controls.Add(groupBoxInjectNumbers);
            Controls.Add(groupBoxCustomNumbers);
            Controls.Add(panelActions);
            Controls.Add(groupBoxMessage);
            Controls.Add(panelPhoneList);
            MinimumSize = new Size(360, 500);
            Name = "PhoneSenderForm";
            Text = "Phone Sender";
            StartPosition = FormStartPosition.CenterParent;
            FormClosing += PhoneSenderForm_FormClosing;
            panelPhoneList.ResumeLayout(false);
            panelPhoneHeader.ResumeLayout(false);
            panelPhoneHeader.PerformLayout();
            groupBoxMessage.ResumeLayout(false);
            groupBoxMessage.PerformLayout();
            panelActions.ResumeLayout(false);
            groupBoxCustomNumbers.ResumeLayout(false);
            groupBoxCustomNumbers.PerformLayout();
            groupBoxInjectNumbers.ResumeLayout(false);
            groupBoxInjectNumbers.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPhoneList;
        private Panel panelPhoneHeader;
        private Label labelPhoneNumbers;
        private Label labelPhoneNumberCount;
        private Button buttonClear;
        private ListBox listBoxPhoneNumbers;
        private GroupBox groupBoxMessage;
        private TextBox textBoxMessage;
        private Panel panelActions;
        private Button buttonSend;
        private Button buttonInject;
        private Button buttonRemove;
        private GroupBox groupBoxCustomNumbers;
        public CheckBox checkBoxCustomNumbers;
        private Label labelCustomNumberCount;
        private TextBox textBoxCustomNumbers;
        private GroupBox groupBoxInjectNumbers;
        private TextBox textBoxInjectNumbers;
        private Panel panelBottom;
        private Label labelStatus;
        private ProgressBar progressBarSending;
    }
}
