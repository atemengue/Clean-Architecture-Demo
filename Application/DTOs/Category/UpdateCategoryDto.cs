namespace Application.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
