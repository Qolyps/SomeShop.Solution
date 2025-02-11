using SomeShop.Domain.Entities;

namespace SomeShop.Domain.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProduct();
        Task<Guid> CreateProduct(Product product);
        Task<Guid> UpdateProduct(Guid id, string name, string description, decimal price);
        Task<Guid> DeleteProduct(Guid id);
    }
}
