using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using UniGuy.Core.Extensions;

// wj
// Created 20120229
// LastModified 20120229
namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 模糊转换器
    /// </summary>
    /// <example>
    /// 假设有A(Unknow)=B(Error),C(1),D(2)(A绑定B,C,D);
    /// 现在希望BCD任一项改变时A更新, 假设A对B的ValueConverter失败, 我们希望A绑定BCD中的第一个成功的绑定;
    /// 另外如果A发生改变, 我们希望BCD都同步更新; 该怎么办;
    /// 如果使用本转换器, 设置convertMode:One, convertBackMode:All即可(默认);
    /// 假设例子中C的值激发改变, 那么绑定后A(1)=B(1,如果反向绑定成功),C(1), D(1); 当然这个时候D改变,A更新还是取的C(1)的值;
    /// </example>
    [ValueConversion(typeof(object), typeof(object))]
    public class FuzzyMultiConverter : IMultiValueConverter
    {
        #region Fields
        /// <summary>
        /// 转换模式(默认为One, 也就是MultiValueConverter下第一个绑定成功的ValueConverter返回的值)
        /// </summary>
        private FuzzyMultiConvertMode convertMode = FuzzyMultiConvertMode.First;
        /// <summary>
        /// 反向转换模式(默认为All, 也就是目标改变, 所有源都会同步)
        /// </summary>
        private FuzzyMultiConvertBackMode convertBackMode = FuzzyMultiConvertBackMode.All;
        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置转换模式
        /// </summary>
        public FuzzyMultiConvertMode ConvertMode
        {
            get { return convertMode; }
            set { convertMode = value; }
        }
        /// <summary>
        /// 获得或者设置反向转换模式
        /// </summary>
        public FuzzyMultiConvertBackMode ConvertBackMode
        {
            get { return convertBackMode; }
            set { convertBackMode = value; }
        }
        #endregion //   Properties

        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object ret = null;

            for(int i =0;i<values.Length;i++)
            {
                switch (ConvertMode)
                {
                    case FuzzyMultiConvertMode.First:
                        return values[i];
                    case FuzzyMultiConvertMode.One:
                        if (values[i] == System.Windows.DependencyProperty.UnsetValue || values[i]==Binding.DoNothing)
                            continue;
                        return values[i];
                    case FuzzyMultiConvertMode.OneNotDefault:
                        if (values[i] == System.Windows.DependencyProperty.UnsetValue || values[i] == Binding.DoNothing || values[i].IsDefault())
                            continue;
                        return values[i];
                    case FuzzyMultiConvertMode.All:
                        if (i == 0)
                            ret = values[i];
                        else if (ret != values[i])
                            return Binding.DoNothing;
                        break;
                    case FuzzyMultiConvertMode.Index:
                        if (!(parameter is int))
                            throw new ArgumentException("must be an Interger", "parameter");
                        int index = (int)parameter;
                        return values[i];
                }
            }

            return ret;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            object[] rets = new object[targetTypes.Length];

            switch (ConvertBackMode)
            {
                case FuzzyMultiConvertBackMode.First:
                    rets[0] = value;
                    break;
                case FuzzyMultiConvertBackMode.All:
                    for (int i = 0; i < rets.Length; i++)
                        rets[i] = value;
                    break;
                case FuzzyMultiConvertBackMode.Index:
                    if (!(parameter is int))
                        throw new ArgumentException("must be an Interger", "parameter");
                    int index = (int)parameter;
                    rets[index] = value;
                    break;
            }

            return rets;
        }
        #endregion

        /// <summary>
        /// 转换模式
        /// </summary>
        public enum FuzzyMultiConvertMode
        {
            First, //   绑定第一项
            One,    //  绑定到第一个成功项, Binding.DoNothing将视作不成功, 考虑到MultiBinding下的子Binding有各种绑定模式
            OneNotDefault,  //  同上, 只不过如果取到默认值, 即使绑定成功也视作不成功
            All,   //  绑定第一项, 所有项必须相同, 否则什么也不做
            Index   //  指定索引项
        }

        public enum FuzzyMultiConvertBackMode
        {
            First,  //  绑定到第一项
            All, //  绑定到所有项
            Index   //  指定索引项
        }
    }
}
