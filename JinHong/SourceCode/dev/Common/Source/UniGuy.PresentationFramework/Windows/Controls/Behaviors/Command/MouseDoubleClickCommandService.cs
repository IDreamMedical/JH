﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Input;

namespace UniGuy.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /* Example. Set it on the TreeView
        <TreeView pc:MouseDoubleClickCommandService.Command="{Binding YourCommand}"
                  pc:MouseDoubleClickCommandService.CommandParameter="{Binding}"
                  .../>
        Example. Set it on TreeViewItem

        <TreeView ItemsSource="{Binding Projects}">
            <TreeView.ItemContainerStyle>
                <Style TargetType="{x:Type TreeViewItem}">
                    <Setter Property="pc:MouseDoubleClickCommandService.Command"
                            Value="{Binding YourCommand}"/>
                    <Setter Property="pc:MouseDoubleClickCommandService.CommandParameter"
                            Value="{Binding}"/>
                </Style>
            </TreeView.ItemContainerStyle>
        </TreeView>
     */
    /// </example>
    public class MouseDoubleClickCommandService
    {
        public static DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command",
            typeof(ICommand),
            typeof(MouseDoubleClickCommandService),
            new UIPropertyMetadata(CommandChanged));

        public static DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter",
                                                typeof(object),
                                                typeof(MouseDoubleClickCommandService),
                                                new UIPropertyMetadata(null));

        public static void SetCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(CommandProperty, value);
        }

        public static void SetCommandParameter(DependencyObject target, object value)
        {
            target.SetValue(CommandParameterProperty, value);
        }
        public static object GetCommandParameter(DependencyObject target)
        {
            return target.GetValue(CommandParameterProperty);
        }

        private static void CommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Control control = target as Control;
            if (control != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    control.MouseDoubleClick += OnMouseDoubleClick;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    control.MouseDoubleClick -= OnMouseDoubleClick;
                }
            }
        }

        private static void OnMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Control control = sender as Control;
            ICommand command = (ICommand)control.GetValue(CommandProperty);
            object commandParameter = control.GetValue(CommandParameterProperty);
            command.Execute(commandParameter);
        }
    }
}
