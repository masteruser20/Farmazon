using Farmazon.ProductsService.Domain;
using Microsoft.EntityFrameworkCore;

namespace Farmazon.ProductsService.Infrastructure
{
    public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}