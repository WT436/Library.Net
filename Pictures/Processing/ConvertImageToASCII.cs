using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Pictures.Processing
{
    public static  class ConvertImageToASCII
    {
        public static Image ConvertASCIIColor(string url)
        {
            Image Imagen_O;
            Image ResizedImage_O;
            Image Imagen_BW;
            Image ResultImage;
            List<WeightedChar> CharSet = GenerateFontWeights();
            Image AdjustedContrast;
            List<List<string>> ImageText;
            Imagen_O = Image.FromFile(url);
            ResizedImage_O = (Image)ResizeImage.ResizeImageV2(Imagen_O, Imagen_O.Width, Imagen_O.Height);
            AdjustedContrast = new Bitmap(Imagen_O);
            AdjustContrast((Bitmap)AdjustedContrast, 1);
            Imagen_BW = (Image)Grayscale(AdjustedContrast);
            int? width = 1;
            int? height = 1;
            List<List<string>> ResultText = new List<List<string>> { };
            ResultImage = Convert2ASCII(Imagen_BW, width, height, CharSet, out ResultText);
            ImageText = ResultText;
            AdjustedContrast = (Image)ResizeImage.ResizeImageV2(AdjustedContrast, AdjustedContrast.Width, AdjustedContrast.Height);
            return Convert2ASCIIColor(AdjustedContrast, CharSet, ImageText);
        }
        public static Image ConvertASCII(string url)
        {

            Image Imagen_O;
            Image ResizedImage_O;
            Image Imagen_BW;
            Image ResultImage;
            List<WeightedChar> CharSet = GenerateFontWeights();
            Image AdjustedContrast;
            List<List<string>> ImageText;
            Imagen_O = Image.FromFile(url);
            ResizedImage_O = (Image)ResizeImage.ResizeImageV2(Imagen_O, Imagen_O.Width, Imagen_O.Height);
            AdjustedContrast = new Bitmap(Imagen_O);
            AdjustContrast((Bitmap)AdjustedContrast, 1);
            Imagen_BW = (Image)Grayscale(AdjustedContrast);
            int? width = 1;
            int? height = 1;
            List<List<string>> ResultText = new List<List<string>> { };
            ResultImage = Convert2ASCII(Imagen_BW, width, height, CharSet, out ResultText);
            ImageText = ResultText;
            AdjustedContrast = (Image)ResizeImage.ResizeImageV2(ResultImage, AdjustedContrast.Width, AdjustedContrast.Height);
            return AdjustedContrast;

        }
        private static List<WeightedChar> LinearMap(List<WeightedChar> characters)
        {
            double max = characters.Max(c => c.Weight);
            double min = characters.Min(c => c.Weight);
            double range = 255;
            // y = mx + n (where y c (0-255))
            double slope = range / (max - min);
            double n = -min * slope;
            foreach (WeightedChar charactertomap in characters)
            {
                charactertomap.Weight = slope * charactertomap.Weight + n;
            }
            return characters;
        }
        private static Image Convert2ASCII(Image BW_Image, int? w, int? h, List<WeightedChar> characters, out List<List<string>> ResultText)
        {
            if (w == null & h == null)
            {
                w = 1;
                h = 1;
            }
            int width = (int)w;
            int height = (int)h;
            Image ResultImage = new Bitmap(BW_Image.Width * characters[0].CharacterImage.Width / width, BW_Image.Height * characters[0].CharacterImage.Height / height);
            Graphics drawing = Graphics.FromImage(ResultImage);
            drawing.Clear(Color.White);
            ResultText = new List<List<string>> { };
            Bitmap BlackAndWhite = (Bitmap)BW_Image;

            for (int j = 0; j < BW_Image.Height; j += height)
            {
                List<string> RowText = new List<string> { };
                for (int i = 0; i < BW_Image.Width; i += width)
                {
                    double targetvalue = 0;

                    for (int nj = j; nj < j + height; nj++)
                    {
                        for (int ni = i; ni < i + width; ni++)
                        {
                            try
                            {
                                targetvalue += BlackAndWhite.GetPixel(ni, nj).R;
                            }
                            catch (Exception ex)
                            {
                                targetvalue += 128;
                            }
                        }
                    }
                    targetvalue /= (width * height);
                    WeightedChar closestchar = characters.Where(t => Math.Abs(t.Weight - targetvalue) == characters.Min(e => Math.Abs(e.Weight - targetvalue))).FirstOrDefault();
                    RowText.Add(closestchar.Character);
                    drawing.DrawImage(closestchar.CharacterImage, (i / width) * closestchar.CharacterImage.Width, (j / height) * closestchar.CharacterImage.Height);
                }
                ResultText.Add(RowText);
            }
            drawing.Dispose();
            return (Image)ResultImage;
        }
        private unsafe static Image Convert2ASCIIColor(Image ResizedImage_O, List<WeightedChar> characters, List<List<string>> ImageText)
        {
            Image ResultImage = new Bitmap(ResizedImage_O.Width * characters[0].CharacterImage.Width, ResizedImage_O.Height * characters[0].CharacterImage.Height);
            Graphics drawing = Graphics.FromImage(ResultImage);
            drawing.Clear(Color.White);
            var bmp = (Bitmap)ResizedImage_O;
            var bitmapdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                             ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int PixelSize = 4;
            int width = characters[0].CharacterImage.Width;
            int height = characters[0].CharacterImage.Height;
            for (int j = 0; j < ResizedImage_O.Height; j++)
            {
                byte* destPixels = (byte*)bitmapdata.Scan0 + (j * bitmapdata.Stride);

                for (int i = 0; i < ResizedImage_O.Width; i++)
                {
                    var B = (int)destPixels[i * PixelSize];
                    var G = (int)destPixels[i * PixelSize + 1];
                    var R = (int)destPixels[i * PixelSize + 2];
                    var character = ImageText[j][i];
                    // create char image
                    var charimage = DrawText(character, Color.FromArgb(R, G, B), Color.White, new SizeF(width, height));
                    // paste char image 
                    drawing.DrawImage(charimage, i * charimage.Width, j * charimage.Height);

                }
            }
            bmp.UnlockBits(bitmapdata);
            drawing.Dispose();
            return (Image)ResultImage;
        }
        private static List<WeightedChar> GenerateFontWeights()
        {
            List<WeightedChar> WeightedChars = new List<WeightedChar>();

            SizeF commonsize = GetGeneralSize();

            for (int i = 32; i <= 126; i++)
            {
                var forweighting = new WeightedChar();
                char c = Convert.ToChar(i);
                if (!char.IsControl(c))
                {
                    forweighting.Weight = GetWeight(c, commonsize);
                    forweighting.Character = c.ToString();
                    forweighting.CharacterImage = (Bitmap)DrawText(c.ToString(), Color.Black, Color.White, commonsize);
                }
                WeightedChars.Add(forweighting);
            }
            WeightedChars = LinearMap(WeightedChars);
            return WeightedChars;
        }
        private static double GetWeight(char c, SizeF size)
        {
            var CharImage = DrawText(c.ToString(), Color.Black, Color.White, size);

            Bitmap btm = new Bitmap(CharImage);
            double totalsum = 0;

            for (int i = 0; i < btm.Width; i++)
            {
                for (int j = 0; j < btm.Height; j++)
                {
                    totalsum = totalsum + (btm.GetPixel(i, j).R
                                        + btm.GetPixel(i, j).G
                                        + btm.GetPixel(i, j).B) / 3;
                }
            }
            return totalsum / (size.Height * size.Width);
        }       
        public static Bitmap Grayscale(Image image)
        {
            Bitmap btm = new Bitmap(image);
            for (int i = 0; i < btm.Width; i++)
            {
                for (int j = 0; j < btm.Height; j++)
                {
                    int ser = (int)(btm.GetPixel(i, j).R * 0.3 + btm.GetPixel(i, j).G * 0.59 + btm.GetPixel(i, j).B * 0.11);
                    btm.SetPixel(i, j, Color.FromArgb(ser, ser, ser));
                }
            }
            return btm;
        }
        private unsafe static void AdjustContrast(Bitmap bmp, double contrast)
        {
            {
                byte[] contrast_lookup_table = new byte[256];
                double newValue = 0;
                double c = (100.0 + contrast) / 100.0;

                c *= c;

                for (int i = 0; i < 256; i++)
                {
                    newValue = (double)i;
                    newValue /= 255.0;
                    newValue -= 0.5;
                    newValue *= c;
                    newValue += 0.5;
                    newValue *= 255;

                    if (newValue < 0)
                        newValue = 0;
                    if (newValue > 255)
                        newValue = 255;
                    contrast_lookup_table[i] = (byte)newValue;
                }

                var bitmapdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                int PixelSize = 4;

                for (int y = 0; y < bitmapdata.Height; y++)
                {
                    byte* destPixels = (byte*)bitmapdata.Scan0 + (y * bitmapdata.Stride);
                    for (int x = 0; x < bitmapdata.Width; x++)
                    {
                        destPixels[x * PixelSize] = contrast_lookup_table[destPixels[x * PixelSize]]; // B
                        destPixels[x * PixelSize + 1] = contrast_lookup_table[destPixels[x * PixelSize + 1]]; // G
                        destPixels[x * PixelSize + 2] = contrast_lookup_table[destPixels[x * PixelSize + 2]]; // R
                        //destPixels[x * PixelSize + 3] = contrast_lookup[destPixels[x * PixelSize + 3]]; //A
                    }
                }
                bmp.UnlockBits(bitmapdata);
            }
        }
        private static Image DrawText(string text, Color textColor, Color backColor, SizeF WidthAndHeight)
        {
            Image dummy_img = new Bitmap(1, 1);
            Graphics dummy_drawing = Graphics.FromImage(dummy_img);
            SizeF textSize = dummy_drawing.MeasureString(text, System.Drawing.SystemFonts.DefaultFont);
            dummy_img.Dispose();
            dummy_drawing.Dispose();
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            img.Dispose();
            drawing.Dispose();
            img = new Bitmap((int)WidthAndHeight.Width, (int)WidthAndHeight.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(backColor);
            Brush textBrush = new SolidBrush(textColor);
            drawing.DrawString(text, System.Drawing.SystemFonts.DefaultFont, textBrush, (WidthAndHeight.Width - textSize.Width) / 2, 0);
            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();
            return img;
        }
        private static SizeF GetGeneralSize()
        {
            SizeF generalsize = new SizeF(0, 0);
            for (int i = 32; i <= 126; i++)
            {
                char c = Convert.ToChar(i);
                Image img = new Bitmap(1, 1);
                Graphics drawing = Graphics.FromImage(img);
                SizeF textSize = drawing.MeasureString(c.ToString(), System.Drawing.SystemFonts.DefaultFont);
                if (textSize.Width > generalsize.Width)
                    generalsize.Width = textSize.Width;
                if (textSize.Height > generalsize.Height)
                    generalsize.Height = textSize.Height;
                img.Dispose();
                drawing.Dispose();
            }
            generalsize.Width = ((int)generalsize.Width);
            generalsize.Height = ((int)generalsize.Height);
            if (generalsize.Width > generalsize.Height)
                generalsize.Height = generalsize.Width;
            else
                generalsize.Width = generalsize.Height;

            return generalsize;
        }
    }
    class WeightedChar
    {
        public string Character { get; set; }
        public Bitmap CharacterImage { get; set; }
        public double Weight { get; set; }
    }
}
