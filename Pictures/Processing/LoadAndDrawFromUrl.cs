using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace Pictures.Processing
{
    public class LoadAndDrawFromUrl
    {
        public Image LoadImageWeb(string url)
        {
            WebRequest webreq = WebRequest.Create(url);
            WebResponse webres = webreq.GetResponse();
            Stream stream = webres.GetResponseStream();

            return Image.FromStream(stream);
        }
    }
}
