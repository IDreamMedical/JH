using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Security.Permissions;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, UnmanagedCode = true)]

namespace UniGuy.Core.Algorithms
{
    /// <summary>
    /// An implementation of rational (fractional) numbers.
    /// Numeric range: -long.MaxValue/1 to Int64.MaxValue/1
    /// Smallest positive number: 1/long.MaxValue
    /// Smallest negative number: -1/long.MaxValue
    /// Zero: 0/1
    /// One: 1/1
    /// 
    /// Fractional numbers exhibit no rounding errors as long as they stay 
    /// in their numeric range. In other cases a System.OverflowException is
    /// raised (coming from the underlying long computation).
    /// Note that overflow exceptions can be raised even if the reduced result
    /// fraction is inside the numeric range.
    /// 
    /// The typeCode of Fraction is Decimal since this is the closest numeric
    /// representation. Note however that conversion to and from Decimal will
    /// loose numeric precision:
    /// - going from Fraction 1/3 to Decimal is limited by the number of digits
    ///		representable by a Decimal (28)
    ///	- going from Decimal 1m/3m to Fraction will introduce rounding errors in
    ///		the resulting Fraction because Fraction is constructed from a Decimal as
    ///		sign * (int / scale) where the int is unsigned 96bit and scale ranges from
    ///		1 to 10^28. We need to reduce the int to signed 64bit and limit the scale
    ///		to 10^18. This results in rounding errors in the scale number as well
    ///		as lost insignificant digits in the int. 
    ///	However it is guaranteed that the conversion is true if the Decimal uses 18 or
    ///	less significant digits. 
    /// </summary>
    /// <remarks>
    /// A fraction is kept normalized in the following way:
    /// - the denominator is always positive
    /// - the fraction is always reduced by the gcd of the nominator and denominator
    /// </remarks>
    public struct Fraction : IComparable, IConvertible, IFormattable
    {
        #region Declarations

        public static Fraction Zero = new Fraction(0, 1);
        public static Fraction MinValue = new Fraction(-long.MaxValue, 1);
        public static Fraction MaxValue = new Fraction(long.MaxValue, 1);
        public static Fraction SmallestNegValue = new Fraction(-1, long.MaxValue);
        public static Fraction SmallestValue = new Fraction(1, long.MaxValue);

        private long numerator;
        private long denominator;

        #endregion

        #region Constructors

        public Fraction(long newNumerator, long newDenominator)
        {
            long gcd;

            if (newDenominator == 0)
                throw new DivideByZeroException("Illegal fraction");
            if (newNumerator == long.MinValue)
                throw new OverflowException("Fraction nominator may not be long.MinValue");
            if (newDenominator == long.MinValue)
                throw new OverflowException("Fraction denominator may not be long.MinValue");

            numerator = newNumerator;
            denominator = newDenominator;
            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }

            gcd = Gcd(numerator, denominator);
            numerator /= gcd;
            denominator /= gcd;
        }

        #endregion

        #region Properties

        public long Numerator
        {
            get
            {
                return numerator;
            }
        }

        public long Denominator
        {
            get
            {
                return denominator;
            }
        }
        #endregion

        #region GCD

        /// <summary>
        /// The classic GCD(greatest common divisor) algorithm of Euclid
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static long Gcd(long smaller, long larger)
        {
            long rest;

            smaller = Math.Abs(smaller);
            larger = Math.Abs(larger);

            if (smaller == 0)
                return larger;
            else
            {
                if (larger == 0)
                    return smaller;
            }


            // the GCD algorithm requires second >= first
            if (smaller > larger)
            {
                rest = larger;
                larger = smaller;
                smaller = rest;
            }

            while ((rest = larger % smaller) != 0)
            {
                larger = smaller; smaller = rest;
            }
            return smaller;
        }
        #endregion

        #region Decimal conversion

        /// <summary>
        /// A lossy conversion from decimal to fraction.
        /// If the integer part of the decimal exceeds the available 64 bits (96 is possible)
        /// we truncate the least significant part of these bits.
        /// If the scale factor is too large we scale down the integer part at the expense of precision.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <remarks>
        /// Note the comments on precision issues when the number of digits used in the Decimal
        /// exceed the precision of long significantly.
        /// </remarks>
        private static Fraction ToFraction(Decimal convertedDecimal)
        {
            unchecked
            {
                Int32[] bits = Decimal.GetBits(convertedDecimal);
                UInt32 low32 = (UInt32)bits[0];
                UInt32 middle32 = (UInt32)bits[1];
                UInt32 high32 = (UInt32)bits[2];
                Int32 scaleAndSign = bits[3];
                Boolean negative = scaleAndSign < 0;
                Int32 scaleFactor = (Int32)(Math.Abs(scaleAndSign) >> 16);

                Decimal scale = (Decimal)Math.Pow(10, (Double)scaleFactor);

                // now we construct the scaled long integer
                // if high32 is not zero then the overall number would be too large for the numeric range of long.
                // we determine how many significant bits of high32 need to be shifted down to the lower two ints
                // and correct the scale accordingly. 
                // this step will loose precision for two reason: bits are truncated in low32 and the scale value
                // might overflow its precision by too many divisions by two. This second reason is aggravated
                // significantly if the decimal is very much larger than 1. 
                while (high32 > 0)
                {
                    low32 >>= 1;
                    if ((middle32 & 1) > 0)
                        low32 |= 0x80000000;
                    middle32 >>= 1;
                    if ((high32 & 1) > 0)
                        middle32 |= 0x80000000;
                    high32 >>= 1;
                    scale /= 2;
                }
                UInt64 scaledInt = ((UInt64)middle32 << 32) + low32;

                // bad things would happen if the highest bit of scaledInt is not zero. 
                // Truncate the ulong integer to stay within the numeric range of long by
                // dividing it by two and correspondingly divide the scale by two as well.
                // Again this step can loose precision by overflowing the precision of scale.
                if ((scaledInt & 0x80000000) > 0)
                {
                    scaledInt >>= 1;
                    scale /= 2;
                }

                // the scale may be in the range 1- 10^28 which would throw us out of the valid
                // numeric range for the denominator:  10^18
                // Note: we could have done this step initially before computing scale and save us from
                // computing the realScaleFactor (which would be equal to scale if done initially). However
                // this would aggravate the precision problems of scale even further. 
                Int32 realScaleFactor = (Int32)Math.Log10((Double)scale);
                if (realScaleFactor > 18)
                {
                    UInt64 scaleCorrection = (UInt64)Math.Pow(10, (Double)realScaleFactor - 18);
                    scaledInt /= scaleCorrection;
                    scale /= scaleCorrection;
                }

                return new Fraction((long)scaledInt * (negative ? -1 : 1), (long)scale);
            }
        }

        #endregion

        #region 128Bit Comparison
        // this is a simple schoolbook algorithm for the comparison of the product
        // of two 64bit integers
        // replace its use in CompareTo as soon as you have better 128bit precision arithmetic at hand.

        /// <summary>
        /// Multiplication the schoolbook way, based on 32-bit steps.
        /// This can be certainly optimized but it is ok for the moment.
        /// </summary>
        /// <param name="AB"></param>
        /// <param name="CD"></param>
        /// <returns></returns>
        [method: CLSCompliant(false)]
        internal UInt32[] UMult128(UInt64 ab, UInt64 cd)
        {
            UInt64 a = ab >> 32;
            UInt64 b = ab & 0xFFFFFFFF;
            UInt64 c = cd >> 32;
            UInt64 d = cd & 0xFFFFFFFF;
            UInt32[] result = { 0, 0, 0, 0 };
            UInt64 tmp;

            UInt64 step1 = b * d;
            UInt64 step2 = a * d;
            UInt64 step3 = b * c;
            UInt64 step4 = a * c;

            result[3] = (UInt32)step1;
            tmp = (step1 >> 32) + (step2 & 0xFFFFFFFF) + (step3 & 0xFFFFFFFF);
            result[2] = (UInt32)tmp;
            tmp >>= 32;
            tmp += (step2 >> 32) + (step3 >> 32) + (step4 & 0xFFFFFFFF);
            result[1] = (UInt32)tmp;
            result[0] = (UInt32)(tmp >> 32) + (UInt32)(step4 >> 32);
            return result;
        }

        /// <summary>
        /// Simple &lt; comparison of the 128bit multiplication result
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        [method: CLSCompliant(false)]
        internal Boolean Less128(UInt32[] first, UInt32[] second)
        {
            for (int i = 0; i < 4; i++)
            {
                if (first[i] < second[i])
                    return true;
                if (first[i] > second[i])
                    return false;
            }
            return false;
        }

        #endregion

        #region Conversions

        public static explicit operator Decimal(Fraction fraction)
        {
            return (Decimal)fraction.numerator / (Decimal)fraction.denominator;
        }

        public static explicit operator Double(Fraction fraction)
        {
            return (Double)fraction.numerator / (Double)fraction.denominator;
        }

        public static explicit operator long(Fraction fraction)
        {
            return (long)fraction.numerator / (long)fraction.denominator;
        }

        [method: CLSCompliant(false)]
        public static explicit operator UInt64(Fraction fraction)
        {
            return (UInt64)(long)fraction;
        }

        public static explicit operator Int32(Fraction fraction)
        {
            return (Int32)(long)fraction;
        }

        [method: CLSCompliant(false)]
        public static explicit operator UInt32(Fraction fraction)
        {
            return (UInt32)(long)fraction;
        }

        public static explicit operator Int16(Fraction fraction)
        {
            return (Int16)(long)fraction;
        }

        [method: CLSCompliant(false)]
        public static explicit operator UInt16(Fraction fraction)
        {
            return (UInt16)(long)fraction;
        }

        public static explicit operator Byte(Fraction fraction)
        {
            return (Byte)(long)fraction;
        }

        public static explicit operator Single(Fraction fraction)
        {
            return (Single)(long)fraction;
        }

        public static implicit operator Fraction(long number)
        {
            return new Fraction(number, 1);
        }

        /// <summary>
        /// This is a potentially lossy conversion since the decimal may use more than the available
        /// 64 bits for the nominator. In this case the least significant bits of the decimal are 
        /// truncated.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Fraction(Decimal number)
        {
            return Fraction.ToFraction(number);
        }

        #endregion

        #region IConvertible Members

        [method: CLSCompliant(false)]
        public ulong ToUInt64(IFormatProvider provider)
        {
            return (UInt64)this;
        }

        [method: CLSCompliant(false)]
        public sbyte ToSByte(IFormatProvider provider)
        {
            return (SByte)this;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (Double)this;
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException("Cannot convert fraction value to DateTime");
        }

        public float ToSingle(IFormatProvider provider)
        {
            return (Single)this;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException("Cannot convert fraction value to Boolean");
        }

        public Int32 ToInt32(IFormatProvider provider)
        {
            return (Int32)this;
        }

        [method: CLSCompliant(false)]
        public ushort ToUInt16(IFormatProvider provider)
        {
            return (UInt16)this;
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (Int16)this;
        }

        public string ToString(IFormatProvider provider)
        {
            throw new InvalidCastException("Cannot convert fraction value to String");
        }

        public byte ToByte(IFormatProvider provider)
        {
            return (Byte)this;
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException("Cannot convert fraction value to Char");
        }

        public long ToInt64(IFormatProvider provider)
        {
            return (long)this;
        }

        public System.TypeCode GetTypeCode()
        {
            return TypeCode.Decimal;
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return (Decimal)this;
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (this.denominator == 1)
                return Convert.ChangeType((long)this, conversionType, provider);
            else
                return Convert.ChangeType((Decimal)this, conversionType, provider);
        }

        [method: CLSCompliant(false)]
        public UInt32 ToUInt32(IFormatProvider provider)
        {
            return (UInt32)this;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// Note: the comparison is turned (by algebraic transformation) into a comparison
        /// of the cross-product of the fractions. This might overflow the available numbers,
        /// in which case we use home-made 128bit multiplication. This is slow! 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Int32 CompareTo(object obj)
        {
            Fraction arg = (Fraction)obj;

            if ((this.numerator == arg.numerator) && (this.denominator == arg.denominator))
                return 0;

            Boolean thisNegative = this.numerator < 0;
            Boolean argNegative = arg.numerator < 0;
            if (thisNegative != argNegative)
            {
                if (argNegative)
                    return 1;
                else
                    return -1;
            }

            try
            {
                checked
                {
                    if (this.numerator * arg.denominator < this.denominator * arg.numerator)
                        return -1;
                    else
                        return 1;
                }
            }
            catch (OverflowException)
            {
                // we need to resort to 128bit precision multiplication here
                UInt32[] product1 = this.UMult128((ulong)Math.Abs(this.numerator), (ulong)Math.Abs(arg.denominator));
                UInt32[] product2 = this.UMult128((ulong)Math.Abs(this.denominator), (ulong)Math.Abs(arg.numerator));
                Boolean less = this.Less128(product1, product2);
                if (thisNegative)
                    less = !less;

                return less ? -1 : 1;
            }
        }

        #endregion

        #region IFormattable Members

        string System.IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            return this.numerator.ToString(format, formatProvider) + "/" + this.denominator.ToString(format, formatProvider);
        }

        #endregion

        #region Operators

        public static Boolean operator <(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.CompareTo(fraction2) < 0;
        }

        public static Boolean operator >(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.CompareTo(fraction2) > 0;
        }

        public static Boolean operator ==(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.CompareTo(fraction2) == 0;
        }

        public static Boolean operator !=(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.CompareTo(fraction2) != 0;
        }

        public override Boolean Equals(object fraction1)
        {
            return this.CompareTo((Fraction)fraction1) == 0;
        }

        public override Int32 GetHashCode()
        {
            return (Int32)this;
        }

        public static Boolean operator <=(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.CompareTo(fraction2) <= 0;
        }

        public static Boolean operator >=(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.CompareTo(fraction2) >= 0;
        }

        // **********************************************************************************

        public static Fraction operator +(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(
                checked((fraction1.numerator * fraction2.denominator) + (fraction2.numerator * fraction1.denominator)),
                checked(fraction1.denominator * fraction2.denominator));
        }

        public static Fraction operator -(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(
                checked((fraction1.numerator * fraction2.denominator) - (fraction2.numerator * fraction1.denominator)),
                checked(fraction1.denominator * fraction2.denominator));
        }

        public static Fraction operator /(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(
                checked(fraction1.numerator * fraction2.denominator),
                checked(fraction1.denominator * fraction2.numerator));
        }

        public static Fraction operator *(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(
                checked(fraction1.numerator * fraction2.numerator),
                checked(fraction1.denominator * fraction2.denominator));
        }

        public static Fraction operator %(Fraction fraction1, Fraction fraction2)
        {
            long quo = (long)checked((fraction1 / fraction2));
            return fraction1 - new Fraction(
                checked(fraction2.numerator * quo),
                checked(fraction2.denominator));
        }

        #endregion

        #region Alternative Operations

        public static Fraction Add(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 + fraction2;
        }

        public static Fraction Subtract(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 - fraction2;
        }

        public static Fraction Multiply(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 * fraction2;
        }

        public static Fraction Divide(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 / fraction2;
        }

        public static Fraction Modulus(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 % fraction2;
        }

        #endregion

        public override string ToString()
        {
            return numerator.ToString(CultureInfo.CurrentCulture) + "/" + denominator.ToString(CultureInfo.CurrentCulture);
        }
    }
}

/*
using System;
using NUnit.Framework;

namespace FractionalNumbers
{
	[TestFixture]
	public class FractionTest
	{
		[Test]
		public void Conversions()
		{
			Fraction fraction = new Fraction(18, 12);
			Assert.AreEqual(3, fraction.Numerator);
			Assert.AreEqual(2, fraction.Denominator);
			Assert.AreEqual(new Fraction(17, 1), (Fraction)17);

			Assert.AreEqual((Decimal)3/2, (Decimal)fraction);
			Assert.AreEqual((Double)3/2, (Double)fraction);
			Assert.AreEqual(3/2, (Int64)fraction);

			Assert.AreEqual("3/2", fraction.ToString());

			Assert.AreEqual(fraction, (Fraction)(3/2m));
			
			// The following tests are commented out because of a bug in C# refactory
			// Uncomment if you need to run the precision tests.
//			// the decimal uses a scale of 10^19, which is truncated, with a corresponding loss in precision
//			// Note that the result has a precision of 18 digits, which is the inner limit for the fraction nominator.
//			Assert.AreEqual(0.3333333333333333332m, (Decimal)((Fraction)(0.3333333333333333333m)));
//
//			// tests the overflow of the scaled integer part of the decimal: high32 is 1 and needs to be coerced.
//			// Note that the precisions stays with 18 digits.
//			Assert.AreEqual(0.3333333333333333332m, (Decimal)((Fraction)(0.33333333333333333333m)));
//
//			// Aggravate the above test by adding three further digits.
//			Assert.AreEqual(0.3333333333333333331968m, (Decimal)((Fraction)(0.33333333333333333333333m)));
//
//			// Now to make a point, we are going to add three more digits and the precision is getting worse!
//			Assert.AreEqual(0.3333333333333333331935232m, (Decimal)((Fraction)(0.33333333333333333333333333m)));
//
//			// this is as worse as it can get: 
//			Assert.AreEqual(0.3333333333333333331543763627m, (Decimal)((Fraction)(1m/3m)));
//
//			// note the loss in precision if the number get larger than 1
//			Assert.AreEqual(33.333333333333333462182352213m, (Decimal)((Fraction)(100m/3m)));
//			Assert.AreEqual(333.3333333333333460750696448m, (Decimal)((Fraction)(1000m/3m)));
//			Assert.AreEqual(3333.3333333333337470818495147m, (Decimal)((Fraction)(10000m/3m)));
//			Assert.AreEqual(33333333.333457834256172346828m, (Decimal)((Fraction)(100000000m/3m)));
//
//			// but only if we need to scale down because the number of digits is 18 or more
//			Assert.AreEqual(33333333.333333333m, (Decimal)((Fraction)(33333333.333333333m)));
//			Assert.AreEqual(333333333333333.33m, (Decimal)((Fraction)(333333333333333.33m)));
		}

		[Test]
		public void Comparisons()
		{
			Fraction larger = new Fraction(18, 12);
			Fraction smaller = new Fraction(4, 3);

			Assert.IsTrue(smaller < larger);
			Assert.IsTrue(smaller <= larger);
			Assert.IsTrue(larger > smaller);
			Assert.IsTrue(larger >= smaller);
			Assert.IsTrue(smaller == smaller);
			Assert.IsFalse(smaller == larger);
			Assert.IsTrue(smaller != larger);
			Assert.IsFalse(smaller != smaller);
			Assert.IsFalse(larger < smaller);
			Assert.IsFalse(larger <= smaller);
			Assert.IsFalse(smaller > larger);
			Assert.IsFalse(smaller >= larger); 
		}

		/// <summary>
		/// This test checks if comparison is possible at the edges of the number range.
		/// </summary>
		[Test]
		public void LargeComparisons()
		{
			Fraction veryLarge = Fraction.MaxValue;
			Fraction verySmall = Fraction.SmallestValue;
			Fraction verySmallNeg = Fraction.SmallestNegValue;
			Fraction veryNegative = Fraction.MinValue;

			Assert.IsTrue(verySmall < veryLarge);
			Assert.IsTrue(verySmallNeg < veryLarge);
			Assert.IsTrue(veryNegative < veryLarge);
			Assert.IsTrue(verySmallNeg < verySmall);
			Assert.IsTrue(veryNegative < verySmall);
			Assert.IsTrue(veryNegative < verySmallNeg);

			Assert.IsFalse(verySmall > veryLarge);
			Assert.IsFalse(verySmallNeg > veryLarge);
			Assert.IsFalse(veryNegative > veryLarge);
			Assert.IsFalse(verySmallNeg > verySmall);
			Assert.IsFalse(veryNegative > verySmall);
			Assert.IsFalse(veryNegative > verySmallNeg);
			
			Assert.IsTrue(verySmall != veryLarge);
			Assert.IsTrue(verySmall != verySmallNeg);
			Assert.IsTrue(verySmall != veryNegative);
			Assert.IsTrue(veryNegative != veryLarge);
			Assert.IsTrue(veryNegative != verySmallNeg);
			Assert.IsTrue(veryLarge != verySmallNeg);
	
			Assert.IsFalse(verySmall == veryLarge);
			Assert.IsFalse(verySmall == verySmallNeg);
			Assert.IsFalse(verySmall == veryNegative);
			Assert.IsFalse(veryNegative == veryLarge);
			Assert.IsFalse(veryNegative == verySmallNeg);
			Assert.IsFalse(veryLarge == verySmallNeg);

			Assert.IsTrue(veryLarge-1 < veryLarge);
			Assert.IsTrue(veryNegative+1 < verySmall);
			Assert.IsTrue(veryNegative+1 < veryLarge);
			Assert.IsTrue(veryNegative < veryNegative+1);
			Assert.IsTrue(verySmall-1 < 0);
			Assert.IsTrue(0 < verySmall);

			Assert.IsTrue(veryLarge == veryLarge);
			Assert.IsTrue(verySmall == verySmall);
			Assert.IsTrue(verySmallNeg == verySmallNeg);
			Assert.IsTrue(veryNegative == veryNegative);

		}

		/// <summary>
		/// Check that both nominator and denominator must stay in the bounds of Int64.MaxValue.
		/// This is a Math.Abs issue.
		/// </summary>
		[Test]
		[ExpectedException(typeof(OverflowException))]
		public void FailedConstructor1()
		{
			Fraction willFail = new Fraction(1, Int64.MinValue);
		}

		[Test]
		[ExpectedException(typeof(OverflowException))]
		public void FailedConstructor2()
		{
			Fraction willFail = new Fraction(Int64.MinValue,1 );
		}

		[Test]
		public void Arithmetic()
		{
			Fraction fraction1 = new Fraction(3, 2);
			Fraction fraction2 = new Fraction(4, 3);
			Fraction fraction3 = new Fraction(5, -2);
			
			Assert.AreEqual(new Fraction(17, 6), fraction1 + fraction2);
			Assert.AreEqual(new Fraction(1, 6), fraction1 - fraction2);
			Assert.AreEqual(new Fraction(2, 1), fraction1 * fraction2);
			Assert.AreEqual(new Fraction(9, 8), fraction1 / fraction2);
			Assert.AreEqual(new Fraction(1, 6), fraction1 % fraction2);

			Assert.AreEqual(new Fraction(-1, 1), fraction1 + fraction3);
			Assert.AreEqual(new Fraction(4, 1), fraction1 - fraction3);
			Assert.AreEqual(new Fraction(-15, 4), fraction1 * fraction3);
			Assert.AreEqual(new Fraction(-3, 5), fraction1 / fraction3);
			Assert.AreEqual(new Fraction(3, 2), fraction1 % fraction3);

			Assert.AreEqual(-1, (int)(fraction1 + fraction3));

		}

		[Test]
		[ExpectedException(typeof(System.OverflowException))]
		public void OverflowPlus()
		{
			Fraction fraction1 = Fraction.MaxValue;
			Fraction fraction2 = new Fraction(4, 3);

			Assert.AreEqual(0, fraction1 + fraction2);
		}

		[Test]
		[ExpectedException(typeof(System.OverflowException))]
		public void OverflowMinus()
		{
			Fraction fraction1 = Fraction.MinValue;
			Fraction fraction2 = new Fraction(4, 3);

			Assert.AreEqual(0, fraction1 - fraction2);
		}

		[Test]
		[ExpectedException(typeof(System.OverflowException))]
		public void OverflowMult()
		{
			Fraction fraction1 = Fraction.MaxValue;
			Fraction fraction2 = new Fraction(4, 3);

			Assert.AreEqual(0, fraction1 * fraction2);
		}

		[Test]
		[ExpectedException(typeof(System.OverflowException))]
		public void OverflowDiv()
		{
			Fraction fraction1 = Int64.MaxValue;
			Fraction fraction2 = new Fraction(4, 3);

			Assert.AreEqual(0, fraction1 / fraction2);
		}

		[Test]
		[ExpectedException(typeof(System.OverflowException))]
		public void OverflowRem()
		{
			Fraction fraction1 = Fraction.MaxValue;
			Fraction fraction2 = new Fraction(4, 3);

			Assert.AreEqual(0, fraction1 % fraction2);
		}

		/// <summary>
		/// A quick test that the long long multiplication stays within the numeric limits
		/// </summary>
		[Test]
		public void Mult128()
		{
			Fraction obj = Fraction.Zero;
			UInt32[] needed = {1073741823, 4294967295, 0, 1};

			Assert.AreEqual(needed, obj.UMult128(Int64.MaxValue, Int64.MaxValue));
		}
	}
}
*/