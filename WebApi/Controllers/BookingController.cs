using Application.Menus;
using Application.Orders;
using Application.Tables;
using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.Orders;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("booking")]
    public class BookingController : Controller
    {
        private readonly IHubContext<OrderHub> _hubContext;
        private readonly IMenuService _menuService;
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;

        public BookingController(IMenuService menuService, ITableService tableService, IOrderService orderService, IHubContext<OrderHub> hubContext)
        {
            _menuService = menuService;
            _tableService = tableService;
            _orderService = orderService;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Get the menu specified by Id of table
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var table = await _tableService.GetById(id);
            var order = await _orderService.GetReccentlyOrder(id);
            var menus = await _menuService.GetAllAvailable();
            if (table == null)
                return BadRequest();
            if (order == null)
            {
                return Ok(new { table, menus });
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get infomation of booking when re-scan qr code
        /// </summary>
        [HttpGet("/booking/tracker/{id}")]
        public async Task<IActionResult> TrackBooking(string id)
        {
            var order = await _orderService.GetReccentlyOrder(id);
            if (order == null)
                return BadRequest();

            return Ok(order);
        }

        /// <summary>
        /// Create new booking and payment
        /// </summary>
        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PlaceOrder([FromForm] OrderCreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _orderService.Create(req);
            await _hubContext.Clients.All.SendAsync("ReceiveOrderNotification", "Có đơn hàng mới!");
            if (result == null)
                return Ok();
            return Ok(result);
        }

        /// <summary>
        /// Payment for the specified booking
        /// </summary>
        [HttpGet("payOrder/{id}")]
        public async Task<IActionResult> PayOrder(string id)
        {
            var result = await _orderService.PayOrder(id);
            await _hubContext.Clients.All.SendAsync("ReceiveOrderNotification", "Đơn hàng vừa được cập nhập!");
            if (result == null)
                return Ok();
            return Ok(result);
        }

        /// <summary>
        /// Gets the list of all booking.
        /// </summary>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _orderService.GetAllBooking();
            return Ok(list.OrderByDescending(x => x.CreatedAt));
        }

        /// <summary>
        /// Gets the list of all booking.
        /// </summary>
        [HttpGet("getCurrentBooking")]
        public async Task<IActionResult> GetCurrentBooking()
        {
            var list = await _orderService.GetCurrentBooking();
            return Ok(list.OrderByDescending(x => x.CreatedAt));
        }

        /// <summary>
        /// Update the order status specified by Id
        /// </summary>
        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> UpdateOrderStatusClient(string id, [FromBody] JsonPatchDocument<Order> patchDoc)
        {
            var result = await _orderService.UpdateOrderStatus(id, patchDoc);
            if (result == null)
                return BadRequest();
            await _hubContext.Clients.All.SendAsync("ReceiveOrderNotification", "Đơn hàng vừa được cập nhập!");
            return Ok();
        }
    }
}