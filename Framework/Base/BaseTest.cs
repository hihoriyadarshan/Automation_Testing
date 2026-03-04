using Allure.Commons;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Base
{
    [AllureNUnit]
    public class BaseTest
    {
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        protected IBrowserContext Context;
        protected IPage Page;
        protected IConfiguration Configuration;

        #region Setup

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            // Load configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Framework/Config/appsettings.json", optional: false)
                .Build();

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        }

        [SetUp]
        [AllureStep("Test Setup - Launch Browser")]
        public async Task Setup()
        {
            var browserType = Configuration["Browser:Type"];
            var headless = bool.Parse(Configuration["Browser:Headless"]);

            Browser = browserType.ToLower() switch
            {
                "chromium" => await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless
                }),

                "firefox" => await Playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless
                }),

                "webkit" => await Playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless
                }),

                _ => throw new ArgumentException("Invalid browser type in configuration")
            };

            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                IgnoreHTTPSErrors = true
            });

            Page = await Context.NewPageAsync();

            Page.SetDefaultTimeout(30000);
            Page.SetDefaultNavigationTimeout(60000);

            await Page.GotoAsync(Configuration["Application:BaseUrl"]);
        }

        #endregion

        #region TearDown

        [TearDown]
        [AllureStep("Test TearDown")]
        public async Task TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                await CaptureScreenshot();
            }

            if (Context != null)
                await Context.CloseAsync();

            if (Browser != null)
                await Browser.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task GlobalTearDown()
        {
            Playwright?.Dispose();
            await Task.CompletedTask;
        }

        #endregion

        #region Helpers

        private async Task CaptureScreenshot()
        {
            var screenshotDir = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "Screenshots");
            Directory.CreateDirectory(screenshotDir);

            var filePath = Path.Combine(
                screenshotDir,
                $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
            );

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = filePath,
                FullPage = true
            });

            AllureApi.AddAttachment(
                "Failure Screenshot",
                "image/png",
                filePath
            );
        }

        #endregion
    }
}