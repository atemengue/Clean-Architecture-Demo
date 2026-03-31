using Application.Contracts.Persistence;
using Application.DTOs.Event;
using Domain.Entities;

namespace Application.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IReadOnlyList<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return events.Select(e => new EventDto
            {
                EventId = e.EventId,
                Name = e.Name,
                Price = e.Price,
                Artist = e.Artist,
                Date = e.Date,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                CategoryId = e.CategoryId,
                CategoryName = e.Category?.Name ?? string.Empty
            }).ToList();
        }

        public async Task<EventDto?> GetEventByIdAsync(Guid id)
        {
            var e = await _eventRepository.GetByIdAsync(id);
            if (e is null) return null;

            return new EventDto
            {
                EventId = e.EventId,
                Name = e.Name,
                Price = e.Price,
                Artist = e.Artist,
                Date = e.Date,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                CategoryId = e.CategoryId,
                CategoryName = e.Category?.Name ?? string.Empty
            };
        }

        public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
        {
            var entity = new Event
            {
                EventId = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                Artist = dto.Artist,
                Date = dto.Date,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _eventRepository.CreateAsync(entity);

            return new EventDto
            {
                EventId = created.EventId,
                Name = created.Name,
                Price = created.Price,
                Artist = created.Artist,
                Date = created.Date,
                Description = created.Description,
                ImageUrl = created.ImageUrl,
                CategoryId = created.CategoryId
            };
        }

        public async Task UpdateEventAsync(UpdateEventDto dto)
        {
            var entity = await _eventRepository.GetByIdAsync(dto.EventId)
                ?? throw new KeyNotFoundException($"Event {dto.EventId} not found.");

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Artist = dto.Artist;
            entity.Date = dto.Date;
            entity.Description = dto.Description;
            entity.ImageUrl = dto.ImageUrl;
            entity.CategoryId = dto.CategoryId;
            entity.LastModifiedDate = DateTime.UtcNow;

            await _eventRepository.UpdateAsync(entity);
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var entity = await _eventRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Event {id} not found.");

            await _eventRepository.DeleteAsync(entity);
        }
    }
}
