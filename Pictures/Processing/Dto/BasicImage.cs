using Pictures.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pictures.Processing.Dto
{
    public class BasicImage
    {
        public Image image { get; set; }
        public string UrlReturn { get; set; }
        public string NameReturn { get; set; }
        public ConfigImaging Quality { get; set; } = ConfigImaging.Low;
    }
}
