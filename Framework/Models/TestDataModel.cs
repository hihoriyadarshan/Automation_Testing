using System;

namespace PlaywrightCSharpFramework.Framework.Models
{
    /// <summary>
    /// Base Test Data Model
    /// Used as a parent class for all Excel-driven test models
    /// </summary>
    public class TestDataModel
    {
        /// <summary>
        /// Indicates whether test should run (Y/N or True/False)
        /// </summary>
        public string Run { get; set; } = string.Empty;

        /// <summary>
        /// Test case unique identifier
        /// </summary>
        public string TestCaseId { get; set; } = string.Empty;

        /// <summary>
        /// Test case description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Expected result for validation
        /// </summary>
        public string ExpectedResult { get; set; } = string.Empty;

        /// <summary>
        /// Optional environment override
        /// </summary>
        public string Environment { get; set; } = string.Empty;

        /// <summary>
        /// Determines if test should execute based on Run column
        /// </summary>
        public bool ShouldRun()
        {
            if (string.IsNullOrWhiteSpace(Run))
                return false;

            return Run.Equals("Y", StringComparison.OrdinalIgnoreCase)
                || Run.Equals("Yes", StringComparison.OrdinalIgnoreCase)
                || Run.Equals("True", StringComparison.OrdinalIgnoreCase)
                || Run.Equals("1");
        }

        /// <summary>
        /// Determines if expected result represents success
        /// </summary>
        public bool IsExpectedSuccess()
        {
            if (string.IsNullOrWhiteSpace(ExpectedResult))
                return false;

            return ExpectedResult.Equals("Success", StringComparison.OrdinalIgnoreCase)
                || ExpectedResult.Equals("Pass", StringComparison.OrdinalIgnoreCase)
                || ExpectedResult.Equals("True", StringComparison.OrdinalIgnoreCase);
        }
    }
}