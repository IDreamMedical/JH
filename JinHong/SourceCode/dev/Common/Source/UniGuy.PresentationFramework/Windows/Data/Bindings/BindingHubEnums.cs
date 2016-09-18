// Copyright (C) Michael Agroskin 2010

using System.Windows;

namespace UniGuy.Controls.Bindings
{
    public enum BindingHubConnectionDirection { InOut = 0, In = 1, Out = 2 }

    public class CoerceValueEventArgs
    {
        public object BaseValue { get; set; }
        public object CoercedValue { get; set; }
    }

    public delegate void CoerceValueHandler(DependencyObject d, CoerceValueEventArgs ev);
}
