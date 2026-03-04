using Allure.Commons;
using Allure.Net.Commons;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Extensions
{
    public static class WaitExtensions
    {
        #region Element Waits

        public static async Task WaitForVisibleAsync(this IPage page, string selector, int timeout = 30000)
        {
            AllureApi.Step($"Wait for element visible: {selector}");

            await page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeout
            });
        }

        public static async Task WaitForHiddenAsync(this IPage page, string selector, int timeout = 30000)
        {
            AllureApi.Step($"Wait for element hidden: {selector}");

            await page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Hidden,
                Timeout = timeout
            });
        }

        public static async Task WaitForAttachedAsync(this IPage page, string selector, int timeout = 30000)
        {
            AllureApi.Step($"Wait for element attached: {selector}");

            await page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Attached,
                Timeout = timeout
            });
        }

        public static async Task WaitForDetachedAsync(this IPage page, string selector, int timeout = 30000)
        {
            AllureApi.Step($"Wait for element detached: {selector}");

            await page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Detached,
                Timeout = timeout
            });
        }

        public static async Task WaitForEnabledAsync(this ILocator locator, int timeout = 30000)
        {
            AllureApi.Step("Wait for element enabled");

            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeout
            });

            await Assertions.Expect(locator).ToBeEnabledAsync(new()
            {
                Timeout = timeout
            });
        }

        public static async Task WaitForDisabledAsync(this ILocator locator, int timeout = 30000)
        {
            AllureApi.Step("Wait for element disabled");

            await Assertions.Expect(locator).ToBeDisabledAsync(new()
            {
                Timeout = timeout
            });
        }

        #endregion
        #region Page Waits

        public static async Task WaitForPageLoadAsync(this IPage page, LoadState state = LoadState.Load)
        {
            AllureApi.Step($"Wait for page load state: {state}");

            await page.WaitForLoadStateAsync(state);
        }

        public static async Task WaitForNetworkIdleAsync(this IPage page)
        {
            AllureApi.Step("Wait for network idle");

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public static async Task WaitForUrlAsync(this IPage page, string urlPart, int timeout = 30000)
        {
            AllureApi.Step($"Wait for URL contains: {urlPart}");

            await page.WaitForURLAsync($"**{urlPart}**", new PageWaitForURLOptions
            {
                Timeout = timeout
            });
        }
        #endregion
    }
}