using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    public class MultiIntRange:MultiRange<int>
    {
        public MultiIntRange() { }
        public MultiIntRange(params IntRange[] ranges) : base(ranges) { }
        public MultiIntRange(params string[] rangeStrs)
        {
            foreach (string rangeStr in rangeStrs)
                Combine(IntRange.Parse(rangeStr));
        }

        public MultiIntRange(MultiRange<int> mr)
        {
            foreach (Range<int> range in mr.Ranges)
                Combine(range);
        }

        public override object Clone()
        {
            MultiIntRange mr = new MultiIntRange();
            foreach (IntRange range in Ranges)
                mr.Ranges.Add((IntRange)range.Clone());
            return mr;
        }
    }
}
