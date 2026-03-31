using Application.DTOs.Category;

namespace Application.Services.Categories
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task UpdateCategoryAsync(UpdateCategoryDto dto);
        Task DeleteCategoryAsync(Guid id);
    }
}
