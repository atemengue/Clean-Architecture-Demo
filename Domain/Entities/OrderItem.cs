namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid EventId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Event Event { get; set; } = default!;
    }
}
