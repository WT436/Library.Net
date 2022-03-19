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
        /// <summary>
        /// Có ít nhất 10 ký tự
        /// </summary>
        public static readonly string CHECK_TEN_CHAR = "^.{0,10}$";

        /// <summary>
        /// Kiểm tra khoảng trắng
        /// </summary>
        public static readonly string CHECK_SPACE = "^[^\\s]+$";

        /// <summary>
        ///  kiểm tra số điện thoại
        /// </summary>
        public static readonly string NUMBER_PHONE = @"(^(84|0))([0-9]{4,10}$)";

        /// <summary>
        /// Kiểm tra số điện thoại việt nam
        /// </summary>
        public static readonly string CHECK_NUMBER_PHONE_VN = @"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$";

        /// <summary>
        /// kiểm tra dấu chéo ngược
        /// </summary>
        public static readonly string CHECK_CHAR_BACKSLASH = String.Format(@"[\\{0}]", "\"");

        /// <summary>
        /// Kiểm tra email
        /// </summary>
        public static readonly string CHECK_EMAIL = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        /// <summary>
        /// kiểm tra độ mạnh mật khẩu
        /// </summary>
        public static readonly string CHECK_STRENGTH = "^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$ ";

        /// <summary>
        /// Kiểm tra định dạng ip v4
        /// </summary>
        public static readonly string CHECK_IP_V4 = @"/\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b/";

        /// <summary>
        /// kiểm tra định dạng ip v6
        /// </summary>
        public static readonly string CHECK_IP_V6 = @"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))";

        /// <summary>
        /// kiểm tra chữ số hằng nghìn
        /// </summary>
        public static readonly string CHECK_THOUSANDS_SEPARATOR = @"/\d{1,3}(?=(\d{3})+(?!\d))/g ";

        /// <summary>
        ///  Lấy tên miền từ URL
        /// </summary>
        public static readonly string GET_DOMAIN_FROM_URL = @"/https?:\/\/(?:[-\w]+\.)?([-\w]+)\.\w+(?:\.\w+)?\/?.*/i ";

        /// <summary>
        ///  Kiểm tra định dạng ngày tháng
        /// </summary>
        public static readonly string CHECK_FORMAT_DATETIME = @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$ ";
           
        public static bool Regex_IsMatch(string Rexge, string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, Rexge);
        }
    }
}
