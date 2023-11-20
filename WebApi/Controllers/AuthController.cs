using Application.Accounts;
using Microsoft.AspNetCore.Mvc;
using Models.Accounts;
using Utilities.Common;
using Utilities.Constants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginRequest req)
        {
            var token = await _accountService.Login(req);

            if (token == SystemConstants.NotFoundAccount)
                return CustomStatus.NotFoundAccount();
            if (token == SystemConstants.WrongPassword)
                return CustomStatus.WrongPassword();
            if (token == SystemConstants.AccountLocked)
                return CustomStatus.AccountLocked();

            return Ok(new { Token = token });
        }

        [HttpPut("resetPassword/{id}")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCreateRequest req)
        {
            var result = await _accountService.ResetPassword(req);

            if (result == SystemConstants.PasswordsNotMatch)
                return CustomStatus.PasswordsNotMatch();
            if (result == SystemConstants.NotFoundAccount)
                return CustomStatus.NotFoundAccount();
            if (result == SystemConstants.AccountLocked)
                return CustomStatus.AccountLocked();

            return Ok(result);
        }
    }
}