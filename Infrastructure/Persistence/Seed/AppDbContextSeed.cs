using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Categories.AnyAsync()) return;

            var concertId = Guid.NewGuid();
            var festivalId = Guid.NewGuid();
            var theatreId = Guid.NewGuid();

            var categories = new List<Category>
            {
                new() { CategoryId = concertId,  Name = "Concert",  CreatedBy = "seed", CreatedDate = DateTime.UtcNow },
                new() { CategoryId = festivalId, Name = "Festival", CreatedBy = "seed", CreatedDate = DateTime.UtcNow },
                new() { CategoryId = theatreId,  Name = "Théâtre",  CreatedBy = "seed", CreatedDate = DateTime.UtcNow }
            };

            await context.Categories.AddRangeAsync(categories);

            var events = new List<Event>
            {
                new()
                {
                    EventId     = Guid.NewGuid(),
                    Name        = "Taylor Swift - Eras Tour",
                    Price       = 149.99m,
                    Artist      = "Taylor Swift",
                    Date        = new DateTime(2026, 6, 15, 20, 0, 0, DateTimeKind.Utc),
                    Description = "La tournée mondiale d'une des plus grandes artistes de notre époque.",
                    ImageUrl    = "https://example.com/images/taylorswift.jpg",
                    CategoryId  = concertId,
                    CreatedBy   = "seed",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    EventId     = Guid.NewGuid(),
                    Name        = "Festival d'été de Québec",
                    Price       = 89.99m,
                    Artist      = "Artistes variés",
                    Date        = new DateTime(2026, 7, 5, 18, 0, 0, DateTimeKind.Utc),
                    Description = "Le plus grand festival de musique en plein air au Canada.",
                    ImageUrl    = "https://example.com/images/feq.jpg",
                    CategoryId  = festivalId,
                    CreatedBy   = "seed",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    EventId     = Guid.NewGuid(),
                    Name        = "Le Misanthrope",
                    Price       = 55.00m,
                    Artist      = "Molière",
                    Date        = new DateTime(2026, 5, 20, 19, 30, 0, DateTimeKind.Utc),
                    Description = "Une comédie classique mise en scène par le Théâtre du Nouveau Monde.",
                    ImageUrl    = "https://example.com/images/misanthrope.jpg",
                    CategoryId  = theatreId,
                    CreatedBy   = "seed",
                    CreatedDate = DateTime.UtcNow
                },
                new()
                {
                    EventId     = Guid.NewGuid(),
                    Name        = "Coldplay - Music of the Spheres",
                    Price       = 129.99m,
                    Artist      = "Coldplay",
                    Date        = new DateTime(2026, 8, 10, 20, 0, 0, DateTimeKind.Utc),
                    Description = "Un spectacle immersif avec une scénographie époustouflante.",
                    ImageUrl    = "https://example.com/images/coldplay.jpg",
                    CategoryId  = concertId,
                    CreatedBy   = "seed",
                    CreatedDate = DateTime.UtcNow
                }
            };

            await context.Events.AddRangeAsync(events);
            await context.SaveChangesAsync();
        }
    }
}
