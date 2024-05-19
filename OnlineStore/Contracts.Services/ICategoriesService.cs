using Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using NonDomain;

namespace Contracts.Services
{
    public interface ICategoriesService
    {
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<OperationResult> AddCategory(CategoryDto category);
        Task<OperationResult> EditCategory(CategoryDto category);
        Task<OperationResult> DeleteCategoryById(int categoryId);
        Task<OperationResult> HardDeleteCategoryById(int categoryId);
        Task<OperationResult> RestoreCategoryById(int categoryId);
        Task<List<CategoryDto>> GetAllNotDeletedCategories();
        Task<List<CategoryDto>> GetAllNotDeletedRootCategories();
        Task<List<CategoryDto>> GetNotDeletedCategoriesByParentId(int categoryId);
        Task<List<CategoryDto>> GetDeletedCategories();
        Task<List<CategoryDto>> GetHierarchicallyOrderedCategories();
        Task<bool> IsCategoryDeleted(int categoryId);
        Task<bool> IsCategoryNameDuplicated(string name);
    }
}
