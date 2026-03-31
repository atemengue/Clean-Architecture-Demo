using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetAllAsync();
        Task<IReadOnlyList<Order>> GetByUserIdAsync(Guid userId);
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> CreateAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(Order entity);
    }
}
