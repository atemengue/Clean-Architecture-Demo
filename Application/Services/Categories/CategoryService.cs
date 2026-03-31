using Application.Contracts.Persistence;
using Application.DTOs.Category;
using Domain.Entities;

namespace Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            }).ToList();
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null) return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = dto.Name,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _categoryRepository.CreateAsync(category);

            return new CategoryDto
            {
                CategoryId = created.CategoryId,
                Name = created.Name
            };
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId)
                ?? throw new KeyNotFoundException($"Category {dto.CategoryId} not found.");

            category.Name = dto.Name;
            category.LastModifiedDate = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Category {id} not found.");

            await _categoryRepository.DeleteAsync(category);
        }
    }
}
