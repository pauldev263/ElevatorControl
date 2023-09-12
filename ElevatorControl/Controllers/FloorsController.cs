﻿using System;
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
    public class FloorsController : ControllerBase
    {
        private readonly ElevatorControlContext _context;

        public FloorsController(ElevatorControlContext context)
        {
            _context = context;
        }

        // GET: api/Floors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floor>>> GetFloors()
        {
          if (_context.Floors == null)
          {
              return NotFound();
          }
            return await _context.Floors.ToListAsync();
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Floor>> GetFloor(long id)
        {
          if (_context.Floors == null)
          {
              return NotFound();
          }
            var floor = await _context.Floors.FindAsync(id);

            if (floor == null)
            {
                return NotFound();
            }

            return floor;
        }

        // PUT: api/Floors/5
        [ApiExplorerSettings(IgnoreApi = true)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFloor(long id, Floor floor)
        {
            if (id != floor.Id)
            {
                return BadRequest();
            }

            _context.Entry(floor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorExists(id))
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

        // POST: api/Floors
        [ApiExplorerSettings(IgnoreApi = true)]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Floor>> PostFloor(Floor floor)
        {
          if (_context.Floors == null)
          {
              return Problem("Entity set 'ElevatorControlContext.Floors'  is null.");
          }
            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFloor", new { id = floor.Id }, floor);
        }

        // DELETE: api/Floors/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFloor(long id)
        {
            if (_context.Floors == null)
            {
                return NotFound();
            }
            var floor = await _context.Floors.FindAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FloorExists(long id)
        {
            return (_context.Floors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
