using mahadalzahrawebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationEmailTemplateController : ControllerBase
    {
        private readonly mzdbContext _context;

        public NotificationEmailTemplateController(mzdbContext context)
        {
            _context = context;
        }

        // GET: api/NotificationEmailTemplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<notification_email_template>>> Getnotification_email_template()
        {
            if (_context.notification_email_template == null)
            {
                return NotFound();
            }
            return await _context.notification_email_template.ToListAsync();
        }

        // GET: api/NotificationEmailTemplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<notification_email_template>> Getnotification_email_template(int id)
        {
            if (_context.notification_email_template == null)
            {
                return NotFound();
            }
            var notification_email_template = await _context.notification_email_template.FindAsync(id);

            if (notification_email_template == null)
            {
                return NotFound();
            }

            return notification_email_template;
        }

        // PUT: api/NotificationEmailTemplate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putnotification_email_template(int id, notification_email_template notification_email_template)
        {
            if (id != notification_email_template.id)
            {
                return BadRequest();
            }

            _context.Entry(notification_email_template).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!notification_email_templateExists(id))
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

        // POST: api/NotificationEmailTemplate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<notification_email_template>> Postnotification_email_template(notification_email_template notification_email_template)
        {
            if (_context.notification_email_template == null)
            {
                return Problem("Entity set 'mzdb_context.notification_email_template'  is null.");
            }
            _context.notification_email_template.Add(notification_email_template);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (notification_email_templateExists(notification_email_template.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getnotification_email_template", new { id = notification_email_template.id }, notification_email_template);
        }

        // DELETE: api/NotificationEmailTemplate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletenotification_email_template(int id)
        {
            if (_context.notification_email_template == null)
            {
                return NotFound();
            }
            var notification_email_template = await _context.notification_email_template.FindAsync(id);
            if (notification_email_template == null)
            {
                return NotFound();
            }

            _context.notification_email_template.Remove(notification_email_template);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool notification_email_templateExists(int id)
        {
            return (_context.notification_email_template?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
