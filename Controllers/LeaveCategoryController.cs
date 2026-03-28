using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings.Leave;
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
    public class LeaveCategoryController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public LeaveCategoryController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        // GET: api/LeaveCategory
        [HttpGet]
        public async Task<ActionResult<List<mzlm_leave_category_dto>>> Getmzlm_leave_category([FromQuery] int? its)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_category == null)
            {
                return NotFound();
            }

            List<mzlm_leave_category> mzlm_leave_category = _context.mzlm_leave_category.Include(x => x.mzlm_leave_application).ThenInclude(x => x.stage).ToList();
            List<mzlm_leave_category_dto> mzlm_leave_category_dto = mzlm_leave_category.Select(x => _mapper.Map<mzlm_leave_category_dto>(x)).ToList();

            if (its != null)
            {
                foreach (mzlm_leave_category_dto item in mzlm_leave_category_dto)
                {
                    item.consumedLeaves = (float)mzlm_leave_category.Where(x => x.id == item.id).First().mzlm_leave_application.Where(x => x.itsId == its && x.stage.isDeductable == true).Sum(x => x.shiftCount) / (float)2;
                }
            }

            return mzlm_leave_category_dto;
        }

        // GET: api/LeaveCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mzlm_leave_category>> Getmzlm_leave_category(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_category == null)
            {
                return NotFound();
            }
            var mzlm_leave_category = await _context.mzlm_leave_category.FindAsync(id);

            if (mzlm_leave_category == null)
            {
                return NotFound();
            }

            return mzlm_leave_category;
        }

        [HttpGet("leavetype/{id}")]
        public async Task<ActionResult<List<mzlm_leave_category_dto>>> Getmzlm_leave_category_By_Leave_Type(int id, [FromQuery] int? its)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_category == null)
            {
                return NotFound();
            }
            List<mzlm_leave_category> mzlm_leave_category = _context.mzlm_leave_category.Where(x => x.leaveTypeId == id).Include(x => x.mzlm_leave_application).ThenInclude(x => x.stage).ToList();

            if (mzlm_leave_category == null)
            {
                return NotFound();
            }

            List<mzlm_leave_category_dto> mzlm_leave_category_dto = mzlm_leave_category.Select(x => _mapper.Map<mzlm_leave_category_dto>(x)).ToList();

            if (its != null)
            {
                foreach (mzlm_leave_category_dto item in mzlm_leave_category_dto)
                {
                    item.consumedLeaves = (float)mzlm_leave_category.Where(x => x.id == item.id).First().mzlm_leave_application.Where(x => x.itsId == its && x.stage.isDeductable == true).Sum(x => x.shiftCount) / (float)2;
                }
            }

            return mzlm_leave_category_dto;
        }

        // PUT: api/LeaveCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmzlm_leave_category(int id, mzlm_leave_category_dto mzlm_leave_category_dto)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (id != mzlm_leave_category_dto.id)
            {
                return BadRequest();
            }
            mzlm_leave_category mzlm_leave_category = _mapper.Map<mzlm_leave_category>(mzlm_leave_category_dto);
            _context.Entry(mzlm_leave_category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mzlm_leave_categoryExists(id))
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

        // POST: api/LeaveCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<mzlm_leave_category>> Postmzlm_leave_category(mzlm_leave_category_dto mzlm_leave_category)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_category == null)
            {
                return Problem("Entity set 'mzdb_context.mzlm_leave_category'  is null.");
            }
            mzlm_leave_category mzlm_leave_category_db = _mapper.Map<mzlm_leave_category>(mzlm_leave_category);
            _context.mzlm_leave_category.Add(mzlm_leave_category_db);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mzlm_leave_categoryExists(mzlm_leave_category_db.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getmzlm_leave_category", new { id = mzlm_leave_category_db.id }, mzlm_leave_category_db);
        }

        // DELETE: api/LeaveCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemzlm_leave_category(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_category == null)
            {
                return NotFound();
            }
            var mzlm_leave_category = await _context.mzlm_leave_category.FindAsync(id);
            if (mzlm_leave_category == null)
            {
                return NotFound();
            }

            _context.mzlm_leave_category.Remove(mzlm_leave_category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mzlm_leave_categoryExists(int id)
        {
            return (_context.mzlm_leave_category?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
