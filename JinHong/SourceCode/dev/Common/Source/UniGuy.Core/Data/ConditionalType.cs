using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 用来描述比较的条件
    /// </summary>
    [Serializable]
    public enum ConditionalType
    {
        Equal,            //相等
        NotEqual,        //不等
        GreateThan,      //大于
        GreaterThanEqual,//大于等于
        LessThan,        //小于
        LessThanEqual,   //小于等于
        Like,            //模糊匹配
        //NotLike,         //模糊不匹配
        BetweenAnd,      //介于一定范围之间
        In               //在几个之间选择
    }
}
