using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core
{
    public static class StringExtensions
    {
        public static List<int> IndexesOf(this string haystack, char needle)
        {
            List<int> output = new List<int>();

            for (int i = 0, max = haystack.Length; i < max; i++)
                if (needle == haystack[i])
                    output.Add(i);

            return output;
        }
    }
}
