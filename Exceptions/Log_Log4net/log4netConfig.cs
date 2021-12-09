using log4net;
using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptions.Log_Log4net
{
    public class LogData : ILogData
    {
        public string Message { set; get; }
        public Exception Error { set; get; }
        public string UserId { set; get; }
        public string GetMessage()
        {
            var a = "User：" + UserId + " Message：" + Message;
            if (Error != null) return a + " Error：" + Error.ToString();
            return a;
        }
    }

    public interface ILogData
    {
        string GetMessage();
        Exception Error { get; }
    }

    public interface IFwLog
    {
        void Debug(ILogData data);
        void Info(ILogData data);
        void Warn(ILogData data);
        void Error(ILogData data);
        void Fatal(ILogData data);
        void Log(Level level, ILogData data);
    }

    public class Log4netConfig : IFwLog
    {
        private readonly ILog log;
        public Log4netConfig()
        {
            log = LogManager.GetLogger(typeof(Log4netConfig));
            foreach (var x in LogManager.GetAllRepositories()
                 .SelectMany(x => x.GetAppenders())
                 .OfType<AppenderSkeleton>()
                 .Select(x => x.ErrorHandler)
                 .OfType<LogErrorHandler>())
            {
                x.ErrorOccurred += ErrorOccurred;
            }
        }
        private void ErrorOccurred(LogErrorData e)
        {
            throw new LogException(e);
        }
        void IFwLog.Debug(ILogData data)
        {
            Write(Level.Debug, data);
        }
        void IFwLog.Info(ILogData data)
        {
            Write(Level.Info, data);
        }
        void IFwLog.Warn(ILogData data)
        {
            Write(Level.Warn, data);
        }
        void IFwLog.Error(ILogData data)
        {
            Write(Level.Error, data);
        }
        void IFwLog.Fatal(ILogData data)
        {
            Write(Level.Fatal, data);
        }
        void IFwLog.Log(Level level, ILogData data)
        {
            Write(level, data);
        }
        private void Write(Level level, ILogData data)
        {
            if (!log.Logger.IsEnabledFor(level))
            {
                return;
            }
            var text = data.GetMessage();
            log.Logger.Log(typeof(Log4netConfig), level, text, data.Error);
        }

        /// <summary>
        /// VSコンソールへの出力
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="p"></param>
        public static void Debug(string msg, params object[] p)
        {
            System.Diagnostics.Debug.WriteLine(msg, p);
        }

    }

    public class LogErrorHandler : IErrorHandler
    {
        public event Action<LogErrorData> ErrorOccurred = null;
        private void OnErrorOccurred(LogErrorData data)
        {
            if (ErrorOccurred == null)
            {
                return;
            }
            ErrorOccurred(data);
        }
        public void Error(string message)
        {
            this.OnErrorOccurred(new LogErrorData(message, null, null));
        }
        public void Error(string message, Exception e)
        {
            this.OnErrorOccurred(new LogErrorData(message, e, null));
        }
        public void Error(string message, Exception e, ErrorCode errorCode)
        {
            OnErrorOccurred(new LogErrorData(message, e, errorCode));
        }
    }

    public class LogErrorData
    {
        public string Message { get; private set; }
        public Exception Error { get; private set; }
        public ErrorCode? ErrorCode { get; private set; }
        public LogErrorData(string message, Exception exception, ErrorCode? errorCode)
        {
            this.Message = message;
            this.Error = exception;
            this.ErrorCode = errorCode;
        }
    }

    public class LogException : Exception
    {
        public new LogErrorData Data { get; private set; }
        public LogException(LogErrorData data)
            : base(data.Message, data.Error)
        {
            this.Data = data;
        }
    }
}
