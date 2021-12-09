using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pictures.Processing.Dto
{
    public class ResizeImageDto : BasicImage
    {
        public bool ratio { get; set; }
        public List<listSizeImage> ListSizeImages { get; set; }
    }
}
