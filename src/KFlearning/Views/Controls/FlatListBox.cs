// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : FlatListBox.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KFlearning.Views.Controls
{
    public class FlatListBox
    {
        public static void DrawItemHandler(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            e.DrawFocusRectangle();

            var listBox = (ListBox) sender;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (e.State.HasFlag(DrawItemState.Selected))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(35, 168, 109)),
                    new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(51, 53, 55)),
                    new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            }

            e.Graphics.DrawString(" " + listBox.Items[e.Index], new Font("Segoe UI", 8), Brushes.White, e.Bounds.X,
                e.Bounds.Y + 2);
            e.Graphics.Dispose();
        }
    }
}