using Application.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("analytics")]
    public class AnalyticsController : Controller
    {
        private readonly IHubContext<OrderHub> _hubContext;
        private readonly IOrderService _orderService;

        public AnalyticsController(IOrderService orderhService, IHubContext<OrderHub> hubContext)
        {
            _orderService = orderhService;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Get revenue.
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("getRevenue")]
        public async Task<IActionResult> GetRevenue()
        {
            var list = await _orderService.GetRevenue();
            return Ok(list);
        }

        /// <summary>
        /// Get top sellers.
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("getTopSellers")]
        public async Task<IActionResult> GetTopSellers()
        {
            var list = await _orderService.GetTopSellers();
            return Ok(list);
        }

        /// <summary>
        /// Count management items.
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("countManagement")]
        public async Task<IActionResult> CountManagement()
        {
            var list = await _orderService.GetCountManagement();
            return Ok(list);
        }
    }
}