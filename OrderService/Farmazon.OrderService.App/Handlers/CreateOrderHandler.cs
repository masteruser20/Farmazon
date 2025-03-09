using System.Text.Json;
using Farmazon.OrderService.App.Commands;
using Farmazon.OrderService.App.DTOs;
using Farmazon.OrderService.App.Interfaces;
using Farmazon.Shared;
using MediatR;

namespace Farmazon.OrderService.App.Handlers
{
    public class CreateOrderCommandHandler(ICartProvider cartProvider, IOrderRepository orderRepository)
        : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var cartItems = cartProvider.GetCartWithItems(userId);

            var orderId = await orderRepository.CreateOrderAsync(userId, cartItems, cancellationToken);

            var order = new
            {
                OrderId = orderId,
                UserId = userId,
                Items = cartItems,
            };

            var orderJson = JsonSerializer.Serialize(order);
            //var kafkaMessage = new Message { Key = orderId.ToString(), Value = orderJson };

            //await kafka.ProduceAsync("orders", kafkaMessage, cancellationToken);

            return new CreateOrderResponse
            {
                OrderId = orderId,
            };
        }
    }
}