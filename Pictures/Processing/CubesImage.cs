using Pictures.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pictures.Processing
{
    public class CubesImage
    {
        public Image triangle(int xt, int yt ,int r, Color colorBackroud, Color colorPen, ConfigImaging configImaging)
        {
            List<ttd> ttds = new List<ttd>();
           
            float grad = (float)((72 * 3.14) / 180);
            var tddf = new ttd()
            {

                x = xt,

                y = yt - r
            };
            ttds.Add(tddf);
            for (int i = 1; i < 5; i++)
            {
                ttds.Add(new ttd()
                {
                    x = (int)(tddf.x * Math.Cos(i * grad) - tddf.y * Math.Sin(i * grad) + yt * Math.Sin(i * grad)
                         + xt * (1 - Math.Cos(i * grad))),

                    y = (int)(tddf.x * Math.Sin(i * grad) + tddf.y * Math.Cos(i * grad) + yt * (1 - Math.Cos(i * grad))
                         - xt * Math.Sin(i * grad))
                });
            }


            Bitmap bitmap = new Bitmap(900, 900);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(colorBackroud), 0, 0, bitmap.Width, bitmap.Height);
            ConfigGraphics.quatityImaging(graphics, configImaging);
            graphics.DrawLine(new Pen(colorPen),
                new Point() { X = ttds[0].x, Y = ttds[0].y },
                new Point() { X = ttds[2].x, Y = ttds[2].y });
            graphics.DrawLine(new Pen(Brushes.Red),
                new Point() { X = ttds[0].x, Y = ttds[0].y },
                new Point() { X = ttds[3].x, Y = ttds[3].y });
            graphics.DrawLine(new Pen(Brushes.Red),
                new Point() { X = ttds[1].x, Y = ttds[1].y },
                new Point() { X = ttds[3].x, Y = ttds[3].y });
            graphics.DrawLine(new Pen(Brushes.Red),
                new Point() { X = ttds[1].x, Y = ttds[1].y },
                new Point() { X = ttds[4].x, Y = ttds[4].y });
            graphics.DrawLine(new Pen(Brushes.Red),
                new Point() { X = ttds[2].x, Y = ttds[2].y },
                new Point() { X = ttds[4].x, Y = ttds[4].y });


            return bitmap;
        }

        private class ttd
        {
            public int x { get; set; }
            public int y { get; set; }
        }
    }
}
