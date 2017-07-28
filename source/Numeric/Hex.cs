using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core.Numeric
{
    public static class Hex
    {
        public static T Parse<T>(string input)
        {
            

            T result = Numeric.Convert<T>(0);

            // Hex
            if (HasHexPrefix(input))
            {
                string preparedInput = StripHexPrefix(input, true);
                for (int i = 0, max = preparedInput.Length; i < max; i++)
                {
                    result = Numeric.Add<T>(result, Numeric.ShiftLeft<T>(Parse<T>(preparedInput[i]), Numeric.Convert<T>(4 * (preparedInput.Length - 1 - i))));
                }
            }
            // Decimal
            else
            {
                for (int i = 0, max = input.Length; i < max; i++)
                {
                    result = Numeric.Add<T>(Numeric.Multiply<T>(result, 10), ParseDecimal<T>(input[i]));
                }
            }

            return result;
        }

        public static T Parse<T>(char c)
            => (c < 48 || (c > 57 && c < 65) || c > 70)
                ? throw new ArgumentOutOfRangeException("c", "not a valid hex digit")
                : ((c < 58)
                    ? Numeric.Convert<T>(c - 48)
                    : Numeric.Convert<T>(c - 55));

        public static T ParseDecimal<T>(char c)
             => char.IsDigit(c) ?
                Numeric.Convert<T>(c - '0') :
                throw new ArgumentOutOfRangeException("c", "not a valid digit");
                



        public static bool TryParse<T>(string input, out T result)
        {
            try
            {
                result = Hex.Parse<T>(input);
                return true;
            }
            catch (ArgumentException)
            {
                result = default(T);
                return false;
            }
        }

        /// <param name="input">either dec or hex with '0x'-prefix</param>
        public static T ParseAny<T>(string input, IFormatProvider provider)
            => (HasHexPrefix(input))
                ? Parse<T>(StripHexPrefix(input))
                : Numeric.Parse<T>(input, provider);

        public static T ParseAny<T>(string input)
            => Hex.ParseAny<T>(input, Numeric.defaultNumberFormatInfo);

        public static bool TryParseAny<T>(string input, IFormatProvider provider, out T result)
            => (HasHexPrefix(input))
                ? Hex.TryParse<T>(StripHexPrefix(input), out result)
                : Numeric.TryParse<T>(input, provider, out result);

        public static bool TryParseAny<T>(string input, out T result)
            => TryParseAny<T>(input, Numeric.defaultNumberFormatInfo, out result);

        public static string StripHexPrefix(string input)
            => input.Substring(2);

        /// <param name="safeMode">Safe Mode: Checks if there is a 2-char prefix, also ensures upper-case</param>
        public static string StripHexPrefix(string input, bool safeMode)
            => (safeMode)
                ? ((HasHexPrefix(input))
                    ? StripHexPrefix(input).ToUpper()
                    : input.ToUpper())
                : StripHexPrefix(input);

        public static bool HasHexPrefix(string input)
            => ((input.Length > 1) && input[0] == '0' && (input[1] == 'x' || input[1] == 'X'));

        public static bool IsAnyNumeric<T>(string input)
            => Hex.TryParseAny<T>(input, out T temp);

        /// <summary>
        /// Is the input any (hex) int number?
        /// Hex prefix - safe
        /// </summary>
        public static bool IsAnyInteger(string input)
            => Hex.IsAnyUnsignedInteger
                (
                    (input.First() == '-')
                    ? input.Substring(1)
                    : input
                );

        /// <summary>
        /// Is the input any positive (hex) int number?
        /// Hex prefix - safe
        /// </summary>
        public static bool IsAnyUnsignedInteger(string unsignedInput)
            => Hex.IsAnyUnsignedIntegerNonHex
                (
                    (HasHexPrefix(unsignedInput))
                    ? StripHexPrefix(unsignedInput)
                    : unsignedInput
                );

        /// <summary>
        /// Is the input any positive int number?
        /// </summary>
        public static bool IsAnyUnsignedIntegerNonHex(string unsignedInputWithoutHexPrefix)
        {
            foreach (char c in unsignedInputWithoutHexPrefix)
                if (!(char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F')))
                    return false;

            return true;
        }


        public static string ToHexString<T>(T Hex)
        {
            byte[] bytes = Numeric.ByteSafeBitConverterGetBytes<T>(Hex);
            return BitConverter.ToString(bytes).Replace("-", "");
        }


    }
}
