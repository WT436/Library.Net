using Pictures.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pictures.Processing
{
    public class MergeX2Image
    {
        public Bitmap MergeTwoImages(Image firstImage, Image secondImage, int LocaX, int LocaY)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = firstImage.Width > secondImage.Width ? firstImage.Width : secondImage.Width;

            int outputImageHeight = firstImage.Height > secondImage.Height ? firstImage.Height : secondImage.Height;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                ConfigGraphics.quatityImaging(graphics,ConfigImaging.High);
                graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
                    new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new Rectangle(new Point(LocaX, LocaY), secondImage.Size),
                    new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
            }

            return outputImage;
        }
    }
}
