using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace PlaywrightCSharpFramework.Framework.Base
{
    public class PlaywrightFactory
    {
        private readonly IConfiguration _configuration;

        private IPlaywright _playwright;
        private IBrowser _browser;

        public PlaywrightFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Initialize Playwright instance
        /// </summary>
        public async Task InitializeAsync()
        {
            _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        }

        /// <summary>
        /// Launch browser based on config
        /// </summary>
        public async Task<IBrowser> LaunchBrowserAsync()
        {
            if (_playwright == null)
                throw new Exception("Playwright is not initialized. Call InitializeAsync() first.");

            string browserType = _configuration["Browser:Type"] ?? "chromium";
            bool headless = bool.Parse(_configuration["Browser:Headless"] ?? "true");

            _browser = browserType.ToLower() switch
            {
                "chromium" => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless
                }),

                "firefox" => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless
                }),

                "webkit" => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless
                }),

                _ => throw new ArgumentException($"Unsupported browser type: {browserType}")
            };

            return _browser;
        }

        /// <summary>
        /// Create new browser context
        /// </summary>
        public async Task<IBrowserContext> CreateContextAsync()
        {
            if (_browser == null)
                throw new Exception("Browser is not launched. Call LaunchBrowserAsync() first.");

            return await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                },
                IgnoreHTTPSErrors = true
            });
        }

        /// <summary>
        /// Create new page
        /// </summary>
        public async Task<IPage> CreatePageAsync(IBrowserContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var page = await context.NewPageAsync();

            page.SetDefaultTimeout(30000);
            page.SetDefaultNavigationTimeout(60000);

            return page;
        }

        /// <summary>
        /// Close browser
        /// </summary>
        public async Task DisposeAsync()
        {
            if (_browser != null)
                await _browser.CloseAsync();

            _playwright?.Dispose();
        }
    }
}