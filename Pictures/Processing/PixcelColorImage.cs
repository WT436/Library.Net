using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pictures.Processing
{
    public class PixcelColorImage
    {
        public Bitmap ChangeColor(Image image, int X, int Y, Color color)
        {
            Bitmap scrBitmap = new Bitmap(image);
            Bitmap newBitmap = new Bitmap(scrBitmap);
            newBitmap.SetPixel(X, Y, color);
            return newBitmap;
        }
        public Bitmap ChangeColor(Bitmap scrBitmap, int X, int Y, Color color)
        {
            Bitmap newBitmap = new Bitmap(scrBitmap);
            newBitmap.SetPixel(X, Y, color);
            return newBitmap;
        }
        public string GetColorAt(Image image, int x, int y)
        {
            Bitmap bmp = new Bitmap(image);
            Rectangle bounds = new Rectangle(x, y, image.Width, image.Height);
            
           var tv =   bmp.GetPixel(x, y);
            return "rgb("+tv.R + "," + tv.G + "," + tv.B+")";
        }
    }
}
