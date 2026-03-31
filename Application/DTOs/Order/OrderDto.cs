namespace Application.DTOs.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
