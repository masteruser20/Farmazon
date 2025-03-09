using AutoMapper;
using Farmazon.ProductsService.App.DTOs;
using Farmazon.ProductsService.Domain;

namespace Farmazon.ProductsService;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductResponse>();
    }
}