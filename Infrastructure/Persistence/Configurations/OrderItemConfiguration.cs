using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.OrderItemId);
            builder.Property(i => i.UnitPrice).HasPrecision(18, 2);

            builder.HasOne(i => i.Event)
                   .WithMany()
                   .HasForeignKey(i => i.EventId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
