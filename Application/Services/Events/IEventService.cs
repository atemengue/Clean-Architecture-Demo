using Application.DTOs.Event;

namespace Application.Services.Events
{
    public interface IEventService
    {
        Task<IReadOnlyList<EventDto>> GetAllEventsAsync();
        Task<EventDto?> GetEventByIdAsync(Guid id);
        Task<EventDto> CreateEventAsync(CreateEventDto dto);
        Task UpdateEventAsync(UpdateEventDto dto);
        Task DeleteEventAsync(Guid id);
    }
}
