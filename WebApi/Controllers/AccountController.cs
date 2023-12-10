using Application.Accounts;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models.Accounts;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Gets the list of all accounts.
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _accountService.GetAll();
            return Ok(list);
        }

        /// <summary>
        /// Get the account specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator,staff")]
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _accountService.GetAccountById(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Gets the list of all roles.
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _accountService.GetAllRoles();
            return Ok(roles);
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] AccountCreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _accountService.Create(req);
            if (result == 0)
                return BadRequest();

            return Ok(result);
        }

        /// <summary>
        /// Delete the account specified by Id
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _accountService.Delete(id);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Update the account specified by Id
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPut("put/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] AccountUpdateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Update(req);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Update the account status specified by Id
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(string id, [FromBody] JsonPatchDocument<Account> patchDoc)
        {
            var result = await _accountService.UpdateStatus(id, patchDoc);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
    }
}