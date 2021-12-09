using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace Pictures.Processing
{
    public class ReflectionImage
    {
        public Image DrawReflection(Image img, Color toBG)
        {
            int height = img.Height + 100;
            Bitmap bmp = new Bitmap(img.Width, height, PixelFormat.Format64bppPArgb);
            Brush brsh = new LinearGradientBrush(new Rectangle(0, 0, img.Width + 10,
                          height), Color.Transparent, toBG, LinearGradientMode.Vertical);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution); 
                                                                                 
            using (Graphics grfx = Graphics.FromImage(bmp)){
                Bitmap bm = (Bitmap)img;
                grfx.DrawImage(bm, 0, 0, img.Width, img.Height);
                Bitmap bm1 = (Bitmap)img;
                bm1.RotateFlip(RotateFlipType.Rotate180FlipX);
                grfx.DrawImage(bm1, 0, img.Height);
                Rectangle rt = new Rectangle(0, img.Height, img.Width, 100);
                grfx.FillRectangle(brsh, rt);
            }
            return bmp;
        }

        public Image DrawReflection(Image img, int Location)
        {
            Bitmap image1 = new Bitmap(img);
            Bitmap image2 = (Bitmap)image1.Clone();
            Bitmap bitmap;
            switch (Location)
            {
                case 1 :
                    image2.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    bitmap = new Bitmap(Math.Max(image1.Width, image2.Width), image1.Height+ image2.Height);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(image1, 0, 0);
                        g.DrawImage(image2, 0, image2.Height);
                    }
                    break;
                case 2:
                    image2.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    bitmap = new Bitmap(Math.Max(image1.Width, image2.Width), image1.Height + image2.Height);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(image2, 0, 0);
                        g.DrawImage(image1, 0, image2.Height);
                    }
                    break;
                case 3:
                    image2.RotateFlip(RotateFlipType.Rotate180FlipY);
                    bitmap = new Bitmap(image1.Width + image2.Width, Math.Max(image1.Height, image2.Height));
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(image2, 0, 0);
                        g.DrawImage(image1, image2.Width, 0);
                    }
                    break;
                default:
                    image2.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    bitmap = new Bitmap(image1.Width + image2.Width, Math.Max(image1.Height, image2.Height));
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(image1, 0, 0);
                        g.DrawImage(image2, image1.Width, 0);
                    }
                    break;
            }
            
            return bitmap;
        }
    }
}
