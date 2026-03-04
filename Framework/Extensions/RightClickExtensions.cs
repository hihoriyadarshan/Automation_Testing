using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Extensions
{
    public static class RightClickExtensions
    {
        /// <summary>
        /// Right click using selector
        /// </summary>
        public static async Task RightClickAsync(this IPage page, string selector)
        {
            AllureApi.Step($"Right Click on element: {selector}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClickAsync(new LocatorClickOptions
            {
                Button = MouseButton.Right
            });
        }

        /// <summary>
        /// Right click using locator
        /// </summary>
        public static async Task RightClickAsync(this ILocator locator)
        {
            AllureApi.Step("Right Click on locator");

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClickAsync(new LocatorClickOptions
            {
                Button = MouseButton.Right
            });
        }

        /// <summary>
        /// Force right click (ignores visibility)
        /// </summary>
        public static async Task ForceRightClickAsync(this IPage page, string selector)
        {
            AllureApi.Step($"Force Right Click on element: {selector}");

            var locator = page.Locator(selector);

            await locator.ClickAsync(new LocatorClickOptions
            {
                Button = MouseButton.Right,
                Force = true
            });
        }

        /// <summary>
        /// Right click with custom timeout
        /// </summary>
        public static async Task RightClickAsync(this IPage page, string selector, int timeout)
        {
            AllureApi.Step($"Right Click on element: {selector} with timeout: {timeout}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                Timeout = timeout,
                State = WaitForSelectorState.Visible
            });

            await locator.ClickAsync(new LocatorClickOptions
            {
                Button = MouseButton.Right,
                Timeout = timeout
            });
        }

        /// <summary>
        /// Right click with retry logic
        /// </summary>
        public static async Task RightClickWithRetryAsync(this IPage page, string selector, int retryCount = 2)
        {
            AllureApi.Step($"Right Click with retry on element: {selector}");

            var locator = page.Locator(selector);

            for (int i = 0; i <= retryCount; i++)
            {
                try
                {
                    await locator.WaitForAsync(new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible
                    });

                    await locator.ClickAsync(new LocatorClickOptions
                    {
                        Button = MouseButton.Right
                    });

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