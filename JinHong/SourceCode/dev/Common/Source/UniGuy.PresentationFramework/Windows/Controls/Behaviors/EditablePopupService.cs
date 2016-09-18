using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Input;

namespace UniGuy.Controls.Behaviors
{
    public class EditablePopupService
    {
        // Fields
        public static readonly DependencyProperty EditablePopupProperty = DependencyProperty.RegisterAttached("EditablePopup", typeof(EditablePopup), typeof(EditablePopupService), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnEditablePopupChanged)));

        //  Properties
        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static EditablePopup GetEditablePopup(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            EditablePopup ep = (EditablePopup)element.GetValue(EditablePopupProperty);
            
            return ep;
        }
        public static void SetEditablePopup(DependencyObject element, EditablePopup value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(EditablePopupProperty, value);
        }

        private static void OnEditablePopupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement ue = d as UIElement;
            if (ue != null)
            {
                if (e.NewValue != null)
                {
                    ((EditablePopup)e.NewValue).PlacementTarget = ue;
                    ue.PreviewMouseRightButtonDown += OnMouseRightButtonDown;
                    ue.PreviewMouseRightButtonUp += OnMouseRightButtonUp;
                }
                else
                {
                    ue.PreviewMouseRightButtonDown -= OnMouseRightButtonDown;
                    ue.PreviewMouseRightButtonUp -= OnMouseRightButtonUp;
                }
            }
        }

        private static void OnMouseRightButtonDown(object sender, MouseButtonEventArgs args)
        {
            UIElement ue = (UIElement)sender;
            ((EditablePopup)ue.GetValue(EditablePopupProperty)).IsOpen = true;
        }
        private static void OnMouseRightButtonUp(object sender, MouseButtonEventArgs args)
        {
            UIElement ue = (UIElement)sender;
            ((EditablePopup)ue.GetValue(EditablePopupProperty)).IsOpen = false;
        }
    }
}
