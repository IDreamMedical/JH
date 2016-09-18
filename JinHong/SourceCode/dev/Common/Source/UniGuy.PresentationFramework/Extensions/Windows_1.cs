using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls; 
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading; 

namespace UniGuy.Extensions
{
    /// <summary>
    /// 一些辅助扩展方法, 主要是WPF需要很多这样的辅助方法
    /// </summary>
    public static class WindowsUtilities
    {
        // Useful for getting the item associated with the element clicked in a ListBox (generically any ItemsControl).    
        public static object GetItemFromElement(this ItemsControl itemsControl, DependencyObject element)
        {
            return itemsControl.ItemContainerGenerator.ItemFromContainer(itemsControl.ContainerFromElement(element));
        }
        // Same as GetBounds method, but offsets bounds by Margin of element.      
        public static Rect GetBoundsWithMargin(this FrameworkElement element)
        {
            var bounds = GetBounds(element);
            var margin = element.Margin;
            return new Rect(bounds.Left + margin.Left, bounds.Top + margin.Top, bounds.Right - margin.Right,
                bounds.Bottom - margin.Bottom);
        }
        public static Rect GetBounds(this UIElement element)
        {
            var window = Window.GetWindow(element);
            var position = element.TranslatePoint(new Point(0, 0), window);
            return new Rect(position, element.RenderSize);
        }
        public static Rect SubtractMargin(this Rect rect, Thickness margin)
        {
            return new Rect(rect.X + margin.Left, rect.Y + margin.Top, rect.Width - margin.Left - margin.Right,
                rect.Height - margin.Top - margin.Bottom);
        }
        public static TAncestor FindVisualAncestor<TAncestor>(this DependencyObject obj)
            where TAncestor : DependencyObject
        {
            var current = obj;
            do
            {
                if (current is TAncestor)
                    return (TAncestor)current;
                current = VisualTreeHelper.GetParent(current);
            } while (current != null);
            return null;
        }
        public static TAncestor FindLogicalAncestor<TAncestor>(this DependencyObject obj)
            where TAncestor : DependencyObject
        {
            var current = obj;
            do
            {
                if (current is TAncestor)
                    return (TAncestor)current;
                current = LogicalTreeHelper.GetParent(current);
            } while (current != null);
            return null;
        }
        public static IEnumerable<TAncestor> FindVisualDescendants<TAncestor>(this DependencyObject obj)
            where TAncestor : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is TAncestor)
                    yield return (TAncestor)child;
                foreach (var subChild in FindVisualDescendants<TAncestor>(child))
                    yield return subChild;
            }
        }
        public static IEnumerable<TAncestor> FindLogicalDescendants<TAncestor>(this DependencyObject obj)
            where TAncestor : DependencyObject
        {
            foreach (var child in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
            {
                if (child is TAncestor)
                    yield return (TAncestor)child;
                foreach (var subChild in FindVisualDescendants<TAncestor>(child))
                    yield return subChild;
            }
        }
        // This eliminates the slightly cumbersome task of casting a lambda expression to a delegate when calling Dispatcher.Invoke.     
        public static void Invoke(this Dispatcher dispatcher, Action action)
        {
            dispatcher.Invoke(action);
        }
    }
}
