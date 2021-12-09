using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace File
{
    public class FolderProcess
    {
        public List<string> ReadFileInFolder(string urlFolder, List<string> typeFile)
        {
            List<string> urlFiles = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(urlFolder);
                for (int i = 0; i < files.Length; i++)
                {
                    if (typeFile.Any(m => m == Path.GetExtension(files[i])))
                    {
                        urlFiles.Add(files[i]);
                    }
                }
            }
            // Lỗi bảo mật khi truy cập vào thư mục mà bạn không có quyền.
            catch { }

            return urlFiles;
        }
    }
}
