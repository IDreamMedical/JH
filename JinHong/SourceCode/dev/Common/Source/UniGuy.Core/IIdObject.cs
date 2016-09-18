using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core
{
    /// <summary>
    /// 定义标识对象接口
    /// </summary>
    public interface IIdObject
    {
        #region Properties
        string Id
        {
            get;
        }
        #endregion
    }
}
