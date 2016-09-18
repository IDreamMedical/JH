using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace JinHong.Helper
{
    public class MsgHelper
    {

        /// <summary>
        /// 默认返回false
        /// </summary>
        /// <returns></returns>
        public static bool ConfirmDel()
        {
            return MessageBox.Show("确定要删除吗？", "系统提示", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Warning) != MessageBoxResult.OK;
        }


    }
}
