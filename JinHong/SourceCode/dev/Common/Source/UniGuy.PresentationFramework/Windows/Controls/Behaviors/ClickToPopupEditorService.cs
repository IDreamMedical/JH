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
    public class ClickToPopupEditorService
    {
        // Fields
        public static readonly DependencyProperty PopupEditorProperty = DependencyProperty.RegisterAttached("PopupEditor", typeof(PopupEditor), typeof(ClickToPopupEditorService), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPopupEditorPropertyChanged)));

        //  Properties
        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static PopupEditor GetPopupEditor(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            PopupEditor pe = (PopupEditor)element.GetValue(PopupEditorProperty);

            return pe;
        }
        public static void SetPopupEditor(DependencyObject element, PopupEditor value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(PopupEditorProperty, value);
        }

        private static void OnPopupEditorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement ue = d as UIElement;
            PopupEditor pe = e.NewValue as PopupEditor;
            if (ue != null)
            {
                if (pe != null)
                {
                    pe.PlacementTarget = ue;
                    pe.LostFocus += OnLostFocus;
                    ue.PreviewMouseDown += OnMouseDown;
                }
                else
                {
                    ((PopupEditor)e.OldValue).PlacementTarget = null;
                    ((PopupEditor)e.OldValue).LostFocus -= OnLostFocus;
                    ue.PreviewMouseDown -= OnMouseDown;
                }
            }

        }

        private static void OnMouseDown(object sender, MouseButtonEventArgs args)
        {
            UIElement ue = (UIElement)sender;
            PopupEditor pe = GetPopupEditor(ue);
            pe.Focus();
            pe.IsOpen = true;
            args.Handled = true;
        }

        private static void OnLostFocus(object sender, EventArgs args)
        {
            PopupEditor pe = (PopupEditor)sender;
            pe.IsOpen = false;
            if (pe.PlacementTarget != null)
                pe.PlacementTarget.Focus();
        }
    }
}
