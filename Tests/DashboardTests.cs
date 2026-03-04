//using System.Threading.Tasks;
//using NUnit.Framework;
//using Allure.NUnit.Attributes;
//using Allure.Commons;
//using PlaywrightCSharpFramework.Framework.Base;
//using PlaywrightCSharpFramework.Framework.Helpers;
//using PlaywrightCSharpFramework.Pages;

//namespace PlaywrightCSharpFramework.Tests
//{
//    [TestFixture]
//    [AllureFeature("Dashboard Feature")]
//    public class DashboardTests : BaseTest
//    {
//        private LoginPage _loginPage;
//        private DashboardPage _dashboardPage;

//        #region Setup

//        [SetUp]
//        public void InitPages()
//        {
//            _loginPage = new LoginPage(Page);
//            _dashboardPage = new DashboardPage(Page);
//        }

//        #endregion

//        #region Tests

//        [Test]
//        [AllureStory("Verify Dashboard Loads Successfully After Login")]
//        public async Task Verify_Dashboard_Loads_After_Login()
//        {
//            Logger.Info("Starting Dashboard load verification test");

//            // Arrange
//            await _loginPage.NavigateToLoginAsync();

//            // Act
//            await _loginPage.LoginAsync("admin", "1234");

//            // Assert
//            Assert.IsTrue(await _dashboardPage.IsDashboardLoadedAsync(),
//                "Dashboard should be loaded successfully after login.");

//            await _dashboardPage.CaptureDashboardScreenshotAsync();
//        }

//        [Test]
//        [AllureStory("Verify Welcome Message Displayed")]
//        public async Task Verify_Welcome_Message_Displayed()
//        {
//            Logger.Info("Starting Welcome message validation test");

//            // Arrange
//            await _loginPage.NavigateToLoginAsync();
//            await _loginPage.LoginAsync("admin", "1234");

//            // Act
//            var welcomeMessage = await _dashboardPage.GetWelcomeMessageAsync();

//            // Assert
//            Assert.IsNotNull(welcomeMessage, "Welcome message should not be null.");
//            Assert.IsTrue(welcomeMessage.Contains("Welcome"),
//                "Welcome message should contain 'Welcome'.");

//            await _dashboardPage.CaptureDashboardScreenshotAsync();
//        }

//        [Test]
//        [AllureStory("Verify User Can Logout Successfully")]
//        public async Task Verify_User_Can_Logout()
//        {
//            Logger.Info("Starting Logout verification test");

//            // Arrange
//            await _loginPage.NavigateToLoginAsync();
//            await _loginPage.LoginAsync("admin", "1234");


//            // Act
//            await _dashboardPage.LogoutAsync();

//            // Assert
//            Assert.IsTrue(await _loginPage.IsVisibleAsync("#loginButton"),
//                "User should be redirected to Login page after logout.");

//            await _dashboardPage.CaptureDashboardScreenshotAsync();
//        }

//        #endregion
//    }
//}