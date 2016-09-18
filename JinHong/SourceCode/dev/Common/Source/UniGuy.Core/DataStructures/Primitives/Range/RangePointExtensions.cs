using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    public static class RangePointExtensions
    {
        public static RangePoint<T>? ToggleOpen<T>(this RangePoint<T>? range) where T:IComparable<T>
        {
            return range.HasValue ? (RangePoint<T>?)new RangePoint<T>(range.Value.Value, !range.Value.Open) : null;
        }

    }
}
