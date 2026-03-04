using Allure.Commons;
using Allure.Net.Commons;
using System;
using System.Threading.Tasks;

namespace PlaywrightCSharpFramework.Framework.Helpers
{
    public static class RetryHelper
    {
        /// <summary>
        /// Retry async action without return value
        /// </summary>
        public static async Task ExecuteAsync(
            Func<Task> action,
            int retryCount = 2,
            int delayMilliseconds = 500,
            string stepDescription = "Retry Operation")
        {
            AllureApi.Step($"{stepDescription} (Max Retries: {retryCount})");

            for (int attempt = 1; attempt <= retryCount + 1; attempt++)
            {
                try
                {
                    Logger.Debug($"Attempt {attempt} started.");
                    await action();
                    Logger.Info($"Operation succeeded on attempt {attempt}.");
                    return;
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Attempt {attempt} failed: {ex.Message}");

                    if (attempt > retryCount)
                    {
                        Logger.Error("All retry attempts failed.", ex);
                        throw;
                    }

                    await Task.Delay(delayMilliseconds);
                }
            }
        }

        /// <summary>
        /// Retry async function with return value
        /// </summary>
        public static async Task<T> ExecuteAsync<T>(
            Func<Task<T>> action,
            int retryCount = 2,
            int delayMilliseconds = 500,
            string stepDescription = "Retry Operation")
        {
            AllureApi.Step($"{stepDescription} (Max Retries: {retryCount})");

            for (int attempt = 1; attempt <= retryCount + 1; attempt++)
            {
                try
                {
                    Logger.Debug($"Attempt {attempt} started.");
                    var result = await action();
                    Logger.Info($"Operation succeeded on attempt {attempt}.");
                    return result;
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Attempt {attempt} failed: {ex.Message}");

                    if (attempt > retryCount)
                    {
                        Logger.Error("All retry attempts failed.", ex);
                        throw;
                    }

                    await Task.Delay(delayMilliseconds);
                }
            }

            throw new Exception("RetryHelper reached unexpected state.");
        }
    }
}