using AutoMapper;
using Farmazon.ProductsService.App.Commands;
using Farmazon.ProductsService.App.Interfaces;
using Farmazon.ProductsService.Domain;
using MediatR;

namespace Farmazon.ProductsService.App.Handlers;

public class UpdateProductHandler(IProductsRepository productsRepository, IMapper mapper)
    : IRequestHandler<UpdateProductCommand, Product>
{
    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetProductById(request.Id);

        if (product == null)
        {
            // custom exception there (404)
            throw new InvalidOperationException("Product not found");
        }

        mapper.Map(request, product);
        var updatedProduct = await productsRepository.UpdateProduct(product);
        return updatedProduct;
    }
}