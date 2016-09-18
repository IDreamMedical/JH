using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 字段读取写入方式
    /// </summary>
    [DefaultValue(ColumnIoType.ReadWrite)][Flags]
    public enum ColumnIoType
    {
        None = 0,
        Read = 1,
        Write = 2,
        ReadWrite = Read|Write
    }
}
