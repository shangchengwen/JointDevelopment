using UnityEngine;
using System.IO;
using System;
using System.Text;

namespace KIT
{
    /// <summary>
    /// Log等级
    /// </summary>
    public enum LogLevel
    {
        None = 0,
        Debug = 1,
        Error = 2,
        Warning = 4,
        Exception = 8,
        All = LogLevel.Debug | LogLevel.Error | LogLevel.Warning | LogLevel.Exception
    }

    public class LogWriter
    {
        //private string m_logPath = Application.persistentDataPath + "/log/";
        //C:\Users\Administrator\AppData\LocalLow\DefaultCompany\ProjectLearn\log
        private string m_logPath = Application.dataPath + "/LogHelper/";
        private string m_logFileName = "log_{0}.txt";
        private string m_logFilePath = string.Empty;

        public LogWriter()
        {
            if (!Directory.Exists(m_logPath))
            {
                Directory.CreateDirectory(m_logPath);
            }
            this.m_logFilePath = this.m_logPath + string.Format(this.m_logFileName, DateTime.Today.ToString("yyyyMMdd"));
        }

        public void ExcuteWrite(string content)
        {
            using (StreamWriter writer = new StreamWriter(m_logFilePath, true, Encoding.UTF8))
            {
                writer.WriteLine(content);
            }
        }
    }

    public class LogHelper
    {
        static public LogLevel m_logLevel = LogLevel.All;
        static LogWriter m_logWriter = new LogWriter();

        static LogHelper()
        {
            Application.logMessageReceived += ProcessExceptionReport;
        }

        private static void ProcessExceptionReport(string message, string stackTrace, LogType type)
        {
            LogLevel dEBUG = LogLevel.Debug;
            switch (type)
            {
                case LogType.Assert:
                    dEBUG = LogLevel.Debug;
                    break;
                case LogType.Error:
                    dEBUG = LogLevel.Error;
                    break;
                case LogType.Exception:
                    dEBUG = LogLevel.Exception;
                    break;
                case LogType.Log:
                    dEBUG = LogLevel.Debug;
                    break;
                case LogType.Warning:
                    dEBUG = LogLevel.Warning;
                    break;
                default:
                    break;
            }
            if (dEBUG == (m_logLevel & dEBUG))
            {
                Log(string.Concat(new object[] { " [", dEBUG, "]: ", message, '\n', stackTrace }));
            }
        }

        private static void Log(string message)
        {
            string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + message;
            m_logWriter.ExcuteWrite(msg);
        }

        static public void Log(object message)
        {
            Log(message, null);
        }

        static public void Log(object message, UnityEngine.Object context)
        {
            if (LogLevel.Debug == (m_logLevel & LogLevel.Debug))
            {
                Debug.Log(message, context);
            }
        }
        static public void LogError(object message)
        {
            LogError(message, null);
        }
        static public void LogError(object message, UnityEngine.Object context)
        {
            if (LogLevel.Error == (m_logLevel & LogLevel.Error))
            {
                Debug.LogError(message, context);
            }
        }
        static public void LogWarning(object message)
        {
            LogWarning(message, null);
        }
        static public void LogWarning(object message, UnityEngine.Object context)
        {
            if (LogLevel.Warning == (m_logLevel & LogLevel.Warning))
            {
                Debug.LogWarning(message, context);
            }
        }
    }
}

