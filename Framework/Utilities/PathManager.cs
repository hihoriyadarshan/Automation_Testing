using System;
using System.IO;

namespace PlaywrightCSharpFramework.Framework.Utilities
{
    public static class PathManager
    {
        #region Root Directory

        /// <summary>
        /// Gets project root directory
        /// </summary>
        public static string RootDirectory =>
            Directory.GetCurrentDirectory();

        #endregion

        #region Reports

        public static string ReportsDirectory =>
            EnsureDirectory(Path.Combine(RootDirectory, Constants.Paths.ReportsFolder));

        public static string ScreenshotsDirectory =>
            EnsureDirectory(Path.Combine(RootDirectory, Constants.Paths.ScreenshotsFolder));

        public static string LogsDirectory =>
            EnsureDirectory(Path.Combine(RootDirectory, Constants.Paths.LogsFolder));

        public static string VideosDirectory =>
            EnsureDirectory(Path.Combine(RootDirectory, Constants.Paths.VideosFolder));

        public static string TracesDirectory =>
            EnsureDirectory(Path.Combine(RootDirectory, Constants.Paths.TracesFolder));

        public static string AllureResultsDirectory =>
            EnsureDirectory(Path.Combine(RootDirectory, Constants.Paths.AllureResultsFolder));

        #endregion

        #region Test Data

        public static string TestDataDirectory =>
            EnsureDirectory(Path.Combine(RootDirectory, Constants.Paths.TestDataFolder));

        public static string GetTestDataFile(string fileName)
        {
            return Path.Combine(TestDataDirectory, fileName);
        }

        #endregion

        #region File Generators

        public static string GenerateScreenshotPath(string name)
        {
            var fileName = $"{name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            return Path.Combine(ScreenshotsDirectory, fileName);
        }

        public static string GenerateLogFilePath()
        {
            var fileName = $"TestRun_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            return Path.Combine(LogsDirectory, fileName);
        }

        public static string GenerateTraceFilePath()
        {
            var fileName = $"Trace_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
            return Path.Combine(TracesDirectory, fileName);
        }

        #endregion

        #region Helpers

        private static string EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        #endregion
    }
}