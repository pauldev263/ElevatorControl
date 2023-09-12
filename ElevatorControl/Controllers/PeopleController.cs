using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElevatorControl.Models;

namespace ElevatorControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ElevatorControlContext _context;

        public PeopleController(ElevatorControlContext context)
        {
            _context = context;
        }

        // GET: api/People
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
          if (_context.Persons == null)
          {
              return NotFound();
          }
            return await _context.Persons.ToListAsync();
        }

        // GET: api/People/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(long id)
        {
          if (_context.Persons == null)
          {
              return NotFound();
          }
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        [ApiExplorerSettings(IgnoreApi = true)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        [ApiExplorerSettings(IgnoreApi = true)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
          if (_context.Persons == null)
          {
              return Problem("Entity set 'ElevatorControlContext.Persons'  is null.");
          }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            if (_context.Persons == null)
            {
                return NotFound();
            }
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/People/RequestElevator/currentfloor=3
        [HttpPost("RequestElevator/currentfloor={floorId}")]
        public async Task<ActionResult<string>> RequestElevator(long floorId)
        {
            var floor = await _context.Floors.FindAsync(floorId);
            if (floor == null)
            {
                return NotFound("Elevators do not go to this floor");
            }

            // TODO Save this request

            return CreatedAtAction("RequestElevator", $"Request sent for floor {floorId}");
        }

        // POST: api/People/PressFloorButton/floor=3
        [HttpPost("PressFloorButton/floor={floorId}")]
        public async Task<ActionResult<string>> PressFloorButton(long floorId)
        {
            var floor = await _context.Floors.FindAsync(floorId);
            if (floor == null)
            {
                return NotFound("Button not working!");
            }

            // TODO Save this request

            return CreatedAtAction("PressFloorButton", $"Elevator going to floor {floorId}");
        }

        private bool PersonExists(long id)
        {
            return (_context.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
