using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperSimpleScheduler_Backend.Services;

namespace SuperSimpleScheduler_Backend.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService){
            _categoryService = categoryService;
        }

        [HttpGet("all-user/{userId:int}")]
        public async Task<IActionResult> GetUserCategories(int userId){
            var result = await _categoryService.GetCategoriesByUserIdAsync(userId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCategory(
            [FromForm] string name, [FromForm] int userId)
        {
            var result = await _categoryService.CreateCategoryAsync(name, userId);
            if (!(result is Models.Category)){
                return BadRequest(result);
            }
            if (result==null){
                return BadRequest("result is null");
            }
            return Ok(result);
        }

        [HttpPut("{categoryId:int}")]
        public async Task<IActionResult> UpdateCategory(
            int categoryId, [FromForm] string name)
        {
            var result = await _categoryService.UpdateCategoryAsync(categoryId, name);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory(int categoryId){
            var result = await _categoryService.DeleteCategoryAsync(categoryId);
            return result == null ? NotFound() : Ok(result);
        }
    }
}