namespace PlaywrightCSharpFramework.Framework.Models
{
    /// <summary>
    /// Login Test Data Model
    /// Used to map Excel login test data
    /// </summary>
    public class LoginTestDataModel : TestDataModel
    {
        /// <summary>
        /// Username used for login
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password used for login
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}