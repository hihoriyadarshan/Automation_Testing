namespace PlaywrightCSharpFramework.Framework.Utilities
{
    public static class Constants
    {
        #region Configuration Keys

        public static class ConfigKeys
        {
            public const string BrowserType = "Browser:Type";
            public const string Headless = "Browser:Headless";
            public const string SlowMo = "Browser:SlowMo";
            public const string Channel = "Browser:Channel";
            public const string LaunchTimeout = "Browser:LaunchTimeout";

            public const string BaseUrl = "Application:BaseUrl";

            public const string DefaultTimeout = "Timeouts:DefaultTimeout";
            public const string NavigationTimeout = "Timeouts:NavigationTimeout";

            public const string ScreenshotPath = "Screenshots:Path";
            public const string CaptureOnFailure = "Screenshots:CaptureOnFailure";

            public const string TraceEnabled = "Tracing:Enabled";
            public const string TraceScreenshots = "Tracing:Screenshots";
            public const string TraceSnapshots = "Tracing:Snapshots";
            public const string TraceSources = "Tracing:Sources";

            public const string VideoEnabled = "Video:Enabled";
            public const string VideoPath = "Video:Path";

            public const string AllureResultsDirectory = "Allure:ResultsDirectory";
        }

        #endregion

        #region Folder Paths

        public static class Paths
        {
            public const string ReportsFolder = "Reports";
            public const string ScreenshotsFolder = "Reports/Screenshots";
            public const string LogsFolder = "Reports/Logs";
            public const string VideosFolder = "Reports/Videos";
            public const string TracesFolder = "Reports/Traces";
            public const string AllureResultsFolder = "Reports/allure-results";
            public const string TestDataFolder = "TestData";
        }

        #endregion

        #region Default Values

        public static class Defaults
        {
            public const string DefaultBrowser = "chromium";
            public const int DefaultTimeout = 30000;
            public const int DefaultNavigationTimeout = 60000;
            public const int DefaultRetryCount = 2;
            public const int DefaultRetryDelay = 500;
        }

        #endregion

        #region Test Status Values

        public static class TestRunFlags
        {
            public const string Yes = "Y";
            public const string No = "N";
            public const string True = "True";
            public const string False = "False";
        }

        #endregion

        #region Common Selectors (Optional Usage)

        public static class CommonSelectors
        {
            public const string Loader = ".loading";
            public const string ToastMessage = ".toast-message";
            public const string Modal = ".modal";
        }

        #endregion
    }
}