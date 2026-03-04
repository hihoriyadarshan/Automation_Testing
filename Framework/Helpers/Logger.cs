using Allure.Commons;
using Allure.Net.Commons;
using PlaywrightCSharpFramework.Framework.Config;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace PlaywrightCSharpFramework.Framework.Helpers
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logDirectory;
        private static readonly string _logFilePath;

        static Logger()
        {
            _logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "Logs");
            Directory.CreateDirectory(_logDirectory);

            var fileName = $"TestRun_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            _logFilePath = Path.Combine(_logDirectory, fileName);
        }

        #region Public Logging Methods

        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        public static void Warn(string message)
        {
            WriteLog("WARN", message);
        }

        public static void Debug(string message)
        {
            WriteLog("DEBUG", message);
        }

        public static void Error(string message)
        {
            WriteLog("ERROR", message);
        }

        public static void Error(string message, Exception ex)
        {
            WriteLog("ERROR", $"{message} | Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }

        #endregion

        #region Core Writer

        private static void WriteLog(string level, string message)
        {
            lock (_lock)
            {
                var logEntry = FormatMessage(level, message);

                // Write to Console
                Console.WriteLine(logEntry);

                // Write to File
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
            }
        }

        private static string FormatMessage(string level, string message)
        {
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {level} | Thread:{Thread.CurrentThread.ManagedThreadId} | {message}";
        }

        #endregion

        #region Allure Integration

        public static void AttachLogToAllure(string name = "Execution Log")
        {
            if (File.Exists(_logFilePath))
            {
                AllureApi.AddAttachment(
                    name,
                    "text/plain",
                    _logFilePath
                );
            }
        }

        #endregion

        #region Get Log File Path

        public static string GetLogFilePath()
        {
            return _logFilePath;
        }

        #endregion
    }
}