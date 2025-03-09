using AutoMapper;
using Farmazon.ProductsService.App.Commands;
using Farmazon.ProductsService.App.Interfaces;
using Farmazon.ProductsService.Domain;
using MediatR;

namespace Farmazon.ProductsService.App.Handlers
{
    public class CreateProductHandler(IProductsRepository productsRepository, IMapper mapper)
        : IRequestHandler<CreateProductCommand, Product>
    {
        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            await productsRepository.AddProduct(product);
            return product;
        }
    }
}