using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    [AttributeUsage(AttributeTargets.Class, Inherited=false, AllowMultiple=false), Serializable]
    public class DataRowAttribute:Attribute
    {
        
    }
}
