using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Globalization;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;


namespace JinHong
{
    /// <summary>
    /// 应用程序设置, 主要是Serializable Options...
    /// </summary>
    [Serializable]
    public class ApplicationSettings : INotifyPropertyChanged, IXmlSerializable
    {
        #region Interface implementations

        #region XmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            //  根节点名称
            string xmlRoot = UniGuy.Core.Helpers.SerializationHelper.GetXmlRoot(this.GetType());

            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == xmlRoot)
            {
                //  TODO
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            //  TODO
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
                this.OnPropertyChanged(property);
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            if (this.GetType().GetProperty(propertyName) == null)
            {
                throw new Exception(string.Format("The property name:{0} does not exist!!!!", propertyName));
            }
        }

        #endregion

        #endregion  //  Interface implementations

        #region Events

        #endregion

        #region Fields

        const string APPLICATION_SETTINGS_FILE = @"ApplicationSettings.xml";

        public static readonly ApplicationSettings Instance = new ApplicationSettings();

        //  TODO
        #endregion

        #region Properties

        public static FileInfo ApplicationSettingsFileInfo
        {
            get
            {
                FileInfo fi = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATION_SETTINGS_FILE));
                if (!fi.Directory.Exists)
                    fi.Directory.Create();
                return fi;
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public ApplicationSettings() { }

        #endregion

        #region Methods
        #endregion
        //  TODO
    }
}
