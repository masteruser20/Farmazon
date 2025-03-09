using Farmazon.ProductsService.App.Interfaces;
using Farmazon.ProductsService.App.Queries;
using Farmazon.ProductsService.Domain;
using MediatR;

namespace Farmazon.ProductsService.App.Handlers;

public class GetProductsHandler(IProductsRepository productsRepository)
    : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetProducts(request.CategoryId);

        return products.ToList();
    }
}