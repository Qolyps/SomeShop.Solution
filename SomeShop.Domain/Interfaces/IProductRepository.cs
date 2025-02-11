using SomeShop.Domain.Entities;

namespace SomeShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Guid> Create(Product product);
        Task<Guid> Delete(Guid id);
        Task<List<Product>> Get();
        Task<Guid> Update(Guid id, string name, string description, decimal price);
    }
}