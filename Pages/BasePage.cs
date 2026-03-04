using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightCSharpFramework.Framework.Config;
using PlaywrightCSharpFramework.Framework.Helpers;
using PlaywrightCSharpFramework.Framework.Utilities;
using PlaywrightCSharpFramework.Framework.Extensions;

namespace PlaywrightCSharpFramework.Pages
{
    public abstract class BasePage
    {
        protected readonly IPage Page;

        protected BasePage(IPage page)
        {
            Page = page;
        }

        #region Navigation

        public virtual async Task NavigateToAsync(string relativeUrl = "")
        {
            var baseUrl = ConfigReader.Instance.BaseUrl;

            var fullUrl = string.IsNullOrEmpty(relativeUrl)
                ? baseUrl
                : $"{baseUrl}/{relativeUrl}";

            Logger.Info($"Navigating to: {fullUrl}");

            await Page.GotoAsync(fullUrl);
            await Page.WaitForPageLoadAsync();
        }

        public async Task RefreshAsync()
        {
            Logger.Info("Refreshing page");
            await Page.ReloadAsync();
            await Page.WaitForPageLoadAsync();
        }

        #endregion

        #region Common Actions

        protected async Task ClickAsync(string selector)
        {
            Logger.Debug($"Clicking: {selector}");
            await Page.ClickAsync(selector);
        }

        protected async Task FillAsync(string selector, string value)
        {
            Logger.Debug($"Filling: {selector} with {value}");
            await Page.FillAsync(selector, value);
        }

        protected async Task SlowFillAsync(string selector, string value)
        {
            Logger.Debug($"Slow filling: {selector}");
            await Page.SlowFillAsync(selector, value);
        }

        protected async Task WaitForVisibleAsync(string selector)
        {
            Logger.Debug($"Waiting for visible: {selector}");
            await Page.WaitForVisibleAsync(selector);
        }

        protected async Task TakeScreenshotAsync(string name)
        {
            Logger.Info($"Capturing screenshot: {name}");
            await Page.CaptureFullPageAsync(name);
        }

        #endregion

        #region Validation Helpers

        protected async Task<bool> IsVisibleAsync(string selector)
        {
            var locator = Page.Locator(selector);
            return await locator.IsVisibleAsync();
        }

        protected async Task<string> GetTextAsync(string selector)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            return await locator.InnerTextAsync();
        }

        #endregion
    }
}