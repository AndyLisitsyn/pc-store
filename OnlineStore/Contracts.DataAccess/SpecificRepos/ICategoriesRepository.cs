using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using NonDomain;

namespace Contracts.DataAccess.SpecificRepos
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        Task<OperationResult> HardDeleteCategoryById(int categoryId, bool requireSave);
        Task<OperationResult> DeleteCategoryById(int categoryId, bool requireSave);
        Task<OperationResult> RestoreCategoryById(int categoryId, bool requireSave);
        Task<List<Category>> GetAllNotDeletedCategories();
        Task<List<Category>> GetAllRootCategories();
        Task<List<Category>> GetNotDeletedRootCategories();
        Task<List<Category>> GetDeletedCategories();
        Task<List<Category>> GetCategoriesByParentId(int categoryId);
        Task<List<Category>> GetNotDeletedCategoriesByParentId(int categoryId);
        Task<int> GetNotDeletedCategoriesCountByParentId(int categoryId);
        Task<List<Category>> GetDeletedCategoriesByParentId(int categoryId);
        Task DeleteCategoriesByParentId(int categoryId);
        Task<bool> IsNameDuplicated(string name);
        Task<bool> IsCategoryDeleted(int categoryId);
    }
}
