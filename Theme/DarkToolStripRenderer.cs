using System.Drawing;
using System.Windows.Forms;

namespace Nomor_Whatsapp_Sender.Theme
{
    /// <summary>
    /// Custom renderer for ToolStrip and StatusStrip in dark theme.
    /// </summary>
    public class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer() : base(new DarkColorTable()) { }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using var brush = new SolidBrush(ThemeManager.Surface);
            e.Graphics.FillRectangle(brush, e.AffectedBounds);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                using var brush = new SolidBrush(ThemeManager.SurfaceAlt);
                var rect = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(brush, rect);
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            var btn = e.Item as ToolStripButton;
            var rect = new Rectangle(Point.Empty, e.Item.Size);

            if (btn != null && btn.Checked)
            {
                using var brush = new SolidBrush(Color.FromArgb(40, ThemeManager.Accent));
                e.Graphics.FillRectangle(brush, rect);
                using var pen = new Pen(ThemeManager.Accent, 1);
                e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
            else if (e.Item.Selected || e.Item.Pressed)
            {
                using var brush = new SolidBrush(ThemeManager.SurfaceAlt);
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? ThemeManager.TextPrimary : ThemeManager.TextSecondary;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            int y = e.Item.ContentRectangle.Height / 2;
            using var pen = new Pen(ThemeManager.Border);
            e.Graphics.DrawLine(pen, 4, y, e.Item.Width - 4, y);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // Draw bottom border line only
            using var pen = new Pen(ThemeManager.Border);
            e.Graphics.DrawLine(pen, 0, e.AffectedBounds.Height - 1,
                e.AffectedBounds.Width, e.AffectedBounds.Height - 1);
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            // No sizing grip in dark mode
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemImage(e);
        }
    }

    /// <summary>
    /// Dark color table for ToolStrip professional renderer.
    /// </summary>
    internal class DarkColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => ThemeManager.Surface;
        public override Color MenuItemSelected => ThemeManager.SurfaceAlt;
        public override Color MenuItemSelectedGradientBegin => ThemeManager.SurfaceAlt;
        public override Color MenuItemSelectedGradientEnd => ThemeManager.SurfaceAlt;
        public override Color MenuBorder => ThemeManager.Border;
        public override Color MenuItemBorder => ThemeManager.Border;
        public override Color MenuItemPressedGradientBegin => ThemeManager.BaseBg;
        public override Color MenuItemPressedGradientEnd => ThemeManager.BaseBg;
        public override Color MenuStripGradientBegin => ThemeManager.Surface;
        public override Color MenuStripGradientEnd => ThemeManager.Surface;
        public override Color ImageMarginGradientBegin => ThemeManager.Surface;
        public override Color ImageMarginGradientMiddle => ThemeManager.Surface;
        public override Color ImageMarginGradientEnd => ThemeManager.Surface;
        public override Color SeparatorDark => ThemeManager.Border;
        public override Color SeparatorLight => ThemeManager.Surface;
        public override Color StatusStripGradientBegin => ThemeManager.HeaderBg;
        public override Color StatusStripGradientEnd => ThemeManager.HeaderBg;
        public override Color ToolStripGradientBegin => ThemeManager.Surface;
        public override Color ToolStripGradientMiddle => ThemeManager.Surface;
        public override Color ToolStripGradientEnd => ThemeManager.Surface;
        public override Color ToolStripBorder => ThemeManager.Border;
        public override Color ToolStripContentPanelGradientBegin => ThemeManager.BaseBg;
        public override Color ToolStripContentPanelGradientEnd => ThemeManager.BaseBg;
        public override Color ButtonSelectedBorder => ThemeManager.Accent;
        public override Color ButtonSelectedGradientBegin => ThemeManager.SurfaceAlt;
        public override Color ButtonSelectedGradientEnd => ThemeManager.SurfaceAlt;
        public override Color ButtonPressedGradientBegin => ThemeManager.BaseBg;
        public override Color ButtonPressedGradientEnd => ThemeManager.BaseBg;
        public override Color CheckBackground => ThemeManager.Accent;
        public override Color CheckSelectedBackground => ThemeManager.AccentHover;
    }
}
