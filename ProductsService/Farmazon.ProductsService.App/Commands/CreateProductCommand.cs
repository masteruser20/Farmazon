using Farmazon.ProductsService.App.DTOs;
using Farmazon.ProductsService.Domain;
using MediatR;

namespace Farmazon.ProductsService.App.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}