using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Entities;
using SomeShop.Domain.Interfaces;
using SomeShop.Infrastructure.Persistence;

namespace SomeShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _context;

        public ProductRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Get()
        {
            var product = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return product;
        }

        public async Task<Guid> Create(Product product)
        {
            var products = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string description, decimal price)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, p => name)
                    .SetProperty(p => p.Description, p => description)
                    .SetProperty(p => p.Price, p => price));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
