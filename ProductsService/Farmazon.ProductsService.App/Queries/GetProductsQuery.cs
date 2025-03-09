using Farmazon.ProductsService.Domain;
using MediatR;
using Newtonsoft.Json;

namespace Farmazon.ProductsService.App.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
    [JsonProperty] public int CategoryId { get; set; }
}