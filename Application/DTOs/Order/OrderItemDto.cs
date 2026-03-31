namespace Application.DTOs.Order
{
    public class OrderItemDto
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
