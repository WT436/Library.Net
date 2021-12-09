using System;
using System.Collections.Generic;
using System.Text;

namespace FilterAttributeCore.ExceptionFilter
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
