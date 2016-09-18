using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 自动排序和合并的区间组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// TODO...
    /// Not tested.
    /// </remarks>
    public class MultiRange<T>:IEqualityComparer<MultiRange<T>>, ICloneable where T : IComparable<T>
    {
        #region Fields
        private List<Range<T>> ranges = new List<Range<T>>();
        #endregion

        #region Properties
        public List<Range<T>> Ranges
        {
            get { return ranges; }
            private set { ranges = value; }
        }
        #endregion

        #region Constructors
        public MultiRange(IEnumerable<Range<T>> ranges)
        {
            foreach (Range<T> range in ranges)
                this.Combine(range);
        }
        public MultiRange(params Range<T>[] ranges):this((IEnumerable<Range<T>>)ranges)
        {
        }
        #endregion

        #region Methods

        #region Interface implementations
        public virtual object Clone()
        {
            MultiRange<T> mr = new MultiRange<T>();
            foreach (Range<T> range in Ranges)
                mr.Ranges.Add((Range<T>)range.Clone());
            return mr;
        }

        public bool Equals(MultiRange<T> x, MultiRange<T> y)
        {
            if (x == null && y == null)
                return true;
            if (ReferenceEquals(x, y))
                return true;
            if (x == null || y == null)
                return false;

            if (x.Ranges.Count != y.Ranges.Count)
                return false;

            for (int i = 0; i < x.Ranges.Count;i++ )
                if (x.Ranges[i] != y.Ranges[i])
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is MultiRange<T>)
                return Equals(this, (MultiRange<T>)obj);

            return base.Equals(obj);
        }

        public int GetHashCode(MultiRange<T> obj)
        {
            int c = 0;
            foreach (Range<T> range in Ranges)
                c ^= range.GetHashCode();
            return c;
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public static bool operator ==(MultiRange<T> x, MultiRange<T> y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(MultiRange<T> x, MultiRange<T> y)
        {
            return !x.Equals(y);
        }
        #endregion

        #region General
        public bool Contains(T value)
        {
            foreach (Range<T> range in ranges)
                if (range.Contains(value))
                    return true;
            return false;
        }

        public bool Contains(Range<T> value)
        {
            foreach (Range<T> range in ranges)
                if (range.Contains(value))
                    return true;
            return false;
        }

        public bool IsOverlapped(Range<T> value)
        {
            foreach (Range<T> range in ranges)
                if (range.IsOverlapped(value))
                    return true;
            return false;
        }

        private int Combine(Range<T> range, int start)
        {
            if (range.IsEmpty)
                return start;
            for (int i = start; i < ranges.Count; i++)
            {
                if (Range<T>.CanBeAsBefore(range.End, ranges[i].Begin))
                {
                    ranges.Insert(i, range);
                    return i+1;
                }
                if (range.IsOverlapped(ranges[i]))
                {
                    range = range.Combine(ranges[i]);
                    ranges.RemoveAt(i--);
                }
            }
            ranges.Add(range);
            return ranges.Count;
        }

        public void Combine(Range<T> range)
        {
            Combine(range, 0);
        }

        private int Intersect(Range<T> range, int start)
        {
            if (range.IsEmpty)
            {
                Ranges.Clear();
                return 0;
            }
            for (int i = start; i < ranges.Count; i++)
            {
                if (Range<T>.CanBeAsBefore(range.End, Ranges[i].Begin))
                {
                    Ranges.RemoveRange(i, Ranges.Count - i);
                    return i;
                }
                if (range.IsOverlapped(ranges[i]))
                    ranges[i] = ranges[i].Intersect(range);
            }
            return ranges.Count;
        }

        public void Intersect(Range<T> range)
        {
            Intersect(range, 0);
        }

        public int Except(Range<T> range, int start)
        {
            if (range.IsEmpty)
                return start;
            for (int i = start; i < ranges.Count; i++)
            {
                if (Range<T>.CanBeAsBefore(range.End, ranges[i].Begin))
                    return start;
                if (range.IsOverlapped(ranges[i]))
                {
                    if (Range<T>.CanBeAsBeforeOrSame(range.Begin, ranges[i].Begin))
                    {
                        //      ******
                        //  ******
                        if (Range<T>.CanBeAsBeforeOrSame(range.End, ranges[i].End))
                        {
                            ranges[i].Begin = range.End.ToggleOpen();
                            return i;
                        }
                        //      ***
                        //  **********
                        else
                        {
                            range.Begin = ranges[i].End.ToggleOpen();
                            ranges.RemoveAt(i--);
                        }
                    }
                    else
                    {
                        //  ************
                        //      ***
                        if (Range<T>.CanBeAsBefore(range.End, ranges[i].End))
                        {
                            Range<T> split1 = new Range<T>(ranges[i].Begin, range.Begin);
                            Range<T> split2 = new Range<T>(range.End, ranges[i].End);
                            ranges.RemoveAt(i);
                            ranges.Insert(i, split1);
                            ranges.Insert(i, split2);
                            return i;
                        }
                        //  ******
                        //      ******
                        else
                        {
                            ranges[i].End = range.Begin.ToggleOpen();
                            range.Begin = ranges[i].End;
                        }
                    }
                }
            }

            return ranges.Count;
        }

        public void Except(Range<T> range)
        {
            Except(range, 0);
        }

        #endregion

        #region Collection operation
        public bool Contains(MultiRange<T> other)
        {
            foreach (Range<T> range in other.ranges)
                if (!Contains(range))
                    return false;
            return true;
        }

        public MultiRange<T> Union(MultiRange<T> other)
        {
            MultiRange<T> mr = (MultiRange<T>)Clone();

            int start = 0;
            foreach (Range<T> range in other.ranges)
                start = mr.Combine(range, start);
            return mr;
        }

        public MultiRange<T> Intersect(MultiRange<T> other)
        {
            MultiRange<T> mr = (MultiRange<T>)Clone();

            int start = 0;
            foreach (Range<T> range in other.ranges)
                start = mr.Intersect(range, start);
            return mr;
        }

        public MultiRange<T> Except(MultiRange<T> other)
        {
            MultiRange<T> mr = (MultiRange<T>)Clone();

            int start = 0;
            foreach (Range<T> range in other.ranges)
                start = mr.Except(range, start);
            return mr;
        }

        public MultiRange<T> Not()
        {
            MultiRange<T> mr = new MultiRange<T>();

            if (Ranges.Count == 0)
            {
                mr.Ranges = new List<Range<T>> { Range<T>.All };
                return mr;
            }

            if (Ranges[0].Begin.HasValue)
                mr.Ranges.Add(new Range<T>(null, Ranges[0].Begin.ToggleOpen()));

            for (int i = 1; i < Ranges.Count; i++)
                mr.Ranges.Add(new Range<T>(Ranges[i - 1].End.ToggleOpen(), Ranges[i].Begin.ToggleOpen()));

            if (Ranges[Ranges.Count].End.HasValue)
                mr.Ranges.Add(new Range<T>(Ranges[Ranges.Count].End.ToggleOpen(), null));

            return mr;
        }

        public static MultiRange<T> operator +(MultiRange<T> mr1, MultiRange<T> mr2)
        {
            return mr1.Union(mr2);
        }

        public static MultiRange<T> operator -(MultiRange<T> mr1, MultiRange<T> mr2)
        {
            return mr1.Except(mr2);
        }

        public static MultiRange<T> operator !(MultiRange<T> mr)
        {
            return mr.Not();
        }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format(@"{{{0}}}", string.Join(",", Ranges));
        }
        #endregion

        #endregion

        
    }
}
