using System;
using System.Collections.Generic;
using System.Text;

namespace FilterAttributeCore.SaveDb
{
    public interface IApiLogger
    {
        void LogWarning(Exception exception);
        void LogError(Exception exception);
    }
}
