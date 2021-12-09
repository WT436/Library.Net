using Pictures.Processing.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Pictures.Processing
{
    public class RotateImage
    {
        public string Rotage(RotateOrFlipImageDto rotateImageDto)
        {
            Matrix rotate_at_origin = new Matrix();
            rotate_at_origin.Rotate(rotateImageDto.RotationAngle);
            PointF[] points =
            {
                new PointF(0, 0),
                new PointF(rotateImageDto.image.Width, 0),
                new PointF(rotateImageDto.image.Width, rotateImageDto.image.Height),
                new PointF(0, rotateImageDto.image.Height),
            };
            rotate_at_origin.TransformPoints(points);
            float xmin, xmax, ymin, ymax;
            GetPointBounds(points, out xmin, out xmax,
                out ymin, out ymax);
            int wid = (int)Math.Round(xmax - xmin);
            int hgt = (int)Math.Round(ymax - ymin);
            Bitmap result = new Bitmap(wid, hgt);
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(rotateImageDto.RotationAngle,
                new PointF(wid / 2f, hgt / 2f));
            using (Graphics gr = Graphics.FromImage(result))
            {
                gr.InterpolationMode = InterpolationMode.High;
                gr.Clear(Color.White);
                gr.Transform = rotate_at_center;
                int x = (wid - rotateImageDto.image.Width) / 2;
                int y = (hgt - rotateImageDto.image.Height) / 2;
                gr.DrawImage(rotateImageDto.image, x, y);
            }
            return SaveImage.ReturnUrl(result, rotateImageDto.NameReturn,
                                        rotateImageDto.image.Width, rotateImageDto.image.Height,
                                        "Rotage");
        }
        private void GetPointBounds(PointF[] points,
                        out float xmin, out float xmax,
                        out float ymin, out float ymax)
        {
            xmin = points[0].X;
            xmax = xmin;
            ymin = points[0].Y;
            ymax = ymin;
            foreach (PointF point in points)
            {
                if (xmin > point.X) xmin = point.X;
                if (xmax < point.X) xmax = point.X;
                if (ymin > point.Y) ymin = point.Y;
                if (ymax < point.Y) ymax = point.Y;
            }
        }
    }
}
