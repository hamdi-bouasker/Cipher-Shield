using System.Drawing;
using System.Windows.Forms;

namespace CipherShield
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DefaultProperty("Text")]
    public class VerticalLabel : Control
    {
        private string text = string.Empty;
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
        private BorderStyle borderStyle = BorderStyle.None;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        [Category("Appearance")]
        [Description("The text associated with the control.")]
        public override string Text
        {
            get => text;
            set
            {
                text = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The alignment of the text.")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment TextAlign
        {
            get => textAlign;
            set
            {
                textAlign = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The border style for the control.")]
        [DefaultValue(BorderStyle.None)]
        public BorderStyle BorderStyle
        {
            get => borderStyle;
            set
            {
                if (borderStyle != value)
                {
                    borderStyle = value;
                    Invalidate();
                }
            }
        }

        public VerticalLabel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            BackColor = Color.Black;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Fill background
            e.Graphics.Clear(BackColor);

            // Measure text before rotation
            SizeF textSize = e.Graphics.MeasureString(Text, Font);

            // Rotate text (top-to-bottom)
            e.Graphics.TranslateTransform(0, Height);
            e.Graphics.RotateTransform(-90);

            // Calculate X/Y for text alignment
            float x = 0, y = 0;

            // Horizontal alignment (after rotation)
            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    x = 0;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    x = (Height - textSize.Width) / 2;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    x = Height - textSize.Width;
                    break;
            }

            // Vertical alignment (after rotation)
            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    y = 0;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    y = (Width - textSize.Height) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    y = Width - textSize.Height;
                    break;
            }

            using (Brush textBrush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, textBrush, x, y);
            }

            e.Graphics.ResetTransform();

            // Draw border if needed
            DrawBorder(e.Graphics);
        }

        private void DrawBorder(Graphics g)
        {
            if (borderStyle == BorderStyle.None)
                return;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            switch (borderStyle)
            {
                case BorderStyle.FixedSingle:
                    ControlPaint.DrawBorder(g, rect, Color.Gray, ButtonBorderStyle.Solid);
                    break;

                case BorderStyle.Fixed3D:
                    ControlPaint.DrawBorder3D(g, rect, Border3DStyle.Sunken);
                    break;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
