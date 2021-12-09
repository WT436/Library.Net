using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FilterAttributeCore.SaveDb
{
    public class ApiLogger : IApiLogger
    {
        public void LogError(Exception exception)
        {
            // hàm này kết nối và lưu vào db
        }

        public void LogWarning(Exception exception)
        {
        }

    }
}
