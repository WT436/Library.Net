using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectExtention.ObjectManager
{
    public static class XmlProcess
    {
        public static string ForceXmlString(string inputStr)
        {
            return inputStr
                .Replace("&", "&#38;")
                .Replace("<", "&#60;")
                .Replace(">", "&#62;")
                .Replace("'", "&#39;")
                .Replace("\"", "&#34;")
                .Replace("\r\n", "<w:br/>");
        }
    }
}
