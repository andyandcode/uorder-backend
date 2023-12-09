using Application.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("action")]
    public class ActionController : Controller
    {
        private readonly IHubContext<ActionHub> _hubContext;
        private readonly IActionService _actionService;

        public ActionController(IHubContext<ActionHub> hubContext, IActionService actionService)
        {
            _hubContext = hubContext;
            _actionService = actionService;
        }

        /// <summary>
        /// Get the table specified by Id of table and send notication
        /// </summary>
        [HttpGet("callStaff/{tableId}")]
        public async Task<IActionResult> CallStaff(string tableId)
        {
            var table = await _actionService.CallStaff(tableId);
            await _hubContext.Clients.All.SendAsync("SendCallStaffNotification", table);
            if (table == null)
                return BadRequest();
            return Ok(table);
        }
    }
}