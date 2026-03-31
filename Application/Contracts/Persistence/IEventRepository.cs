using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IEventRepository
    {
        Task<IReadOnlyList<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event> CreateAsync(Event entity);
        Task UpdateAsync(Event entity);
        Task DeleteAsync(Event entity);
    }
}
