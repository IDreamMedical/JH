using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 用来描述主键的类型
    /// Identity：自增的主键
    /// Assign：用户自己设定主键，必须保证唯一，否则将会引发异常
    /// Foreign:外键（此功能正在增加目前没有完成）
    /// </summary>
    [Serializable]
    public enum PrimaryKeyType
    {
        Identity,
        Assign,
        Foreign
    }

}
