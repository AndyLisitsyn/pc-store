using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Contracts.Services;
using Utils;
using Services.Dto;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly ILogger<HomeController> _logger;

        public CategoryController(
            ICategoriesService categoriesService,
            ILogger<HomeController> logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        [HttpGet("Index/{id}")]
        public async Task<JsonResult> Index(int id)
        {
            var childCategories = await _categoriesService.GetNotDeletedCategoriesByParentId(id);
            if (childCategories.Count != 0)
            {
                return Json(new 
                { 
                    IsAnyChildren = true,
                    Category = await _categoriesService.GetCategoryById(id), 
                    ChildCategories = childCategories 
                });
            }
            else
            {
                return Json(new { IsAnyChildren = false });
            }
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<CategoryDto> GetCategoryById(int id)
        {
            return await _categoriesService.GetCategoryById(id);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            return await _categoriesService.GetHierarchicallyOrderedCategories();
        }

        [HttpGet("GetDeletedCategories")]
        public async Task<IEnumerable<CategoryDto>> GetDeletedCategories()
        {
             return await _categoriesService.GetDeletedCategories();
        }

        [HttpGet("GetAllRootCategories")]
        public async Task<IEnumerable<CategoryDto>> GetNotDeletedCategories()
        {
            return await _categoriesService.GetAllNotDeletedRootCategories();
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CategoryDto category)
        {
            if (await _categoriesService.IsCategoryNameDuplicated(category.Name))
            {
                return BadRequest("Category name can't be duplicated.");
            }
            else if (ModelState.IsValid)
            {
                var operationResult = await _categoriesService.AddCategory(category);
                if (!operationResult.IsSuccess)
                {
                    _logger.LogInformation($"An error occurred while adding a category:\n{operationResult.Message}");
                    return BadRequest("Adding category error");
                }
                return Ok();
            }
            return BadRequest("Adding category error");
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(CategoryDto category)
        {
            var categoryToEdit = await _categoriesService.GetCategoryById(category.Id);
            if (categoryToEdit.Name != category.Name && await _categoriesService.IsCategoryNameDuplicated(category.Name))
            {
                return BadRequest("Category name can't be duplicated.");
            }
            else if (ModelState.IsValid)
            {
                var operationResult = await _categoriesService.EditCategory(category);
                if (!operationResult.IsSuccess)
                {
                    _logger.LogInformation($"An error occurred while editing a category:\n{operationResult.Message}");
                    return BadRequest("Editing category error");
                }
                return Ok();
            }
            return BadRequest("Editing category error");
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Hide/{id}")]
        public async Task<IActionResult> Hide(int id)
        {
            var operationResult = await _categoriesService.DeleteCategoryById(id);
            if (!operationResult.IsSuccess)
            {
                _logger.LogInformation($"An error occurred while hiding a category:\n{operationResult.Message}");
                return BadRequest("Hiding category error");
            }
            return Ok();
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var operationResult = await _categoriesService.RestoreCategoryById(id);
            if (!operationResult.IsSuccess)
            {
                _logger.LogInformation($"An error occurred while restoring a category:\n{operationResult.Message}");
                return BadRequest("Restoring category error");
            }
            return Ok();
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var operationResult = await _categoriesService.HardDeleteCategoryById(id);
            if (!operationResult.IsSuccess)
            {
                _logger.LogInformation($"An error occurred while deleting a category:\n{operationResult.Message}");
                return BadRequest("Deleting category error");
            }
            return Ok();
        }
    }
}
