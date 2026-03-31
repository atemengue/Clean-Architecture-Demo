using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Price).HasPrecision(18, 2);
            builder.Property(e => e.Artist).HasMaxLength(200);
            builder.Property(e => e.ImageUrl).HasMaxLength(500);
            builder.Property(e => e.CreatedBy).HasMaxLength(100);
            builder.Property(e => e.LastModifiedBy).HasMaxLength(100);

            builder.HasOne(e => e.Category)
                   .WithMany(c => c.Events)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
