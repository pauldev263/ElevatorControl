using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElevatorControl.Models;
using Microsoft.IdentityModel.Tokens;

namespace ElevatorControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly ElevatorControlContext _context;

        public ElevatorsController(ElevatorControlContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevators()
        {
          if (_context.Elevators == null)
          {
              return NotFound();
          }
            return await _context.Elevators.ToListAsync();
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
          if (_context.Elevators == null)
          {
              return NotFound();
          }
            var elevator = await _context.Elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        // PUT: api/Elevators/5
        [ApiExplorerSettings(IgnoreApi = true)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevator(long id, Elevator elevator)
        {
            if (id != elevator.Id)
            {
                return BadRequest();
            }

            _context.Entry(elevator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
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

        // POST: api/Elevators
        [ApiExplorerSettings(IgnoreApi = true)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        {
          if (_context.Elevators == null)
          {
              return Problem("Entity set 'ElevatorControlContext.Elevators'  is null.");
          }
            _context.Elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElevator", new { id = elevator.Id }, elevator);
        }

        // DELETE: api/Elevators/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevator(long id)
        {
            if (_context.Elevators == null)
            {
                return NotFound();
            }
            var elevator = await _context.Elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.Elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Elevators/5/NextFloor
        [HttpGet("{id}/NextFloor")]
        public async Task<ActionResult<string>> GetElevatorNextFloor(long id)
        {
            if (_context.Elevators == null)
            {
                return NotFound();
            }
            var elevator = await _context.Elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            if (elevator.FloorsToService.IsNullOrEmpty()) 
            { 
                return "-"; 
            }

            return elevator.FloorsToService.Split(',')[0];
        }

        // GET: api/Elevators/5/ServicingFloors
        [HttpGet("{id}/ServicingFloors")]
        public async Task<ActionResult<IEnumerable<string>>> GetServicingFloors(long id)
        {
            if (_context.Elevators == null)
            {
                return NotFound();
            }
            var elevator = await _context.Elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            if (elevator.FloorsToService.IsNullOrEmpty()) 
            { 
                return new List<string>(); 
            }

            return elevator.FloorsToService.Split(',').ToList();
        }

        private bool ElevatorExists(long id)
        {
            return (_context.Elevators?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
