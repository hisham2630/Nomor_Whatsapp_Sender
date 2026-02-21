using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Nomor_Whatsapp_Sender.Theme
{
    public static class ThemeManager
    {
        // ── Base Palette ──────────────────────────────────────
        public static readonly Color BaseBg       = ColorTranslator.FromHtml("#1E1E2E");
        public static readonly Color Surface      = ColorTranslator.FromHtml("#282A3A");
        public static readonly Color SurfaceAlt   = ColorTranslator.FromHtml("#323550");
        public static readonly Color Border       = ColorTranslator.FromHtml("#3A3C50");

        // ── Text ──────────────────────────────────────────────
        public static readonly Color TextPrimary   = ColorTranslator.FromHtml("#E4E4E8");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#9CA3AF");

        // ── Accent ────────────────────────────────────────────
        public static readonly Color Accent       = ColorTranslator.FromHtml("#00BFA6");
        public static readonly Color AccentHover  = ColorTranslator.FromHtml("#00E5C3");

        // ── Semantic ──────────────────────────────────────────
        public static readonly Color Danger       = ColorTranslator.FromHtml("#EF4444");
        public static readonly Color DangerHover  = ColorTranslator.FromHtml("#F87171");
        public static readonly Color Warning      = ColorTranslator.FromHtml("#F59E0B");
        public static readonly Color Success      = ColorTranslator.FromHtml("#22C55E");

        // ── DataGridView ──────────────────────────────────────
        public static readonly Color HeaderBg     = ColorTranslator.FromHtml("#1A1A2E");
        public static readonly Color Selection    = Color.FromArgb(100, 0, 191, 166); // Teal 40%
        public static readonly Color RowHighlight = Color.FromArgb(80, 245, 158, 11); // Warning 30%

        // ── Fonts ─────────────────────────────────────────────
        public static readonly Font FontBody      = new Font("Segoe UI", 9.75F, FontStyle.Regular);
        public static readonly Font FontBold      = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular);
        public static readonly Font FontTitle     = new Font("Segoe UI Semibold", 11F, FontStyle.Regular);
        public static readonly Font FontSmall     = new Font("Segoe UI", 8.25F, FontStyle.Regular);

        /// <summary>
        /// Apply dark theme to a form and all its children recursively.
        /// Call once in constructor after InitializeComponent().
        /// </summary>
        public static void ApplyTheme(Form form)
        {
            form.BackColor = BaseBg;
            form.ForeColor = TextPrimary;
            form.Font = FontBody;

            ApplyToControls(form.Controls);
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                switch (ctrl)
                {
                    case StatusStrip ss:
                        StyleStatusStrip(ss);
                        break;
                    case ToolStrip ts:
                        StyleToolStrip(ts);
                        break;
                    case DataGridView dgv:
                        StyleDataGridView(dgv);
                        break;
                    case GroupBox gb:
                        StyleGroupBox(gb);
                        break;
                    case Button btn:
                        StyleButton(btn);
                        break;
                    case TextBox tb:
                        StyleTextBox(tb);
                        break;
                    case ComboBox cb:
                        StyleComboBox(cb);
                        break;
                    case CheckBox chk:
                        StyleCheckBox(chk);
                        break;
                    case CheckedListBox clb:
                        StyleCheckedListBox(clb);
                        break;
                    case ListBox lb:
                        StyleListBox(lb);
                        break;
                    case ProgressBar pb:
                        StyleProgressBar(pb);
                        break;
                    case DateTimePicker dtp:
                        StyleDateTimePicker(dtp);
                        break;
                    case Label lbl:
                        StyleLabel(lbl);
                        break;
                    case Panel pnl:
                        pnl.BackColor = pnl.Tag?.ToString() == "surface" ? Surface : BaseBg;
                        pnl.ForeColor = TextPrimary;
                        break;
                }

                // Recurse into children
                if (ctrl.HasChildren)
                {
                    ApplyToControls(ctrl.Controls);
                }
            }
        }

        // ── Individual Stylers ────────────────────────────────

        public static void StyleButton(Button btn, bool isPrimary = false, bool isDanger = false)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.Cursor = Cursors.Hand;
            btn.Font = FontBold;

            if (isDanger)
            {
                btn.BackColor = Danger;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = Danger;
                btn.FlatAppearance.MouseOverBackColor = DangerHover;
            }
            else if (isPrimary)
            {
                btn.BackColor = Accent;
                btn.ForeColor = BaseBg;
                btn.FlatAppearance.BorderColor = Accent;
                btn.FlatAppearance.MouseOverBackColor = AccentHover;
            }
            else
            {
                btn.BackColor = Surface;
                btn.ForeColor = TextPrimary;
                btn.FlatAppearance.BorderColor = Border;
                btn.FlatAppearance.MouseOverBackColor = SurfaceAlt;
            }

            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 0, 191, 166);
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            // Enable double buffering via reflection to prevent flicker during resize
            var type = dgv.GetType();
            var prop = type.GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            prop?.SetValue(dgv, true, null);

            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = BaseBg;
            dgv.GridColor = Border;
            dgv.DefaultCellStyle.BackColor = Surface;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = Selection;
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.DefaultCellStyle.Font = FontBody;
            dgv.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = SurfaceAlt;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Selection;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextPrimary;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = HeaderBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Accent;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBold;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 4, 6, 4);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersHeight = 36;

            dgv.RowHeadersDefaultCellStyle.BackColor = HeaderBg;
            dgv.RowHeadersDefaultCellStyle.ForeColor = TextSecondary;
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowTemplate.Height = 32;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public static void StyleTextBox(TextBox tb)
        {
            tb.BackColor = Surface;
            tb.ForeColor = TextPrimary;
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.Font = FontBody;
        }

        public static void StyleLabel(Label lbl)
        {
            lbl.ForeColor = TextPrimary;
            lbl.Font = FontBody;
        }

        public static void StyleGroupBox(GroupBox gb)
        {
            gb.BackColor = Surface;
            gb.ForeColor = Accent;
            gb.Font = FontBold;
        }

        public static void StyleComboBox(ComboBox cb)
        {
            cb.BackColor = Surface;
            cb.ForeColor = TextPrimary;
            cb.FlatStyle = FlatStyle.Flat;
            cb.Font = FontBody;
        }

        public static void StyleCheckBox(CheckBox chk)
        {
            chk.ForeColor = TextPrimary;
            chk.Font = FontBody;
        }

        public static void StyleCheckedListBox(CheckedListBox clb)
        {
            clb.BackColor = Surface;
            clb.ForeColor = TextPrimary;
            clb.BorderStyle = BorderStyle.None;
            clb.Font = FontBody;
        }

        public static void StyleListBox(ListBox lb)
        {
            lb.BackColor = Surface;
            lb.ForeColor = TextPrimary;
            lb.BorderStyle = BorderStyle.None;
            lb.Font = FontBody;
        }

        public static void StyleProgressBar(ProgressBar pb)
        {
            pb.Style = ProgressBarStyle.Continuous;
        }

        public static void StyleDateTimePicker(DateTimePicker dtp)
        {
            dtp.CalendarMonthBackground = Surface;
            dtp.CalendarForeColor = TextPrimary;
            dtp.CalendarTitleBackColor = HeaderBg;
            dtp.CalendarTitleForeColor = Accent;
            dtp.Font = FontBody;
        }

        public static void StyleToolStrip(ToolStrip ts)
        {
            ts.BackColor = Surface;
            ts.ForeColor = TextPrimary;
            ts.Renderer = new DarkToolStripRenderer();
            ts.GripStyle = ToolStripGripStyle.Hidden;
            ts.Padding = new Padding(8, 4, 8, 4);
            ts.Font = FontBody;
        }

        public static void StyleStatusStrip(StatusStrip ss)
        {
            ss.BackColor = HeaderBg;
            ss.ForeColor = TextSecondary;
            ss.Font = FontSmall;
            ss.Renderer = new DarkToolStripRenderer();
            ss.SizingGrip = false;
        }
    }
}
