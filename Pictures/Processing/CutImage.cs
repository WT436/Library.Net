using Pictures.Processing.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Pictures.Processing
{
    public class CutImage
    {
        /// <summary>
        /// Cắt theo tọa độ điểm truyền vào
        /// </summary>
        public void CroppedLocationImage(List<CutImageDtos> Data)
        {
            List<string> AllImageRetunName = new List<string>();
            foreach (var imageItem in Data)
            {
                if (imageItem.image == null) continue;
                List<PointF> point = new List<PointF>();
                int MaxWidth = 0, MaxHeight = 0, MinWidth = 100000000, MinHeight = 10000000;
                foreach (var lp in imageItem.listPoints)
                {
                    if (lp.X > imageItem.image.Width) lp.X = imageItem.image.Width;
                    if (lp.X < 0) lp.X = 0;
                    if (lp.Y > imageItem.image.Height) lp.Y = imageItem.image.Height;
                    if (lp.Y < 0) lp.Y = 0;
                    MaxWidth = Math.Max(lp.X, MaxWidth);
                    MaxHeight = Math.Max(lp.Y, MaxHeight);
                    MinWidth = Math.Min(lp.X, MinWidth);
                    MinHeight = Math.Min(lp.Y, MinHeight);
                    point.Add(new Point() { X = lp.X, Y = lp.Y });
                }
                var Width = MaxWidth - MinWidth;
                var Height = MaxHeight - MinHeight;
                if (false)
                {
                    Bitmap bmp1 = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                    bmp1.MakeTransparent();
                    using (Graphics G = Graphics.FromImage(bmp1))
                    {
                        ConfigGraphics.quatityImaging(G, imageItem.Quality);
                        Color newColor = Color.FromName("Black");
                        Brush aBrush = (Brush)Brushes.Black;
                        G.FillRectangle(new SolidBrush(newColor), 0, 0, Width, Height);
                        G.DrawImage(imageItem.image,
                            new Rectangle(0, 0, Width, Height),
                            MinWidth, MinHeight, Width, Height,
                            GraphicsUnit.Pixel);
                        AllImageRetunName.Add(SaveImage.ReturnUrl(bmp1, Path.GetFileName(imageItem.NameReturn),
                                                        imageItem.image.Height, imageItem.image.Height, "Cut"));
                    }
                }
                else
                {
                    Bitmap bmp1 = new Bitmap(imageItem.image.Width, imageItem.image.Height, PixelFormat.Format24bppRgb);
                    bmp1.MakeTransparent();
                    //Không cắt được bình thường , thì ta cắt kiểu mất dậy : "Phao chuyên dẫn ngọc"
                    using (Graphics G = Graphics.FromImage(bmp1))
                    {
                        GraphicsPath gp = new GraphicsPath();
                        gp.AddPolygon(point.ToArray());
                        G.Clip = new Region(gp);
                        Matrix m = new Matrix(1, 0, 0, 1, -10, -10);
                        gp.Transform(m);
                        G.DrawImage(imageItem.image, 0, 0);
                        using (Bitmap ImageRetun = new Bitmap(Width, Height, PixelFormat.Format24bppRgb))
                        {
                            using (Graphics G2 = Graphics.FromImage(ImageRetun))
                            {
                                ConfigGraphics.quatityImaging(G2, imageItem.Quality);
                                Color newColor = Color.FromName("Black");
                                Brush aBrush = (Brush)Brushes.Black;
                                G2.FillRectangle(new SolidBrush(newColor), 0, 0, Width, Height);
                                G2.DrawImage(bmp1,
                                    new Rectangle(0, 0, Width, Height),
                                    MinWidth, MinHeight, Width, Height,
                                    GraphicsUnit.Pixel);
                                AllImageRetunName.Add(SaveImage.ReturnUrl(ImageRetun, Path.GetFileName(imageItem.NameReturn),
                                                        imageItem.image.Height, imageItem.image.Height, "Cut"));
                            }
                        }
                    }
                }
            }
        }
    }
}
