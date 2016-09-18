using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;

namespace UniGuy.Windows.Themes
{

    /// <summary>
    /// A class to centrally manage the application of styles and themes
    /// </summary>
    public class SkinProvider : IThemeManager
    {

        public List<string> m_themes;

        private static volatile SkinProvider instance = null;
        private Application m_app = null;
        private Dictionary<Uri, ResourceDictionary> m_mergedUris = new Dictionary<Uri, ResourceDictionary>();
        private string m_appTheme = string.Empty;
        private ShutdownMode m_appShutdownMode = ShutdownMode.OnMainWindowClose;
        bool m_largeFonts = false;

        event ThemeChangedEventHandler m_themeChanged;
        event ThemeChangingEventHandler m_themeChanging;


        static public SkinProvider GetInstance()
        {
            if (instance == null)
                instance = new SkinProvider();
            return instance;
        }

        ~SkinProvider() { instance = null; }

        private SkinProvider()
        {
            m_themes = new List<string>();
            m_themes.Add("BlueLight");
            m_themes.Add("BlueDark");
        }

        public string DefaultTheme
        {
            get { return "BlueDark"; }
        }

        private Application GetApp()
        {
            if (m_app != null)
                return m_app;

            if (Application.Current == null)
                new Application{ShutdownMode = m_appShutdownMode};

            m_app = Application.Current;
            return m_app;

        }

        public Application Application
        {
            get { return GetApp(); }
            set { m_app = value; }
        }

        public ShutdownMode ApplicationShutdownMode
        {
            get { return m_appShutdownMode; }
            set { m_appShutdownMode = value; }
        }

        public event ThemeChangedEventHandler ThemeChanged
        {
            add { m_themeChanged+= value; }
            remove { m_themeChanged-=value; }
        }

        public event ThemeChangingEventHandler ThemeChanging
        {
            add { m_themeChanging+=value; }
            remove { m_themeChanging-=value; }
        }


        private Uri ThemeToUri(string t)
        {
            switch (t)
            {
                case "BlueLight":
                    return new Uri("UniGuy.PresentationFramework;component/Themes/Styles/BlueLight.xaml", UriKind.Relative);
                case "BlueDark":
                    return new Uri("UniGuy.PresentationFramework;component/Themes/Styles/BlueDark.xaml", UriKind.Relative);
                default:
                    return new Uri(string.Format("UniGuy.PresentationFramework;component/Themes/Styles/{0}.xaml", t), UriKind.Relative);
                    //return null;
            }
        }

        public bool UseLightIcons
        {
            get { return m_appTheme == "GreenLight" || m_appTheme == "OrangeLight" || m_appTheme.EndsWith("Light") ? false : true; }
        }

       
        public string ThemeFriendlyName(string theme)
        {
            return theme;
        }
        
        public IList<string> AllThemes 
        {
            get { return m_themes; }
        }

        public string AppTheme
        {
            get { return m_appTheme; }
            set
            {
                SetAppTheme(value, m_largeFonts);
            }
        }


        public void SetAppTheme(string appTheme, bool useLargeFonts)
        {
            if (appTheme == m_appTheme && useLargeFonts == m_largeFonts)
                return;

            ThemeChangingEventArgs changingArgs = new ThemeChangingEventArgs(appTheme, useLargeFonts);
            if (m_themeChanging != null)
                m_themeChanging(this, changingArgs);
            if (changingArgs.Cancel)
                return;

            ReplaceDictionary(ThemeToUri(m_appTheme), ThemeToUri(appTheme));

            m_appTheme = appTheme;
            m_largeFonts = useLargeFonts;

            if (m_themeChanged != null)
                m_themeChanged(this, new ThemeChangeEventArgs(m_appTheme, useLargeFonts));
        }

        public void MergeDictionary(Uri uri)
        {
            lock (this)
            {
                ResourceDictionary rd = Application.LoadComponent(uri) as ResourceDictionary;
                MergeDictionary(rd);
                m_mergedUris.Add(uri, rd);
            }
        }

        public void MergeDictionary(ResourceDictionary rd)
        {
            lock (this)
            {
                GetApp().Resources.MergedDictionaries.Add(rd);
            }
        }


        public void ReplaceDictionary(Uri old, Uri newd)
        {
            lock (this)
            {
                if (old != null && m_mergedUris.ContainsKey(old))
                {
                    ResourceDictionary newrd = Application.LoadComponent(newd) as ResourceDictionary;
                    ResourceDictionary oldrd = m_mergedUris[old];
                    ReplaceDictionary(oldrd, newrd);
                    m_mergedUris.Remove(old);
                    m_mergedUris.Add(newd, newrd);
                }
                else
                {
                    MergeDictionary(newd);
                }
            }
        }


        public void ReplaceDictionary(ResourceDictionary old, ResourceDictionary newd)
        {
            lock (this)
            {
                if (GetApp().Resources.MergedDictionaries.Contains(old))
                {
                    GetApp().Resources.MergedDictionaries.Remove(old);
                    GetApp().Resources.MergedDictionaries.Add(newd);
                }
            }
        }

        public void UnmergeDictionary(Uri uri)
        {
            lock (this)
            {
                if (m_mergedUris.ContainsKey(uri))
                {
                    ResourceDictionary rd = m_mergedUris[uri];
                    UnmergeDictionary(rd);
                    m_mergedUris.Remove(uri);
                }
            }
        }

        public void UnmergeDictionary(ResourceDictionary rd)
        {
            lock (this)
            {
                GetApp().Resources.MergedDictionaries.Remove(rd);
            }
        }
    }
}