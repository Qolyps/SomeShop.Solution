using Microsoft.AspNetCore.Http.HttpResults;
using SomeShop.Domain.Entities;
using SomeShop.Domain.Interfaces;

namespace SomeShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                throw new KeyNotFoundException("Продукт не найден.");

            await _productRepository.DeleteAsync(id);
        }
    }
}
