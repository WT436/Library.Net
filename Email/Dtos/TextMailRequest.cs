using System;
using System.Collections.Generic;
using System.Text;

namespace Email.Dtos
{
    public class TextMailRequest
    {
        public string ToEmail { get; set; }
        public string Title { get; set; }
        public string TextString { get; set; }
    }
}