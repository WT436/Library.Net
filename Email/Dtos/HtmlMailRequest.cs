using System;
using System.Collections.Generic;
using System.Text;

namespace Email.Dtos
{
   public class HtmlMailRequest
    {
        public string ToEmail { get; set; }
        public string Title { get; set; }
        public string HtmlPath { get; set; }
    }
}