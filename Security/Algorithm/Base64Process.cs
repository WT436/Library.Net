using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Algorithm
{
    public static class Base64Process
    {
        public static byte[] ToByteArray(this string value) =>
              Convert.FromBase64String(value);
    }
}
