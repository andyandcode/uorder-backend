using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace Utilities.Common
{
    public class CustomStatus
    {
        public static IActionResult NotFoundAccount()
        {
            var result = new ContentResult
            {
                StatusCode = SystemConstants.NotFoundAccountCode,
                Content = "{\"message\": \"Not Found Account.\"}",
                ContentType = "application/json"
            };

            return result;
        }

        public static IActionResult AccountLocked()
        {
            var result = new ContentResult
            {
                StatusCode = SystemConstants.AccountLockedCode,
                Content = "{\"message\": \"Account has been locked.\"}",
                ContentType = "application/json"
            };

            return result;
        }

        public static IActionResult ResetSuccessfully()
        {
            var result = new ContentResult
            {
                StatusCode = SystemConstants.ResetSuccessfullyCode,
                Content = "{\"message\": \"Reset Successfully.\"}",
                ContentType = "application/json"
            };

            return result;
        }

        public static IActionResult PasswordsNotMatch()
        {
            var result = new ContentResult
            {
                StatusCode = SystemConstants.PasswordsNotMatchCode,
                Content = "{\"message\": \"Passwords Not Match.\"}",
                ContentType = "application/json"
            };

            return result;
        }

        public static IActionResult PasswordsNotNull()
        {
            var result = new ContentResult
            {
                StatusCode = SystemConstants.PasswordsNotNullCode,
                Content = "{\"message\": \"Passwords Not Null.\"}",
                ContentType = "application/json"
            };

            return result;
        }

        public static IActionResult WrongPassword()
        {
            var result = new ContentResult
            {
                StatusCode = SystemConstants.WrongPasswordCode,
                Content = "{\"message\": \"Wrong Password.\"}",
                ContentType = "application/json"
            };

            return result;
        }
    }
}