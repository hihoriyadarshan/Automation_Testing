using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightCSharpFramework.Framework.Config;

namespace PlaywrightCSharpFramework.Framework.Drivers
{
    public class BrowserManager
    {
        private IPlaywright _playwright;
        private IBrowser _browser;

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
        }

        public async Task<IBrowser> LaunchBrowserAsync()
        {
            if (_playwright == null)
                throw new Exception("Playwright is not initialized.");

            var config = ConfigReader.Instance;

            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = config.Headless,
                SlowMo = config.SlowMo,
                Timeout = config.GetValue("Browser:LaunchTimeout") != null
                    ? int.Parse(config.GetValue("Browser:LaunchTimeout"))
                    : 30000
            };

            if (!string.IsNullOrEmpty(config.GetValue("Browser:Channel")))
            {
                launchOptions.Channel = config.GetValue("Browser:Channel");
            }

            _browser = config.BrowserType.ToLower() switch
            {
                "chromium" => await _playwright.Chromium.LaunchAsync(launchOptions),
                "firefox" => await _playwright.Firefox.LaunchAsync(launchOptions),
                "webkit" => await _playwright.Webkit.LaunchAsync(launchOptions),
                _ => throw new ArgumentException($"Unsupported browser: {config.BrowserType}")
            };

            return _browser;
        }

        public async Task<IBrowserContext> CreateContextAsync()
        {
            if (_browser == null)
                throw new Exception("Browser is not launched.");

            var config = ConfigReader.Instance;

            var contextOptions = new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                },
                IgnoreHTTPSErrors = true
            };

            // Enable video recording if configured
            if (bool.TryParse(config.GetValue("Video:Enabled"), out var videoEnabled) && videoEnabled)
            {
                var videoPath = config.GetValue("Video:Path");
                Directory.CreateDirectory(videoPath);

                contextOptions.RecordVideoDir = videoPath;
            }

            var context = await _browser.NewContextAsync(contextOptions);

            // Enable tracing if configured
            if (bool.TryParse(config.GetValue("Tracing:Enabled"), out var tracingEnabled) && tracingEnabled)
            {
                await context.Tracing.StartAsync(new TracingStartOptions
                {
                    Screenshots = bool.Parse(config.GetValue("Tracing:Screenshots") ?? "true"),
                    Snapshots = bool.Parse(config.GetValue("Tracing:Snapshots") ?? "true"),
                    Sources = bool.Parse(config.GetValue("Tracing:Sources") ?? "true")
                });
            }

            return context;
        }

        public async Task<IPage> CreatePageAsync(IBrowserContext context)
        {
            var page = await context.NewPageAsync();

            var config = ConfigReader.Instance;

            page.SetDefaultTimeout(config.DefaultTimeout);
            page.SetDefaultNavigationTimeout(config.NavigationTimeout);

            return page;
        }

        public async Task StopTracingAsync(IBrowserContext context)
        {
            var config = ConfigReader.Instance;

            if (bool.TryParse(config.GetValue("Tracing:Enabled"), out var tracingEnabled) && tracingEnabled)
            {
                var traceDir = Path.Combine("Reports", "Traces");
                Directory.CreateDirectory(traceDir);

                var tracePath = Path.Combine(traceDir, $"trace_{DateTime.Now:yyyyMMdd_HHmmss}.zip");

                await context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = tracePath
                });
            }
        }

        public async Task DisposeAsync()
        {
            if (_browser != null)
                await _browser.CloseAsync();

            _playwright?.Dispose();
        }
    }
}