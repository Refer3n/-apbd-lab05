using Lab05.Data;
using Lab05.DTOs;
using Lab05.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAll(
            [FromQuery] int? minCapacity,
            [FromQuery] bool? hasProjector,
            [FromQuery] bool? activeOnly)
        {
            IEnumerable<Room> rooms = InMemoryDb.Rooms;

            if (minCapacity.HasValue)
            {
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
            }

            if (hasProjector.HasValue)
            {
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
            }

            if (activeOnly == true)
            {
                rooms = rooms.Where(r => r.IsActive);
            }
            
            return Ok(rooms);
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<Room> GetById(int id)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == id);
            
            return room == null ? NotFound() : Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public ActionResult<IEnumerable<Room>> GetByBuildingCode(string buildingCode)
        {
            var rooms = InMemoryDb.Rooms.Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(rooms);
        }

        [HttpPost]
        public ActionResult<Room> Create([FromBody] CreateRoomDto roomDto)
        {
            var newId = InMemoryDb.Rooms.Count == 0 ? 1 : InMemoryDb.Rooms.Max(r => r.Id);

            var room = new Room(
                newId,
                roomDto.Name,
                roomDto.BuildingCode,
                roomDto.Floor,
                roomDto.Capacity,
                roomDto.HasProjector,
                roomDto.IsActive);

            InMemoryDb.Rooms.Add(room);

            return CreatedAtAction(nameof(GetById), new { room.Id }, room);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Room> Update(int id, [FromBody] UpdateRoomDto roomDto)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            room.Update(
                roomDto.Name,
                roomDto.BuildingCode,
                roomDto.Floor,
                roomDto.Capacity,
                roomDto.HasProjector,
                roomDto.IsActive);

            return Ok(room);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }
            
            var hasReservations = InMemoryDb.Reservations.Any(r => r.RoomId == id);

            if (hasReservations)
            {
                return Conflict(new { message = "Cannot delete room because related reservations exist." });
            }

            InMemoryDb.Rooms.Remove(room);

            return NoContent();
        }
    }
}
