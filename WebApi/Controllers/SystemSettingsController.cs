using Application.SystemSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.SystemSettings;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("systemSettings")]
    public class SystemSettingsController : Controller
    {
        private readonly ISystemSettingService _systemSettingsService;

        public SystemSettingsController(ISystemSettingService systemSettingService)
        {
            _systemSettingsService = systemSettingService;
        }

        /// <summary>
        /// Gets the list of all settings.
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("getSettings")]
        public async Task<IActionResult> GetSettings()
        {
            var dish = await _systemSettingsService.GetSettings();
            return Ok(dish);
        }

        /// <summary>
        /// Update the setting specified by Id
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPut("put/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] SystemSettingUpdateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _systemSettingsService.Update(req);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
    }
}