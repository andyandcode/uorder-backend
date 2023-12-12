using Application.DiscountCodes;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models.DiscountCodes;
using Models.Orders;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("discount")]
    public class DiscountCodeController : Controller
    {
        private readonly IDiscountCodeService _discountCodeService;

        public DiscountCodeController(IDiscountCodeService discountCodeService)
        {
            _discountCodeService = discountCodeService;
        }

        /// <summary>
        /// Gets the list of all discount codes.
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _discountCodeService.GetAll();
            return Ok(list);
        }

        /// <summary>
        /// Gets the list of all discount code is available for use.
        /// </summary>
        [HttpPost("getAvailableCodes")]
        [Consumes("application/json")]
        public async Task<IActionResult> ReturnAvailableCodes(CartItemVm vm)
        {
            var list = await _discountCodeService.ReturnAvailableCodes(vm);
            return Ok(list);
        }

        /// <summary>
        /// Apply discount code is available for order.
        /// </summary>
        [HttpGet("applyDiscountCode")]
        public async Task<IActionResult> ApplyDiscountCode([FromBody] OrderCreateRequest req)
        {
            var order = await _discountCodeService.ApplyDiscountCode(req);
            return Ok(order);
        }

        /// <summary>
        /// Creates a new discount code.
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] DiscountCodeCreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _discountCodeService.Create(req);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Delete the discount code specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _discountCodeService.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Undo delete action
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpPost("undoDelete/{itemId}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UndoDelete(string itemId)
        {
            var result = await _discountCodeService.UndoDelete(itemId);
            return Ok(result);
        }

        /// <summary>
        /// Update the order status specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> UpdatePatch(string id, [FromBody] JsonPatchDocument<DiscountCode> patchDoc)
        {
            var result = await _discountCodeService.UpdatePatch(id, patchDoc);
            if (result == null)
                return BadRequest();
            return Ok();
        }
    }
}