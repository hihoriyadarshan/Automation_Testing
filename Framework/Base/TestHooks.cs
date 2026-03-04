using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Base
{
    public static class TestHooks
    {
        /// <summary>
        /// Capture screenshot and attach to Allure if test fails
        /// </summary>
        public static async Task CaptureFailureAsync(IPage page)
        {
            if (page == null)
                return;

            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;

            var testName = TestContext.CurrentContext.Test.Name;
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            var screenshotDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Reports",
                "Screenshots"
            );

            Directory.CreateDirectory(screenshotDir);

            var filePath = Path.Combine(
                screenshotDir,
                $"{testName}_{timestamp}.png"
            );

            await page.ScreenshotAsync(new PageScreenshotOptions
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

        /// <summary>
        /// Attach Playwright trace file to Allure
        /// </summary>
        public static void AttachTrace(string traceFilePath)
        {
            if (File.Exists(traceFilePath))
            {
                AllureApi.AddAttachment(
                    "Playwright Trace",
                    "application/zip",
                    traceFilePath
                );
            }
        }

        /// <summary>
        /// Attach text log to Allure
        /// </summary>
        public static void AttachTextLog(string name, string content)
        {
            AllureApi.AddAttachment(
                name,
                "text/plain",
                System.Text.Encoding.UTF8.GetBytes(content)
            );
        }
    }
}