using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core
{
    public static class TextFormatter
    {
        public static string NumberToStringNDigits(int number, int n)
        {
            return number.ToString("D" + n);
        }
    }
}
