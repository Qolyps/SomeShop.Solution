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

        public async Task<List<Product>> GetAllProduct()
        {
            return await _productRepository.Get();
        }

        public async Task<Guid> CreateProduct(Product product)
        { 
            return await _productRepository.Create(product);
        }

        public async Task<Guid> UpdateProduct(Guid id, string name, string description, decimal price)
        {
            return await _productRepository.Update(id, name, description, price);
        }

        public async Task<Guid> DeleteProduct(Guid id)
        {
            return await _productRepository.Delete(id);
        }
    }
}
