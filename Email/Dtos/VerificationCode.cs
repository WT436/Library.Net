using System;
using System.Collections.Generic;
using System.Text;

namespace Email.Dtos
{
    public class VerificationCode
    {
        public string ImgSrc { get; set; } // Đường dẫn logo
        public string ImgAlt { get; set; } // Text hỗ trợ logo
        public string Greeting { get; set; } // Câu Xin Chào
        public string UserName { get; set; } // Full name người dùng
        public string Instruction { get; set; } // để xác nhận mật khẩu vui lòng nhấn vào đường dẫn hoặc nhập mã xác thực vào trang
        public string InstructionUrl { get; set; } // Đường dẫn xác thực nhanh
        public string Url { get; set; } //  https://www.google.com/
        public string Click { get; set; } // Nhấn vào đây
        public string VerificationCodesText { get; set; } // Mã xác thực
        public string VerificationCodes { get; set; } // 035 64255
    }
}