using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 区间
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// TODO...
    /// Not tested.
    /// </remarks>
    public class Range<T>:IEquatable<Range<T>>, ICloneable where T:IComparable<T>
    {
        #region Fields
        public static Range<T> Empty = new Range<T>(RangePoint<T>.Zero, RangePoint<T>.Zero);
        public static Range<T> All = new Range<T>(null, null);

        private RangePoint<T>? begin;
        private RangePoint<T>? end;
        #endregion

        #region Properties
        public RangePoint<T>? Begin
        {
            get { return begin; }
            set { begin = value; }
        }
        public RangePoint<T>? End
        {
            get { return end; }
            set { end = value; }
        }
        public bool IsEmpty
        {
            get { return Begin.HasValue && Begin == End && Begin.Value.Open; }
        }
        public bool IsConfined
        {
            get { return Begin.HasValue && End.HasValue; }
        }
        #endregion

        #region Constructors
        public Range(RangePoint<T>? begin, RangePoint<T>? end)
        {
            if (!Verify(begin, end))
                throw new ArgumentException("Begin must be less than end.");

            this.Begin = begin;
            this.End = end;
        }
        public Range(T begin, bool bOpen, T end, bool eOpen)
            : this(new RangePoint<T>(begin, bOpen), new RangePoint<T>(end, eOpen))
        {
        }
        public Range(T begin, T end):this(begin, false, end, false)
        {
        }
        #endregion

        #region Methods

        #region Interface implementations
        public virtual object Clone()
        {
            return new Range<T>(Begin.HasValue ? (RangePoint<T>?)Begin.Value.Clone() : null, End.HasValue ? (RangePoint<T>?)End.Value.Clone() : null);
        }
        #endregion

        #region Basic
        public static bool Verify(RangePoint<T>? begin, RangePoint<T>? end)
        {
            if (begin.HasValue && end.HasValue)
                return begin.Value <= end.Value;
            return true;
        }
        #endregion

        #region General
        public bool Contains(T value)
        {
            if (begin.HasValue)
            {
                if (begin.Value.Value.CompareTo(value)>0)
                    return false;
                if (begin.Value.Value.CompareTo(value) == 0 && begin.Value.Open)
                    return false;
            }
            if (end.HasValue)
            {
                if (end.Value.Value.CompareTo(value) < 0)
                    return false;
                if (end.Value.Value.CompareTo(value) == 0 && end.Value.Open)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 有重叠部分
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <remarks>
        /// 两个Range的关系有
        /// IsOverlapped: Contains, IsCross
        /// 反之为NotOverlapped
        /// </remarks>
        public bool IsOverlapped(Range<T> other)
        {
            if (this.IsEmpty || other.IsEmpty)
                return false;
            return CanBeAsBeforeOrSame(this.Begin, other.End) && CanBeAsBeforeOrSame(other.Begin, this.End);
        }

        /// <summary>
        /// 比如首先IsOverlapped,否则会抛异常
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Range<T> Combine(Range<T> other)
        {
            if (IsOverlapped(other))
                return new Range<T>(CanBeAsBeforeOrSame(this.Begin, other.Begin) ? this.Begin : other.Begin,
                    CanBeAsAfterOrSame(this.End, other.End) ? this.End : other.End);
            throw new Exception("Can not combine.");
        }

        /// <summary>
        /// 如果没有交集，返回Empty
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Range<T> Intersect(Range<T> other)
        {
            if (IsOverlapped(other))
                return new Range<T>(CanBeAsBeforeOrSame(this.Begin, other.Begin) ? other.Begin : this.Begin,
                    CanBeAsAfterOrSame(this.End, other.End) ? other.End : this.End);
            return Empty;
        }

        public bool Contains(Range<T> other)
        {
            return CanBeAsBeforeOrSame(this.Begin, other.Begin) && CanBeAsAfterOrSame(this.End, other.End);
        }

        public bool IsCross(Range<T> other)
        {
            return CanBeAsBeforeOrSame(this.Begin, other.Begin) ?
                CanBeAsAfter(this.End, other.Begin) :
                CanBeAsBeforeOrSame(this.Begin, other.End);
        }

        public bool Equals(Range<T> other)
        {
            return this.Begin == other.Begin && this.End == other.End;
        }

        public override bool Equals(object obj)
        {
            return obj is Range<T>?Equals((Range<T>)obj):base.Equals(obj);
        }

        public static bool operator==(Range<T> range1, Range<T> range2)
        {
            return range1.Equals(range2);
        }

        public static bool operator!=(Range<T> range1, Range<T> range2)
        {
            return !range1.Equals(range2);
        }
        #endregion

        #region Thought of as
        /// <summary>
        /// Can be thought of as before
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point"></param>
        public static bool CanBeAsBefore(RangePoint<T>? point1, RangePoint<T>? point2)
        {
            if (!point1.HasValue)
                return true;
            if (!point2.HasValue)
                return false;
            return point1.Value.CanBeAsBefore(point2.Value);
        }

        public static bool CanBeAsBeforeOrSame(RangePoint<T>? point1, RangePoint<T>? point2)
        {
            if (!point1.HasValue)
                return true;
            if (!point2.HasValue)
                return false;
            return point1.Value.CanBeAsBeforeOrSame(point2.Value);
        }

        public static bool CanBeAsAfter(RangePoint<T>? point1, RangePoint<T>? point2)
        {
            if (!point1.HasValue)
                return true;
            if (!point2.HasValue)
                return false;
            return point1.Value.CanBeAsAfter(point2.Value);
        }

        public static bool CanBeAsAfterOrSame(RangePoint<T>? point1, RangePoint<T>? point2)
        {
            if (!point1.HasValue)
                return true;
            if (!point2.HasValue)
                return false;
            return point1.Value.CanBeAsAfterOrSame(point2.Value);
        }
        public static bool CanBeAsSame(RangePoint<T>? point1, RangePoint<T>? point2)
        {
            if (!point1.HasValue)
                return !point2.HasValue;
            return point1.Value.CanBeAsSame(point2.Value);
        }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0}{1}, {2}{3}", Begin.HasValue ? (Begin.Value.Open ? '(' : '[') : '(',
                Begin.HasValue ? Begin.Value.Value.ToString() : null, End.HasValue ? End.Value.Value.ToString() : null,
                End.HasValue ? (End.Value.Open ? ')' : ']') : ')');
        }
        #endregion

        #endregion

    }
}
