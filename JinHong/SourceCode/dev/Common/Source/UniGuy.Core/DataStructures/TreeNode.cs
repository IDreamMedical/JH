using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    public class TreeNode
    {
        private List<TreeNode> children;
        private object value;
        private TreeNode parent;

        public List<TreeNode> Children
        {
            get { return children; }
            set { children = value; }
        }

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public TreeNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public TreeNode GetChild(object value)
        {
            foreach (TreeNode tn in children)
                if (tn.value == value)
                    return tn;
            return null;
        }

        public TreeNode AddChild(TreeNode tn)
        {
            if (!children.Contains(tn))
            {
                if (children == null)
                    children = new List<TreeNode>();
                tn.Parent = this;
                children.Add(tn);
                return tn;
            }
            return null;
        }

        public TreeNode AddChildValue(object value)
        {
            return AddChild(new TreeNode(value));
        }

        public void AddChildren(ICollection<TreeNode> tns)
        {
            foreach (TreeNode tn in tns)
                AddChild(tn);
        }

        public TreeNode RemoveChild(TreeNode tn)
        {
            tn.parent = null;
            children.Remove(tn);
            return tn;
        }

        public void ClearChildren()
        {
            if (children != null)
                if (children.Count > 0)
                    RemoveChild(children[0]);
        }

        public TreeNode AddSibling(TreeNode tn)
        {
            return this.parent.AddChild(tn);
        }

        public TreeNode AddSiblingValue(object value)
        {
            return AddSibling(new TreeNode(value));
        }

        public void RemoveSibling(TreeNode tn)
        {
            this.parent.RemoveChild(tn);
        }

        public TreeNode this[object value]
        {
            get
            {
                return GetChild(value);
            }
        }

        public TreeNode()
        {
        }

        public TreeNode(object value):this()
        {
            this.value = value;
        }
    }
}
