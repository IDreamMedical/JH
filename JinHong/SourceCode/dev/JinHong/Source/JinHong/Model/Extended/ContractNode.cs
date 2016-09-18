using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace JinHong.Model
{
    public class ContractNode : ObservableCollection<ContractNode>
    {
        #region Fields

        static readonly ContractNode dummyNode = new ContractNode();

        private bool isPopulated;
        private bool isFile;
        private string directory;
        private string file;

        #endregion

         #region Properties

        public bool IsPopulated
        {
            get { return isPopulated; }
            private set
            {
                if (isPopulated != value)
                {
                    isPopulated = value;
                    OnPropertyChanged("IsPopulated");
                }
            }
        }

        public bool IsFile
        {
            get { return isFile; }
            private set
            {
                if (isFile != value)
                {
                    isFile = value;
                    OnPropertyChanged("IsFile");
                }
            }
        }

        public string Directory
        {
            get { return directory; }
            private set
            {
                if (directory != value)
                {
                    directory = value;
                    OnPropertyChanged("Directory");
                }
            }
        }

        public string File
        {
            get { return file; }
            private set
            {
                if (file != value)
                {
                    file = value;
                    OnPropertyChanged("File");
                }
            }
        }

        public ContractNode Parent { get; set; }
        #endregion

        #region Constructors

        public static ContractNode CreateDirectoryNode(string directory)
        {
            ContractNode node = new ContractNode { Directory = GlobalVariables.Smc.GetDirectory(directory) };
            node.Add(dummyNode);
            return node;
        }

        public static ContractNode CreateFileNode(string file)
        {
            return new ContractNode { File = GlobalVariables.Smc.GetFile(file), IsFile = true };
        }

        protected internal ContractNode() { }
        public ContractNode(string directory)
        {
            if (System.IO.Directory.Exists(directory))
            {
                Directory = directory;
                Add(dummyNode);
            }
            else
                throw new DirectoryNotFoundException();
        }

        #endregion

        #region Methods

        public void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public void Populate()
        {
            if (IsFile || IsPopulated)
                throw new Exception("It is a contract file, or is already populated.");

            Clear();
            foreach (string dir in GlobalVariables.Smc.GetDirectories(directory))
                Add(CreateDirectoryNode(dir));
            foreach (string file in GlobalVariables.Smc.GetFiles(directory))
                Add(CreateFileNode(file));

            IsPopulated = true;
        }

        public void Repopulate()
        {
            if (IsFile)
                throw new Exception("It is a contract file.");

            Clear();
            foreach (string dir in GlobalVariables.Smc.GetDirectories(directory))
                Add(CreateDirectoryNode(dir));
            foreach (string file in GlobalVariables.Smc.GetFiles("*.*"))
                Add(CreateFileNode(file));

            if (!IsPopulated)
                IsPopulated = true;
        }

        protected override void InsertItem(int index, ContractNode item)
        {
            base.InsertItem(index, item);
            item.Parent = this;
        }

        protected override void SetItem(int index, ContractNode item)
        {
            base.SetItem(index, item);
            item.Parent = this;
        }

        #endregion
    }

    public class ContractTree : ObservableCollection<ContractNode>
    {
        public void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
