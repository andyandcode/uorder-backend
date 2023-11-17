using Application.ActiveLogs;
using Data.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("activeLog")]
    public class ActiveLogController : Controller
    {
        private readonly IActiveLogService _activeLogService;

        public ActiveLogController(IActiveLogService activeLogService)
        {
            _activeLogService = activeLogService;
        }

        /// <summary>
        /// Create list active log by entity id.
        /// </summary>
        [HttpGet("getByEntityId/{id}")]
        public async Task<IActionResult> GetByEntityId(string id)
        {
            var list = await _activeLogService.GetActiveLogByEntityId(id);
            return Ok(list);
        }

        /// <summary>
        /// Create list active log by entity type.
        /// </summary>
        [HttpGet("getByEntityType/{type}")]
        public async Task<IActionResult> GetByEntityId(EntityType type)
        {
            var list = await _activeLogService.GetActiveLogByEntityType(type);
            return Ok(list);
        }
    }
}