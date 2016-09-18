using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    [AttributeUsageAttribute(AttributeTargets.Property, Inherited = false, AllowMultiple = false), Serializable]
    public class IndexAttribute : Attribute
    {
        public IndexAttribute(string prename, int maxindex)
        {
            this.preName = prename;
            this.maxIndex = maxindex;
        }


        public IndexAttribute(string prename, int maxindex, string lastname)
        {
            this.preName = prename;
            this.maxIndex = maxindex;
            this.lastName = lastname;
        }

        public IndexAttribute(string prename, int maxindex, string lastname, int minIndex)
        {
            this.preName = prename;
            this.maxIndex = maxindex;
            this.lastName = lastname;
            this.minIndex = minIndex;
        }

        public IndexAttribute(string prename, int maxindex, int minIndex, int arrNum)
        {
            this.preName = prename;
            this.maxIndex = maxindex;
            this.minIndex = minIndex;
            this.arrNum = arrNum;
        }

        protected string preName = "";

        /// <summary>
        /// 属性的前缀名
        /// 如数据库里有一些列D_0、D_1、D_2……、D_96，前缀就是"D_"
        /// </summary>
        public string PreName
        {
            get { return preName; }
            set { preName = value; }
        }         private int maxIndex;

        /// <summary>
        /// 最大索引数
        /// 如数据库里有一些列D_0、D_1、D_2……、D_96，最大的索引数就是"96"
        /// </summary>
        public int MaxIndex
        {
            get { return maxIndex; }
            set { maxIndex = value; }
        }

        protected string lastName = "";

        /// <summary>
        /// 后缀名
        /// 如数据库里有一些列D_0_Size、D_1_Size、D_2_Size……、D_96_Size，后缀名就是"Size"
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        protected int minIndex;

        public int MinIndex
        {
            get { return minIndex; }
            set { minIndex = value; }
        }

        protected int arrNum = 2;

        /// <summary>
        /// 数组的维数
        /// </summary>
        public int ArrNum
        {
            get { return arrNum; }
            set { arrNum = value; }
        }
    }
}
