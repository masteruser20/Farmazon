using Farmazon.OrderService.App.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Farmazon.OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            // fluentvalidation before

            var result = await mediator.Send(command);

            return CreatedAtAction(nameof(GetOrder), new { orderId = result.OrderId }, result);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            // get order by id
            return Ok();
        }
    }
}