using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Dtos
{
    public class ExternalClientJsonConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string RsaPrivateKey { get; set; }
        public string RsaPublicKey { get; set; }
    }
}
