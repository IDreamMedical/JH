using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UniGuy.Controls.Panels
{
    /// <summary>
    /// 来自网上
    /// This one is old school.  I created it for Kaxaml originally, but continue to get a lot of mileage from it.  It’s a derived StackPanel that sets attached properties on its children to indicate where they show up in the panel.  I use it a lot with an ItemsControl.  It tells you whether it’s odd/even (slightly less useful now that we have alternating row colors built into the framework), first/last/middle and where it is relative to the selected item in a Selector.
    /// My favorite use case for this is dealing with rounded corners on a menu or in a list.  The problem shows up if the thing that contains your list has rounded corners on the top and bottom.  In that case, you want all items to be square except the top and bottom.  With IndexingStackPanel you can just trigger of the attached property to find if you’re top or bottom or middle and adjust the corner radius of the item as appropriate.
    ///By the way, if you use this with an ItemsControl, you need to create the triggers in the ItemContainerStyle (not the ItemTemplate).
    /// </summary>
    public class IndexingStackPanel : StackPanel
    {
        #region Index (Attached Dependency Property)

        public static int GetIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(IndexProperty);
        }

        public static void SetIndex(DependencyObject obj, int value)
        {
            obj.SetValue(IndexProperty, value);
        }

        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.RegisterAttached("Index", typeof(int), typeof(IndexingStackPanel), new UIPropertyMetadata(default(int)));

        #endregion

        #region SelectionLocation

        public static SelectionLocation GetSelectionLocation(DependencyObject obj)
        {
            return (SelectionLocation)obj.GetValue(SelectionLocationProperty);
        }

        public static void SetSelectionLocation(DependencyObject obj, SelectionLocation value)
        {
            obj.SetValue(SelectionLocationProperty, value);
        }

        public static readonly DependencyProperty SelectionLocationProperty =
            DependencyProperty.RegisterAttached("SelectionLocation", typeof(SelectionLocation), typeof(IndexingStackPanel), new UIPropertyMetadata(default(SelectionLocation)));

        #endregion

        #region StackLocation (Attached Dependency Property)

        public static StackLocation GetStackLocation(DependencyObject obj)
        {
            return (StackLocation)obj.GetValue(StackLocationProperty);
        }

        public static void SetStackLocation(DependencyObject obj, StackLocation value)
        {
            obj.SetValue(StackLocationProperty, value);
        }

        public static readonly DependencyProperty StackLocationProperty =
            DependencyProperty.RegisterAttached("StackLocation", typeof(StackLocation), typeof(IndexingStackPanel), new UIPropertyMetadata(default(StackLocation)));

        #endregion

        #region IndexOddEven (Attached DependencyProperty)

        public static IndexOddEven GetIndexOddEven(DependencyObject obj)
        {
            return (IndexOddEven)obj.GetValue(IndexOddEvenProperty);
        }

        public static void SetIndexOddEven(DependencyObject obj, IndexOddEven value)
        {
            obj.SetValue(IndexOddEvenProperty, value);
        }

        public static readonly DependencyProperty IndexOddEvenProperty =
            DependencyProperty.RegisterAttached("IndexOddEven", typeof(IndexOddEven), typeof(IndexingStackPanel), new UIPropertyMetadata(default(IndexOddEven)));


        #endregion

        #region Overrides

        protected override Size MeasureOverride(Size constraint)
        {
            int index = 0;
            bool isEven = true;
            bool foundSelected = false;

            foreach (UIElement element in this.Children)
            {

                if (this.IsItemsHost)
                {
                    Selector SelectorParent = this.TemplatedParent as Selector;

                    if (SelectorParent != null)
                    {
                        UIElement selectedElement = (SelectorParent.ItemContainerGenerator.ContainerFromItem(SelectorParent.SelectedItem) as UIElement);

                        if (selectedElement != null)
                        {
                            if (element == selectedElement)
                            {
                                element.SetValue(SelectionLocationProperty, SelectionLocation.Selected);
                                foundSelected = true;
                            }
                            else if (foundSelected)
                            {
                                element.SetValue(SelectionLocationProperty, SelectionLocation.After);
                            }
                            else
                            {
                                element.SetValue(SelectionLocationProperty, SelectionLocation.Before);
                            }
                        }
                    }
                }

                // StackLocation

                if (Children.Count - 1 == 0)
                {
                    element.SetValue(StackLocationProperty, StackLocation.FirstAndLast);
                }
                else if (index == 0)
                {
                    element.SetValue(StackLocationProperty, StackLocation.First);
                }
                else if (index == Children.Count - 1)
                {
                    element.SetValue(StackLocationProperty, StackLocation.Last);
                }
                else
                {
                    element.SetValue(StackLocationProperty, StackLocation.Middle);
                }

                // IndexOddEven

                if (isEven)
                {
                    element.SetValue(IndexOddEvenProperty, IndexOddEven.Even);
                }
                else
                {
                    element.SetValue(IndexOddEvenProperty, IndexOddEven.Odd);
                }

                element.SetValue(IndexProperty, index);
                index++;

            }

            return base.MeasureOverride(constraint);
        }

        #endregion
    }

    public enum StackLocation
    {
        First,
        Middle,
        Last,
        FirstAndLast
    }

    public enum SelectionLocation
    {
        Before,
        Selected,
        After
    }

    public enum IndexOddEven
    {
        Odd,
        Even
    }
}
