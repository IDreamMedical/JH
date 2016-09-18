﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Controls.Behaviors.AdornerBehavior
{
    public class ContentAdorner : Adorner
    {
        VisualCollection children;
        FrameworkElement child;

        // Be sure to call the base class constructor.
        public ContentAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.children = new VisualCollection(this);

            //
            // Create the content control
            //
            var contentControl = new ContentControl();

            //
            // Bind the content control to the Adorner's ContentTemplate property, so we know what to display
            //
            var contentTemplateBinding = new Binding();
            contentTemplateBinding.Path = new PropertyPath(AdornerContentTemplateProperty);
            contentTemplateBinding.Source = adornedElement;
            contentControl.SetBinding(ContentControl.ContentTemplateProperty, contentTemplateBinding);

            //
            // Add the ContentControl as a child
            //
            this.child = contentControl;
            this.children.Add(this.child);
            this.AddLogicalChild(this.child);
        }

        //
        // The AdornerContentTemplate property is used to attach a template for adorner content to a given item.
        //

        public static DependencyProperty AdornerContentTemplateProperty = DependencyProperty.RegisterAttached("AdornerContentTemplate", typeof(DataTemplate), typeof(ContentAdorner), new PropertyMetadata(null, OnAdornerContentTemplatePropertyChanged));

        public static DataTemplate GetAdornerContentTemplate(DependencyObject element)
        {
            return (DataTemplate)element.GetValue(AdornerContentTemplateProperty);
        }

        public static void SetAdornerContentTemplate(DependencyObject element, DataTemplate value)
        {
            element.SetValue(AdornerContentTemplateProperty, value);
        }

        static void OnAdornerContentTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (FrameworkElement)sender;

            //
            // TODO: Remove old template
            //

            if (e.NewValue != null)
            {
                if (target.IsLoaded)
                {
                    ApplyContentAdorner(target);
                }
                else
                {
                    //
                    // Controls not loaded don't have an adorner layer yet.
                    //
                    target.Loaded += OnAdornerTargetLoaded;
                }
            }
        }

        static void OnAdornerTargetLoaded(object sender, RoutedEventArgs e)
        {
            var target = (FrameworkElement)sender;
            target.Loaded -= OnAdornerTargetLoaded;
            ApplyContentAdorner(target);
        }

        private static void ApplyContentAdorner(FrameworkElement target)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(target);

            var adorner = new ContentAdorner(target);

            adornerLayer.Add(adorner);
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.children[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return this.children.Count;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.child.Measure(constraint);
            return AdornedElement.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Point location = new Point(0, 0);
            Rect rect = new Rect(location, finalSize);
            this.child.Arrange(rect);
            return this.child.RenderSize;
        }
    }
}
