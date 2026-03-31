using Application.Contracts.Persistence;
using Application.DTOs.Order;
using Domain.Entities;

namespace Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventRepository _eventRepository;

        public OrderService(IOrderRepository orderRepository, IEventRepository eventRepository)
        {
            _orderRepository = orderRepository;
            _eventRepository = eventRepository;
        }

        public async Task<IReadOnlyList<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(MapToDto).ToList();
        }

        public async Task<IReadOnlyList<OrderDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return orders.Select(MapToDto).ToList();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order is null) return null;
            return MapToDto(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var orderItems = new List<OrderItem>();
            decimal total = 0;

            foreach (var item in dto.OrderItems)
            {
                var ev = await _eventRepository.GetByIdAsync(item.EventId)
                    ?? throw new KeyNotFoundException($"Event {item.EventId} not found.");

                orderItems.Add(new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    EventId = item.EventId,
                    Quantity = item.Quantity,
                    UnitPrice = ev.Price
                });

                total += ev.Price * item.Quantity;
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                OrderTotal = total,
                OrderPlaced = DateTime.UtcNow,
                OrderPaid = false,
                OrderItems = orderItems,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _orderRepository.CreateAsync(order);
            return MapToDto(created);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Order {id} not found.");

            await _orderRepository.DeleteAsync(order);
        }

        private static OrderDto MapToDto(Order order) => new()
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderTotal = order.OrderTotal,
            OrderPlaced = order.OrderPlaced,
            OrderPaid = order.OrderPaid,
            OrderItems = order.OrderItems.Select(i => new OrderItemDto
            {
                EventId = i.EventId,
                EventName = i.Event?.Name ?? string.Empty,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
    }
}
