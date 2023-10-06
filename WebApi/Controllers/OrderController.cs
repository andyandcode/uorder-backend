using Application.Orders;
using Microsoft.AspNetCore.Mvc;
using Models.Orders;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderhService)
        {
            _orderService = orderhService;
        }

        /// <summary>
        /// Gets the list of all orders.
        /// </summary>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _orderService.GetAllOrder();
            return Ok(list);
        }

        /// <summary>
        /// Get the order specified by Id
        /// </summary>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _orderService.GetOrderById(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] OrderCreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _orderService.Create(req);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Delete the order specified by Id
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _orderService.Delete(id);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        /// <summary>
        /// Update the order specified by Id
        /// </summary>
        [HttpPut("put/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] OrderUpdateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderService.Update(req);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
    }
}