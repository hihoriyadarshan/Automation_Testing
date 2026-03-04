using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PlaywrightCSharpFramework.Framework.Config
{
    public sealed class ConfigReader
    {
        private static readonly Lazy<ConfigReader> _instance =
            new Lazy<ConfigReader>(() => new ConfigReader());

        private readonly IConfigurationRoot _configuration;

        public static ConfigReader Instance => _instance.Value;

        private ConfigReader()
        {
            var environment = Environment.GetEnvironmentVariable("TEST_ENV") ?? "QA";

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Framework/Config/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Framework/Config/appsettings.{environment}.json", optional: true)
                .Build();
        }

        #region Browser Settings

        public string BrowserType =>
            _configuration["Browser:Type"] ?? "chromium";

        public bool Headless =>
            bool.TryParse(_configuration["Browser:Headless"], out var result) && result;

        public int SlowMo =>
            int.TryParse(_configuration["Browser:SlowMo"], out var result) ? result : 0;

        #endregion

        #region Application Settings

        public string BaseUrl =>
            _configuration["Application:BaseUrl"] ?? throw new Exception("BaseUrl is not configured.");

        #endregion

        #region Timeout Settings

        public int DefaultTimeout =>
            int.TryParse(_configuration["Timeouts:DefaultTimeout"], out var result) ? result : 30000;

        public int NavigationTimeout =>
            int.TryParse(_configuration["Timeouts:NavigationTimeout"], out var result) ? result : 60000;

        #endregion

        #region Generic Getter

        public string GetValue(string key)
        {
            return _configuration[key] ?? throw new Exception($"Configuration key '{key}' not found.");
        }

        #endregion
    }
}