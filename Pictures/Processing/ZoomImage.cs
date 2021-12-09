using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pictures.Processing
{
    public class ZoomImage
    {
        protected void ImageZoom(Image im)
        {
            Graphics g1 = Graphics.FromImage(im);
            Graphics g2 = Graphics.FromImage(im);
            Rectangle rec;
            Rectangle recPart;

            if (true)
            {
              
                rec = new Rectangle(0, 0, 100,100);
                g1.DrawImage(im, rec);

                recPart = new Rectangle(im.Width / 4, im.Height / 4, im.Width / 2, im.Height / 2);
                if (true)
                    recPart = new Rectangle(0, 0, im.Width / 2, im.Height / 2);
                if (true)
                    recPart = new Rectangle(im.Width / 2, 0, im.Width / 2, im.Height / 2);
                if (true)
                    recPart = new Rectangle(0, im.Height / 2, im.Width / 2, im.Height / 2);
                if (true)
                    recPart = new Rectangle(im.Width / 2, im.Height / 2, im.Width / 2, im.Height / 2);
                g2.DrawImage(im, rec, recPart, GraphicsUnit.Pixel);
            }
            else
            {
                g1.Clear(Color.Black);g1.Dispose();
                g2.Clear(Color.Black);g2.Dispose();
            }
            
            g1.Dispose(); g2.Dispose();
        }
    }
}
