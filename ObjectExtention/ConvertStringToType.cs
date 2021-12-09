using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ObjectExtention
{
    public class ConvertStringToType
    {
        public T FromStringToType<T>(string mystring)
        {
            next:
            try
            {
                var foo = TypeDescriptor.GetConverter(typeof(T));
                return (T)(foo.ConvertFromInvariantString(mystring));
            }
            catch (Exception)
            {
                mystring = "0";
                goto next;
                throw new Exception("Input Không hợp lệ");
            }
        }

        public string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
