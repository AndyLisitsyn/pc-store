using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DataAccess.SpecificRepos;
using Contracts.Services;
using Model;
using NonDomain;
using Services.Dto;

namespace Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesService(
            ICategoriesRepository categoryRepository,
            IMapper mapper)
        {
            _categoriesRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            var category = await _categoriesRepository.GetByIdAsync(categoryId);
            return _mapper.Map<CategoryDto>(category);
        }

        public Task<OperationResult> AddCategory(CategoryDto category)
        {
            return _categoriesRepository.InsertAsync(_mapper.Map<Category>(category), true);
        }

        public Task<OperationResult> EditCategory(CategoryDto category)
        {
            return _categoriesRepository.UpdateAsync(_mapper.Map<Category>(category), true);
        }

        public async Task<OperationResult> DeleteCategoryById(int categoryId)
        {
            await DeleteCategoryByIdWhithoutSaving(categoryId);
            return await _categoriesRepository.SaveAsync();
        }

        private async Task<OperationResult> DeleteCategoryByIdWhithoutSaving(int categoryId)
        {
            var children = await _categoriesRepository.GetNotDeletedCategoriesByParentId(categoryId);
            if (children.Count != 0)
            {
                foreach (var c in children)
                {
                    await DeleteCategoryByIdWhithoutSaving(c.Id);
                }
            }
            return await _categoriesRepository.DeleteCategoryById(categoryId, false);
        }

        public async Task<OperationResult> HardDeleteCategoryById(int categoryId)
        {
            await HardDeleteCategoryByIdWhithoutSaving(categoryId);
            return await _categoriesRepository.SaveAsync();
        }

        private async Task<OperationResult> HardDeleteCategoryByIdWhithoutSaving(int categoryId)
        {
            var children = await _categoriesRepository.GetCategoriesByParentId(categoryId);
            if (children.Count != 0)
            {
                foreach (var c in children)
                {
                    await HardDeleteCategoryByIdWhithoutSaving(c.Id);
                }
            }
            return await _categoriesRepository.HardDeleteCategoryById(categoryId, false);
        }

        public async Task<OperationResult> RestoreCategoryById(int categoryId)
        {
            await RestoreCategoryByIdWithoutSaving(categoryId);
            return await _categoriesRepository.SaveAsync();
        }

        private async Task<OperationResult> RestoreCategoryByIdWithoutSaving(int categoryId)
        {
            var children = await _categoriesRepository.GetDeletedCategoriesByParentId(categoryId);
            if (children.Count != 0)
            {
                foreach (var c in children)
                {
                    await RestoreCategoryByIdWithoutSaving(c.Id);
                }
            }

            return await _categoriesRepository.RestoreCategoryById(categoryId, false);
        }

        public async Task<List<CategoryDto>> GetAllNotDeletedRootCategories()
        {
            var rootCategories = await _categoriesRepository.GetNotDeletedRootCategories();
            return _mapper.Map<List<CategoryDto>>(rootCategories);
        }

        public async Task<List<CategoryDto>> GetNotDeletedCategoriesByParentId(int categoryId)
        {
            var categoryChildren = await _categoriesRepository.GetNotDeletedCategoriesByParentId(categoryId);
            return _mapper.Map<List<CategoryDto>>(categoryChildren);
        }

        public async Task<List<CategoryDto>> GetDeletedCategories()
        {
            var categories = await _categoriesRepository.GetDeletedCategories();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<CategoryDto>> GetAllNotDeletedCategories()
        {
            var categories = await _categoriesRepository.GetAllNotDeletedCategories();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<CategoryDto>> GetHierarchicallyOrderedCategories()
        {
            var result = new List<CategoryDto>();
            var rootCategories = await GetAllNotDeletedRootCategories();
            foreach (var c in rootCategories)
            {
                result.Add(c);
                result.AddRange(await GetHierarchicallyOrderedCategoriesByParentId(c.Id, 1));
            }
            return result;
        }

        private async Task<List<CategoryDto>> GetHierarchicallyOrderedCategoriesByParentId(int categoryId, int spaces)
        {
            var result = new List<CategoryDto>();
            var categories = await GetNotDeletedCategoriesByParentId(categoryId);
            foreach (var c in categories)
            {
                c.Name = string.Concat(Enumerable.Repeat(new string('-', 3), spaces)) + c.Name;
                result.Add(c);
                if (await _categoriesRepository.GetNotDeletedCategoriesCountByParentId(c.Id) != 0)
                {
                    result.AddRange(await GetHierarchicallyOrderedCategoriesByParentId(c.Id, spaces + 1));
                }
            }
            return result;
        }

        public Task<bool> IsCategoryNameDuplicated(string name)
        {
            return _categoriesRepository.IsNameDuplicated(name);
        }

        public Task<bool> IsCategoryDeleted(int categoryId)
        {
            return _categoriesRepository.IsCategoryDeleted(categoryId);
        }
    }
}
