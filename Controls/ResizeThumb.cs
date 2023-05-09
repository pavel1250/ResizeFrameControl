using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace ResizeFrameControl.Controls
{
    public class ResizeThumb : Thumb
    {
        public Func<Canvas?> GetCanvas;
        public Func<Control?> GetControl;

        public ResizeThumb(VerticalAlignment vertical, HorizontalAlignment horizont)
        {
            Thickness margin = this.Margin;
            margin.Top = margin.Left = margin.Right = margin.Bottom = 0;
            this.Margin = margin;
            this.VerticalAlignment = vertical;
            this.HorizontalAlignment = horizont;

            if(vertical == VerticalAlignment.Top && horizont == HorizontalAlignment.Left)
            {
                this.Width = this.Height = 7;
                this.Cursor = Cursors.SizeNWSE;
            }
            else if (vertical == VerticalAlignment.Top && horizont == HorizontalAlignment.Stretch)
            {
                this.Height = 3;
                this.Cursor = Cursors.SizeNS;
            }
            else if (vertical == VerticalAlignment.Top && horizont == HorizontalAlignment.Right)
            {
                this.Width = this.Height = 7;
                this.Cursor = Cursors.SizeNESW;
            }
            else if (vertical == VerticalAlignment.Stretch && horizont == HorizontalAlignment.Left)
            {
                this.Width = 3;
                this.Cursor = Cursors.SizeWE;
            }
            else if (vertical == VerticalAlignment.Stretch && horizont == HorizontalAlignment.Right)
            {
                this.Width = 3;
                this.Cursor = Cursors.SizeWE;
            }

            if (vertical == VerticalAlignment.Bottom && horizont == HorizontalAlignment.Left)
            {
                this.Width = this.Height = 7;
                this.Cursor = Cursors.SizeNESW;
            }
            else if (vertical == VerticalAlignment.Bottom && horizont == HorizontalAlignment.Stretch)
            {
                this.Height = 3;
                this.Cursor = Cursors.SizeNS;
            }
            else if (vertical == VerticalAlignment.Bottom && horizont == HorizontalAlignment.Right)
            {
                this.Width = this.Height = 7;
                this.Cursor = Cursors.SizeNWSE;
            }

            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Canvas? canvas = GetCanvas();
            Control? control = GetControl();

            if (canvas != null && control != null && control.IsEnabled)
            {
                //Canvas? childCanvas = Utils.FindFirstChild<Canvas>(designerItem) as Canvas;
                //Control? child = Utils.FindFirstChild<Control>(childCanvas) as Control;

                double current_left = (int)Canvas.GetLeft(control);
                double current_top = (int)Canvas.GetTop(control);
                double maxWidth = (int)(canvas.ActualWidth - current_left);
                double maxHeight = (int)(canvas.ActualHeight - current_top);

                double deltaVertical;
                switch (this.VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, control.ActualHeight - control.MinHeight);
                        control.Height = Utils.Clamp(control.Height - deltaVertical, 0, maxHeight);
                        break;

                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, control.ActualHeight - control.MinHeight);
                        double new_top = current_top + deltaVertical;
                        if (new_top < 0) deltaVertical = -current_top;
                        Canvas.SetTop(control, current_top + deltaVertical);
                        control.Height = control.Height - deltaVertical;
                        break;
                    default: break;
                }

                double deltaHorizontal;
                switch (this.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, control.ActualWidth - control.MinWidth);
                        double new_left = current_left + deltaHorizontal;
                        if (new_left < 0) deltaHorizontal = -current_left;
                        Canvas.SetLeft(control, current_left + deltaHorizontal);
                        control.Width = control.Width - deltaHorizontal;

                        break;

                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, control.ActualWidth - control.MinWidth);
                        control.Width = Utils.Clamp(control.Width - deltaHorizontal, 0, maxWidth);
                        break;
                    default: break;
                }
            }
            e.Handled = true;
        }
    }
    
    
}
