using mahadalzahrawebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveStageController : ControllerBase
    {
        private readonly mzdbContext _context;

        public LeaveStageController(mzdbContext context)
        {
            _context = context;
        }

        // GET: api/LeaveStage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<mzlm_leave_stage>>> Getmzlm_leave_stage()
        {
            if (_context.mzlm_leave_stage == null)
            {
                return NotFound();
            }
            return await _context.mzlm_leave_stage.ToListAsync();
        }

        // GET: api/LeaveStage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mzlm_leave_stage>> Getmzlm_leave_stage(int id)
        {
            if (_context.mzlm_leave_stage == null)
            {
                return NotFound();
            }
            var mzlm_leave_stage = await _context.mzlm_leave_stage.FindAsync(id);

            if (mzlm_leave_stage == null)
            {
                return NotFound();
            }

            return mzlm_leave_stage;
        }

        // PUT: api/LeaveStage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmzlm_leave_stage(int id, mzlm_leave_stage mzlm_leave_stage)
        {
            if (id != mzlm_leave_stage.id)
            {
                return BadRequest();
            }

            _context.Entry(mzlm_leave_stage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mzlm_leave_stageExists(id))
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

        // POST: api/LeaveStage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<mzlm_leave_stage>> Postmzlm_leave_stage(mzlm_leave_stage mzlm_leave_stage)
        {
            if (_context.mzlm_leave_stage == null)
            {
                return Problem("Entity set 'mzdb_context.mzlm_leave_stage'  is null.");
            }
            _context.mzlm_leave_stage.Add(mzlm_leave_stage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mzlm_leave_stageExists(mzlm_leave_stage.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getmzlm_leave_stage", new { id = mzlm_leave_stage.id }, mzlm_leave_stage);
        }

        // DELETE: api/LeaveStage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemzlm_leave_stage(int id)
        {
            if (_context.mzlm_leave_stage == null)
            {
                return NotFound();
            }
            var mzlm_leave_stage = await _context.mzlm_leave_stage.FindAsync(id);
            if (mzlm_leave_stage == null)
            {
                return NotFound();
            }

            _context.mzlm_leave_stage.Remove(mzlm_leave_stage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mzlm_leave_stageExists(int id)
        {
            return (_context.mzlm_leave_stage?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
