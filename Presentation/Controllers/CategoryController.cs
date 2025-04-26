using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.DataTransferObject.CategoryDto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contrats;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController :ControllerBase
    {
        private readonly IServiceManager _services;

        public CategoryController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            return Ok(await _services.CategoryService.GetAllCategoriesAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCategoryByIdAsync([FromRoute(Name = "id")] int id)
        {
            return Ok(await _services.CategoryService.GetOneCategoryByIdAsync(id,false));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDtoForInsertion categoryDto)
        {
            var addedCategory =  _services.CategoryService.CreateOneCategoryAsync(categoryDto);
            return StatusCode(201,addedCategory);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute(Name = "id")] int id)
        {
            await _services.CategoryService.DeleteOneCategoryAsync(id, false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute(Name = "id")] int id, [FromBody] CategoryDtoForUpdate categoryDto)
        {

            await _services.CategoryService.UpdateOneCategoryAsync(id,categoryDto, false);
            return NoContent(); //204
        }
    }
}
