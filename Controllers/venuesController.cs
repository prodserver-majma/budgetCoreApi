using AutoMapper;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;

        public VenueController(mzdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/venue
        [HttpGet]
        public async Task<ActionResult<List<venue_dto>>> Getvenue([FromQuery] int? qismId)
        {
            if (_context.venue == null)
            {
                return NotFound();
            }

            List<venue_dto> v = await _context.venue.Select(x => new venue_dto()
            {
                Id = x.Id,
                ActiveStatus = x.ActiveStatus,
                CampId = x.CampId,
                CampVenue = x.CampVenue,
                CashBalance = x.CashBalance,
                currency = x.currency,
                displayName = x.displayName,
                ho = x.ho,
                qismTahfeez = x.qismTahfeez,
                qismId = x.qismId ?? 0,

                pset = _context.registrationform_dropdown_set.Where( c => c.venueId == x.Id).Select(c => new pset_dto
                {
                    id = c.id,
                    name = _context.registrationform_subprograms.Where(s => s.id == c.subprogramId).Select(s => s.name).FirstOrDefault()
                }).ToList()
            }).ToListAsync();

            //v = v.Where(x => x.qismId != 0).OrderBy(x => x.qismId).ToList();

            //if (qismId != null)
            //{
            //    v = v.Where(x => x.qismId == qismId).ToList();
            //}

            v = v.OrderBy(x => x.displayName).ToList();

            return v;
        }

        // GET: api/venue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<venue>> Getvenue(int id)
        {
            if (_context.venue == null)
            {
                return NotFound();
            }
            var venue = await _context.venue.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return venue;
        }

        // PUT: api/venue/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putvenue(int id, venue venue)
        {
            if (id != venue.Id)
            {
                return BadRequest();
            }

            _context.Entry(venue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!venueExists(id))
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

        // POST: api/venue
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<venue>> Postvenue(venue venue)
        {
            if (_context.venue == null)
            {
                return Problem("Entity set 'mzdb_context.venue'  is null.");
            }
            _context.venue.Add(venue);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (venueExists(venue.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getvenue", new { id = venue.Id }, venue);
        }

        // DELETE: api/venue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletevenue(int id)
        {
            if (_context.venue == null)
            {
                return NotFound();
            }
            var venue = await _context.venue.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            _context.venue.Remove(venue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool venueExists(int id)
        {
            return (_context.venue?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
