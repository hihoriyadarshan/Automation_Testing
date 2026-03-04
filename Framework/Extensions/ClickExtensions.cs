using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Extensions
{
    public static class ClickExtensions
    {
        /// <summary>
        /// Click using selector
        /// </summary>
        public static async Task ClickAsync(this IPage page, string selector)
        {
            AllureApi.Step($"Click on element: {selector}");

            var locator = page.Locator(selector);
            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClickAsync();
        }

        /// <summary>
        /// Click using locator
        /// </summary>
        public static async Task ClickAsync(this ILocator locator)
        {
            AllureApi.Step($"Click on locator: {locator}");

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClickAsync();
        }

        /// <summary>
        /// Force click (ignores visibility checks)
        /// </summary>
        public static async Task ForceClickAsync(this IPage page, string selector)
        {
            AllureApi.Step($"Force Click on element: {selector}");

            var locator = page.Locator(selector);

            await locator.ClickAsync(new LocatorClickOptions
            {
                Force = true
            });
        }

        /// <summary>
        /// Click with custom timeout
        /// </summary>
        public static async Task ClickAsync(this IPage page, string selector, int timeout)
        {
            AllureApi.Step($"Click on element: {selector} with timeout: {timeout}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                Timeout = timeout,
                State = WaitForSelectorState.Visible
            });

            await locator.ClickAsync(new LocatorClickOptions
            {
                Timeout = timeout
            });
        }

        /// <summary>
        /// Click with retry logic
        /// </summary>
        public static async Task ClickWithRetryAsync(this IPage page, string selector, int retryCount = 2)
        {
            AllureApi.Step($"Click with retry on element: {selector}");

            var locator = page.Locator(selector);

            for (int i = 0; i <= retryCount; i++)
            {
                try
                {
                    await locator.WaitForAsync(new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible
                    });

                    await locator.ClickAsync();
                    return;
                }
                catch (Exception)
                {
                    if (i == retryCount)
                        throw;

                    await Task.Delay(500);
                }
            }
        }
    }
}