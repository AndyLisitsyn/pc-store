using System.Collections.Generic;
using System.Threading.Tasks;
using NonDomain;
using Services.Dto;

namespace Contracts.Services
{
    public interface IProductsService
    {
        Task<ProductDto> GetProductById(int productId);
        Task<OperationResult> AddProduct(ProductDto product);
        Task<OperationResult> EditProduct(ProductDto product);
         Task<OperationResult> HardDeleteProductById(int productId);
        Task<OperationResult> DeleteProductById(int productId);
        Task<OperationResult> RestoreProductById(int productId);
        Task<List<ProductDto>> GetProductsByCategoryId(int categoryId);
        Task<List<ProductDto>> GetDeletedProducts();
        Task<List<ProductDto>> GetProductsPageByCategoryId(int categoryId, int pageNumber, int pageSize);
        Task<int> GetProductsCountByCategoryId(int categoryId);
        Task<int> GetProductsPagesCountByCategoryId(int categoryId, int pageSize);
        Task<bool> IsProductDeleted(int productId);
    }
}
