using Application.Menus;
using Microsoft.AspNetCore.Mvc;
using Models.Menus;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("menu")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Gets the list of all menus.
        /// </summary>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var dish = await _menuService.GetAll();
            return Ok(dish);
        }

        /// <summary>
        /// Gets the list of all menus have active status is true.
        /// </summary>
        [HttpGet("getAllAvailable")]
        public async Task<IActionResult> GetAllAvailable()
        {
            var dish = await _menuService.GetAllAvailable();
            return Ok(dish);
        }

        /// <summary>
        /// Get the menu specified by Id
        /// </summary>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _menuService.GetById(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new menu.
        /// </summary>
        [HttpPost("post")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] MenuCreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _menuService.Create(req);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Delete the menu specified by Id
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _menuService.Delete(id);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        /// <summary>
        /// Update the menu specified by Id
        /// </summary>
        [HttpPut("put/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromForm] MenuUpdateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _menuService.Update(req);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
    }
}