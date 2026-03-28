using AutoMapper;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly HelperService _helperService;
        private readonly globalConstants _globalConstants;

        public LeaveTypeController(mzdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _helperService = new HelperService(context);
            _globalConstants = new globalConstants();
        }

        // GET: api/LeaveType
        [HttpGet]
        public async Task<ActionResult<List<mzlm_leave_type_dto>>> Getmzlm_leave_type([FromQuery] int? its, [FromQuery] string? applicableTos, [FromQuery] string? accessTos)
        {
            if (_context.mzlm_leave_type == null)
            {
                return NotFound();
            }


            IQueryable<mzlm_leave_type> leave_types_db = _context.mzlm_leave_type;

            if (its != null)
            {
                leave_types_db = _context.mzlm_leave_type
                        .Include(x => x.mzlm_leave_application.Where(x => x.itsId == its))
                        .ThenInclude(x => x.stage);
            }

            // Apply filters based on 'applicableTos' if it is not null
            if (applicableTos != null)
            {
                List<string> applicableTo = _helperService.parseStrings(applicableTos);
                leave_types_db = leave_types_db.Where(x => applicableTo.Any(y => x.applicableTo.Contains(y)));
            }

            // Apply filters based on 'accessTos' if it is not null
            if (accessTos != null)
            {
                List<string> accessTo = _helperService.parseStrings(accessTos);
                leave_types_db = leave_types_db.Where(x => accessTo.Contains(x.accessTo));
            }

            List<mzlm_leave_type> mzlm_Leave_Types = await leave_types_db.ToListAsync();

            List<mzlm_leave_type_dto> leave_types = _mapper.Map<List<mzlm_leave_type_dto>>(mzlm_Leave_Types);

            if (its != null)
            {

                foreach (mzlm_leave_type_dto item in leave_types)
                {
                    item.consumedLeaves = (float)leave_types_db.Where(x => x.id == item.id).First().mzlm_leave_application.Where(x => x.itsId == its && x.stage.isDeductable == true && x.hijrAcademicYear == _globalConstants.currentAcademicYear).Sum(x => x.shiftCount) / (float)2;
                }
            }

            return leave_types;
        }

        // GET: api/LeaveType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mzlm_leave_type>> Getmzlm_leave_type(int id)
        {
            if (_context.mzlm_leave_type == null)
            {
                return NotFound();
            }
            var mzlm_leave_type = await _context.mzlm_leave_type.FindAsync(id);

            if (mzlm_leave_type == null)
            {
                return NotFound();
            }

            return mzlm_leave_type;
        }

        // PUT: api/LeaveType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmzlm_leave_type(int id, mzlm_leave_type mzlm_leave_type)
        {
            if (id != mzlm_leave_type.id)
            {
                return BadRequest();
            }

            _context.Entry(mzlm_leave_type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mzlm_leave_typeExists(id))
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

        // POST: api/LeaveType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<mzlm_leave_type>> Postmzlm_leave_type(mzlm_leave_type mzlm_leave_type)
        {
            if (_context.mzlm_leave_type == null)
            {
                return Problem("Entity set 'mzdb_context.mzlm_leave_type'  is null.");
            }
            _context.mzlm_leave_type.Add(mzlm_leave_type);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mzlm_leave_typeExists(mzlm_leave_type.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getmzlm_leave_type", new { id = mzlm_leave_type.id }, mzlm_leave_type);
        }

        // DELETE: api/LeaveType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemzlm_leave_type(int id)
        {
            if (_context.mzlm_leave_type == null)
            {
                return NotFound();
            }
            var mzlm_leave_type = await _context.mzlm_leave_type.FindAsync(id);
            if (mzlm_leave_type == null)
            {
                return NotFound();
            }

            _context.mzlm_leave_type.Remove(mzlm_leave_type);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mzlm_leave_typeExists(int id)
        {
            return (_context.mzlm_leave_type?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
