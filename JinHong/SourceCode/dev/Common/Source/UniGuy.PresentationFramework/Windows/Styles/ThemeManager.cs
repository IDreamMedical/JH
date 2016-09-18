﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UniGuy.Controls.Styles
{
    public static class ThemeManager
    {
        public static ResourceDictionary GetThemeResourceDictionary(string theme)
        {
            if (theme != null)
            {
                Assembly assembly = typeof(ThemeManager).Assembly;
                string packUri = String.Format(@"/UniGuy.Controls;component/Styles/Standard/{0}/Theme.xaml", theme);
                return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
            }
            return null;
        }

        public static string[] GetThemes()
        {
            string[] themes = new string[] 
            { 
                //  Wj Added
                "DummyDemo",
                "ExpressionDark", "ExpressionLight", 
                //"RainierOrange", "RainierPurple", "RainierRadialBlue", 
                "ShinyBlue", "ShinyRed", 
                //"ShinyDarkTeal", "ShinyDarkGreen", "ShinyDarkPurple",
                "DavesGlossyControls", 
                "WhistlerBlue", 
                "BureauBlack", "BureauBlue", 
                "BubbleCreme", 
                "TwilightBlue",
                "UXMusingsRed", "UXMusingsGreen", 
                //"UXMusingsRoughRed", "UXMusingsRoughGreen", 
                "UXMusingsBubblyBlue"
            };
            return themes;
        }

        public static void ApplyTheme(this Application app, string theme)
        {
            ResourceDictionary dictionary = ThemeManager.GetThemeResourceDictionary(theme);

            if (dictionary != null)
            {
                //  直接清空可能会有问题吧，不同Dictionary中相同资源名如何处理，只有同一ResourceDictionary中的资源名是互斥的
                //  这里保持原样，但需要注意。
                app.Resources.MergedDictionaries.Clear();
                app.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        public static void ApplyTheme(this ContentControl control, string theme)
        {
            ResourceDictionary dictionary = ThemeManager.GetThemeResourceDictionary(theme);

            if (dictionary != null)
            {
                control.Resources.MergedDictionaries.Clear();
                control.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        #region Theme

        /// <summary>
        /// Theme Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(string), typeof(ThemeManager),
                new FrameworkPropertyMetadata((string)string.Empty,
                    new PropertyChangedCallback(OnThemeChanged)));

        /// <summary>
        /// Gets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static string GetTheme(DependencyObject d)
        {
            return (string)d.GetValue(ThemeProperty);
        }

        /// <summary>
        /// Sets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetTheme(DependencyObject d, string value)
        {
            d.SetValue(ThemeProperty, value);
        }

        /// <summary>
        /// Handles changes to the Theme property.
        /// </summary>
        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string theme = e.NewValue as string;
            if (theme == string.Empty)
                return;

            ContentControl control = d as ContentControl;
            if (control != null)
            {
                control.ApplyTheme(theme);
            }
        }

        #endregion
    }
}