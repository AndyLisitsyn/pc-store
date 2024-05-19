using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using NonDomain;

namespace Contracts.DataAccess.SpecificRepos
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<Product> GetProductById(int productId);
        Task<OperationResult> HardDeleteProductById(int productId);
        Task<OperationResult> DeleteProductById(int productId);
        Task<OperationResult> RestoreProductById(int productId);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
        Task<List<Product>> GetNotDeletedProductsByCategoryId(int categoryId);
        Task<List<Product>> GetDeletedProducts();
        Task DeleteProductsByCategoryId(int categoryId);
        Task<int> GetProductsCountByCategoryId(int categoryId);
        Task<int> GetNotDeletedProductsCountByCategoryId(int categoryId);
        Task<List<Product>> GetProductsPage(int categoryId, int pageNumber, int pageSize);
        Task<List<Product>> GetNotDeletedProductsPage(int categoryId, int pageNumber, int pageSize);
        Task<int> GetProductsPagesCount(int categoryId, int pageSize);
        Task<int> GetNotDeletedProductsPagesCount(int categoryId, int pageSize);
        Task<bool> IsProductDeleted(int productId);
    }
}
