using Pictures.Dto.Enums;
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
    public static class ResizeImage
    {
        /// <summary>
        /// ResizeImage đơn từng ảnh một
        /// </summary>
        public static List<string> resizeImage(List<ResizeImageDto> Data)
        {
            List<string> AllImageRetunName = new List<string>();
            foreach (var resizeImageDto in Data)
            {
                // cho phép tăng cả chiều cao và chiều dài hay không .
                // nếu true thì chỉ tăng theo cạnh nhỏ nhất cạnh còn lại phụ thuộc vào tỷ lệ hình gốc
                if (resizeImageDto.ratio)
                {
                    foreach (var ItemSize in resizeImageDto.ListSizeImages)
                    {
                        #region Ảnh theo kích thước truyền vào và giữ nguyên cấu trúc ảnh
                        // tỉ lệ các cạnh
                        var ratioX = (double)ItemSize.maxWidth / resizeImageDto.image.Width;
                        var ratioY = (double)ItemSize.maxHeight / resizeImageDto.image.Height;
                        //lấy tỷ lệ thấp nhất trong 2 tỷ lệ truyền vào để giữ cấu trúc của ảnh
                        var ratioNow = Math.Min(ratioX, ratioY);
                        //Tinh lai cac canh moi
                        int newWidth = (int)(resizeImageDto.image.Width * ratioNow);
                        int newHeight = (int)(resizeImageDto.image.Height * ratioNow);

                        using (var newImage = new Bitmap(newWidth, newHeight))
                        {
                            using (Graphics thumbGraph = Graphics.FromImage(newImage))
                            {
                                ConfigGraphics.quatityImaging(thumbGraph, resizeImageDto.Quality);
                                thumbGraph.DrawImage(resizeImageDto.image, 0, 0, newWidth, newHeight);
                            }
                            AllImageRetunName.Add(SaveImage.ReturnUrl(newImage, resizeImageDto.NameReturn,
                                                            ItemSize.maxWidth, ItemSize.maxHeight, "Resize"));
                        }
                        #endregion
                    }
                }
                else
                {
                    foreach (var ItemSize in resizeImageDto.ListSizeImages)
                    {
                        #region Ảnh theo kích thước truyền vào và giữ nguyên cấu trúc ảnh
                        // tỉ lệ các cạnh
                        var ratioX = (double)ItemSize.maxWidth / resizeImageDto.image.Width;
                        var ratioY = (double)ItemSize.maxHeight / resizeImageDto.image.Height;
                        //lấy tỷ lệ thấp nhất trong 2 tỷ lệ truyền vào để giữ cấu trúc của ảnh
                        var ratioNow = Math.Min(ratioX, ratioY);
                        //Tinh lai cac canh moi
                        int newWidth = (int)(resizeImageDto.image.Width * ratioNow);
                        int newHeight = (int)(resizeImageDto.image.Height * ratioNow);

                        using (var newImage = new Bitmap(newWidth, newHeight))
                        {
                            using (Graphics thumbGraph = Graphics.FromImage(newImage))
                            {
                                ConfigGraphics.quatityImaging(thumbGraph, resizeImageDto.Quality);
                                thumbGraph.DrawImage(resizeImageDto.image, 0, 0, newWidth, newHeight);
                            }
                            AllImageRetunName.Add(SaveImage.ReturnUrl(newImage, resizeImageDto.NameReturn,
                                                            ItemSize.maxWidth, ItemSize.maxHeight, "ResizeFull"));
                        }
                        #endregion

                        #region Ảnh theo kích thước truyền vào và theo tỷ lệ truyền vào
                        // tỉ lệ các cạnh
                        ratioX = (double)ItemSize.maxWidth / resizeImageDto.image.Width;
                        ratioY = (double)ItemSize.maxHeight / resizeImageDto.image.Height;
                        //tỉ lệ ảnh và chiều dài rộng truyền vào => ảnh sẽ có kích thước như tham số truyền vào
                        newWidth = (int)(resizeImageDto.image.Width * ratioX);
                        newHeight = (int)(resizeImageDto.image.Height * ratioY);

                        using (var newImage = new Bitmap(newWidth, newHeight))
                        {
                            using (Graphics thumbGraph = Graphics.FromImage(newImage))
                            {
                                ConfigGraphics.quatityImaging(thumbGraph, resizeImageDto.Quality);
                                thumbGraph.DrawImage(resizeImageDto.image, 0, 0, newWidth, newHeight);
                            }
                            AllImageRetunName.Add(SaveImage.ReturnUrl(newImage, resizeImageDto.NameReturn, ItemSize.maxWidth,
                                                            ItemSize.maxHeight, "ResizeFull"));
                        }
                        #endregion
                    }
                }
            }
            return AllImageRetunName;
        }

        public static Image ResizeNotChangeStructure(Image image, int maxWidth, int maxHeight, ConfigImaging configImaging)
        {
            #region Ảnh theo kích thước truyền vào và giữ nguyên cấu trúc ảnh
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratioNow = Math.Min(ratioX, ratioY);
            int newWidth = (int)(image.Width * ratioNow);
            int newHeight = (int)(image.Height * ratioNow);

            var newImage = new Bitmap(newWidth, newHeight);
            using (Graphics thumbGraph = Graphics.FromImage(newImage))
            {
                ConfigGraphics.quatityImaging(thumbGraph, configImaging);
                thumbGraph.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
            #endregion
        }

        public static Image ResizeChangeStructure(Image image, int maxWidth, int maxHeight, ConfigImaging configImaging)
        {
            #region Ảnh theo kích thước truyền vào và giữ nguyên cấu trúc ảnh
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            int newWidth = (int)(image.Width * ratioX);
            int newHeight = (int)(image.Height * ratioY);

            var newImage = new Bitmap(newWidth, newHeight);
            using (Graphics thumbGraph = Graphics.FromImage(newImage))
            {
                ConfigGraphics.quatityImaging(thumbGraph, configImaging);
                thumbGraph.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
            #endregion
        }

        public static Image ResizeImageV2(Image Imagen, int maxWidth, int MaxHeight)
        {
            Image resizedimage = Imagen;

            if (((double)maxWidth / (double)Imagen.Width) < ((double)MaxHeight / (double)Imagen.Height))
                resizedimage = (Image)(new Bitmap((Bitmap)resizedimage, new Size((int)(((double)maxWidth / (double)resizedimage.Width) * (double)resizedimage.Width), (int)(((double)maxWidth / (double)resizedimage.Width) * (double)resizedimage.Height))));
            else
                resizedimage = (Image)(new Bitmap((Bitmap)resizedimage, new Size((int)(((double)MaxHeight / (double)resizedimage.Height) * (double)resizedimage.Width), (int)(((double)MaxHeight / (double)resizedimage.Height) * (double)resizedimage.Height))));
            return resizedimage;
        }
    }
}
