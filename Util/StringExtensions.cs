using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Util
{
    public static class StringExtensions
    {
        public static string Reverse(this String str)
        {
            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}
