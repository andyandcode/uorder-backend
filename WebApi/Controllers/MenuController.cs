using Application.Menus;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models.Menus;

namespace WebApi.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "admin,creator")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var dish = await _menuService.GetAll();
            return Ok(dish);
        }

        /// <summary>
        /// Gets the list of all menus have active status is true.
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpGet("getAllAvailable")]
        public async Task<IActionResult> GetAllAvailable()
        {
            var dish = await _menuService.GetAllAvailable();
            return Ok(dish);
        }

        /// <summary>
        /// Get the menu specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
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
        [Authorize(Roles = "admin,creator")]
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
        [Authorize(Roles = "admin,creator")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _menuService.Delete(id);
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
            var result = await _menuService.UndoDelete(itemId);
            return Ok(result);
        }

        /// <summary>
        /// Delete the menu specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpPost("removeDish")]
        [Consumes("application/json")]
        public async Task<IActionResult> RemoveDishFromMenu(RemoveDishFromMenuRequest req)
        {
            var result = await _menuService.RemoveDishFromMenu(req);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        /// <summary>
        /// Update the menu specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
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

        /// <summary>
        /// Update the menu active status specified by Id
        /// </summary>
        [Authorize(Roles = "admin,creator")]
        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(string id, [FromBody] JsonPatchDocument<Menu> patchDoc)
        {
            var result = await _menuService.UpdateStatus(id, patchDoc);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
    }
}