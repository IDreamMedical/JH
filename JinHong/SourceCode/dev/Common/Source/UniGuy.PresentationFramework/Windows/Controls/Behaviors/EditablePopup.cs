using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 弹出式文本框
    /// </summary>
    public class EditablePopup:TextBox
    {
        #region Fields
        /// <summary>
        /// 父弹出项
        /// </summary>
        protected Popup popupParent;

        #region 6 DependencyProperties from Popup
        public static readonly DependencyProperty PlacementProperty
            = Popup.PlacementProperty.AddOwner(typeof(EditablePopup), new PropertyMetadata(PlacementMode.Bottom));
        public static readonly DependencyProperty PlacementTargetProperty
            = Popup.PlacementTargetProperty.AddOwner(typeof(EditablePopup));
        public static readonly DependencyProperty PlacementRectangleProperty
            = Popup.PlacementRectangleProperty.AddOwner(typeof(EditablePopup));
        public static readonly DependencyProperty HorizontalOffsetProperty
            = Popup.HorizontalAlignmentProperty.AddOwner(typeof(EditablePopup));
        public static readonly DependencyProperty VerticalOffsetProperty
            = Popup.VerticalAlignmentProperty.AddOwner(typeof(EditablePopup));
        public static readonly DependencyProperty IsOpenProperty
            = Popup.IsOpenProperty.AddOwner(typeof(EditablePopup)
            , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                , new PropertyChangedCallback(OnIsOpenChanged)));
        #endregion

        #endregion

        #region Properties
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }
        public UIElement PlacementTarget
        {
            get { return (UIElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }
        public Rect PlacementRectangle
        {
            get { return (Rect)GetValue(PlacementRectangleProperty); }
            set { SetValue(PlacementRectangleProperty, value); }
        }
        public new double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }
        public new double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        #endregion

        #region Ctor
        public EditablePopup()
            : base()
        {
        }
        #endregion

        static EditablePopup()
        {
            //This OverrideMetadata call tells the system that this element 
            //wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(EditablePopup),
                //new FrameworkPropertyMetadata(typeof(EditablePopup)));
        }

        #region Methods

        private void HookupParentPopup()
        {
            popupParent = new Popup();
            popupParent.AllowsTransparency = true;
            //  这会建立以上六个依赖属性的双向绑定，同时设置本对象为popupParent的Child
            //  Now that we’ve made the control, there are a few things to keep in mind before you use the control.  First, set PlacementTarget before you call CreateRootPopup.  If you call CreateRootPopup first, the PlacementTarget is ignored.  Essentially this means you need to set PlacementTarget before setting IsOpen to true, just like you need to for a Popup.
            //  Second, CreateRootPopup sets the Child property of the Popup to your custom control.  As a result, your custom control cannot have a logical or visual parent and the following doesn’t work
            Popup.CreateRootPopup(popupParent, this);
        }

        #region Callbacks
        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EditablePopup ep = (EditablePopup)d;

            if ((bool)e.NewValue)
            {
                if (ep.popupParent == null)
                    ep.HookupParentPopup();
            }
        }
        #endregion

        #endregion
    }
}
