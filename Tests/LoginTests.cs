using Allure.Commons;
using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using PlaywrightCSharpFramework.Framework.Base;
using PlaywrightCSharpFramework.Framework.Helpers;
using PlaywrightCSharpFramework.Framework.Models;
using PlaywrightCSharpFramework.Framework.Utilities;
using PlaywrightCSharpFramework.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Tests
{
    [TestFixture]
    [AllureFeature("Login Feature")]
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage;
        private DashboardPage _dashboardPage;
        private List<LoginTestDataModel> _testData;

        #region Setup

        [OneTimeSetUp]
        public void LoadTestData()
        {
            var filePath = PathManager.GetTestDataFile("D:/Automation_Testing/TestData/TestData.xlsx");
            var excel = new ExcelReader(filePath);

            _testData = excel.ReadSheetAs<LoginTestDataModel>("LoginData");
        }

        [SetUp]
        public void InitPages()
        {
            _loginPage = new LoginPage(Page);
            _dashboardPage = new DashboardPage(Page);
        }

        #endregion

        #region Tests

        [Test]
        [AllureStory("Verify Login Scenarios From Excel")]
        public async Task Verify_Login_Scenarios_From_Excel()
        {
            foreach (var data in _testData)
            {
                //if (!data.ShouldRun())
                //{
                //    Logger.Info($"Skipping TestCase: {data.TestCaseId}");
                //    continue;
                //}

                AllureApi.Step($"Executing TestCase: {data.TestCaseId}");
                Logger.Info($"Executing TestCase: {data.TestCaseId} - {data.Description}");

                // Arrange
                await _loginPage.NavigateToLoginAsync();

                // Act

                await _loginPage.LoginAsync(data.Username, data.Password);

                // Assert
                if (data.ExpectedResult.Equals("Success"))
                {
                    var dashboardLoaded = await _dashboardPage.IsDashboardLoadedAsync();

                    Assert.That(dashboardLoaded, Is.True,
                        $"Login should succeed for {data.TestCaseId}");
                }
                else
                {
                    //var errorMessage = await _loginPage.GetErrorMessageAsync();

                    //Assert.That(errorMessage, Is.Not.Null.And.Not.Empty,
                        //$"Error message should appear for {data.TestCaseId}");
                }

                await _loginPage.CaptureLoginScreenshotAsync();
            }
        }

        #endregion
    }
}