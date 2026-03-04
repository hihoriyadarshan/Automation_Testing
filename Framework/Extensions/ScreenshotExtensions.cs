using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using PlaywrightCSharpFramework.Framework.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Extensions
{
    public static class ScreenshotExtensions
    {
        private static string GetScreenshotDirectory()
        {
            var configPath = ConfigReader.Instance.GetValue("Screenshots:Path");
            var directory = Path.Combine(Directory.GetCurrentDirectory(), configPath);

            Directory.CreateDirectory(directory);
            return directory;
        }

        private static string GenerateFileName(string name)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return $"{name}_{timestamp}.png";
        }

        /// <summary>
        /// Capture full page screenshot
        /// </summary>
        public static async Task CaptureFullPageAsync(this IPage page, string name = "FullPage")
        {
            AllureApi.Step($"Capture full page screenshot: {name}");

            var directory = GetScreenshotDirectory();
            var fileName = GenerateFileName(name);
            var filePath = Path.Combine(directory, fileName);

            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = filePath,
                FullPage = true
            });

            AttachToAllure(filePath);
        }

        /// <summary>
        /// Capture screenshot of specific selector
        /// </summary>
        public static async Task CaptureElementAsync(this IPage page, string selector, string name = "Element")
        {
            AllureApi.Step($"Capture element screenshot: {selector}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            var directory = GetScreenshotDirectory();
            var fileName = GenerateFileName(name);
            var filePath = Path.Combine(directory, fileName);

            await locator.ScreenshotAsync(new LocatorScreenshotOptions
            {
                Path = filePath
            });

            AttachToAllure(filePath);
        }

        /// <summary>
        /// Capture screenshot using locator
        /// </summary>
        public static async Task CaptureElementAsync(this ILocator locator, string name = "Element")
        {
            AllureApi.Step("Capture locator screenshot");

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            var directory = GetScreenshotDirectory();
            var fileName = GenerateFileName(name);
            var filePath = Path.Combine(directory, fileName);

            await locator.ScreenshotAsync(new LocatorScreenshotOptions
            {
                Path = filePath
            });

            AttachToAllure(filePath);
        }

        /// <summary>
        /// Attach screenshot to Allure report
        /// </summary>
        private static void AttachToAllure(string filePath)
        {
            if (File.Exists(filePath))
            {
                AllureApi.AddAttachment(
                    "Screenshot",
                    "image/png",
                    filePath
                );
            }
        }
    }
}