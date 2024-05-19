using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DataAccess.SpecificRepos;
using Contracts.Services;
using Model;
using NonDomain;
using Services.Dto;

namespace Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsService(
            IProductsRepository productsRepository,
            IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _productsRepository.GetProductById(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public Task<OperationResult> AddProduct(ProductDto product)
        {
            return _productsRepository.InsertAsync(_mapper.Map<Product>(product), true);
        }

        public Task<OperationResult> EditProduct(ProductDto product)
        {
            return _productsRepository.UpdateAsync(_mapper.Map<Product>(product), true);
        }

        public Task<OperationResult> HardDeleteProductById(int productId)
        {
            return _productsRepository.HardDeleteProductById(productId);
        }

        public Task<OperationResult> DeleteProductById(int productId)
        {
            return _productsRepository.DeleteProductById(productId);
        }

        public Task<OperationResult> RestoreProductById(int productId)
        {
            return _productsRepository.RestoreProductById(productId);
        }

        public async Task<List<ProductDto>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productsRepository.GetNotDeletedProductsByCategoryId(categoryId);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetDeletedProducts()
        {
            var products = await _productsRepository.GetDeletedProducts();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetProductsPageByCategoryId(int categoryId, int pageNumber, int pageSize)
        {
            var products = await _productsRepository.GetNotDeletedProductsPage(categoryId, pageNumber, pageSize);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public Task<int> GetProductsCountByCategoryId(int categoryId)
        {
            return _productsRepository.GetNotDeletedProductsCountByCategoryId(categoryId);
        }

        public Task<int> GetProductsPagesCountByCategoryId(int categoryId, int pageSize)
        {
            return _productsRepository.GetNotDeletedProductsPagesCount(categoryId, pageSize);
        }

        public Task<bool> IsProductDeleted(int productId)
        {
            return _productsRepository.IsProductDeleted(productId);
        }
    }
}
