// Copyright (C) Michael Agroskin 2010
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace UniGuy.Controls.Bindings
{
    /// <summary>
    /// Component to extend the WPF binding functionality 
    /// and to use bindings in ViewModels as well
    /// </summary>
    /// <remarks>
    /// Description: http://www.codeproject.com/KB/WPF/BindingHub.aspx
    /// </remarks>
    [ContentProperty("Extension")]
    public class BindingHub : FrameworkElement
    {
        #region Sockets

        public static DependencyProperty Socket1Property = DependencyProperty.Register(
            "Socket1",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket1CoerceValue,
                PropertyChangedCallback = OnSocket1Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket1
        {
            get { return GetValue(Socket1Property); }
            set { SetValue(Socket1Property, value); }
        }

        private static object OnSocket1CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket1CoerceValue, d, baseValue);
        }

        private static void OnSocket1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket1Changed, d, e);
            UpdateRelatedValues(1, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket1CoerceValue;
        public event PropertyChangedCallback Socket1Changed;

        public static DependencyProperty Socket2Property = DependencyProperty.Register(
            "Socket2",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket2CoerceValue,
                PropertyChangedCallback = OnSocket2Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket2
        {
            get { return GetValue(Socket2Property); }
            set { SetValue(Socket2Property, value); }
        }

        private static object OnSocket2CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket2CoerceValue, d, baseValue);
        }

        private static void OnSocket2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket2Changed, d, e);
            UpdateRelatedValues(2, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket2CoerceValue;
        public event PropertyChangedCallback Socket2Changed;

        public static DependencyProperty Socket3Property = DependencyProperty.Register(
            "Socket3",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket3CoerceValue,
                PropertyChangedCallback = OnSocket3Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket3
        {
            get { return GetValue(Socket3Property); }
            set { SetValue(Socket3Property, value); }
        }

        private static object OnSocket3CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket3CoerceValue, d, baseValue);
        }

        private static void OnSocket3Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket3Changed, d, e);
            UpdateRelatedValues(3, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket3CoerceValue;
        public event PropertyChangedCallback Socket3Changed;

        public static DependencyProperty Socket4Property = DependencyProperty.Register(
            "Socket4",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket4CoerceValue,
                PropertyChangedCallback = OnSocket4Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket4
        {
            get { return GetValue(Socket4Property); }
            set { SetValue(Socket4Property, value); }
        }

        private static object OnSocket4CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket4CoerceValue, d, baseValue);
        }

        private static void OnSocket4Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket4Changed, d, e);
            UpdateRelatedValues(4, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket4CoerceValue;
        public event PropertyChangedCallback Socket4Changed;

        public static DependencyProperty Socket5Property = DependencyProperty.Register(
            "Socket5",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket5CoerceValue,
                PropertyChangedCallback = OnSocket5Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket5
        {
            get { return GetValue(Socket5Property); }
            set { SetValue(Socket5Property, value); }
        }

        private static object OnSocket5CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket5CoerceValue, d, baseValue);
        }

        private static void OnSocket5Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket5Changed, d, e);
            UpdateRelatedValues(5, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket5CoerceValue;
        public event PropertyChangedCallback Socket5Changed;

        public static DependencyProperty Socket6Property = DependencyProperty.Register(
            "Socket6",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket6CoerceValue,
                PropertyChangedCallback = OnSocket6Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket6
        {
            get { return GetValue(Socket6Property); }
            set { SetValue(Socket6Property, value); }
        }

        private static object OnSocket6CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket6CoerceValue, d, baseValue);
        }

        private static void OnSocket6Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket6Changed, d, e);
            UpdateRelatedValues(6, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket6CoerceValue;
        public event PropertyChangedCallback Socket6Changed;

        public static DependencyProperty Socket7Property = DependencyProperty.Register(
            "Socket7",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket7CoerceValue,
                PropertyChangedCallback = OnSocket7Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket7
        {
            get { return GetValue(Socket7Property); }
            set { SetValue(Socket7Property, value); }
        }

        private static object OnSocket7CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket7CoerceValue, d, baseValue);
        }

        private static void OnSocket7Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket7Changed, d, e);
            UpdateRelatedValues(7, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket7CoerceValue;
        public event PropertyChangedCallback Socket7Changed;

        public static DependencyProperty Socket8Property = DependencyProperty.Register(
            "Socket8",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket8CoerceValue,
                PropertyChangedCallback = OnSocket8Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket8
        {
            get { return GetValue(Socket8Property); }
            set { SetValue(Socket8Property, value); }
        }

        private static object OnSocket8CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket8CoerceValue, d, baseValue);
        }

        private static void OnSocket8Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket8Changed, d, e);
            UpdateRelatedValues(8, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket8CoerceValue;
        public event PropertyChangedCallback Socket8Changed;

        public static DependencyProperty Socket9Property = DependencyProperty.Register(
            "Socket9",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket9CoerceValue,
                PropertyChangedCallback = OnSocket9Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket9
        {
            get { return GetValue(Socket9Property); }
            set { SetValue(Socket9Property, value); }
        }

        private static object OnSocket9CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket9CoerceValue, d, baseValue);
        }

        private static void OnSocket9Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket9Changed, d, e);
            UpdateRelatedValues(9, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket9CoerceValue;
        public event PropertyChangedCallback Socket9Changed;

        public static DependencyProperty Socket10Property = DependencyProperty.Register(
            "Socket10",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket10CoerceValue,
                PropertyChangedCallback = OnSocket10Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket10
        {
            get { return GetValue(Socket10Property); }
            set { SetValue(Socket10Property, value); }
        }

        private static object OnSocket10CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket10CoerceValue, d, baseValue);
        }

        private static void OnSocket10Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket10Changed, d, e);
            UpdateRelatedValues(10, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket10CoerceValue;
        public event PropertyChangedCallback Socket10Changed;

        public static DependencyProperty Socket11Property = DependencyProperty.Register(
            "Socket11",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket11CoerceValue,
                PropertyChangedCallback = OnSocket11Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket11
        {
            get { return GetValue(Socket11Property); }
            set { SetValue(Socket11Property, value); }
        }

        private static object OnSocket11CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket11CoerceValue, d, baseValue);
        }

        private static void OnSocket11Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket11Changed, d, e);
            UpdateRelatedValues(11, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket11CoerceValue;
        public event PropertyChangedCallback Socket11Changed;

        public static DependencyProperty Socket12Property = DependencyProperty.Register(
            "Socket12",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket12CoerceValue,
                PropertyChangedCallback = OnSocket12Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket12
        {
            get { return GetValue(Socket12Property); }
            set { SetValue(Socket12Property, value); }
        }

        private static object OnSocket12CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket12CoerceValue, d, baseValue);
        }

        private static void OnSocket12Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket12Changed, d, e);
            UpdateRelatedValues(12, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket12CoerceValue;
        public event PropertyChangedCallback Socket12Changed;

        public static DependencyProperty Socket13Property = DependencyProperty.Register(
            "Socket13",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket13CoerceValue,
                PropertyChangedCallback = OnSocket13Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket13
        {
            get { return GetValue(Socket13Property); }
            set { SetValue(Socket13Property, value); }
        }

        private static object OnSocket13CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket13CoerceValue, d, baseValue);
        }

        private static void OnSocket13Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket13Changed, d, e);
            UpdateRelatedValues(13, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket13CoerceValue;
        public event PropertyChangedCallback Socket13Changed;

        public static DependencyProperty Socket14Property = DependencyProperty.Register(
            "Socket14",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket14CoerceValue,
                PropertyChangedCallback = OnSocket14Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket14
        {
            get { return GetValue(Socket14Property); }
            set { SetValue(Socket14Property, value); }
        }

        private static object OnSocket14CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket14CoerceValue, d, baseValue);
        }

        private static void OnSocket14Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket14Changed, d, e);
            UpdateRelatedValues(14, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket14CoerceValue;
        public event PropertyChangedCallback Socket14Changed;

        public static DependencyProperty Socket15Property = DependencyProperty.Register(
            "Socket15",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket15CoerceValue,
                PropertyChangedCallback = OnSocket15Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket15
        {
            get { return GetValue(Socket15Property); }
            set { SetValue(Socket15Property, value); }
        }

        private static object OnSocket15CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket15CoerceValue, d, baseValue);
        }

        private static void OnSocket15Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket15Changed, d, e);
            UpdateRelatedValues(15, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket15CoerceValue;
        public event PropertyChangedCallback Socket15Changed;

        public static DependencyProperty Socket16Property = DependencyProperty.Register(
            "Socket16",
            typeof(object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(object),
                CoerceValueCallback = OnSocket16CoerceValue,
                PropertyChangedCallback = OnSocket16Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public object Socket16
        {
            get { return GetValue(Socket16Property); }
            set { SetValue(Socket16Property, value); }
        }

        private static object OnSocket16CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Socket16CoerceValue, d, baseValue);
        }

        private static void OnSocket16Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Socket16Changed, d, e);
            UpdateRelatedValues(16, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Socket16CoerceValue;
        public event PropertyChangedCallback Socket16Changed;

        /// <summary>
        /// Connections between Sockets
        /// Specifying values in XAML:
        /// Connect="(1 in, 2 out, 3 out, 4 out), (5, 6)"
        /// "inout" is optional, so "(1 inout, 2 inout)" == "(1, 2)"
        /// </summary>
        public static DependencyProperty ConnectProperty = DependencyProperty.Register(
            "Connect",
            typeof(List<List<BindingHubConnection>>),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                CoerceValueCallback = OnConnectCoerceValue,
                PropertyChangedCallback = OnConnectChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        [TypeConverter(typeof(BindingHubConnectionConverter))]
        public List<List<BindingHubConnection>> Connect
        {
            get { return (List<List<BindingHubConnection>>)GetValue(ConnectProperty); }
            set { SetValue(ConnectProperty, value); }
        }

        private static object OnConnectCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).ConnectCoerceValue, d, baseValue);
        }

        private static void OnConnectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).ConnectChanged, d, e);
        }

        public event CoerceValueHandler ConnectCoerceValue;
        public event PropertyChangedCallback ConnectChanged;

        /// <summary>
        /// Allows to chain BindingHubs together
        /// </summary>
        public static DependencyProperty ExtensionProperty = DependencyProperty.Register(
            "Extension",
            typeof(BindingHub),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                CoerceValueCallback = OnExtensionCoerceValue,
                PropertyChangedCallback = OnExtensionChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public BindingHub Extension
        {
            get { return (BindingHub)GetValue(ExtensionProperty); }
            set { SetValue(ExtensionProperty, value); }
        }

        private static object OnExtensionCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).ExtensionCoerceValue, d, baseValue);
        }

        private static void OnExtensionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bindingHub = (BindingHub)d;
            if (e.OldValue != null)
            {
                bindingHub.RemoveLogicalChild(e.OldValue);
            }
            if (e.NewValue != null)
            {
                bindingHub.AddLogicalChild(e.NewValue);
            }
            InvokePropertyChanged(bindingHub.ExtensionChanged, d, e);
        }

        public event CoerceValueHandler ExtensionCoerceValue;
        public event PropertyChangedCallback ExtensionChanged;

        public static DependencyProperty BindingHubProperty = DependencyProperty.RegisterAttached(
            "BindingHub",
            typeof(BindingHub),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(BindingHub),
                CoerceValueCallback = OnBindingHubCoerceValue,
                PropertyChangedCallback = OnBindingHubChanged,
                BindsTwoWayByDefault = false,
                Inherits = false,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public static BindingHub GetBindingHub(FrameworkElement element)
        {
            return (BindingHub)element.GetValue(BindingHubProperty);
        }

        public static void SetBindingHub(FrameworkElement element, BindingHub value)
        {
            element.SetValue(BindingHubProperty, value);
        }

        public static object OnBindingHubCoerceValue(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        public static void OnBindingHubChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                BindingOperations.ClearBinding((BindingHub)e.OldValue, DataContextProperty);
                BindingOperations.ClearBinding((BindingHub)e.OldValue, NameScope.NameScopeProperty);
            }
            if (e.NewValue != null)
            {
                var binding = new Binding("DataContext") { Source = d, Mode = BindingMode.OneWay };

                if (BindingOperations.GetBinding((BindingHub)e.NewValue, DataContextProperty) == null)
                    BindingOperations.SetBinding((BindingHub)e.NewValue, DataContextProperty, binding);

                binding = new Binding()
                {
                    Path = new PropertyPath(NameScope.NameScopeProperty),
                    Source = d,
                    Mode = BindingMode.OneWay
                };

                //PresentationTraceSources.SetTraceLevel(binding, PresentationTraceLevel.High);

                if (BindingOperations.GetBinding((BindingHub)e.NewValue, NameScope.NameScopeProperty) == null)
                    BindingOperations.SetBinding((BindingHub)e.NewValue, NameScope.NameScopeProperty, binding);
            }
        }

        #endregion

        #region Animations

        public static DependencyProperty AnimatedSizeProperty = DependencyProperty.Register(
    "AnimatedSize",
    typeof(Size),
    typeof(BindingHub),
    new FrameworkPropertyMetadata
    {
        DefaultValue = default(Size),
        CoerceValueCallback = OnAnimatedSizeCoerceValue,
        PropertyChangedCallback = OnAnimatedSizeChanged,
        BindsTwoWayByDefault = true,
        DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
    });

        public Size AnimatedSize
        {
            get { return (Size)GetValue(AnimatedSizeProperty); }
            set { SetValue(AnimatedSizeProperty, value); }
        }

        public static object OnAnimatedSizeCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).AnimatedSizeCoerceValue, d, baseValue);
        }

        public static void OnAnimatedSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).AnimatedSizeChanged, d, e);
            UpdateRelatedValues(17, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler AnimatedSizeCoerceValue;
        public event PropertyChangedCallback AnimatedSizeChanged;

        public static DependencyProperty BooleanProperty = DependencyProperty.Register(
                    "Boolean",
                    typeof(Boolean),
                    typeof(BindingHub),
                    new FrameworkPropertyMetadata
                    {
                        DefaultValue = default(Boolean),
                        CoerceValueCallback = OnBooleanCoerceValue,
                        PropertyChangedCallback = OnBooleanChanged,
                        BindsTwoWayByDefault = true,
                        DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });

        public Boolean Boolean
        {
            get { return (Boolean)GetValue(BooleanProperty); }
            set { SetValue(BooleanProperty, value); }
        }

        public static object OnBooleanCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).BooleanCoerceValue, d, baseValue);
        }

        public static void OnBooleanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).BooleanChanged, d, e);
            UpdateRelatedValues(18, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler BooleanCoerceValue;
        public event PropertyChangedCallback BooleanChanged;

        public static DependencyProperty ByteProperty = DependencyProperty.Register(
                    "Byte",
                    typeof(Byte),
                    typeof(BindingHub),
                    new FrameworkPropertyMetadata
                    {
                        DefaultValue = default(Byte),
                        CoerceValueCallback = OnByteCoerceValue,
                        PropertyChangedCallback = OnByteChanged,
                        BindsTwoWayByDefault = true,
                        DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });

        public Byte Byte
        {
            get { return (Byte)GetValue(ByteProperty); }
            set { SetValue(ByteProperty, value); }
        }

        public static object OnByteCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).ByteCoerceValue, d, baseValue);
        }

        public static void OnByteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).ByteChanged, d, e);
            UpdateRelatedValues(19, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler ByteCoerceValue;
        public event PropertyChangedCallback ByteChanged;

        public static DependencyProperty CharProperty = DependencyProperty.Register(
                    "Char",
                    typeof(Char),
                    typeof(BindingHub),
                    new FrameworkPropertyMetadata
                    {
                        DefaultValue = default(Char),
                        CoerceValueCallback = OnCharCoerceValue,
                        PropertyChangedCallback = OnCharChanged,
                        BindsTwoWayByDefault = true,
                        DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });

        public Char Char
        {
            get { return (Char)GetValue(CharProperty); }
            set { SetValue(CharProperty, value); }
        }

        public static object OnCharCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).CharCoerceValue, d, baseValue);
        }

        public static void OnCharChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).CharChanged, d, e);
            UpdateRelatedValues(20, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler CharCoerceValue;
        public event PropertyChangedCallback CharChanged;

        public static DependencyProperty Int16Property = DependencyProperty.Register(
            "Int16",
            typeof(Int16),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Int16),
                CoerceValueCallback = OnInt16CoerceValue,
                PropertyChangedCallback = OnInt16Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Int16 Int16
        {
            get { return (Int16)GetValue(Int16Property); }
            set { SetValue(Int16Property, value); }
        }

        public static object OnInt16CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Int16CoerceValue, d, baseValue);
        }

        public static void OnInt16Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Int16Changed, d, e);
            UpdateRelatedValues(24, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Int16CoerceValue;
        public event PropertyChangedCallback Int16Changed;

        public static DependencyProperty Int32Property = DependencyProperty.Register(
            "Int32",
            typeof(Int32),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Int32),
                CoerceValueCallback = OnInt32CoerceValue,
                PropertyChangedCallback = OnInt32Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Int32 Int32
        {
            get { return (Int32)GetValue(Int32Property); }
            set { SetValue(Int32Property, value); }
        }

        public static object OnInt32CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Int32CoerceValue, d, baseValue);
        }

        public static void OnInt32Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Int32Changed, d, e);
            UpdateRelatedValues(25, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Int32CoerceValue;
        public event PropertyChangedCallback Int32Changed;

        public static DependencyProperty Int64Property = DependencyProperty.Register(
            "Int64",
            typeof(Int64),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Int64),
                CoerceValueCallback = OnInt64CoerceValue,
                PropertyChangedCallback = OnInt64Changed,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Int64 Int64
        {
            get { return (Int64)GetValue(Int64Property); }
            set { SetValue(Int64Property, value); }
        }

        public static object OnInt64CoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Int64CoerceValue, d, baseValue);
        }

        public static void OnInt64Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Int64Changed, d, e);
            UpdateRelatedValues(26, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Int64CoerceValue;
        public event PropertyChangedCallback Int64Changed;

        public static DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Color),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Color),
                CoerceValueCallback = OnColorCoerceValue,
                PropertyChangedCallback = OnColorChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static object OnColorCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).ColorCoerceValue, d, baseValue);
        }

        public static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).ColorChanged, d, e);
            UpdateRelatedValues(21, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler ColorCoerceValue;
        public event PropertyChangedCallback ColorChanged;

        public static DependencyProperty SingleProperty = DependencyProperty.Register(
            "Single",
            typeof(Single),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Single),
                CoerceValueCallback = OnSingleCoerceValue,
                PropertyChangedCallback = OnSingleChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Single Single
        {
            get { return (Single)GetValue(SingleProperty); }
            set { SetValue(SingleProperty, value); }
        }

        public static object OnSingleCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).SingleCoerceValue, d, baseValue);
        }

        public static void OnSingleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).SingleChanged, d, e);
            UpdateRelatedValues(34, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler SingleCoerceValue;
        public event PropertyChangedCallback SingleChanged;

        public static DependencyProperty DoubleProperty = DependencyProperty.Register(
            "Double",
            typeof(Double),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Double),
                CoerceValueCallback = OnDoubleCoerceValue,
                PropertyChangedCallback = OnDoubleChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Double Double
        {
            get { return (Double)GetValue(DoubleProperty); }
            set { SetValue(DoubleProperty, value); }
        }

        public static object OnDoubleCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).DoubleCoerceValue, d, baseValue);
        }

        public static void OnDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).DoubleChanged, d, e);
            UpdateRelatedValues(23, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler DoubleCoerceValue;
        public event PropertyChangedCallback DoubleChanged;

        public static DependencyProperty DecimalProperty = DependencyProperty.Register(
            "Decimal",
            typeof(Decimal),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Decimal),
                CoerceValueCallback = OnDecimalCoerceValue,
                PropertyChangedCallback = OnDecimalChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Decimal Decimal
        {
            get { return (Decimal)GetValue(DecimalProperty); }
            set { SetValue(DecimalProperty, value); }
        }

        public static object OnDecimalCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).DecimalCoerceValue, d, baseValue);
        }

        public static void OnDecimalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).DecimalChanged, d, e);
            UpdateRelatedValues(22, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler DecimalCoerceValue;
        public event PropertyChangedCallback DecimalChanged;

        public static DependencyProperty MatrixProperty = DependencyProperty.Register(
            "Matrix",
            typeof(Matrix),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Matrix),
                CoerceValueCallback = OnMatrixCoerceValue,
                PropertyChangedCallback = OnMatrixChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Matrix Matrix
        {
            get { return (Matrix)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public static object OnMatrixCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).MatrixCoerceValue, d, baseValue);
        }

        public static void OnMatrixChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).MatrixChanged, d, e);
            UpdateRelatedValues(27, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler MatrixCoerceValue;
        public event PropertyChangedCallback MatrixChanged;

        public static DependencyProperty ObjectProperty = DependencyProperty.Register(
            "Object",
            typeof(Object),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Object),
                CoerceValueCallback = OnObjectCoerceValue,
                PropertyChangedCallback = OnObjectChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Object Object
        {
            get { return (Object)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        public static object OnObjectCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).ObjectCoerceValue, d, baseValue);
        }

        public static void OnObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).ObjectChanged, d, e);
            UpdateRelatedValues(28, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler ObjectCoerceValue;
        public event PropertyChangedCallback ObjectChanged;

        public static DependencyProperty Point3DProperty = DependencyProperty.Register(
            "Point3D",
            typeof(Point3D),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Point3D),
                CoerceValueCallback = OnPoint3DCoerceValue,
                PropertyChangedCallback = OnPoint3DChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Point3D Point3D
        {
            get { return (Point3D)GetValue(Point3DProperty); }
            set { SetValue(Point3DProperty, value); }
        }

        public static object OnPoint3DCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Point3DCoerceValue, d, baseValue);
        }

        public static void OnPoint3DChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Point3DChanged, d, e);
            UpdateRelatedValues(30, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Point3DCoerceValue;
        public event PropertyChangedCallback Point3DChanged;

        public static DependencyProperty PointProperty = DependencyProperty.Register(
            "Point",
            typeof(Point),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Point),
                CoerceValueCallback = OnPointCoerceValue,
                PropertyChangedCallback = OnPointChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Point Point
        {
            get { return (Point)GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }

        public static object OnPointCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).PointCoerceValue, d, baseValue);
        }

        public static void OnPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).PointChanged, d, e);
            UpdateRelatedValues(29, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler PointCoerceValue;
        public event PropertyChangedCallback PointChanged;

        public static DependencyProperty QuaternionProperty = DependencyProperty.Register(
            "Quaternion",
            typeof(Quaternion),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Quaternion),
                CoerceValueCallback = OnQuaternionCoerceValue,
                PropertyChangedCallback = OnQuaternionChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Quaternion Quaternion
        {
            get { return (Quaternion)GetValue(QuaternionProperty); }
            set { SetValue(QuaternionProperty, value); }
        }

        public static object OnQuaternionCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).QuaternionCoerceValue, d, baseValue);
        }

        public static void OnQuaternionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).QuaternionChanged, d, e);
            UpdateRelatedValues(31, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler QuaternionCoerceValue;
        public event PropertyChangedCallback QuaternionChanged;

        public static DependencyProperty RectProperty = DependencyProperty.Register(
            "Rect",
            typeof(Rect),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Rect),
                CoerceValueCallback = OnRectCoerceValue,
                PropertyChangedCallback = OnRectChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Rect Rect
        {
            get { return (Rect)GetValue(RectProperty); }
            set { SetValue(RectProperty, value); }
        }

        public static object OnRectCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).RectCoerceValue, d, baseValue);
        }

        public static void OnRectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).RectChanged, d, e);
            UpdateRelatedValues(32, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler RectCoerceValue;
        public event PropertyChangedCallback RectChanged;

        public static DependencyProperty Rotation3DProperty = DependencyProperty.Register(
            "Rotation3D",
            typeof(Rotation3D),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Rotation3D),
                CoerceValueCallback = OnRotation3DCoerceValue,
                PropertyChangedCallback = OnRotation3DChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Rotation3D Rotation3D
        {
            get { return (Rotation3D)GetValue(Rotation3DProperty); }
            set { SetValue(Rotation3DProperty, value); }
        }

        public static object OnRotation3DCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Rotation3DCoerceValue, d, baseValue);
        }

        public static void OnRotation3DChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Rotation3DChanged, d, e);
            UpdateRelatedValues(33, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Rotation3DCoerceValue;
        public event PropertyChangedCallback Rotation3DChanged;

        public static DependencyProperty StringProperty = DependencyProperty.Register(
            "String",
            typeof(String),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(String),
                CoerceValueCallback = OnStringCoerceValue,
                PropertyChangedCallback = OnStringChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public String String
        {
            get { return (String)GetValue(StringProperty); }
            set { SetValue(StringProperty, value); }
        }

        public static object OnStringCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).StringCoerceValue, d, baseValue);
        }

        public static void OnStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).StringChanged, d, e);
            UpdateRelatedValues(35, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler StringCoerceValue;
        public event PropertyChangedCallback StringChanged;

        public static DependencyProperty Vector3DProperty = DependencyProperty.Register(
            "Vector3D",
            typeof(Vector3D),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Vector3D),
                CoerceValueCallback = OnVector3DCoerceValue,
                PropertyChangedCallback = OnVector3DChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Vector3D Vector3D
        {
            get { return (Vector3D)GetValue(Vector3DProperty); }
            set { SetValue(Vector3DProperty, value); }
        }

        public static object OnVector3DCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).Vector3DCoerceValue, d, baseValue);
        }

        public static void OnVector3DChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).Vector3DChanged, d, e);
            UpdateRelatedValues(38, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler Vector3DCoerceValue;
        public event PropertyChangedCallback Vector3DChanged;

        public static DependencyProperty VectorProperty = DependencyProperty.Register(
            "Vector",
            typeof(Vector),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Vector),
                CoerceValueCallback = OnVectorCoerceValue,
                PropertyChangedCallback = OnVectorChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Vector Vector
        {
            get { return (Vector)GetValue(VectorProperty); }
            set { SetValue(VectorProperty, value); }
        }

        public static object OnVectorCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).VectorCoerceValue, d, baseValue);
        }

        public static void OnVectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).VectorChanged, d, e);
            UpdateRelatedValues(37, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler VectorCoerceValue;
        public event PropertyChangedCallback VectorChanged;

        public static DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness",
            typeof(Thickness),
            typeof(BindingHub),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(Thickness),
                CoerceValueCallback = OnThicknessCoerceValue,
                PropertyChangedCallback = OnThicknessChanged,
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public Thickness Thickness
        {
            get { return (Thickness)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static object OnThicknessCoerceValue(DependencyObject d, object baseValue)
        {
            return InvokeCoerceValue(((BindingHub)d).ThicknessCoerceValue, d, baseValue);
        }

        public static void OnThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InvokePropertyChanged(((BindingHub)d).ThicknessChanged, d, e);
            UpdateRelatedValues(36, (BindingHub)d, e.NewValue);
        }

        public event CoerceValueHandler ThicknessCoerceValue;
        public event PropertyChangedCallback ThicknessChanged;

        #endregion

        internal static readonly List<DependencyProperty> Sockets = new List<DependencyProperty>
        {
            Socket1Property, // 1
            Socket2Property, // 2
            Socket3Property, // 3
            Socket4Property, // 4
            Socket5Property, // 5
            Socket6Property, // 6
            Socket7Property, // 7
            Socket8Property, // 8
            Socket9Property, // 9
            Socket10Property, // 10
            Socket11Property, // 11
            Socket12Property, // 12
            Socket13Property, // 13
            Socket14Property, // 14
            Socket15Property, // 15
            Socket16Property, // 16
            AnimatedSizeProperty, // 17
            BooleanProperty, // 18
            ByteProperty, // 19
            CharProperty, // 20
            ColorProperty, // 21
            DecimalProperty, // 22
            DoubleProperty, // 23
            Int16Property, // 24
            Int32Property, // 25
            Int64Property, // 26
            MatrixProperty, // 27
            ObjectProperty, // 28
            PointProperty, // 29
            Point3DProperty, // 30
            QuaternionProperty, // 31
            RectProperty, // 32
            Rotation3DProperty, //33
            SingleProperty, // 34
            StringProperty, // 35
            ThicknessProperty, // 36
            VectorProperty, // 37
            Vector3DProperty, // 38
        };

        #region Utils

        private static void UpdateRelatedValues(int index, BindingHub hub, object value)
        {
            var connect = hub.Connect;

            if (index <= 0 || index > Sockets.Count || connect == null)
                return;

            foreach (var group in
                   (from g in connect
                    where g.Find(c => c.Socket == index &&
                        c.Direction != BindingHubConnectionDirection.Out) != null
                    select g))
            {
                foreach (var connection in group)
                {
                    if (connection.Socket == index ||
                        connection.Socket < 1 || connection.Socket > Sockets.Count ||
                        connection.Direction == BindingHubConnectionDirection.In)
                        continue;

                    hub.SetValue(Sockets[connection.Socket - 1], value);
                }
            }
        }

        private static object InvokeCoerceValue(CoerceValueHandler action, DependencyObject d, object baseValue)
        {
            if (action != null)
            {
                var ev = new CoerceValueEventArgs { BaseValue = baseValue, CoercedValue = baseValue };
                action(d, ev);
                return ev.BaseValue;
            }
            return baseValue;
        }

        private static void InvokePropertyChanged(PropertyChangedCallback action, DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (action != null)
                action(d, e);
        }

        #endregion
    }
}
