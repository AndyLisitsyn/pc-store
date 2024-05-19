using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Contracts.DataAccess.SpecificRepos;
using Model;
using NonDomain;

namespace DataAccess
{
    public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
    { 
        public CategoriesRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<OperationResult> HardDeleteCategoryById(int categoryId, bool requireSave)
        {
            var categoryToDelete = await _dbSet.FirstOrDefaultAsync(c => c.Id == categoryId);
            return await DeleteAsync(categoryToDelete, requireSave);
        }

        public async Task<OperationResult> DeleteCategoryById(int categoryId, bool requireSave)
        {
            var categoryToDelete = await _dbSet.FirstOrDefaultAsync(c => c.Id == categoryId);
            categoryToDelete.IsDeleted = true;
            return await UpdateAsync(categoryToDelete, requireSave);
        }

        public async Task<OperationResult> RestoreCategoryById(int categoryId, bool requireSave)
        {
            var categoryToDelete = await _dbSet.FirstOrDefaultAsync(c => c.Id == categoryId);
            categoryToDelete.IsDeleted = false;
            return await UpdateAsync(categoryToDelete, requireSave);
        }

        public Task<List<Category>> GetAllNotDeletedCategories()
        {
            return _dbSet.AsNoTracking().Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<List<Category>> GetAllRootCategories()
        {
            var categories = await _dbSet.AsNoTracking().Where(c => c.ParentCategoryId == null).ToListAsync();
            categories.Reverse();
            return categories;
        }

        public async Task<List<Category>> GetNotDeletedRootCategories()
        {
            var categories = await _dbSet.AsNoTracking().Where(c => c.ParentCategoryId == null && !c.IsDeleted).ToListAsync();
            categories.Reverse();
            return categories;
        }

        public Task<List<Category>> GetDeletedCategories()
        {
            return _dbSet.AsNoTracking().Where(c => c.IsDeleted).ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesByParentId(int categoryId)
        {
            var categories = await _dbSet.AsNoTracking().Where(c => c.ParentCategoryId == categoryId).ToListAsync();
            categories.Reverse();
            return categories;
        }

        public async Task<List<Category>> GetNotDeletedCategoriesByParentId(int categoryId)
        {
            var categories = await _dbSet.AsNoTracking().Where(c => c.ParentCategoryId == categoryId && !c.IsDeleted).ToListAsync();
            categories.Reverse();
            return categories;
        }

        public Task<int> GetNotDeletedCategoriesCountByParentId(int categoryId)
        {
            return _dbSet.CountAsync(c => c.ParentCategoryId == categoryId && !c.IsDeleted);
        }

        public Task<List<Category>> GetDeletedCategoriesByParentId(int categoryId)
        {
            return _dbSet.AsNoTracking().Where(c => c.ParentCategoryId == categoryId && c.IsDeleted).ToListAsync();
        }

        public async Task DeleteCategoriesByParentId(int categoryId)
        {
            var categoriesToDelete = await GetNotDeletedCategoriesByParentId(categoryId);
            foreach(var c in categoriesToDelete)
            {
                c.IsDeleted = true;
                await UpdateAsync(c, true);
            }
        }

        public Task<bool> IsNameDuplicated(string name)
        {
            return _dbSet.AnyAsync(c => c.Name == name);
        }

        public async Task<bool> IsCategoryDeleted(int categoryId)
        {
            var category = await GetByIdAsync(categoryId);
            return category.IsDeleted;
        }
    }
}
