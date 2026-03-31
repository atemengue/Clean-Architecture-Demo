namespace Application.DTOs.Order
{
    public class CreateOrderItemDto
    {
        public Guid EventId { get; set; }
        public int Quantity { get; set; }
    }
}
