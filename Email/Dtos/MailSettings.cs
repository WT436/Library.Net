using System;
using System.Collections.Generic;
using System.Text;

namespace Email.Dtos
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}