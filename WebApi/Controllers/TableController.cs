using Application.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("table")]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        /// <summary>
        /// Gets the list of all tables.
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _tableService.GetAll();
            return Ok(list);
        }

        /// <summary>
        /// Get the table specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tableService.GetById(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new table.
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] TableCreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _tableService.Create(req);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Delete the table specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _tableService.Delete(id);
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
            var result = await _tableService.UndoDelete(itemId);
            return Ok(result);
        }

        /// <summary>
        /// Update the table specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpPut("put/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] TableUpdateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tableService.Update(req);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
    }
}