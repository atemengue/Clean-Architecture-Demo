using Application.DTOs.Order;

namespace Application.Services.Orders
{
    public interface IOrderService
    {
        Task<IReadOnlyList<OrderDto>> GetAllOrdersAsync();
        Task<IReadOnlyList<OrderDto>> GetOrdersByUserIdAsync(Guid userId);
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
        Task DeleteOrderAsync(Guid id);
    }
}
