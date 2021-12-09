using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Pictures.Dto.Enums;

namespace Pictures.Processing
{
    public class DrawTextImage
    {
        public Image WriteTextOnThePicture(Image image, string text,
                                           string font,int size,
                                           FontStyle fontStyle, Color color,
                                           int X,int Y)
        {
            Graphics graphicImage = Graphics.FromImage(image);
            ConfigGraphics.quatityImaging(graphicImage,ConfigImaging.Low);
            graphicImage.DrawString(text,
                   new Font(font, size, fontStyle), new SolidBrush(color), new Point(X, Y));
            graphicImage.Dispose();
            return image; 
        }
    }
}
