using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Windows.Themes
{
    public class ThemeChangeEventArgs : EventArgs
    {
        public ThemeChangeEventArgs(string t, bool largeFonts)
        {
            this.Theme = t;
            this.LargeFonts = largeFonts;
        }

        public string Theme;
        public bool LargeFonts;
    }

    public class ThemeChangingEventArgs : EventArgs
    {
        public ThemeChangingEventArgs(string t, bool largeFonts)
        {
            this.Theme = t;
            this.LargeFonts = largeFonts;
            this.Cancel = false;
        }

        public string Theme;
        public bool LargeFonts;

        public bool Cancel;
    }

    public delegate void ThemeChangedEventHandler(object sender, ThemeChangeEventArgs args);
    public delegate void ThemeChangingEventHandler(object sender, ThemeChangingEventArgs args);

    public interface IThemeManager
    {
        string DefaultTheme { get; }

        IList<string> AllThemes { get; }

        string AppTheme { get; set; }

        event ThemeChangedEventHandler ThemeChanged;
        event ThemeChangingEventHandler ThemeChanging;

        bool UseLightIcons { get; }

        string ThemeFriendlyName(string theme);
    }
}
