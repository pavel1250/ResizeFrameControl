using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizeFrameControl
{
    class ViewModel : BaseBindable
    {
        public ViewModel()
        {
            First = new Frame("Hello", 10,200);
            Second = new Frame("My friend", 10, 100);
        }
        public class Frame
        {
            public Frame(string name, double top_left, double height_weight)
            {
                Name = name;
                Top = Left = top_left;
                Height = Width = height_weight;
            }
            public Frame(string name, double top, double left, double height, double weight)
            {
                Name = name;
                Top = top;
                Left = left;
                Height = height;
                Width = weight;
            }
            public string Name { get; set; }
            public double Top { get; set; }
            public double Left { get; set; }
            public double Height { get; set; }
            public double Width { get; set; }
        }
        public Frame First { get; set; }
        public Frame Second { get; set; }
    }
}
