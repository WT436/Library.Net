using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Pictures.Processing
{
    public static class SaveImage
    {
        /// <summary>
        /// Save Image
        /// </summary>
        /// <param name="bitmap">Image</param>
        /// <param name="nameImage">Tên thay đổi</param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="typeStr">form convert</param>
        public static string ReturnUrl(Bitmap bitmap, string nameImage, int maxWidth, int maxHeight, string typeStr)
        {
            // đường dẫn trả về
            string ReturnUrlImage = @"F:\Program\MyLib\ConsoleTest\Output\";
            if (!Directory.Exists(ReturnUrlImage))
            {
                Directory.CreateDirectory(ReturnUrlImage);
            }

            // đuôi ảnh
            string[] arrListStr = nameImage.Split('.');

            var photoStyle = (arrListStr[arrListStr.Length - 1] != "png" || arrListStr[arrListStr.Length - 1] != "jpg")
                              ? "jpg"
                              : arrListStr[arrListStr.Length];

            // Chống trùng lặp - đặt là do while đẻ lấy i++
            var Rand = new Random().Next(0, 10000).ToString();

            // ngày tháng tạo ảnh
            var DateCreate = DateTime.Now.ToString("HHmmss-ddMMyyyy");

            // tên trả về
            nameImage = DateCreate + "_" + nameImage + "(" + Rand + ")" + "." + photoStyle;

            string fileRelativePath =
                                      typeStr + "_" + maxWidth + "x" + maxHeight +
                                      "_" + nameImage;

            bitmap.Save(ReturnUrlImage + fileRelativePath, bitmap.RawFormat);

            return (ReturnUrlImage + fileRelativePath);
        }
    }
}
