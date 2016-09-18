using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 序列化方式
    /// </summary>
    public enum SerializationType
    {
        None,   //  直接使用ToString()方法对对象处理
        Xml,    //  使用Xml序列化对象(扩展行好,但是要自定义很多序列化代码)
        Binary, //  使用二进制序列化对象(使用方便,但是不开放,不可读)
        Soap,   //  Soap序列化(操作方法和Binary一样,可以不支持泛型等)
        Xaml,   //  Xaml序列化,.NET3.5依靠XamlReader和XamlWriter,不支持泛型,.NET4.0开始支持泛型,操作方便,相对于Xml序列化不需要写自定义序列化方法(强烈推荐)
        DateTime,   //  时间类型，保持时间原样
        Dictionary,  //  字典扩展，对应的ColumnIoType只能为Write
        BoolToByte,
        EnumToByte
    }
}
