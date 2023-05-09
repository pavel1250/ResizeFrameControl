using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace ResizeFrameControl
{
    public static class Utils
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        public static IEnumerable<DependencyObject> GetChilds(DependencyObject depObj)
        {
            List<DependencyObject> childs = new List<DependencyObject>();
            if (depObj == null) return childs.ToArray();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                childs.Add(ithChild);
            }
            return childs;
        }
        public static DependencyObject? FindFirstChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            IEnumerable<DependencyObject> childs = Utils.GetChilds(depObj);
            foreach (DependencyObject ch in childs)
            {
                if (ch is T) return (DependencyObject)ch;
            }
            foreach (DependencyObject ch in childs)
            {
                return FindFirstChild<T>(ch);
            }
            return null;
        }
    }
}
