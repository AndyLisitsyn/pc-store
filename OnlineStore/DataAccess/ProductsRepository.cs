using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using Contracts.DataAccess.SpecificRepos;
using NonDomain;

namespace DataAccess
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public Task<Product> GetProductById(int productId)
        {
            return _dbSet.AsNoTracking().Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<OperationResult> HardDeleteProductById(int productId)
        {
            var productToDelete = await _dbSet.FirstOrDefaultAsync(p => p.Id == productId);
            return await DeleteAsync(productToDelete, true);
        }

        public async Task<OperationResult> DeleteProductById(int productId)
        {
            var productToDelete = await _dbSet.FirstOrDefaultAsync(p => p.Id == productId);
            productToDelete.IsDeleted = true;
            return await UpdateAsync(productToDelete, true);
        }

        public async Task<OperationResult> RestoreProductById(int productId)
        {
            var productToDelete = await _dbSet.FirstOrDefaultAsync(p => p.Id == productId);
            productToDelete.IsDeleted = false;
            return await UpdateAsync(productToDelete, true);
        }

        public Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            return _dbSet.AsNoTracking().Include(p => p.Images).Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public Task<List<Product>> GetNotDeletedProductsByCategoryId(int categoryId)
        {
            return _dbSet.AsNoTracking().Include(p => p.Images).Where(p => p.CategoryId == categoryId && !p.IsDeleted).ToListAsync();
        }

        public Task<List<Product>> GetDeletedProducts()
        {
            return _dbSet.AsNoTracking().Include(p => p.Images).Where(p => p.IsDeleted).ToListAsync();
        }

        public async Task DeleteProductsByCategoryId(int categoryId)
        {
            var productsToDelete = await GetNotDeletedProductsByCategoryId(categoryId);
            foreach (var p in productsToDelete)
            {
                p.IsDeleted = true;
                await UpdateAsync(p, true);
            }
        }

        public Task<int> GetProductsCountByCategoryId(int categoryId)
        {
            return _dbSet.AsNoTracking().Where(p => p.CategoryId == categoryId).CountAsync();
        }

        public Task<int> GetNotDeletedProductsCountByCategoryId(int categoryId)
        {
            return _dbSet.AsNoTracking().Where(p => p.CategoryId == categoryId && !p.IsDeleted).CountAsync();
        }

        public async Task<List<Product>> GetProductsPage(int categoryId, int pageNumber, int pageSize)
        {
            var products = await GetProductsByCategoryId(categoryId);
            products.Reverse();
            int totalProductsCount = await GetProductsCountByCategoryId(categoryId);
            int productsCountToSkip = (pageNumber - 1) * pageSize;
            int productsCountToTake = pageSize;
            if (pageNumber * pageSize > totalProductsCount)
            {
                productsCountToTake = totalProductsCount - productsCountToSkip;
            }
            return products.Skip(productsCountToSkip).Take(productsCountToTake).ToList();
        }

        public async Task<List<Product>> GetNotDeletedProductsPage(int categoryId, int pageNumber, int pageSize)
        {
            var products = await GetNotDeletedProductsByCategoryId(categoryId);
            products.Reverse();
            int totalProductsCount = await GetNotDeletedProductsCountByCategoryId(categoryId);
            int productsCountToSkip = (pageNumber - 1) * pageSize;
            int productsCountToTake = pageSize;
            if (pageNumber * pageSize > totalProductsCount)
            {
                productsCountToTake = totalProductsCount - productsCountToSkip;
            }
            return products.Skip(productsCountToSkip).Take(productsCountToTake).ToList();
        }

        public async Task<int> GetProductsPagesCount(int categoryId, int pageSize)
        {
            int productsCount = await GetProductsCountByCategoryId(categoryId);
            return (int)Math.Ceiling((decimal)productsCount / pageSize);
        }

        public async Task<int> GetNotDeletedProductsPagesCount(int categoryId, int pageSize)
        {
            int productsCount = await GetNotDeletedProductsCountByCategoryId(categoryId);
            return (int)Math.Ceiling((decimal)productsCount / pageSize);
        }

        public async Task<bool> IsProductDeleted(int productId)
        {
            var product = await _dbSet.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == productId);
            return product.IsDeleted || product.Category.IsDeleted;
        }
    }
}
