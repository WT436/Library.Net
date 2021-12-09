using Pictures.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Pictures.Processing
{
    public static class ConfigGraphics
    {
        #region Cấp độ , độ phân giải, làm mềm ......
        /**
         * CompositingQuality: mức chất lượng để  tổng hợp.
         * => HighQuality : tổng hợp chất lượng cao tốc độ thấp
         * => HighSpeed : Tổng cao, chất lượng thấp
         ** CombineMode : các vùng cắt khác nhau có thể được kết hợp.
         * => Complement : khu vực hiện tại bị loại trừ khỏi khu vực mới
         * => Intersect : được kết hợp bằng cách lấy giao điểm
         * => Replace : bị thay thế
         * InterpolationMode : thuật toán được sử dụng khi hình ảnh được chia tỷ lệ hoặc xoay.
         * => Bicubic Giảm 25%
         * => High : chất lượng cao
         * => Low : chất lượng thấp
         * PixelOffsetMode :  cách các pixel được bù đắp trong quá trình hiển thị.
         * => Half : bù đắp bởi 5 đơn vị gần nhất
         * => HighQuality : chất lượng cao kết xuất
         * => HighSpeed : chất lượng cao tốc độ
         * QualityMode :  chất lượng tổng thể khi hiển thị các đối tượng GDI +.
         * => High: chất lượng cao , tốc độ thấp
         * => Low : Chất lượng thấp , tốc độ cao
         * SmoothingMode :  liệu làm mịn (khử răng cưa) có được áp dụng cho các đường thẳng và đường cong và các cạnh.
         * làm mịn => HighQuality 
         * Not => HighSpeed
         */
        #endregion Kết thúc cấp độ
        public static Graphics quatityImaging(Graphics graphics, ConfigImaging configImaging)
        {
            switch (configImaging)
            {
                // full
                case ConfigImaging.High:
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    break;
                //tb 1
                case ConfigImaging.TB1:
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    break;
                // tb2
                case ConfigImaging.TB2:
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.Bicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    break;
                //tb3
                case ConfigImaging.TB3:
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Bicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    break;
                //tb4
                case ConfigImaging.TB4:
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    break;
                // thấp
                default:
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    break;
            }
            return graphics;
        }
        public static Graphics quatityTypography(Graphics graphics, int QualityBar)
        {
            switch (QualityBar)
            {
                case 12:
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    break;
                default:
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.Low;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    break;
            }
            return graphics;
        }
    }
}
