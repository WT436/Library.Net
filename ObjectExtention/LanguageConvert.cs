using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ObjectExtention
{
    public class LanguageConvert
    {
        public unsafe static string ConvertVietNamese(string text)
        {
            string[] pattern = new string[7];
            char[] replaceChar = new char[14];

            // Khởi tạo giá trị thay thế
            replaceChar[0] = 'a';
            replaceChar[1] = 'd';
            replaceChar[2] = 'e';
            replaceChar[3] = 'i';
            replaceChar[4] = 'o';
            replaceChar[5] = 'u';
            replaceChar[6] = 'y';
            replaceChar[7] = 'A';
            replaceChar[8] = 'D';
            replaceChar[9] = 'E';
            replaceChar[10] = 'I';
            replaceChar[11] = 'O';
            replaceChar[12] = 'U';
            replaceChar[13] = 'Y';

            pattern[0] = "(á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ)";
            pattern[1] = "đ";
            pattern[2] = "(é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ)";
            pattern[3] = "(í|ì|ỉ|ĩ|ị)";
            pattern[4] = "(ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ)";
            pattern[5] = "(ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự)";
            pattern[6] = "(ý|ỳ|ỷ|ỹ|ỵ)";

            fixed (char* ptrChar = replaceChar)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    MatchCollection matchs = Regex.Matches(text, pattern[i], RegexOptions.IgnoreCase);
                    foreach (Match m in matchs)
                    {
                        char ch = char.IsLower(m.Value[0]) ? *(ptrChar + i) : *(ptrChar + i + 7);
                        text = text.Replace(m.Value[0], ch);
                    }
                }
            }
            return text;
        }

    }
}
