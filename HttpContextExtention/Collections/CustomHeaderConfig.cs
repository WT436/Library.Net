using System;
using System.Collections.Generic;
using System.Text;

namespace HttpContextExtention.Collections
{
    public class CustomHeaderConfig
    {
        public IEnumerable<IDictionary<string, string>> CustomHeader { get; set; }
        public Headers Headers { get; set; }
        public string Host { get; set; }
    }
}
