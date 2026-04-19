using Lab05.Data;
using Lab05.DTOs;
using Lab05.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll(
            [FromQuery] DateOnly? date,
            [FromQuery] string? status,
            [FromQuery] int? roomId)
        {
            IEnumerable<Reservation> reservations = InMemoryDb.Reservations;

            if (date.HasValue)
            {
                reservations = reservations.Where(r => r.Date == date.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                reservations = reservations.Where(r =>
                    r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (roomId.HasValue)
            {
                reservations = reservations.Where(r => r.RoomId == roomId.Value);
            }

            return Ok(reservations);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> GetById(int id)
        {
            var reservation = InMemoryDb.Reservations.FirstOrDefault(r => r.Id == id);

            return reservation is null ? NotFound() : Ok(reservation);
        }

        [HttpPost]
        public ActionResult<Reservation> Create([FromBody] CreateReservationDto dto)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);

            if (room is null)
            {
                return BadRequest(new { message = "Room does not exist." });
            }

            if (!room.IsActive)
            {
                return BadRequest(new { message = "Cannot create reservation for an inactive room." });
            }

            var hasOverlap = InMemoryDb.Reservations.Any(r =>
                r.RoomId == dto.RoomId &&
                r.Date == dto.Date &&
                dto.StartTime < r.EndTime &&
                dto.EndTime > r.StartTime);

            if (hasOverlap)
            {
                return Conflict(new { message = "Reservation overlaps with an existing reservation." });
            }

            var newId = InMemoryDb.Reservations.Count == 0
                ? 1
                : InMemoryDb.Reservations.Max(r => r.Id) + 1;

            var reservation = new Reservation(
                newId,
                dto.RoomId,
                dto.OrganizerName,
                dto.Topic,
                dto.Date,
                dto.StartTime,
                dto.EndTime,
                dto.Status);

            InMemoryDb.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Reservation> Update(int id, [FromBody] UpdateReservationDto dto)
        {
            var reservation = InMemoryDb.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation is null)
            {
                return NotFound();
            }

            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);

            if (room is null)
            {
                return BadRequest(new { message = "Room does not exist." });
            }

            if (!room.IsActive)
            {
                return BadRequest(new { message = "Cannot assign reservation to an inactive room." });
            }

            var hasOverlap = InMemoryDb.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == dto.RoomId &&
                r.Date == dto.Date &&
                dto.StartTime < r.EndTime &&
                dto.EndTime > r.StartTime);

            if (hasOverlap)
            {
                return Conflict(new { message = "Reservation overlaps with an existing reservation." });
            }

            reservation.Update(
                dto.RoomId,
                dto.OrganizerName,
                dto.Topic,
                dto.Date,
                dto.StartTime,
                dto.EndTime,
                dto.Status);

            return Ok(reservation);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var reservation = InMemoryDb.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation is null)
            {
                return NotFound();
            }

            InMemoryDb.Reservations.Remove(reservation);
            return NoContent();
        }
    }
}
