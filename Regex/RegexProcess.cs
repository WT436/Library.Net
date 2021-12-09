using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Regex
{
    public class RegexProcess
    {
        /// <summary>
        /// Kiểm tra ký tự đặc biệt
        /// </summary>
        public static readonly string SPECIAL_CHAR = "[+=`!#$%*()'\":;<>?]";

        /// <summary>
        /// Kiểm tra chuỗi ít nhất phải có 1 số
        /// </summary>
        public static readonly string CHECK_NUMBER = "[0-9]{1,}";

        /// <summary>
        /// Kiểm tra ký tự thường xuất hiện  2 lần trở lên
        /// </summary>
        public static readonly string DOUBLE_NORMAL_CHAR = "[a-z]{1,}";

        /// <summary>
        /// Số lượng ký tự cần có
        /// </summary>
        public static readonly string NUMBER_CHAR = ".{8,}";

        /// <summary>
        /// Chuỗi phải có một ký tự
        /// </summary>
        public static readonly string CHAR_LETTER = "[A-Z]{1,}";

        /// <summary>
        /// Kiểm tra cấu trúc gmail
        /// </summary>
        public static readonly string CHECK_TYPES_EMAIL = "[a-zA-Z0-9]{0,}([.]?[a-zA-Z0-9]{1,})[@](gmail.com)";

        public static readonly string KT7 = "[+=`!#$%&*()'\":;<>?]";

        public static readonly string KT8 = @" ^[-+]?[0 - 9] *\.?[0-9]+$";

        /// <summary>
        /// Có ít nhất 10 ký tự
        /// </summary>
        public static readonly string CHECK_TEN_CHAR = "^.{0,10}$";

        public static readonly string KT10 = "[+`!#$%&*()'\":;<>,?]";

        /// <summary>
        /// Kiểm tra khoảng trắng
        /// </summary>
        public static readonly string CHECK_SPACE = "^[^\\s]+$";

        public static readonly string KT12 = "[A-Z]{1,}";

        /// <summary>
        ///  kiểm tra số điện thoại
        /// </summary>
        public static readonly string NUMBER_PHONE = @"(^(84|0))([0-9]{4,10}$)";

        public static bool Regex_IsMatch(string Rexge, string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, Rexge);
        }
    }
}
