using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core
{
    /// <summary>
    /// 定义拥有名称的对象接口
    /// </summary>
    public interface INamedObject
    {
        #region Properties
        string Name
        {
            get;
        }
        #endregion
    }
}
