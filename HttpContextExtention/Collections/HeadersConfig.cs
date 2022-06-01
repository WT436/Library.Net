using System;
using System.Collections.Generic;
using System.Text;

namespace HttpContextExtention.Collections
{
    public class Headers
    {
        public IEnumerable<HeadersConfig> HeadersConfigs { get; set; }
        public IEnumerable<TokenConfig> TokenConfigs { get; set; }
    }

    public class HeadersConfig
    {
        public string KeyHeaders { get; set; }
        public string ValuesHeaders { get; set; }
    }

    public class TokenConfig
    {
        public string KeyToken { get; set; }
        public string ValuesToken { get; set; }
    }
}
