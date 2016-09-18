using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 一个通用的用于Popup内容的ListBox, 主要用在TextBox的一些行为中, 比如AutoComplete等等, AutoCompleteTip可以用本类替换, 暂时还没处理
    /// </summary>
    public class ListBoxForPopup:ListBox
    {
        #region Fields
        /// <summary>
        /// 父弹出项
        /// </summary>
        protected Popup popupParent;

        #region 6 DependencyProperties from Popup
        public static readonly DependencyProperty CustomPopupPlacementCallbackProperty
            = Popup.CustomPopupPlacementCallbackProperty.AddOwner(typeof(ListBoxForPopup));
        public static readonly DependencyProperty StaysOpenProperty
            = Popup.StaysOpenProperty.AddOwner(typeof(ListBoxForPopup));
        public static readonly DependencyProperty PlacementProperty
            = Popup.PlacementProperty.AddOwner(typeof(ListBoxForPopup), new PropertyMetadata(PlacementMode.Bottom));
        public static readonly DependencyProperty PlacementTargetProperty
            = Popup.PlacementTargetProperty.AddOwner(typeof(ListBoxForPopup));
        public static readonly DependencyProperty PlacementRectangleProperty
            = Popup.PlacementRectangleProperty.AddOwner(typeof(ListBoxForPopup));
        public static readonly DependencyProperty HorizontalOffsetProperty
            = Popup.HorizontalOffsetProperty.AddOwner(typeof(ListBoxForPopup));
        public static readonly DependencyProperty VerticalOffsetProperty
            = Popup.VerticalOffsetProperty.AddOwner(typeof(ListBoxForPopup));
        public static readonly DependencyProperty IsOpenProperty
            = Popup.IsOpenProperty.AddOwner(typeof(ListBoxForPopup)
            , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                , new PropertyChangedCallback(OnIsOpenChanged)));
        #endregion

        #endregion

        #region Properties
        // Properties
        [Bindable(false), Category("Layout")]
        public CustomPopupPlacementCallback CustomPopupPlacementCallback
        {
            get
            {
                return (CustomPopupPlacementCallback)base.GetValue(CustomPopupPlacementCallbackProperty);
            }
            set
            {
                base.SetValue(CustomPopupPlacementCallbackProperty, value);
            }
        }

        [Category("Behavior"), Bindable(true)]
        public bool StaysOpen
        {
            get
            {
                return (bool)base.GetValue(StaysOpenProperty);
            }
            set
            {
                base.SetValue(StaysOpenProperty, value);
            }
        }

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
        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }
        public double VerticalOffset
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
        public ListBoxForPopup()
            : base()
        {
        }
        #endregion

        static ListBoxForPopup()
        {
            //This OverrideMetadata call tells the system that this element 
            //wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteTip),
                //new FrameworkPropertyMetadata(typeof(AutoCompleteTip)));
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
            ListBoxForPopup lbfp = (ListBoxForPopup)d;

            if ((bool)e.NewValue)
            {
                if (lbfp.popupParent == null)
                    lbfp.HookupParentPopup();
            }
        }
        #endregion

        #endregion
    }
}
