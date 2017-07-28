using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CoE.em8.Core.Numeric
{
    public static class Numeric
    {
        #region Math
        public static T Add<T>(dynamic a, dynamic b)
        {
            return (T)(a + b);
        }

        public static T Subtract<T>(dynamic a, dynamic b)
        {
            return (T)(a - b);
        }

        public static T Multiply<T>(dynamic a, dynamic b)
        {
            return (T)(a * b);
        }
        public static T Divide<T>(dynamic a, dynamic b)
        {
            return (T)(a / b);
        }
        public static T Modulo<T>(dynamic a, dynamic b)
        {
            return (T)(a % b);
        }
        /// <summary>
        /// <<
        /// </summary>
        public static T ShiftLeft<T>(dynamic a, dynamic b)
        {
            return (T)(a << (int)b);
        }
        /// <summary>
        /// >>
        /// </summary>
        public static T ShiftRight<T>(dynamic a, dynamic b)
        {
            return (T)(a >> (int)b);
        }
        public static T And<T>(dynamic a, dynamic b)
        {
            return (T)(a & b);
        }
        public static T Or<T>(dynamic a, dynamic b)
        {
            return (T)(a | b);
        }
        public static T Xor<T>(dynamic a, dynamic b)
        {
            return (T)(a ^ b);
        }
        public static T Not<T>(dynamic a)
        {
            return (T)(~a);
        }
        #endregion


        #region Parsing

        // Default value like in the .NET Framework
        internal static readonly NumberFormatInfo defaultNumberFormatInfo = NumberFormatInfo.CurrentInfo;

        public static T Parse<T>(string input, NumberStyles style, IFormatProvider provider)
        {
            dynamic statix = StaticMembersDynamicWrapper.Proxy(typeof(T));
            return statix.Parse(input, style, provider);
        }

        public static T Parse<T>(string input, NumberStyles style)
            => Parse<T>(input, style, defaultNumberFormatInfo);

        public static T Parse<T>(string input, IFormatProvider provider)
        {
            // NumberStyle differs for each type, therefore this relys on the .NET implementation
            dynamic statix = StaticMembersDynamicWrapper.Proxy(typeof(T));
            return statix.Parse(input, provider);
        }

        public static T Parse<T>(string input)
            => Parse<T>(input, defaultNumberFormatInfo);

        public static bool TryParse<T>(string input, NumberStyles style, IFormatProvider provider, out T result)
        {
            try
            {
                result = Parse<T>(input, style, provider);
                return true;
            }
            catch (Exception e) when
                (e is ArgumentNullException || e is FormatException || e is OverflowException)
            {
                result = default(T);
                return false;
            }
        }

        public static bool TryParse<T>(string input, NumberStyles style, out T result)
            => TryParse<T>(input, style, out result);

        public static bool TryParse<T>(string input, IFormatProvider provider, out T result)
        {
            // NumberStyle differs for each type, therefore this relys on the .NET implementation
            try
            {
                result = Parse<T>(input, provider);
                return true;
            }
            catch (Exception e) when
                (e is ArgumentNullException || e is FormatException || e is OverflowException)
            {
                result = default(T);
                return false;
            }
        }

        public static bool TryParse<T>(string input, out T result)
            => TryParse<T>(input, defaultNumberFormatInfo, out result);

        #endregion


        #region Conversion
        public static T Convert<T>(IConvertible input)
            => (T)System.Convert.ChangeType(input, typeof(T));

        public static byte[] ByteSafeBitConverterGetBytes<T>(T input)
            => (input is byte)
                ? new byte[] { (dynamic)input }
                : BitConverter.GetBytes((dynamic)input);

        public static T Cast<T>(dynamic inp)
        {
            return (T)inp;
        }

        #endregion



        /// <summary>
        /// Throws an Exception, when T is not an numeric Integer Type (byte, short, ushort, int, ...)
        /// </summary>
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static void TestInteger<T>()
        {
            try
            {
                T test = (T)(dynamic)18;
                test = (T)(test + (dynamic)5);
                test = (T)(test - (dynamic)3);
                test = (T)(test * (dynamic)2);
                test = (T)(test / (dynamic)4);
                test = (T)(test % (dynamic)100);

                test = (T)(test << (dynamic)1);
                test = (T)(test >> (dynamic)1);

                test = (T)(test & (dynamic)test);
                test = (T)(test | (dynamic)test);
                test = (T)(~(dynamic)test);
                test = (T)(test ^ (dynamic)2);


                bool b = test < (dynamic)10;
                b = test > (dynamic)5;
                b = test == (dynamic)7;
            }
            catch (Exception ex)
            {
                throw new NotIntegerException(typeof(T).FullName + " is not an Integer Type", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static void TestSignedInteger<T>()
        {
            TestInteger<T>();

            T test = (T)(dynamic)(-1);
            if (test > (dynamic)0)
            {
                throw new NotSignedIntegerException(typeof(T).FullName + " is not an signed Integer Type");
            }
        }



    }
}
