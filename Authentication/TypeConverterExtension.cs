using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication
{
    public static class TypeConverterExtension
    {
        public static byte[] ToByteArray(this string value) =>
         Convert.FromBase64String(value);
    }
}
