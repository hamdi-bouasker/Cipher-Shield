using System;
using System.Drawing;
using System.Windows.Forms;

namespace CipherShield
{
    public class VerticalLabel : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            // Save the current state of the graphics object
            Graphics g = e.Graphics;
            g.TranslateTransform(0, this.Height);
            g.RotateTransform(-90); // Rotate the text 90 degrees counter-clockwise

            // Draw the text
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), 0, 0);
        }
    }
}
