using Lab05.Data;
using Lab05.Models;
using Microsoft.AspNetCore.Http;
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
        public ActionResult GetById(int id)
        {
            var room = InMemoryDb.Rooms.FirstOrDefault(r => r.Id == id);
            
            return room == null ? NotFound() : Ok(room);
        }

        // POST api/<c>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<c>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<c>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
