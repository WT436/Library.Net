using Pictures.Processing.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace Pictures.Processing
{
    public class FlipImage
    {
        public enum FlipMode
        {
            NoneFlip = 0,
            Flip180 = 1,
            Flip90 = 2,
            Flip270 = 3,
            NoneFlipX = 4,
            FlipY270 = 5,
            NoneFlipY = 6,
            FlipX270 = 7
        }

        public Image FlipImageV(Image myImage, FlipMode myFlipmode)
        {
            var ret = myImage;
            switch (myFlipmode)
            {
                case FlipMode.NoneFlip:
                    ret.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                case FlipMode.Flip90:
                    ret.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case FlipMode.Flip180:
                    ret.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case FlipMode.Flip270:
                    ret.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case FlipMode.NoneFlipX:
                    ret.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case FlipMode.FlipY270:
                    ret.RotateFlip(RotateFlipType.Rotate270FlipY);
                    break;
                case FlipMode.NoneFlipY:
                    ret.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                case FlipMode.FlipX270:
                    ret.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
            }
            return ret;
        }
    }
}
