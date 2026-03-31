namespace Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public Guid UserId { get; set; }
        public ICollection<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
}
