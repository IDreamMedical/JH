//  操作符重载都没有 TODO
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionLibrary.Mathematics
{
    /// <summary>
    /// 复数类
    /// </summary>
    public class Complex
    {
        #region Fields

        private double imag;

        private double real;

        #endregion

        #region Properties

        public double Imag
        {
            get { return imag; }
            set { imag = value; }
        }

        public double Real
        {
            get { return real; }
            set { real = value; }
        }

        #endregion

        #region Constructors

        public Complex()
        {
            imag = 0.0;
            real = 0.0;
        }

        public Complex(double imag, double real)
        {
            this.imag = imag;
            this.real = real;
        }

        #endregion

        #region Methods

        public static Complex Add(Complex c1, Complex c2)
        {
            return new Complex(c1.imag + c2.imag, c1.real + c2.real);
        }

        public static Complex Substract(Complex c1, Complex c2)
        {
            return new Complex(c1.imag - c2.imag, c1.real - c2.real);
        }

        public static Complex Multiple(Complex c1, Complex c2)
        {
            throw new NotImplementedException();            
        }

        public static Complex Divide(Complex c1, Complex c2)
        {
            throw new NotImplementedException();
        }

        public static Complex Negative(Complex c)
        {
            return new Complex(-c.imag, -c.real);
        }

        public static Complex Sin(Complex c)
        {
            throw new NotImplementedException();
        }

        public static Complex Cos(Complex c)
        {
            throw new NotImplementedException();
        }

        public static Complex Tan(Complex c)
        {
            throw new NotImplementedException();
        }

        public static Complex Sinh(Complex c)
        {
            throw new NotImplementedException();
        }

        public static Complex Cosh(Complex c)
        {
            throw new NotImplementedException();
        }

        public static Complex Tanh(Complex c)
        {
            throw new NotImplementedException();
        }

        public static Complex Sqrt(Complex c)
        {
            throw new NotImplementedException();
        }

        public static Complex Exp(Complex c)
        {
            throw new NotImplementedException();            
        }

        public static Complex Pow(Complex x, Complex y)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override string ToString()
        {
            if (double.IsNaN(real) || double.IsNaN(imag))
            {
                return double.NaN.ToString();
            }

            if (double.IsNegativeInfinity(real) || double.IsNegativeInfinity(imag))
            {
                return double.NegativeInfinity.ToString();
            }

            if (double.IsPositiveInfinity(real) || double.IsPositiveInfinity(imag))
            {
                return double.NegativeInfinity.ToString();
            }

            if (imag == 0.0)
            {
                return real.ToString();
            }
            
            if (real == 0.0)
            {
                return imag == 0.0 ? "0" : string.Format("{0}i", imag);
            }

            return string.Format("{0} + {1}i", real, imag);
        }
    }
}
