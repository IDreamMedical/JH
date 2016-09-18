using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 用来描述要对数据库执行的操作
    /// </summary>
    [Serializable]
    public enum SqlType
    {
        Select,
        Insert,
        Update,
        Delete,
        IsExists
    }
}
