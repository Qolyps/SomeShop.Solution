using Microsoft.EntityFrameworkCore;
using SomeShop.Domain.Entities;

namespace SomeShop.Infrastructure.Persistence
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
        }
    }
}
