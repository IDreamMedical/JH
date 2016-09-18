using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

using UniGuy.Core.Extensions;

namespace UniGuy.Core.DataStructures.C5.Wj
{
    public class TreeDictionary<T>
    {
        private readonly string key;
        private T value;
        private IList<TreeDictionary<T>> children;

        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public IList<TreeDictionary<T>> Children
        {
            get { return children; }
            set { children = value; }
        }

        public TreeDictionary(string key, T val) 
        {
            this.key = key;
            this.value = val; 
        }
        public TreeDictionary(string key, T val, IList<TreeDictionary<T>> children)
            :this(key, val)
        {
            this.children = children;
        }
        public TreeDictionary(string key, T val, params TreeDictionary<T>[] children)
            : this(key, val)
        {
            this.children = new ArrayList<TreeDictionary<T>>();
            foreach (TreeDictionary<T> child in children)
                this.children.Add(child);
        }

        /// <summary>
        /// 是否包含键(只看子级)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            for (int i = 0; i < children.Count; i++)
                if (children[i].key == key)
                    return true;

            return false;
        }

        public TreeDictionary<T> GetChild(string key)
        {
            for (int i = 0; i < children.Count; i++)
                if (children[i].key == key)
                    return children[i];

            return null;
        }

        public T this[string key]
        {
            get
            {
                for (int i = 0; i < children.Count; i++)
                    if (children[i].key == key)
                        return children[i].value;
                throw new Exception(string.Format("Tree dictionary does not contain key '{0}'.", key));
            }
            set
            {
                for (int i = 0; i < children.Count; i++)
                {
                    if (children[i].key == key)
                    {
                        children[i].value = value;
                        return;
                    }
                }

                children.Add(new TreeDictionary<T>(key, value));
            }
        }

        public TreeDictionary<T> GetByPath(params string[] path)
        {
            TreeDictionary<T> temp = this;
            foreach (string pathNode in path)
                temp = temp.GetChild(pathNode);

            return temp;
        }
        public void SetValueByPath(T value, params string[] path)
        {
#if DOT_NET_35
            string[] temp = path.Remove(path.Length - 1);
#else
            string[] temp = path;
            Array.Copy(temp, path.Length, temp, path.Length - 1, temp.Length - path.Length);
        Array.Resize(ref temp, temp.Length - 1);
#endif
            TreeDictionary<T> temp2 = GetByPath(temp);
            temp2[path[path.Length-1]] = value;
        }
        public void SetValueByPathEnhanced(T value, params string[] path)
        {
            TreeDictionary<T> temp = this;
            for (int i = 0; i < path.Length - 1; i++)
            {
                if (temp.ContainsKey(path[i]))
                    temp = temp.GetChild(path[i]);
                else
                {
                    TreeDictionary<T> temp2 = new TreeDictionary<T>(path[i], default(T));
                    temp.children.Add(temp2);
                    temp = temp2;
                }
            }

            temp[path[path.Length - 1]] = value;
        }

        /// <summary>
        /// 判断树中是否有Key为key的节点树
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool TranversalContainsKey(string key)
        {
            if (key==this.key)
                return true;

            for (int i = 0; i < children.Count; i++)
                if (children[i].TranversalContainsKey(key))
                    return true;

            return false;
        }
    }
}
