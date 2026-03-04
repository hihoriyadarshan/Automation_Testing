using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Extensions
{
    public static class DoubleClickExtensions
    {
        /// <summary>
        /// Double click using selector
        /// </summary>
        public static async Task DoubleClickAsync(this IPage page, string selector)
        {
            AllureApi.Step($"Double Click on element: {selector}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.DblClickAsync();
        }

        /// <summary>
        /// Double click using locator
        /// </summary>
        public static async Task DoubleClickAsync(this ILocator locator)
        {
            AllureApi.Step($"Double Click on locator");

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.DblClickAsync();
        }

        /// <summary>
        /// Force double click (ignores visibility checks)
        /// </summary>
        public static async Task ForceDoubleClickAsync(this IPage page, string selector)
        {
            AllureApi.Step($"Force Double Click on element: {selector}");

            var locator = page.Locator(selector);

            await locator.DblClickAsync(new LocatorDblClickOptions
            {
                Force = true
            });
        }

        /// <summary>
        /// Double click with custom timeout
        /// </summary>
        public static async Task DoubleClickAsync(this IPage page, string selector, int timeout)
        {
            AllureApi.Step($"Double Click on element: {selector} with timeout: {timeout}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                Timeout = timeout,
                State = WaitForSelectorState.Visible
            });

            await locator.DblClickAsync(new LocatorDblClickOptions
            {
                Timeout = timeout
            });
        }

        /// <summary>
        /// Double click with retry logic
        /// </summary>
        public static async Task DoubleClickWithRetryAsync(this IPage page, string selector, int retryCount = 2)
        {
            AllureApi.Step($"Double Click with retry on element: {selector}");

            var locator = page.Locator(selector);

            for (int i = 0; i <= retryCount; i++)
            {
                try
                {
                    await locator.WaitForAsync(new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible
                    });

                    await locator.DblClickAsync();
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