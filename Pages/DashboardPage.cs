using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightCSharpFramework.Framework.Helpers;
using PlaywrightCSharpFramework.Framework.Extensions;

namespace PlaywrightCSharpFramework.Pages
{
    public class DashboardPage : BasePage
    {
        #region Selectors

        private const string DashboardHeader = "#dashboardHeader";
        private const string UserProfileIcon = "#userProfile";
        private const string LogoutButton = "#logout";
        private const string SidebarMenu = "#sidebar";
        private const string WelcomeMessage = ".welcome-message";
        private const string Loader = ".loading";

        #endregion

        public DashboardPage(IPage page) : base(page)
        {
        }

        #region Navigation Validation

        public async Task<bool> IsDashboardLoadedAsync()
        {
            Logger.Info("Validating Dashboard is loaded");

            await Page.WaitForPageLoadAsync();
            await Page.WaitForVisibleAsync(DashboardHeader);

            return await IsVisibleAsync(DashboardHeader);
        }

        #endregion

        #region User Actions

        public async Task OpenUserProfileAsync()
        {
            Logger.Info("Opening user profile");

            await ClickAsync(UserProfileIcon);
        }

        public async Task LogoutAsync()
        {
            Logger.Info("Logging out from application");

            await OpenUserProfileAsync();
            await ClickAsync(LogoutButton);
        }

        public async Task ClickSidebarMenuAsync(string menuSelector)
        {
            Logger.Info($"Clicking sidebar menu: {menuSelector}");

            await Page.WaitForVisibleAsync(SidebarMenu);
            await ClickAsync(menuSelector);
        }

        #endregion

        #region Validations

        public async Task<string> GetWelcomeMessageAsync()
        {
            Logger.Info("Fetching welcome message");

            await WaitForVisibleAsync(WelcomeMessage);
            return await GetTextAsync(WelcomeMessage);
        }

        public async Task WaitForLoaderToDisappearAsync()
        {
            Logger.Debug("Waiting for loader to disappear");

            await Page.WaitForHiddenAsync(Loader);
        }

        #endregion

        #region Utilities

        public async Task CaptureDashboardScreenshotAsync()
        {
            await TakeScreenshotAsync("DashboardPage");
        }

        #endregion
    }
}