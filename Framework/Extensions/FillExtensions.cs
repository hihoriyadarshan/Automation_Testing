using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Extensions
{
    public static class FillExtensions
    {
        /// <summary>
        /// Fill input using selector
        /// </summary>
        public static async Task FillAsync(this IPage page, string selector, string value)
        {
            AllureApi.Step($"Fill element: {selector} with value: {value}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.FillAsync(value);
        }

        /// <summary>
        /// Fill using locator
        /// </summary>
        public static async Task FillAsync(this ILocator locator, string value)
        {
            AllureApi.Step($"Fill locator with value: {value}");

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.FillAsync(value);
        }

        /// <summary>
        /// Clear then fill input
        /// </summary>
        public static async Task ClearAndFillAsync(this IPage page, string selector, string value)
        {
            AllureApi.Step($"Clear and Fill element: {selector} with value: {value}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClearAsync();
            await locator.FillAsync(value);
        }

        /// <summary>
        /// Fill with custom timeout
        /// </summary>
        public static async Task FillAsync(this IPage page, string selector, string value, int timeout)
        {
            AllureApi.Step($"Fill element: {selector} with value: {value} and timeout: {timeout}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                Timeout = timeout,
                State = WaitForSelectorState.Visible
            });

            await locator.FillAsync(value, new LocatorFillOptions
            {
                Timeout = timeout
            });
        }

        /// <summary>
        /// Fill with retry logic
        /// </summary>
        public static async Task FillWithRetryAsync(this IPage page, string selector, string value, int retryCount = 2)
        {
            AllureApi.Step($"Fill with retry on element: {selector}");

            var locator = page.Locator(selector);

            for (int i = 0; i <= retryCount; i++)
            {
                try
                {
                    await locator.WaitForAsync(new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible
                    });

                    await locator.FillAsync(value);
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

        /// <summary>
        /// Fill and press Enter
        /// </summary>
        public static async Task FillAndPressEnterAsync(this IPage page, string selector, string value)
        {
            AllureApi.Step($"Fill element: {selector} and press Enter");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.FillAsync(value);
            await locator.PressAsync("Enter");
        }
    }
}