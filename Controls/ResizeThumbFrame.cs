using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Data;

namespace ResizeFrameControl.Controls
{
    public class ResizeThumbFrame : ContentControl
    {
        public static readonly DependencyProperty ResizeThumbFrameCanvasProperty = DependencyProperty.Register("ResizeThumbFrameCanvas", typeof(Canvas), typeof(ResizeThumbFrame));
        public Canvas? ResizeThumbFrameCanvas
        {
            get => (Canvas)GetValue(ResizeThumbFrameCanvasProperty);
            set => SetValue(ResizeThumbFrameCanvasProperty, value);
        }

        public static readonly DependencyProperty ResizeThumbFrameControlProperty = DependencyProperty.Register("ResizeThumbFrameControl", typeof(Control), typeof(ResizeThumbFrame));
        public Control? ResizeThumbFrameControl
        {
            get => (Control)GetValue(ResizeThumbFrameControlProperty);
            set => SetValue(ResizeThumbFrameControlProperty, value);
        }

        public static readonly DependencyProperty ResizeThumbFrameTemplateProperty = DependencyProperty.Register("ResizeThumbFrameTemplate", typeof(ControlTemplate), typeof(ResizeThumbFrame));
        public ControlTemplate? ResizeThumbFrameTemplate
        {
            get => (ControlTemplate)GetValue(ResizeThumbFrameTemplateProperty);
            set => SetValue(ResizeThumbFrameTemplateProperty, value);
        }

        public ResizeThumbFrame()
        {
            __Grid = new Grid();
            __TopLeft = new ResizeThumb(VerticalAlignment.Top, HorizontalAlignment.Left);
            __TopMid = new ResizeThumb(VerticalAlignment.Top, HorizontalAlignment.Stretch);
            __TopRight = new ResizeThumb(VerticalAlignment.Top, HorizontalAlignment.Right);

            __MidLeft = new ResizeThumb(VerticalAlignment.Stretch, HorizontalAlignment.Left);
            __MidRight = new ResizeThumb(VerticalAlignment.Stretch, HorizontalAlignment.Right);

            __BotLeft = new ResizeThumb(VerticalAlignment.Bottom, HorizontalAlignment.Left);
            __BotMid = new ResizeThumb(VerticalAlignment.Bottom, HorizontalAlignment.Stretch);
            __BotRight = new ResizeThumb(VerticalAlignment.Bottom, HorizontalAlignment.Right);

            List = new List<ResizeThumb>();
            List.Add(__TopMid);
            List.Add(__MidLeft);
            List.Add(__MidRight);
            List.Add(__BotMid);
            List.Add(__TopLeft);
            List.Add(__TopRight);
            List.Add(__BotLeft);
            List.Add(__BotRight);

            foreach (var item in List)
            {
                item.GetCanvas = () => { return ResizeThumbFrameCanvas; };
                item.GetControl = () => { return ResizeThumbFrameControl; };
                __Grid.Children.Add(item);
            }
            this.AddChild(__Grid);
        }

        List<ResizeThumb> List;

        private ResizeThumb __TopLeft;
        private ResizeThumb __TopMid;
        private ResizeThumb __TopRight;
        private ResizeThumb __MidLeft;
        private ResizeThumb __MidRight;
        private ResizeThumb __BotLeft;
        private ResizeThumb __BotMid;
        private ResizeThumb __BotRight;
        private Grid __Grid;
    }
}
