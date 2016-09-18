using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 通放委托
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public delegate object ThroughPutDelegate(Type type, string key);
}
