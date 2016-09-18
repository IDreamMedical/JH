using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    public class MultiDoubleRange : MultiRange<double>
    {
        public MultiDoubleRange() { }
        public MultiDoubleRange(params DoubleRange[] ranges) : base(ranges) { }
        public MultiDoubleRange(params string[] rangeStrs)
        {
            foreach (string rangeStr in rangeStrs)
                Combine(DoubleRange.Parse(rangeStr));
        }

        public MultiDoubleRange(MultiRange<double> mr)
        {
            foreach (Range<double> range in mr.Ranges)
                Combine(range);
        }

        public override object Clone()
        {
            MultiDoubleRange mr = new MultiDoubleRange();
            foreach (DoubleRange range in Ranges)
                mr.Ranges.Add((DoubleRange)range.Clone());
            return mr;
        }
    }
}
