using Application.Menus;
using Application.Orders;
using Application.Tables;
using Microsoft.AspNetCore.Mvc;
using Models.Orders;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("booking")]
    public class BookingController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;

        public BookingController(IMenuService menuService, ITableService tableService, IOrderService orderService)
        {
            _menuService = menuService;
            _tableService = tableService;
            _orderService = orderService;
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
        /// Get the menu specified by Id of table
        /// </summary>
        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PlaceOrder([FromForm] OrderCreateRequest req)
        {
            //var result = await _menuService.GetById(id);
            //if (result == null)
            //    return BadRequest();
            return Ok();
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
    }
}