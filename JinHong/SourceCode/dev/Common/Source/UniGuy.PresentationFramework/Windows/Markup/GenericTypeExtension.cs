using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace UniGuy.Controls.Markup
{
    /*
      <Grid.Resources> 
        <x:Array Type="{x:Type sys:Type}"  
                 x:Key="TypeParams"> 
            <x:Type TypeName="sys:Int32" /> 
        </x:Array> 
 
        <local:GenericType BaseType="{x:Type TypeName=coll:List`1}"  
                           InnerTypes="{StaticResource TypeParams}" 
                           x:Key="ListOfInts" /> 
 
        <x:Array Type="{x:Type sys:Type}"  
                 x:Key="DictionaryParams"> 
            <x:Type TypeName="sys:Int32" /> 
            <local:GenericType BaseType="{x:Type TypeName=coll:List`1}"  
                               InnerTypes="{StaticResource TypeParams}" /> 
        </x:Array> 
 
        <local:GenericType BaseType="{x:Type TypeName=coll:Dictionary`2}" 
                           InnerTypes="{StaticResource DictionaryParams}" 
                           x:Key="DictionaryOfIntsToListOfInts" /> 
    </Grid.Resources>
     */

    /*
        There's a few key ideas here:

        A generic type has to be specified using the standard notation. So, System.Collections.Generic.List<> is "System.Collections.Generic.List1". The character ` indicates that the type is generic and the number after it indicates the number of generic parameters the type has. 
        The x:Type markup extension is able to retrieve these base generic types quite easily. 
        The generic parameter types are passed as an array of Type objects. This array is then passed into the MakeGenericType(...) call. 
     */
    public class GenericTypeExtension:MarkupExtension
    {
        public Type BaseType { get; set; }
        public Type[] InnerTypes { get; set; }

        public GenericTypeExtension() { }
        public GenericTypeExtension(Type baseType, params Type[] innerTypes)
        {
            BaseType = baseType;
            InnerTypes = innerTypes;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Type result = BaseType.MakeGenericType(InnerTypes);
            return result;
        }
    }
}
