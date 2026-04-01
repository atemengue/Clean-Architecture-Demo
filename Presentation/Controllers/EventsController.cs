using Application.DTOs.Event;
using Application.Services.Events;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EventDto>>> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EventDto>> GetById(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev is null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        public async Task<ActionResult<EventDto>> Create(CreateEventDto dto)
        {
            var created = await _eventService.CreateEventAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EventId }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateEventDto dto)
        {
            if (id != dto.EventId) return BadRequest();
            await _eventService.UpdateEventAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
