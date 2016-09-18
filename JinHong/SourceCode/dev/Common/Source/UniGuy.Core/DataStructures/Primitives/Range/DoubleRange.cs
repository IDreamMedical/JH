using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    public class DoubleRange:Range<double>
    {
        public DoubleRange(Range<double> range)
            : base(range.Begin, range.End)
        {
        }
        public DoubleRange(RangePoint<double>? b, RangePoint<double>? e)
            : base(b, e)
        {
        }

        //  [a, b)
        public static bool TryParse(string str, out DoubleRange range)
        {
            range = null;
            str = str.Trim();
            if(string.IsNullOrEmpty(str)|| str.Length<3)
                return false;
            bool beginOpen = false, endOpen = false;
            if (str[0] == '(')
                beginOpen = true;
            else if (str[0] != '[')
                return false;
            if (str[str.Length - 1] == ')')
                endOpen = true;
            else if (str[str.Length - 1] != ']')
                return false;
            str = str.Substring(1, str.Length - 2).Trim();
            int index = str.IndexOf(',');
            if (index < 0)
                return false;

            RangePoint<double>? point1 = null;
            string beginStr = str.Substring(0, index);
            if (!string.IsNullOrEmpty(beginStr))
            {
                double begin = 0;
                if (!double.TryParse(beginStr, out begin))
                    return false;
                point1 = new RangePoint<double>(begin, beginOpen);
            }
            else
                point1 = null;

            RangePoint<double>? point2 = null;
            string endStr = str.Substring(index+1).Trim();
            if (!string.IsNullOrEmpty(endStr))
            {
                double end = 0;
                if (!double.TryParse(endStr, out end))
                    return false;
                point2 = new RangePoint<double>(end, endOpen);
            }
            else
                point2 = null;

            range = new DoubleRange(point1, point2);
            return true;
        }

        public static DoubleRange Parse(string str)
        {
            bool beginOpen = false, endOpen = false;
            if (str[0] == '(')
                beginOpen = true;
            if (str[str.Length - 1] == ')')
                endOpen = true;
            str = str.Substring(1, str.Length - 2).Trim();
            int index = str.IndexOf(',');

            RangePoint<double>? point1 = null;
            string beginStr = str.Substring(0, index);
            if (!string.IsNullOrEmpty(beginStr))
            {
                double begin = double.Parse(beginStr);
                point1 = new RangePoint<double>(begin, beginOpen);
            }
            else
                point1 = null;

            RangePoint<double>? point2 = null;
            string endStr = str.Substring(index + 1).Trim();
            if (!string.IsNullOrEmpty(endStr))
            {
                double end = double.Parse(endStr);
                point2 = new RangePoint<double>(end, endOpen);
            }
            else
                point2 = null;

            return new DoubleRange(point1, point2);
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}{1}, {2}{3}", Begin.HasValue ? (Begin.Value.Open ? '(' : '[') : '(',
        //        Begin.HasValue ? Begin.Value.Value.ToString() : null, End.HasValue ? End.Value.Value.ToString() : null,
        //        End.HasValue ? (End.Value.Open ? ')' : ']') : ')');

        //}

        public override object Clone()
        {
            return new DoubleRange((Range<double>)base.Clone());
        }
    }
}
