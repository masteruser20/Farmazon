using AutoMapper;
using Farmazon.ProductsService.Domain;

namespace Farmazon.ProductsService.App.Interfaces;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProducts(int categoryId);

    Task<Product> AddProduct(Product product);

    Task<Product?> GetProductById(Guid productId);
    
    Task<Product> UpdateProduct(Product product);
}