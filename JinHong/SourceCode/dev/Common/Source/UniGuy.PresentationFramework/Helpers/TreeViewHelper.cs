using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;
using System.IO;
using System.Windows.Media;

namespace UniGuy.Controls.Helpers
{
    public class TreeViewHelper
    {
        #region WJ collected
        /// <summary>
        /// 什么都不是
        /// </summary>
        /// <remarks>
        /// 有的时候null也是一个值, 或者代表了什么
        /// </remarks>
        //public static object Nothing = new object();
        /*
         * TreeView的ItemContainerGenerator是hierarchy aware. 意思是TreeView.ItemContainerGenerator只知道直接下属TreeViewItem. 
         * 所以一个TreeView住院有n个TreeViewItem, 就会有n+1个ItemContainerGenerator. 而这些ItemContainerGenerators也同样只知道它下层的TreeViewItems.
         */

        /// <summary>
        /// 遍历TreeView或者TreeViewItem, 不包括自身节点
        /// </summary>
        /// <param name="treeViewThing"></param>
        /// <param name="act"></param>
        public static void TraverseTree(ItemsControl treeViewThing, Action<TreeViewItem, object> act)
        {
            System.Diagnostics.Debug.Assert(treeViewThing is TreeView || treeViewThing is TreeViewItem, "Parameter 'treeViewThing' can only be a TreeView or TreeViewItem");
            if (treeViewThing.Items.Count > 0)
            {
                for (int i = 0; i < treeViewThing.Items.Count; i++)
                {
                    object item = treeViewThing.Items[i];
                    TreeViewItem node = item as TreeViewItem;

                    if (node == null)
                    {
                        node = treeViewThing.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    }

                    act(node, item);
                    TraverseTree(node, act);
                }
            }
        }

        /// <summary>
        /// 从内容得到容器(TreeViewItem), 对于收缩的项, 可能某些项的Container还没创建, 那就没办法了, 只能返回null.
        /// </summary>
        /// <param name="treeViewThing"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TreeViewItem ContainerFromItem(ItemsControl treeViewThing, object item)
        {
            System.Diagnostics.Debug.Assert(treeViewThing is TreeView || treeViewThing is TreeViewItem, "Parameter 'treeViewThing' can only be a TreeView or TreeViewItem");
            if (treeViewThing.Items.Count > 0)
            {
                for (int i = 0; i < treeViewThing.Items.Count; i++)
                {
                    object tempItem = treeViewThing.Items[i];
                    TreeViewItem node = tempItem as TreeViewItem;

                    if (tempItem == item)
                        return node ?? treeViewThing.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;

                    if (node == null)
                    {
                        node = treeViewThing.ItemContainerGenerator.ContainerFromItem(tempItem) as TreeViewItem;
                    }

                    //  有的时候界面还没生成, 那么上面返回null
                    System.Diagnostics.Debug.Assert(node != null);

                    node = ContainerFromItem(node, item);
                    if (node != null)
                        return node;

                }
            }

            return null;
        }

        public static object ItemFromContainer(ItemsControl treeViewThing, TreeViewItem container)
        {
            System.Diagnostics.Debug.Assert(treeViewThing is TreeView || treeViewThing is TreeViewItem, "Parameter 'treeViewThing' can only be a TreeView or TreeViewItem");
            if (treeViewThing.Items.Count > 0)
            {
                for (int i = 0; i < treeViewThing.Items.Count; i++)
                {
                    object item = treeViewThing.Items[i];
                    TreeViewItem node = item as TreeViewItem;

                    if (node == null)
                    {
                        node = treeViewThing.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    }
                    //  有的时候界面还没生成, 那么上面返回null
                    System.Diagnostics.Debug.Assert(node != null);

                    if (node == container)
                        return item;

                    item = ItemFromContainer(node, container);
                    if (item != null)
                        return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 展开TreeView或者TreeViewItem    [WJ 20120321][Not tested]
        /// </summary>
        /// <param name="treeViewThing"></param>
        /// <remarks>"Parameter 'treeViewThing' can only be a TreeView or TreeViewItem"
        /// Learning: private和internal方法不要使用断言, public 可以用;
        /// </remarks>
        public static void ExpandTree(ItemsControl treeViewThing)
        {
            System.Diagnostics.Debug.Assert(treeViewThing is TreeView || treeViewThing is TreeViewItem, "Parameter 'treeViewThing' can only be a TreeView or TreeViewItem");
            if (treeViewThing.Items.Count > 0)
            {
                for (int i = 0; i < treeViewThing.Items.Count; i++)
                {
                    TreeViewItem node = treeViewThing.Items[i] as TreeViewItem;

                    if (node == null)
                    {
                        node = treeViewThing.ItemContainerGenerator.ContainerFromItem(treeViewThing.Items[i]) as TreeViewItem;
                    }

                    //  有的时候界面还没生成, 那么上面返回null
                    System.Diagnostics.Debug.Assert(node != null);

                    node.IsExpanded = true;
                    ExpandTree(node);
                }
            }
        }
        /// <summary>
        /// 收缩TreeView或者TreeViewItem [20120321 WJ][Not tested]
        /// </summary>
        /// <param name="treeViewThing"></param>
        /// <param name="isRecursional">是否递归收缩子节点. 一个节点在收缩的状态下, 其下层节点仍然可能由两种状态</param>
        public static void CollapseTree(ItemsControl treeViewThing, bool isRecursional)
        {
            System.Diagnostics.Debug.Assert(treeViewThing is TreeView || treeViewThing is TreeViewItem, "Parameter 'treeViewThing' can only be a TreeView or TreeViewItem");
            if (treeViewThing.Items.Count > 0)
            {
                for (int i = 0; i < treeViewThing.Items.Count; i++)
                {
                    TreeViewItem node = treeViewThing.Items[i] as TreeViewItem;

                    if (node != null)
                    {
                        node.IsExpanded = false;
                        if (isRecursional)
                            CollapseTree(node, true);
                    }
                }
            }
        }
        /// <summary>
        /// 同上, 参照自网上, 应该逻辑上问题不大
        /// </summary>
        /// <param name="treeViewThing"></param>
        public static void CollapseTree(ItemsControl treeViewThing)
        {
            System.Diagnostics.Debug.Assert(treeViewThing is TreeView || treeViewThing is TreeViewItem, "Parameter 'treeViewThing' can only be a TreeView or TreeViewItem");
            CollapseTree(treeViewThing, false);
        }
        #endregion

        public static TreeViewItem GetTreeViewItemByIndex(TreeView treeView, int index)
        {
            return GetItemByIndexInternal(treeView, index);
        }
        public static TreeViewItem GetTreeViewItemByIndex(TreeViewItem treeViewItem, int index)
        {
            return GetItemByIndexInternal(treeViewItem, index);
        }
        public static TreeViewItem GetTreeViewItemByData(TreeView treeView, object reference)
        {
            return GetItemByReferenceInternal(treeView, reference);
        }
        public static TreeViewItem GetTreeViewItemByData(TreeViewItem treeViewItem, object reference)
        {
            return GetItemByReferenceInternal(treeViewItem, reference);
        }

        public static IList<TreeViewItem> GetTreeViewItems(TreeView treeView)
        {
            return GetItemsInternal(treeView);
        }
        public static IList<TreeViewItem> GetTreeViewItems(TreeViewItem treeView)
        {
            return GetItemsInternal(treeView);
        }

        public static TreeViewItem Show(TreeView treeView, object[] objectPath)
        {
            return ShowItemInternal(treeView, objectPath, null);
        }
        public static TreeViewItem Show(TreeViewItem treeViewItem, object[] objectPath)
        {
            return ShowItemInternal(treeViewItem, objectPath, null);
        }

        public static TreeViewItem Show(TreeView treeView, int[] indexPath)
        {
            return ShowItemInternal(treeView, null, indexPath);
        }
        public static TreeViewItem Show(TreeViewItem treeViewItem, int[] indexPath)
        {
            return ShowItemInternal(treeViewItem, null, indexPath);
        }

        public static void ExpandAll(TreeView treeView)
        {
            ExpandAllInternal(treeView);
        }
        public static void ExpandAll(ItemsControl treeViewItem)
        {
            ExpandAllInternal(treeViewItem);
        }

        #region implementation
        private static TreeViewItem GetItemByReferenceInternal(ItemsControl treeViewThing, object reference)
        {
            ValidateItemsControl(treeViewThing);
            if (treeViewThing.Items.Contains(reference))
            {
                return treeViewThing.ItemContainerGenerator.ContainerFromItem(reference) as TreeViewItem;
            }
            else
            {
                throw new ArgumentOutOfRangeException("reference");
            }
        }
        private static TreeViewItem GetItemByIndexInternal(ItemsControl treeViewThing, int index)
        {
            ValidateItemsControl(treeViewThing);
            if (index >= 0 && index < treeViewThing.Items.Count)
            {
                return treeViewThing.ItemContainerGenerator.ContainerFromIndex(index) as TreeViewItem;
            }
            else
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }
        private static IList<TreeViewItem> GetItemsInternal(ItemsControl treeViewThing)
        {
            ValidateItemsControl(treeViewThing);

            TreeViewItem[] nodes = new TreeViewItem[treeViewThing.Items.Count];

            for (int i = 0; i < treeViewThing.Items.Count; i++)
            {
                TreeViewItem node = treeViewThing.Items[i] as TreeViewItem;

                if (node == null)
                {
                    node = treeViewThing.ItemContainerGenerator.ContainerFromItem(treeViewThing.Items[i]) as TreeViewItem;
                }

                nodes[i] = node;
            }

            return nodes;
        }

        private static void ExpandAllInternal(ItemsControl treeViewThing)
        {
            ValidateItemsControl(treeViewThing);

            TreeViewItem theNode;

            theNode = treeViewThing as TreeViewItem;
            if (theNode != null)
            {
                UIExpand(theNode);
            }

            for (int i = 0; i < treeViewThing.Items.Count; i++)
            {
                theNode = treeViewThing.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                if (theNode != null)
                {
                    UIExpand(theNode);
                    ExpandAllInternal(theNode);
                }
            }
        }

        private static TreeViewItem ShowItemInternal(ItemsControl treeViewThing,
            object[] objectPath, int[] indexPath)
        {
            ValidateItemsControl(treeViewThing);


            bool useObjectPath = true;
            TreeViewItem theItem = null;

            GetTreeViewItem(treeViewThing, objectPath, indexPath, out useObjectPath, out theItem);

            //2) See if the target item is already visible

            //Current state:
            //If 'theItem' is null, the parent needs to be expanded.
            //If 'theItem' != null, the parent doesn't need to be expanded. If this was "the item"
            //that was looked for (the path array.Legth == 0) then return the TVI
            //otherwise, go down another level

            if (theItem == null)
            {
                //the item is null. This is sad.

                //the first thing to do is make sure the "conditions are right" for the item to appear.
                //this means the parent TVI should be expanded.

                TreeViewItem tvi = treeViewThing as TreeViewItem;
                if (tvi != null)
                {
                    tvi.IsExpanded = true;
                }
                else
                {
                    Debug.Assert(treeViewThing is TreeView, "if it's not a TVI, it should be a TreeView");
                }


                //pump the dispatcher
                WaitForPriority(DispatcherPriority.Background);

                bool useObjectPath2;

                GetTreeViewItem(treeViewThing, objectPath, indexPath, out useObjectPath2, out theItem);
                Debug.Assert(useObjectPath2 == useObjectPath);
                if (theItem == null)
                {
                    throw new ApplicationException("Error getting ahold of the item");
                }

            }

            Debug.Assert(theItem != null);

            //we have the item
            if (useObjectPath)
            {
                Debug.Assert(objectPath.Length != 0);
                if (objectPath.Length == 1)
                {
                    return theItem;
                }
                else
                {
                    //we need to go down another level
                    objectPath = TrimArray<object>(objectPath);
                }
            }
            else
            {
                Debug.Assert(indexPath.Length != 0);
                if (indexPath.Length == 1)
                {
                    return theItem;
                }
                else
                {
                    indexPath = TrimArray<int>(indexPath);
                }
            }

            Debug.Assert(theItem != null);
            return ShowItemInternal(theItem, objectPath, indexPath);
        }

        private static void GetTreeViewItem(ItemsControl treeViewThing, object[] objectPath, int[] indexPath,
            out bool useObjectPath, out TreeViewItem theItem)
        {
            if (objectPath == null)
            {
                useObjectPath = false;
                if (indexPath == null)
                {
                    throw new ArgumentException("If objectPath is null, indexPath must have a value", "indexPath");
                }
                if (indexPath.Length < 1)
                {
                    throw new ArgumentException("indexPath.Length must be at least 1", "indexPath");
                }


                if (indexPath[0] < 0 || indexPath[0] >= treeViewThing.Items.Count)
                {
                    throw new ArgumentException("indexPath[0] is out of rang for treeViewThing.Items", "indexPath");
                }
                else
                {
                    theItem = GetItemByIndexInternal(treeViewThing, indexPath[0]);
                }
            }
            else
            {
                useObjectPath = true;
                if (indexPath != null)
                {
                    throw new ArgumentException("If objectPath is defined, indexPath should be null", "indexPath");
                }
                if (objectPath.Length < 1)
                {
                    throw new ArgumentException("objectPath.Length must be at least 1", "objectPath");
                }

                if (!treeViewThing.Items.Contains(objectPath[0]))
                {
                    throw new ArgumentException("treeViewThing does not contain the first item defined in objectPath", "objectPath");
                }
                else
                {
                    theItem = GetItemByReferenceInternal(treeViewThing, objectPath[0]);
                }
            }
        }

        private static void UIExpand(TreeViewItem treeViewItem)
        {
            treeViewItem.IsExpanded = true;

            //this is the special sauce: it let's layout happen for this object
            //KMOORE 2006-05-19: stollen from DRTS. Needs to be reviewed by someone "smart"
            WaitForPriority(DispatcherPriority.Background);

//#if DEBUG
//            if (treeViewItem.Items.Count > 0)
//            {
//                TreeViewItem subNode = treeViewItem.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem;
//                Debug.Assert(subNode != null, "we should have a good node now");
//            }
//#endif
        }

        private static void ValidateItemsControl(ItemsControl itemsControl)
        {
            Debug.Assert(itemsControl is TreeViewItem || itemsControl is TreeView);
        }

        private static T[] TrimArray<T>(T[] items)
        {
            if (items.Length > 0)
            {
                T[] newItems = new T[items.Length - 1];
                Array.Copy(items, 1, newItems, 0, newItems.Length);
                return newItems;
            }
            else
            {
                return new T[0];
            }
        }

        public void DumpLogicalTree(object parent, TextWriter writer)
        {
            string typeName = parent.GetType().Name;
            string name = null;
            DependencyObject doParent = parent as DependencyObject;
            if (doParent == null) return;

            // Not everything in the logical tree is a dependency object
            if (doParent != null)
            {
                name = (string)(doParent.GetValue(FrameworkElement.NameProperty) ?? "");
            }
            else
            {
                name = parent.ToString();
            }

            writer.Write(string.Format("{0}: {1}", typeName, name));

            foreach (object child in LogicalTreeHelper.GetChildren(doParent))
            {
                DumpLogicalTree(child, writer);
            }
        }

        public void DumpVisualTree(DependencyObject parent, TextWriter writer)
        {
            string typeName = parent.GetType().Name;
            string name = (string)(parent.GetValue(FrameworkElement.NameProperty) ?? "");

            writer.Write(string.Format("{0}: {1}", typeName, name));

            if (parent == null) return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                DumpVisualTree(child, writer);
            }
        }

        #endregion

        #region Stolen DRT code

        private static bool WaitForPriority(DispatcherPriority priority)
        {
            const int defaultTimeout = 30000;

            // Schedule the ExitFrame operation to end the nested pump after the timeout trigger happens
            TimeoutFrame frame = new TimeoutFrame();

            FrameTimer timeoutTimer = new FrameTimer(frame, defaultTimeout,
                TimeoutFrameOperationInstance, DispatcherPriority.Send);
            timeoutTimer.Start();

            // exit after a priortity has been processed
            DispatcherOperation opExit = Dispatcher.CurrentDispatcher.BeginInvoke(priority,
                ExitFrameOperationInstance, frame);

            // Pump the dispatcher
            Dispatcher.PushFrame(frame);

            // abort the operations that did not get processed
            if (opExit.Status != DispatcherOperationStatus.Completed)
            {
                opExit.Abort();
            }
            if (!timeoutTimer.IsCompleted)
            {
                timeoutTimer.Stop();
            }

            return !frame.TimedOut;
        }

        private static object ExitFrameOperation(object obj)
        {
            DispatcherFrame frame = obj as DispatcherFrame;
            frame.Continue = false;
            return null;
        }

        private static object TimeoutFrameOperation(object obj)
        {
            TimeoutFrame frame = obj as TimeoutFrame;
            frame.Continue = false;
            frame.TimedOut = true;
            return null;
        }

        private static readonly DispatcherOperationCallback
            ExitFrameOperationInstance = new DispatcherOperationCallback(ExitFrameOperation);
        private static readonly DispatcherOperationCallback
            TimeoutFrameOperationInstance = new DispatcherOperationCallback(TimeoutFrameOperation);

        #region helper classes

        private class TimeoutFrame : DispatcherFrame
        {
            bool timedout = false;

            public bool TimedOut
            {
                get { return timedout; }
                set { timedout = value; }
            }
        }

        private class FrameTimer : DispatcherTimer
        {
            DispatcherFrame frame;
            DispatcherOperationCallback callback;
            bool isCompleted = false;

            public FrameTimer(DispatcherFrame frame, int milliseconds, DispatcherOperationCallback callback, DispatcherPriority priority)
                : base(priority)
            {
                this.frame = frame;
                this.callback = callback;
                Interval = TimeSpan.FromMilliseconds(milliseconds);
                Tick += new EventHandler(OnTick);
            }

            public bool IsCompleted
            {
                get { return isCompleted; }
            }

            void OnTick(object sender, EventArgs args)
            {
                isCompleted = true;
                Stop();
                callback(frame);
            }
        }

        #endregion
        #endregion
    }
}
