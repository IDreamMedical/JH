using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 当数据发生改变时，可以记录SourceUpdatedTime和TargetUpdatedTime;
    /// SourceUpdated和TargetUpdated是Binding的附加事件，
    /// 如果一个控件的对应Binding设置了NotifyOnTargetUpdated或者NotifyOnSourceUpdated，
    /// 该控件就会适时激发SourceUpdated事件或者TargetUpdated时间，
    /// 本行为就在这些设置满足的情况下，分别在控件的SourceUpdated事件发生的时候记录其SourceUpdatedTime
    /// 和TargetUpated事件发生的时候记录其TargetUpdatedTime，这些时间数据可供程序使用
    /// </summary>
    public static class DataChangedTimeRecording
    {
        #region Dependencies

        #region IsSourceUpdatedTimeRecording
        /// <summary>
        /// 是否记录源更新时间
        /// </summary>
        public static readonly DependencyProperty IsSourceUpdatedTimeRecordingProperty
            = DependencyProperty.RegisterAttached("IsSourceUpdatedTimeRecording", typeof(bool), typeof(DataChangedTimeRecording),
            new PropertyMetadata(false, OnIsSourceUpdatedTimeRecordingPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static bool GetIsSourceUpdatedTimeRecording(DependencyObject d)
        {
            return (bool)d.GetValue(IsSourceUpdatedTimeRecordingProperty);
        }

        public static void SetIsSourceUpdatedTimeRecording(DependencyObject d, bool value)
        {
            d.SetValue(IsSourceUpdatedTimeRecordingProperty, value);
        }

        private static void OnIsSourceUpdatedTimeRecordingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement el = (FrameworkElement)d;

            if ((bool)args.NewValue)
                el.SourceUpdated += OnSourceUpdated;
            else
                el.SourceUpdated -= OnSourceUpdated;
        }

        #endregion

        #region IsSourceUpdatedTimeRecording
        /// <summary>
        /// 是否记录目标更新时间
        /// </summary>
        public static readonly DependencyProperty IsTargetUpdatedTimeRecordingProperty
            = DependencyProperty.RegisterAttached("IsTargetUpdatedTimeRecording", typeof(bool), typeof(DataChangedTimeRecording),
            new PropertyMetadata(false, OnIsTargetUpdatedTimeRecordingPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static bool GetIsTargetUpdatedTimeRecording(DependencyObject d)
        {
            return (bool)d.GetValue(IsTargetUpdatedTimeRecordingProperty);
        }

        public static void SetIsTargetUpdatedTimeRecording(DependencyObject d, bool value)
        {
            d.SetValue(IsTargetUpdatedTimeRecordingProperty, value);
        }

        private static void OnIsTargetUpdatedTimeRecordingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement el = (FrameworkElement)d;

            if ((bool)args.NewValue)
                el.TargetUpdated += OnTargetUpdated;
            else
                el.TargetUpdated -= OnTargetUpdated;
        }

        #endregion

        #region SourceUpdatedTime
        /// <summary>
        /// 记录的源更新时间
        /// </summary>
        public static readonly DependencyProperty SourceUpdatedTimeProperty
            = DependencyProperty.RegisterAttached("SourceUpdatedTime", typeof(DateTime?), typeof(DataChangedTimeRecording));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static DateTime? GetSourceUpdatedTime(DependencyObject d)
        {
            return (DateTime?)d.GetValue(SourceUpdatedTimeProperty);
        }

        public static void SetSourceUpdatedTime(DependencyObject d, DateTime? value)
        {
            d.SetValue(SourceUpdatedTimeProperty, value);
        }
        #endregion

        #region TargetUpdatedTime
        /// <summary>
        /// 记录的目标更新时间
        /// </summary>
        public static readonly DependencyProperty TargetUpdatedTimeProperty
            = DependencyProperty.RegisterAttached("TargetUpdatedTime", typeof(DateTime?), typeof(DataChangedTimeRecording));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static DateTime? GetTargetUpdatedTime(DependencyObject d)
        {
            return (DateTime?)d.GetValue(TargetUpdatedTimeProperty);
        }

        public static void SetTargetUpdatedTime(DependencyObject d, DateTime? value)
        {
            d.SetValue(TargetUpdatedTimeProperty, value);
        }
        #endregion

        #endregion

        #region Event Handlers
        /// <summary>
        /// 记录源更新时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnSourceUpdated(object sender, DataTransferEventArgs args)
        {
            SetSourceUpdatedTime((FrameworkElement)sender, DateTime.Now);
        }

        /// <summary>
        /// 记录目标更新时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnTargetUpdated(object sender, DataTransferEventArgs args)
        {
            SetSourceUpdatedTime((FrameworkElement)sender, DateTime.Now);
        }
        #endregion
    }
}
