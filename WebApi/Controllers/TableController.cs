using Application.Tables;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;

namespace WebApi.Controllers
{
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
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var list = _tableService.GetAllTable();
            return Ok(list);
        }

        /// <summary>
        /// Get the table specified by Id
        /// </summary>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tableService.GetTableById(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new table.
        /// </summary>
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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _tableService.Delete(id);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        /// <summary>
        /// Update the table specified by Id
        /// </summary>
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