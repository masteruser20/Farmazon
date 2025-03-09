using Farmazon.ProductsService.App.Interfaces;
using Farmazon.ProductsService.Domain;
using Microsoft.EntityFrameworkCore;

namespace Farmazon.ProductsService.Infrastructure.Repositories
{
    public class ProductsRepository(ProductsDbContext context) : IProductsRepository
    {
        public async Task<IEnumerable<Product>> GetProducts(int categoryId)
        {
            return await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return product;
        }

        public Task<Product?> GetProductById(Guid productId)
        {
            return context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();

            return product;
        }
    }
}