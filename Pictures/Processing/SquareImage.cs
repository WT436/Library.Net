using Pictures.Processing.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Pictures.Processing
{
    public class SquareImage
    {
        /// <summary>
        /// Chuyển đổi ảnh vuông và ảnh nằm giữa
        /// </summary>
        public List<string> ConvertSquareImage(List<ResizeImageDto> Data)
        {
            List<string> AllImageRetunName = new List<string>();
            foreach (var imageItem in Data)
            {
                // Lấy cạnh lớn nhất
                var MaxEdge = Math.Max(imageItem.image.Width, imageItem.image.Height);
                // Tạo bitMap Vuông
                using (Bitmap newImage = new Bitmap(MaxEdge, MaxEdge, PixelFormat.Format24bppRgb))
                {
                    // cài độ phân giải
                    newImage.SetResolution(80, 60);

                    using (Graphics gfx = Graphics.FromImage(newImage))
                    {
                        ConfigGraphics.quatityImaging(gfx, imageItem.Quality);
                        Brush aBrush = (Brush)Brushes.LightGray;
                        // ảnh đang nằm ngang => cho ảnh nằm ngang ở giữa nền hình vuông
                        // lấy Width làm cạnh hình vuông
                        if (imageItem.image.Width > imageItem.image.Height)
                        {
                            gfx.FillRectangle(aBrush, 0, 0, imageItem.image.Width, imageItem.image.Width);
                            gfx.DrawImage(imageItem.image,
                                          new Rectangle(0, (imageItem.image.Width - imageItem.image.Height) / 2,
                                                        imageItem.image.Width, imageItem.image.Height),
                                          0, 0, imageItem.image.Width, imageItem.image.Height, GraphicsUnit.Pixel);
                        }
                        else
                        {
                            // gán màu nền
                            gfx.FillRectangle(aBrush, 0, 0, imageItem.image.Height, imageItem.image.Height);
                            // di chuyển ảnh vào trung tâm
                            gfx.DrawImage(imageItem.image,
                                          new Rectangle((imageItem.image.Height - imageItem.image.Width) / 2, 0,
                                          imageItem.image.Width, imageItem.image.Height),
                                          0, 0, imageItem.image.Width, imageItem.image.Height, GraphicsUnit.Pixel);
                        }
                        // trả về ảnh cơ bản khung hình vuông và ảnh căn giữa
                        AllImageRetunName.Add(SaveImage.ReturnUrl(newImage, imageItem.NameReturn,
                                                                       imageItem.image.Height, imageItem.image.Height, "Square"));
                        // do người dùng có nhiều khuân khác nhau nên cần phóng to ảnh vừa chuyển thành hình vuông để cho vừa khung hình đưa vào
                        // hàm trên là list nên thay đổi 1 phần tư thành 1 list nhưng gây chậm
                        AllImageRetunName.AddRange(ResizeImage.resizeImage(new List<ResizeImageDto>
                        {
                            new ResizeImageDto
                            {
                                image = newImage,
                                UrlReturn = imageItem.UrlReturn,
                                NameReturn = imageItem.NameReturn,
                                ratio = imageItem.ratio,
                                ListSizeImages = imageItem.ListSizeImages
                            }
                        }));
                    }
                }
            }
            return AllImageRetunName;
        }

    }
}
