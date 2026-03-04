using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Extensions
{
    public static class SlowFillExtensions
    {
        /// <summary>
        /// Slowly type text into element (default delay 100ms)
        /// </summary>
        public static async Task SlowFillAsync(this IPage page, string selector, string value, int delay = 100)
        {
            AllureApi.Step($"Slow Fill element: {selector} with value: {value}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClearAsync();

            foreach (char character in value)
            {
                await locator.TypeAsync(character.ToString(), new LocatorTypeOptions
                {
                    Delay = delay
                });
            }
        }

        /// <summary>
        /// Slowly type text using locator
        /// </summary>
        public static async Task SlowFillAsync(this ILocator locator, string value, int delay = 100)
        {
            AllureApi.Step($"Slow Fill locator with value: {value}");

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClearAsync();

            foreach (char character in value)
            {
                await locator.TypeAsync(character.ToString(), new LocatorTypeOptions
                {
                    Delay = delay
                });
            }
        }

        /// <summary>
        /// Clear and slow fill
        /// </summary>
        public static async Task ClearAndSlowFillAsync(this IPage page, string selector, string value, int delay = 100)
        {
            AllureApi.Step($"Clear and Slow Fill element: {selector}");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClearAsync();

            foreach (char character in value)
            {
                await locator.TypeAsync(character.ToString(), new LocatorTypeOptions
                {
                    Delay = delay
                });
            }
        }

        /// <summary>
        /// Slow fill and press Enter
        /// </summary>
        public static async Task SlowFillAndPressEnterAsync(this IPage page, string selector, string value, int delay = 100)
        {
            AllureApi.Step($"Slow Fill element: {selector} and press Enter");

            var locator = page.Locator(selector);

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await locator.ClearAsync();

            foreach (char character in value)
            {
                await locator.TypeAsync(character.ToString(), new LocatorTypeOptions
                {
                    Delay = delay
                });
            }

            await locator.PressAsync("Enter");
        }
    }
}