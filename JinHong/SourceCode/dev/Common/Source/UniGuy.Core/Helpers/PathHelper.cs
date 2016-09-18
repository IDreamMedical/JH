using System;
using System.Collections.Generic;
using System.Text;
using UniGuy.Core.DataStructures;
using System.IO;
using System.Linq;

namespace UniGuy.Core.Helpers
{
    /// <summary>
    /// 路径辅助类
    /// </summary>
    public class PathHelper
    {
        /// <summary>
        /// 获得父路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="separator">路径分隔符</param>
        /// <returns>父路径</returns>
        public static string GetParent(string path, string sep)
        {
            int i = path.LastIndexOf(sep);
            return i >= 0 ? path.Substring(0, i) : null;
        }

        public static string[] Split(string path, string sep)
        {
            return path.Split(new string[]{sep}, StringSplitOptions.None);
        }

        public static TreeNode AddToTreeNode(TreeNode tn, string path, string sep)
        {
            TreeNode t = tn;
            foreach (string frag in Split(path, sep))
                t = t.GetChild(frag) ?? t.AddChildValue(frag);
            return t;
        }

        public static TreeNode FindTreeNode(TreeNode tn, string path, string sep)
        {
            TreeNode t = tn;
            foreach (string frag in Split(path, sep))
                if ((t = t.GetChild(frag)) == null)
                    return null;
            return t;
        }

        public static TreeNode ConstructDirectory(string[] paths, string sep)
        {
            TreeNode tn = new TreeNode();
            foreach (string path in paths)
                AddToTreeNode(tn, path, sep);
            return tn;
        }

        public static string[] GetSubDirectories(TreeNode tn, string path, string sep)
        {
            TreeNode ctn = FindTreeNode(tn, path, sep);
            if (ctn != null)
            {
                string[] dirs = new string[ctn.Children.Count];
                for (int i = 0; i < ctn.Children.Count; i++)
                    dirs[i] = ctn.Children[i].Value.ToString();
            }
            return null;
        }

        public static string[] GetSubDirectories(string[] paths, string path, string sep)
        {
            return GetSubDirectories(ConstructDirectory(paths, sep), path, sep);
        }

        /// <summary>
        /// 获得是否是合法文件名或者文件夹名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsValidFileName(string fileName)
        {
            bool isValid = true;
            string errChar = "\\/:*?\"<>|";  //
            if (string.IsNullOrEmpty(fileName))
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < errChar.Length; i++)
                {
                    if (fileName.Contains(errChar[i]))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }
    }
}
