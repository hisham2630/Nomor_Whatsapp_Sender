using System;
using System.Windows.Forms;
using Nomor_Whatsapp_Sender.Theme;

namespace Nomor_Whatsapp_Sender
{
    public partial class MessageForm : Form
    {
        private static readonly string DefaultTemplateActive =
            "عزيزي المشترك\n%CustomerName%\n\nنود إعلامك بأن اشتراكك في خدمة الإنترنت مع مكتب النمور للانترنت سينتهي قريبًا.\n\nتاريخ انتهاء الاشتراك: %Expiration%\nنوع الاشتراك: %BundleName%\n\nتبقى على انتهاء اشتراكك: %يوم% يوم | %ساعة% ساعة | %دقيقة% دقيقة";

        private static readonly string DefaultTemplateExpired =
            "عزيزي المشترك\n%CustomerName%\n\nنود إعلامك بأن اشتراكك في خدمة الإنترنت مع مكتب النمور للانترنت منتهي الصلاحية.\n\n\nتاريخ انتهاء الاشتراك: %Expiration%\nنوع الاشتراك: %BundleName%\n\nانتهى اشتراكك قبل: %يوم% يوم | %ساعة% ساعة | %دقيقة% دقيقة";

        public MessageForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleButton(buttonSend, isPrimary: true);
            ThemeManager.StyleButton(buttonPreview);
            ThemeManager.StyleButton(buttonReset, isDanger: true);

            LoadTemplates();
            UpdatePreview();
        }

        public string TemplateActive => textBoxTemplateActive.Text;
        public string TemplateExpired => textBoxTemplateExpired.Text;

        private void LoadTemplates()
        {
            string activeTemplate = Properties.Settings.Default.TemplateActive;
            string expiredTemplate = Properties.Settings.Default.TemplateExpired;

            textBoxTemplateActive.Text = string.IsNullOrEmpty(activeTemplate)
                ? DefaultTemplateActive
                : activeTemplate;

            textBoxTemplateExpired.Text = string.IsNullOrEmpty(expiredTemplate)
                ? DefaultTemplateExpired
                : expiredTemplate;
        }

        private void SaveTemplates()
        {
            Properties.Settings.Default.TemplateActive = textBoxTemplateActive.Text;
            Properties.Settings.Default.TemplateExpired = textBoxTemplateExpired.Text;
            Properties.Settings.Default.Save();
        }

        private void UpdatePreview()
        {
            string template = tabControlTemplates.SelectedIndex == 0
                ? textBoxTemplateActive.Text
                : textBoxTemplateExpired.Text;

            string preview = template
                .Replace("%CustomerName%", "أحمد محمد")
                .Replace("%Expiration%", DateTime.Now.AddDays(3).ToString("yyyy-MM-dd h:mm tt"))
                .Replace("%BundleName%", "باقة 10 ميجا")
                .Replace("%يوم%", "3")
                .Replace("%ساعة%", "12")
                .Replace("%دقيقة%", "30");

            textBoxPreview.Text = preview;
        }

        private void textBoxTemplate_TextChanged(object? sender, EventArgs e)
        {
            SaveTemplates();
        }

        private void tabControlTemplates_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void buttonPreview_Click(object? sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void buttonReset_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "هل تريد إعادة القوالب إلى الإعدادات الافتراضية؟\nReset both templates to defaults?",
                "Reset Templates",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                textBoxTemplateActive.Text = DefaultTemplateActive;
                textBoxTemplateExpired.Text = DefaultTemplateExpired;
                SaveTemplates();
                UpdatePreview();
            }
        }

        private void buttonSend_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void MessageForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveTemplates();
        }
    }
}
