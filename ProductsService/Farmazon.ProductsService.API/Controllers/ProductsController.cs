using AutoMapper;
using Farmazon.ProductsService.App.Commands;
using Farmazon.ProductsService.App.DTOs;
using Farmazon.ProductsService.App.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Farmazon.ProductsService.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProductResponse>> GetProducts([FromBody] GetProductsQuery query)
    {
        var products = await mediator.Send(query);

        var result = mapper.Map<List<ProductResponse>>(products);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(Guid id)
    {
        // getById query
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
    {
        var product = await mediator.Send(command);

        var result = mapper.Map<ProductResponse>(product);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        // delete command
        return NoContent();
    }
}