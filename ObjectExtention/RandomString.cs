using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectExtention
{
    public static class RandomString
    {
        public static string OnlyRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Range(1, length)
                   .Select(_ => chars[new Random().Next(chars.Length)])
                   .ToArray());
        }

        public static string RandomStringHaveNumber(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                       .Select(s => s[new Random().Next(s.Length)])
                       .ToArray());
        }
    }
}
