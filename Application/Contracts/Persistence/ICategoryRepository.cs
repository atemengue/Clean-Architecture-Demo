using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> CreateAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(Category entity);
    }
}
