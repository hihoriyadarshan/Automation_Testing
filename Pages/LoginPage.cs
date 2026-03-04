using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightCSharpFramework.Framework.Helpers;
using PlaywrightCSharpFramework.Framework.Extensions;

namespace PlaywrightCSharpFramework.Pages
{
    public class LoginPage : BasePage
    {
        #region Selectors

        private const string UsernameInput = "//input[@data-qa='login-email']";
        private const string PasswordInput = "//input[@data-qa='login-password']";
        private const string LoginButton = "//button[@data-qa='login-button']";
        //private const string ErrorMessage = ".error-message";
        //private const string DashboardHeader = "#dashboard";

        #endregion

        public LoginPage(IPage page) : base(page)
        {
        }


        #region Actions

        public async Task NavigateToLoginAsync()
        {
            Logger.Info("Navigating to Login page");
            await NavigateToAsync("login");
        }

        public async Task EnterUsernameAsync(string username)
        {
            Logger.Debug($"Entering username: {username}");
            await FillAsync(UsernameInput, username);
        }

        public async Task EnterPasswordAsync(string password)
        {
            Logger.Debug("Entering password");
            await FillAsync(PasswordInput, password);
        }

        public async Task ClickLoginAsync()
        {
            Logger.Debug("Clicking Login button");
            await ClickAsync(LoginButton);
        }

        public async Task LoginAsync(string username, string password)
        {
            Logger.Info("Performing login");

            await EnterUsernameAsync(username);
            await EnterPasswordAsync(password);
            await ClickLoginAsync();
        }

        #endregion

        #region Validations

        //public async Task<bool> IsLoginSuccessfulAsync()
        //{
        //    Logger.Info("Validating successful login");

        //    await Page.WaitForPageLoadAsync();

        //    return await IsVisibleAsync(DashboardHeader);
        //}

        //public async Task<string> GetErrorMessageAsync()
        //{
        //    Logger.Info("Fetching login error message");

        //    await WaitForVisibleAsync(ErrorMessage);

        //    return await GetTextAsync(ErrorMessage);
        //}

        #endregion

        #region Utilities

        public async Task CaptureLoginScreenshotAsync()
        {
            await TakeScreenshotAsync("LoginPage");
        }

        #endregion
    }
}