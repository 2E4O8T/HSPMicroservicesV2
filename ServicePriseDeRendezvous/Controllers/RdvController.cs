using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicePriseDeRendezvous.Data;
using ServicePriseDeRendezvous.Models;

namespace ServicePriseDeRendezvous.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("ClientIdPolicy")]
    public class RdvsController : ControllerBase
    {
        private readonly RdvsApiContext _context;

        public RdvsController(RdvsApiContext context)
        {
            _context = context;
        }

        // GET: api/Rdvs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rdv>>> GetRdvs()
        {
            return await _context.Rdvs.ToListAsync();
        }

        // GET: api/Rdvs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rdv>> GetRdv(int id)
        {
            var rdv = await _context.Rdvs.FindAsync(id);

            if (rdv == null)
            {
                return NotFound();
            }

            return rdv;
        }

        // PUT: api/Rdvs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRdv(int id, Rdv rdv)
        {
            if (id != rdv.Id)
            {
                return BadRequest();
            }

            _context.Entry(rdv).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RdvExists(id))
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

        // POST: api/Rdvs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rdv>> PostRdv(Rdv rdv)
        {
            _context.Rdvs.Add(rdv);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRdv", new { id = rdv.Id }, rdv);
        }

        // DELETE: api/Rdvs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRdv(int id)
        {
            var rdv = await _context.Rdvs.FindAsync(id);
            if (rdv == null)
            {
                return NotFound();
            }

            _context.Rdvs.Remove(rdv);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RdvExists(int id)
        {
            return _context.Rdvs.Any(e => e.Id == id);
        }
    }
}
