using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 可比点(分为开点和闭点)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// TODO...
    /// Not tested
    /// </remarks>
    public struct RangePoint<T> : IComparable<RangePoint<T>>, IEquatable<RangePoint<T>>, ICloneable where T : IComparable<T>
    {
        #region Fields
        public static RangePoint<T> Zero = new RangePoint<T>(default(T), true);
        /// <summary>闭点</summary>
        private bool open;
        private T value;
        #endregion

        #region Properties
        public bool Open
        {
            get { return open; }
        }

        public T Value
        {
            get { return value; }
        }
        #endregion

        #region Constructors
        public RangePoint(T value, bool open)
        {
            this.value = value;
            this.open = open;
        }
        public RangePoint(T value):this(value, false)
        {
        }
        #endregion

        #region Methods

        #region Basic
        /// <summary>
        /// 不处理开闭
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(RangePoint<T> other)
        {
            return this.value.CompareTo(other.value);
        }

        public static bool Equals(RangePoint<T> x, RangePoint<T> y)
        {
            return x.open == y.open && x.value.CompareTo(y.value) == 0;
        }

        public bool Equals(RangePoint<T> other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object o)
        {
            if(o is RangePoint<T>)
                return Equals(this, (RangePoint<T>)o);
            return ReferenceEquals(this, o);
        }

        public int GetHashCode(RangePoint<T> obj)
        {
            return this.open.GetHashCode() ^ this.value.GetHashCode();
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        } 

        public RangePoint<T> ToggleOpen()
        {
            return new RangePoint<T>(Value, !Open);
        }
        

        public object Clone()
        {
            return new RangePoint<T>(Value, Open);
        }
        #endregion

        #region Operator overrides
        public static bool operator==(RangePoint<T> point1, RangePoint<T> point2)
        {
            return Equals(point1, point2);
        }

        public static bool operator !=(RangePoint<T> point1, RangePoint<T> point2)
        {
            return !Equals(point1, point2);
        }

        public static bool operator >(RangePoint<T> point1, RangePoint<T> point2)
        {
            return point1.CompareTo(point2)>0;
        }

        public static bool operator <(RangePoint<T> point1, RangePoint<T> point2)
        {
            return point1.CompareTo(point2) < 0;
        }

        public static bool operator >=(RangePoint<T> point1, RangePoint<T> point2)
        {
            return point1.CompareTo(point2) > 0 || point1==point2;
        }

        public static bool operator <=(RangePoint<T> point1, RangePoint<T> point2)
        {
            return point1.CompareTo(point2) < 0 || point1 == point2;
        }
        #endregion

        #region Thought of as
        /// <summary>
        /// Can be thought of as before
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point"></param>
        public bool CanBeAsBefore(RangePoint<T> other)
        {
            if (this < other)
                return true;
            if (this.Value.Equals(other.Value))
                return this.Open && !other.Open;
            return false;
        }

        public bool CanBeAsBeforeOrSame(RangePoint<T> other)
        {
            return this <= other;
        }

        public bool CanBeAsAfter(RangePoint<T> other)
        {
            if (this > other)
                return true;
            if (this.Value.Equals(other.Value))
                return !this.Open && other.Open;
            return false;
        }

        public bool CanBeAsAfterOrSame(RangePoint<T> other)
        {
            return this >= other;
        }
        public bool CanBeAsSame(RangePoint<T> other)
        {
            return this == other;
        }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0}{1}{2}", Open ? '(' : '[', Value, Open ? ')' : ']');
        }
        #endregion

        #endregion


    }
}
