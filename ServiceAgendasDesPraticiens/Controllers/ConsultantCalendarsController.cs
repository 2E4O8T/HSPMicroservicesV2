using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceAgendasDesPraticiens.Data;
using ServiceAgendasDesPraticiens.Models;

namespace ServiceAgendasDesPraticiens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantCalendarsController : ControllerBase
    {
        private readonly HSPCalendarDbContext _context;

        public ConsultantCalendarsController(HSPCalendarDbContext context)
        {
            _context = context;
        }

        // GET: api/ConsultantCalendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultantCalendar>>> GetConsultantsCalendars()
        {
          if (_context.ConsultantsCalendars == null)
          {
              return NotFound();
          }
            return await _context.ConsultantsCalendars.ToListAsync();
        }

        // GET: api/ConsultantCalendars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultantCalendar>> GetConsultantCalendar(int id)
        {
          if (_context.ConsultantsCalendars == null)
          {
              return NotFound();
          }
            var consultantCalendar = await _context.ConsultantsCalendars.FindAsync(id);

            if (consultantCalendar == null)
            {
                return NotFound();
            }

            return consultantCalendar;
        }

        // PUT: api/ConsultantCalendars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultantCalendar(int id, ConsultantCalendar consultantCalendar)
        {
            if (id != consultantCalendar.Id)
            {
                return BadRequest();
            }

            _context.Entry(consultantCalendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantCalendarExists(id))
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

        // POST: api/ConsultantCalendars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConsultantCalendar>> PostConsultantCalendar(ConsultantCalendar consultantCalendar)
        {
          if (_context.ConsultantsCalendars == null)
          {
              return Problem("Entity set 'HSPCalendarDbContext.ConsultantsCalendars'  is null.");
          }
            _context.ConsultantsCalendars.Add(consultantCalendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsultantCalendar", new { id = consultantCalendar.Id }, consultantCalendar);
        }

        // DELETE: api/ConsultantCalendars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultantCalendar(int id)
        {
            if (_context.ConsultantsCalendars == null)
            {
                return NotFound();
            }
            var consultantCalendar = await _context.ConsultantsCalendars.FindAsync(id);
            if (consultantCalendar == null)
            {
                return NotFound();
            }

            _context.ConsultantsCalendars.Remove(consultantCalendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultantCalendarExists(int id)
        {
            return (_context.ConsultantsCalendars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
