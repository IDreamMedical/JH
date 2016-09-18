using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using JinHong.ViewModel;
using JinHong.Model;
using Microsoft.Win32;
using JinHong.View.Dialogs;
using System.IO.Compression;
using JinHong.Helper;
using pt = System.IO.Path;
using JinHong.Extensions;
using System.Data;
using System.Windows.Threading;
using UniGuy.Core.Extensions;



namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 合同管理.xaml
    /// TODO... 合同以各种文件格式保存在服务端
    /// </summary>
    public partial class WpfContract : UserControl
    {
        #region Properties
        public ContractVM ViewModel { get; set; }

        #endregion

        #region Constructors

        public WpfContract()
        {
            InitializeComponent();
            ViewModel = new ContractVM(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion

        #region Methods

       

        #region Command handlers

        
        private void AddDirectory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  在Directory下新增Directory, 弹对话框填名称,描述等后确认
            ContractNode node = e.Parameter as ContractNode;
            Debug.Assert(node != null && !node.IsFile);

            AddOrModifyDirectoryDialog dialog = new AddOrModifyDirectoryDialog { Owner = Window.GetWindow(this), Mode = AddOrModifyDirectoryDialog.EditingMode.Add };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                Directory.CreateDirectory(System.IO.Path.Combine(node.Directory, dialog.DirectoryName));
                if (node.IsPopulated)
                    node.Repopulate();
            }
        }




       

       

       
        private void RemoveDirectory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  删除Directory以及其包含的子Directory和Contract, 需弹对话框确认, 因为是实际删除文件
            ContractNode node = e.Parameter as ContractNode;
            Debug.Assert(node != null && !node.IsFile);

            if (MessageBox.Show(Window.GetWindow(this), "是否确定删除?",
                "删除", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel)
                == MessageBoxResult.OK)
            {
                //  TODO 不支持
                GlobalVariables.Smc.DeleteDirectory(node.Directory);
                if (node.Parent != null && node.Parent.IsPopulated)
                    node.Parent.Repopulate();
            }
        }

       

        private void RemoveContract_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO... 删除Contract, 需弹对话框确认, 因为是实际删除文件
            ContractNode node = e.Parameter as ContractNode;
            Debug.Assert(node != null && node.IsFile);

            if (MessageBox.Show(Window.GetWindow(this), "是否确定删除?",
                "删除", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel)
                == MessageBoxResult.OK)
            {
                //  TODO 不支持
                GlobalVariables.Smc.DeleteFile(node.File);
                if (node.Parent != null && node.Parent.IsPopulated)
                    node.Parent.Repopulate();
            }
        }

       

        private void RenameDirectory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  弹对话框重命名Directory名称
            ContractNode node = e.Parameter as ContractNode;
            Debug.Assert(node != null && !node.IsFile);

            RenameDialog dialog = new RenameDialog { Owner = Window.GetWindow(this), TargetName = node.Directory };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                string newName = dialog.TargetName;
                //  TODO 不支持
                Debug.Assert(node.Parent != null);
                GlobalVariables.Smc.RenameDirectory(node.Directory, System.IO.Path.Combine(node.Parent.Directory, newName));
                if (node.Parent != null && node.Parent.IsPopulated)
                    node.Parent.Repopulate();
            }
        }

        

        private void RenameContract_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  弹对话框重命名Test名称
            ContractNode node = e.Parameter as ContractNode;
            Debug.Assert(node != null && node.IsFile);

            RenameDialog dialog = new RenameDialog { Owner = Window.GetWindow(this), TargetName = node.File };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                string newName = dialog.TargetName;
                //  TODO 不支持
                Debug.Assert(node.Parent != null);
                GlobalVariables.Smc.RenameFile(node.File, System.IO.Path.Combine(node.Parent.Directory, newName));
                if (node.Parent != null && node.Parent.IsPopulated)
                    node.Parent.Repopulate();
            }
        }

        #endregion

        #region Event handlers

        private void treeViewContracts_Expanded(object sender, RoutedEventArgs e)
        {
            ContractNode node = ((TreeViewItem)e.OriginalSource).DataContext as ContractNode;
            if (node != null && !node.IsFile && !node.IsPopulated)
                node.Populate();
        }

        private void treeViewContracts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ContractNode node = ((FrameworkElement)e.OriginalSource).DataContext as ContractNode;
            if (node != null && node.IsFile)
            {
                string fileName = node.File;
                string localFileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), new FileInfo(fileName).Name);
                if (!File.Exists(localFileName))
                    GlobalVariables.Smc.DownloadFile(fileName, localFileName);
                FileInfo fiLocalFile = new FileInfo(localFileName);
                //  文本文件
                if (string.Equals(fiLocalFile.Extension, ".txt", StringComparison.InvariantCultureIgnoreCase))
                {
                    //contentControl.Content = File.ReadAllText(localFileName);
                }
                //  图片集
                else if (string.Equals(fiLocalFile.Extension, ".rar", StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(fiLocalFile.Extension, ".zip", StringComparison.InvariantCultureIgnoreCase))
                {
                    string tempDirectory = pt.Combine(pt.GetTempPath(), /*pt.GetRandomFileName()*/fiLocalFile.Name.Replace(".", "_"));
                    if (!Directory.Exists(tempDirectory))
                    {
                        Directory.CreateDirectory(tempDirectory);
                        //ZipHelper.DecompressToDirectory(localFileName, tempDirectory);
                        //ZipHelper2.DecompressFile(localFileName, tempDirectory);
                        ZipHelper3.DecompressFile(localFileName, tempDirectory);
                    }
                    // contentControl.Content = Directory.GetFiles(tempDirectory);
                }
                //  其他, Word等
                else
                {
                    Process.Start(localFileName);
                }
            }
        }

        public void ViewContract(string contractFile)
        {
            if (string.IsNullOrEmpty(contractFile))
                return;

            string fileName = contractFile;
            string localFileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), new FileInfo(fileName).Name);
            if (!File.Exists(localFileName))
                GlobalVariables.Smc.DownloadFile(fileName, localFileName);
            FileInfo fiLocalFile = new FileInfo(localFileName);
            //  文本文件
            if (string.Equals(fiLocalFile.Extension, ".txt", StringComparison.InvariantCultureIgnoreCase))
            {
                // contentControl.Content = File.ReadAllText(localFileName);
            }
            //  图片集
            else if (string.Equals(fiLocalFile.Extension, ".rar", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(fiLocalFile.Extension, ".zip", StringComparison.InvariantCultureIgnoreCase))
            {
                string tempDirectory = pt.Combine(pt.GetTempPath(), /*pt.GetRandomFileName()*/fiLocalFile.Name.Replace(".", "_"));
                if (!Directory.Exists(tempDirectory))
                {
                    Directory.CreateDirectory(tempDirectory);
                    ZipHelper3.DecompressFile(localFileName, tempDirectory);
                }
                //  contentControl.Content = Directory.GetFiles(tempDirectory);
            }
            //  其他, Word等
            else
            {
                Process.Start(localFileName);
            }
        }

       

        #endregion


        //  TODO

        #endregion

        



        private void listViewContractTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                //ViewModel.SelectedContractInfo = row.BuildEntity<ContractInfo>();
            }

        }

        private void ContractTbl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  根据权限, 浏览汇总的详细信息
            //  查看汇总详情
            //if (ViewModel.SelectedContractInfo != null)
            //{
            //    ViewContractDetailDialog dialog = new ViewContractDetailDialog
            //    {
            //        Owner = Window.GetWindow(this),
            //        ContractDetailTbl = ViewModel.GetContractDetailTbl(),
            //        SocialUnitName = ViewModel.SelectedContractInfo.SocialUnitName

            //    };
            //    dialog.ShowDialog();
            //}
        }



        private void Remove_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            //DataRowView row = listViewContractTbl.SelectedValue as DataRowView;

            //if (ViewModel.SelectedContractInfo != null)
            //{
            //    if (ViewModel.SelectedContractInfo.EffectiveDate.Value.CompareTo(DateTime.Today) < 0)
            //    {
            //        MessageBox.Show("已生效，不能删除！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
            //        return;
            //    }
            //    if (ViewModel.SelectedContractInfo.Status == 1)
            //    {
            //        MessageBox.Show("已经删除，请勿重复操作！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
            //        return;
            //    }
            //    if (ViewModel.SelectedContractInfo != null && ViewModel.SelectedContractInfo.EffectiveDate.Value.CompareTo(DateTime.Now.Date) < 0)
            //    {
            //        MessageBox.Show(Window.GetWindow(this), "合同已生效不能修改！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            //        return;
            //    }
            //    if (MessageBox.Show("确定删除？ ", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            //    {
            //        //0,启用,1。 删除
            //        ViewModel.SelectedContractInfo.Status = 1;
            //        GlobalVariables.Smc.Update<ContractInfo>(ViewModel.SelectedContractInfo);
            //        if (row != null)
            //        {
            //            ViewModel.ContractInfoTbl.Rows.Remove(row.Row);
            //            listViewContractTbl.Items.Refresh();
            //        }
            //        Query();
            //    }
            //}

        }



     
       

    }
}
