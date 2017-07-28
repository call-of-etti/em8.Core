using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoE.em8.Core.UnitTesting
{
    public static class MessageBuilder
    {
        const string NULL = "*null*";
        const string SHOULD = "👉 ";
        const string IS = "👎🏻 ";
        const string NOT = "!";

        public static string ShouldButIs(string name, string expected, string actual)
        {
            return "\n`" + name + "`\n" + SHOULD + IfNull(expected) + "\n" + IS + IfNull(actual);
        }

        public static string ShouldNotButIs(string name, string notExpected, string actual)
        {
            return "\n`" + name + "`\n" + SHOULD + NOT + IfNull(notExpected) + "\n" + IS + IfNull(actual);
        }

        public static string NotNull(string name, string actual)
        {
            return ShouldNotButIs(name, NULL, actual);
        }

        public static string True(string name, bool actual)
        {
            return ShouldButIs(name, true.ToString(), actual.ToString());
        }

        public static string IfNull(string input, string replace = NULL)
        {
            return input ?? replace;
        }
    }
}
