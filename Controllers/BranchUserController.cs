using AutoMapper;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchUserController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public BranchUserController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult<List<branch_user_dto>>> Getbranch_user()
        {
            var v = _context.branch_user.ToList();
            if (_context.user == null)
            {
                return NotFound();
            }



            return _mapper.Map<List<branch_user_dto>>(v);
        }

        // GET: api/branch_user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<branch_user>> Getbranch_user(int id)
        {
            if (_context.user == null)
            {
                return NotFound();
            }
            var user = await _context.branch_user.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/branch_user/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbranch_user(int id, branch_user branch_user)
        {
            if (id != branch_user.itsId)
            {
                return BadRequest();
            }

            _context.Entry(branch_user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!branch_userExists(id))
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

        // POST: api/branch_user
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<branch_user>> Postbranch_user(branch_user branch_user)
        {
            if (_context.user == null)
            {
                return Problem("Entity set 'mzdb_context.branch_user'  is null.");
            }
            _context.branch_user.Add(branch_user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (branch_userExists(branch_user.its))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getbranch_user", new { id = branch_user.its }, branch_user);
        }

        private bool branch_userExists(object id)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/branch_user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletebranch_user(int id)
        {
            if (_context.user == null)
            {
                return NotFound();
            }
            var user = await _context.branch_user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.branch_user.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
