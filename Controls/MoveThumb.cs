using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace ResizeFrameControl.Controls
{

    public class MoveThumb : Thumb
    {
        public static readonly DependencyProperty MoveThumbCanvasProperty = DependencyProperty.Register("MoveThumbCanvas", typeof(Canvas), typeof(MoveThumb));
        public Canvas? MoveThumbCanvas
        {
            get => (Canvas)GetValue(MoveThumbCanvasProperty);
            set => SetValue(MoveThumbCanvasProperty, value);
        }

        public static readonly DependencyProperty MoveThumbControlProperty = DependencyProperty.Register("MoveThumbControl", typeof(Control), typeof(MoveThumb));
        public Control? MoveThumbControl
        {
            get => (Control)GetValue(MoveThumbControlProperty);
            set => SetValue(MoveThumbControlProperty, value);
        }
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
            MouseDoubleClick += new MouseButtonEventHandler(this.MoveThumb_MouseDoubleClick);
        }
        private void MoveThumb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           

            bool isCtrl = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
            bool isAlt = Keyboard.Modifiers.HasFlag(ModifierKeys.Alt);
            
            if (MoveThumbCanvas != null && MoveThumbControl != null && MoveThumbControl.IsEnabled)
            {
                if (isCtrl && !isAlt)
                {
                    MoveThumbControl.Height = MoveThumbCanvas.ActualHeight;
                    MoveThumbControl.Width = MoveThumbCanvas.ActualWidth;
                    Canvas.SetLeft(MoveThumbControl, 0);
                    Canvas.SetTop(MoveThumbControl, 0);
                }
                else if (isAlt && !isCtrl)
                {
                    MoveThumbControl.Height = MoveThumbCanvas.ActualHeight / 2;
                    MoveThumbControl.Width = MoveThumbCanvas.ActualWidth / 2;
                    Canvas.SetLeft(MoveThumbControl, MoveThumbControl.Width / 2);
                    Canvas.SetTop(MoveThumbControl, MoveThumbControl.Height / 2);
                }
                else
                {
                    double maxLeft = MoveThumbCanvas.ActualWidth - MoveThumbControl.Width;
                    double maxTop = MoveThumbCanvas.ActualHeight - MoveThumbControl.Height;

                    double new_top = maxTop / 2;
                    double new_left = maxLeft / 2;

                    SetWithClamp(MoveThumbControl, new_left, new_top);
                }
            }
        }
        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (MoveThumbControl != null && MoveThumbControl.IsEnabled)
            {

                double new_left = Canvas.GetLeft(MoveThumbControl) + e.HorizontalChange;
                double new_top = Canvas.GetTop(MoveThumbControl) + e.VerticalChange;

                SetWithClamp(MoveThumbControl, new_left, new_top);
            }
        }
        private void SetWithClamp(Control item, double new_left, double new_top)
        {
            if (MoveThumbCanvas != null && item != null)
            {
                double maxLeft = MoveThumbCanvas.ActualWidth - item.Width;
                double maxTop = MoveThumbCanvas.ActualHeight - item.Height;

                Canvas.SetLeft(item, Utils.Clamp(new_left, 0, maxLeft));
                Canvas.SetTop(item, Utils.Clamp(new_top, 0, maxTop));
            }
        }
    }
}
