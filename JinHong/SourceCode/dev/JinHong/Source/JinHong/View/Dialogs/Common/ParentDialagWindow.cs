using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace JinHong.View.Dialogs
{
    public class ParentDialagWindow : Window, INotifyPropertyChanged
    {


        #region Filed
        DialogViewMode _mode;


        #endregion
        #region Dependency properties
        public static readonly DependencyProperty ModeProperty
    = DependencyProperty.Register("Mode", typeof(DialogViewMode), typeof(ParentDialagWindow), new PropertyMetadata(ModePropertyChanged));


        #endregion
        public ParentDialagWindow()
        {
        }


        #region Properties
        //public DialogViewMode Mode
        //{
        //    get { return _mode; }
        //    set
        //    {
        //        if (_mode != value)
        //        {
        //            _mode = value;
        //            OnPropertyChanged("Mode");
        //        }
        //    }
        //}

        public DialogViewMode Mode
        {
            get { return (DialogViewMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        #endregion

        #region Interface

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {

            ParentDialagWindow @this = (ParentDialagWindow)d;
            DialogViewMode mode = (DialogViewMode)args.NewValue;
            switch (mode)
            {
                case DialogViewMode.Edit:
                    @this.Title = "编辑";
                    @this.IsEnabled = true;
                    break;
                case DialogViewMode.View:
                    @this.Title = "查看";
                    @this.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 弹出对话框状态
        /// </summary>
    }

    public enum DialogViewMode
    {
        Add,
        Edit,
        View


    }

}
