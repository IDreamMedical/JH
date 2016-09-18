using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace UniGuy.Controls.Panels
{
    /// <summary>
    /// From PixelLab
    /// This is a super simple panel that derives from Grid.  It allows you to specify names for your rows and columns and then assign items by name (rather than by index).  I really like this for two reasons: first, it gives you self documenting XAML and second because it allows you to add and remove columns and rows without need to reindex all the content in your panel.  Here’s what the XAML looks like.
    /*
    <c:SmartGrid>
        <c:SmartGrid.RowDefinitions>
            <c:SmartRowDefinition RowName="Header" Height="65" />
            <c:SmartRowDefinition RowName="Body" Height="*" />
        </c:SmartGrid.RowDefinitions>
     
        <Rectangle Fill="YellowGreen" c:SmartGrid.RowName="Header" />
        <Rectangle Fill="Orange" c:SmartGrid.RowName="Body" />
     
    </c:SmartGrid>*/
    /// I tried to port this one from WPF to Silverlight but I couldn’t find the right hooks.  Unfortunately Silverlight Grid has sealed up Measure and Arrange and the other sizing events were too late to set the indexes properly on the items.  There may have been something else too, but you may succeed here where I failed.  Let me know if your port it.
    /// </summary>
    public class SmartGrid : Grid
    {
        #region RowName

        /// <summary>
        /// RowName Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty RowNameProperty =
            DependencyProperty.RegisterAttached("RowName", typeof(string), typeof(SmartGrid),
                new FrameworkPropertyMetadata((string)null,
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        /// <summary>
        /// Gets the RowName property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static string GetRowName(DependencyObject d)
        {
            return (string)d.GetValue(RowNameProperty);
        }

        /// <summary>
        /// Sets the RowName property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetRowName(DependencyObject d, string value)
        {
            d.SetValue(RowNameProperty, value);
        }

        #endregion

        #region ColumnName

        /// <summary>
        /// ColumnName Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ColumnNameProperty =
            DependencyProperty.RegisterAttached("ColumnName", typeof(string), typeof(SmartGrid),
                new FrameworkPropertyMetadata((string)null,
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        /// <summary>
        /// Gets the ColumnName property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static string GetColumnName(DependencyObject d)
        {
            return (string)d.GetValue(ColumnNameProperty);
        }

        /// <summary>
        /// Sets the ColumnName property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetColumnName(DependencyObject d, string value)
        {
            d.SetValue(ColumnNameProperty, value);
        }

        #endregion

        #region Overrides

        protected override Size MeasureOverride(Size constraint)
        {
            foreach (UIElement child in this.Children)
            {
                string rowName = child.GetValue(SmartGrid.RowNameProperty) as string;
                if (rowName != null)
                {
                    int? rowIndex = GetRowIndexFromName(rowName);
                    if (rowIndex != null) child.SetValue(Grid.RowProperty, rowIndex.Value);
                }

                string columnName = child.GetValue(SmartGrid.ColumnNameProperty) as string;
                if (columnName != null)
                {
                    int? columnIndex = GetColumnIndexFromName(columnName);
                    if (columnIndex != null) child.SetValue(Grid.ColumnProperty, columnIndex.Value);
                }
            }

            return base.MeasureOverride(constraint);
        }

        #endregion

        #region Private Fields

        Dictionary<string, int> RowNames = new Dictionary<string, int>();
        Dictionary<string, int> ColumnNames = new Dictionary<string, int>();

        #endregion

        #region Private Methods

        private int? GetRowIndexFromName(string Name)
        {
            if (RowNames.ContainsKey(Name))
            {
                return RowNames[Name];
            }
            else
            {
                int index = 0;
                foreach (RowDefinition r in this.RowDefinitions)
                {
                    SmartRowDefinition s = r as SmartRowDefinition;
                    if (s != null && s.RowName.Equals(Name))
                    {
                        RowNames.Add(Name, index);
                        return index;
                    }
                    index++;
                }
            }

            return null;
        }

        private int? GetColumnIndexFromName(string Name)
        {
            if (ColumnNames.ContainsKey(Name))
            {
                return ColumnNames[Name];
            }
            else
            {
                int index = 0;
                foreach (ColumnDefinition c in this.ColumnDefinitions)
                {
                    SmartColumnDefinition s = c as SmartColumnDefinition;
                    if (s != null && s.ColumnName.Equals(Name))
                    {
                        ColumnNames.Add(Name, index);
                        return index;
                    }
                    index++;
                }
            }

            return null;
        }

        #endregion

        #region Public Methods

        // This implementation optimizes for the (very common) case where Row and Column definitions
        // don't change at runtime.  This allows us to build up a cache to make lookup super quick.
        // If you do find yourself making changes to the RowDefinitions or ColumnDefinitions collection
        // at runtime, then call ClearNameCache to tell the grid to flush the current cache and begin
        // rebuilding.

        public void ClearNameCaches()
        {
            RowNames.Clear();
            ColumnNames.Clear();
        }

        #endregion
    }

    public class SmartRowDefinition : RowDefinition
    {
        #region RowName

        /// <summary>
        /// RowName Dependency Property
        /// </summary>
        public static readonly DependencyProperty RowNameProperty =
            DependencyProperty.Register("RowName", typeof(string), typeof(SmartRowDefinition),
                new FrameworkPropertyMetadata((string)null));

        /// <summary>
        /// Gets or sets the RowName property.  This dependency property 
        /// indicates ....
        /// </summary>
        public string RowName
        {
            get { return (string)GetValue(RowNameProperty); }
            set { SetValue(RowNameProperty, value); }
        }

        #endregion
    }

    public class SmartColumnDefinition : ColumnDefinition
    {
        #region ColumnName

        /// <summary>
        /// ColumnName Dependency Property
        /// </summary>
        public static readonly DependencyProperty ColumnNameProperty =
            DependencyProperty.Register("ColumnName", typeof(string), typeof(SmartColumnDefinition),
                new FrameworkPropertyMetadata((string)null));

        /// <summary>
        /// Gets or sets the ColumnName property.  This dependency property 
        /// indicates ....
        /// </summary>
        public string ColumnName
        {
            get { return (string)GetValue(ColumnNameProperty); }
            set { SetValue(ColumnNameProperty, value); }
        }

        #endregion
    }

}

