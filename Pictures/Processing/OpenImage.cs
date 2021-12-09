using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pictures.Processing
{
    public class OpenImage
    {
        public Image Open(string url)
        {
            return  Image.FromFile(url);
        }
    }
}
