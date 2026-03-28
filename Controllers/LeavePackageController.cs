using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeavePackageController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly globalConstants globalConstants;
        private readonly HelperService _helperService;
        private readonly TokenService _tokenService;
        private readonly NotificationService _notificationService;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly string adminEmails = "admin@mahadalzahra.com, juzerdiwan@jameasaifiyah.edu";

        public LeavePackageController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _helperService = new HelperService(context);
            _tokenService = tokenService;
            globalConstants = new globalConstants();
            _notificationService = new NotificationService();
            _whatsAppApiService = new WhatsAppApiService(context);
        }

        // GET: api/LeavePackage
        [HttpGet]
        public async Task<ActionResult<List<mzlm_leave_package_dto>>> Getmzlm_leave_package(
            [FromQuery] string? appliedBy,
            [FromQuery] int? qismId,
            [FromQuery] string? stageIds,
            [FromQuery] int? academicYear,
            [FromQuery] string? approvalStages
        )
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (academicYear == null)
            {
                academicYear = globalConstants.currentAcademicYear;
            }

            // Fetch all necessary data first
            var query = _context.mzlm_leave_application
                .AsNoTracking()
                .Where(x => x.hijrAcademicYear == academicYear && x.packageId != null && x.its.mauze != null);

            // Apply filters
            if (appliedBy != null)
            {
                query = query.Where(la => la.appliedBy == appliedBy);
            }

            if (qismId != null)
            {
                query = query.Where(la => la.its.mauzeNavigation.qismId == qismId);
            }

            if (stageIds != null)
            {
                List<int> stageList = _helperService.parseIds(stageIds);
                query = query.Where(la => stageList.Contains(la.stageId));
            }

            if (approvalStages != null)
            {
                List<string> approvalStagesList = _helperService.parseStrings(approvalStages);
                query = query.Where(la => approvalStagesList.Contains(la.type.approvalFlow));
            }

            // Include necessary related data
            query = query.Include(la => la.category)
                         .Include(la => la.type)
                         .Include(la => la.package)
                         .Include(la => la.stage)
                         .Include(la => la.its)
                             .ThenInclude(its => its.mauzeNavigation)
                                 .ThenInclude(mauze => mauze.qism);

            // Fetch data as a list
            var leaveApplications = await query.ToListAsync();

            // Group by packageId and qismId and take the first application per group on the client side
            var groupedPackages = leaveApplications
                .GroupBy(x => new { x.packageId })
                .Select(g => g.First())
                .ToList();

            // Mapping entities to DTOs
            List<mzlm_leave_package_dto> mzlmPackageDto;
            try
            {
                mzlmPackageDto = _mapper.Map<List<mzlm_leave_package_dto>>(groupedPackages);
            }
            catch (Exception ex)
            {
                var errorDetails = $"An error occurred while mapping entities: {ex.Message}. StackTrace: {ex.StackTrace}";
                if (ex.InnerException != null)
                {
                    errorDetails += $" | InnerException: {ex.InnerException.Message}. Inner StackTrace: {ex.InnerException.StackTrace}";
                }
                // Log errorDetails for further inspection
                return BadRequest(errorDetails);
            }

            // Filter and sort the data
            mzlmPackageDto = mzlmPackageDto.Where(x => x.remainingDays >= 0)
                                            .OrderBy(x => x.remainingDays)
                                            .Concat(mzlmPackageDto
                                                .Where(x => x.remainingDays <= 0)
                                                .OrderByDescending(x => x.remainingDays))
                                            .ToList();

            return mzlmPackageDto;
        }




        // GET: api/LeavePackage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mzlm_leave_package>> Getmzlm_leave_package(int id)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_package == null)
            {
                return NotFound();
            }
            var mzlm_leave_package = await _context.mzlm_leave_package.FindAsync(id);

            if (mzlm_leave_package == null)
            {
                return NotFound();
            }

            return mzlm_leave_package;
        }

        [HttpGet("leavetype/{id}")]
        public async Task<ActionResult<IEnumerable<mzlm_leave_package>>> Getmzlm_leave_package_By_Leave_Type(int id)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_package == null)
            {
                return NotFound();
            }
            var mzlm_leave_package = _context.mzlm_leave_package.Where(x => x.id == id).ToImmutableArray();

            if (mzlm_leave_package == null)
            {
                return NotFound();
            }
            return mzlm_leave_package;
        }

        // PUT: api/LeavePackage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmzlm_leave_package(int id, mzlm_leave_package_dto mzlm_leave_packagedto)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateTime current = DateTime.UtcNow;
            if (id != mzlm_leave_packagedto.id)
            {
                return BadRequest();
            }

            // Retrieve the existing package
            mzlm_leave_package mzlm_leave_package_old = _context.mzlm_leave_package
                                                        .Include(p => p.mzlm_leave_application)  // Make sure to include related entities if needed
                                                        .FirstOrDefault(x => x.id == id);

            if (mzlm_leave_package_old == null)
            {
                return BadRequest(new { message = "Package Not Found" });
            }

            // Map changes from DTO onto the retrieved package
            _mapper.Map(mzlm_leave_packagedto, mzlm_leave_package_old);

            // Make any additional changes as needed
            foreach (var application in mzlm_leave_package_old.mzlm_leave_application)
            {
                application.stageId = mzlm_leave_packagedto.stageId;
                application.fromMonthId = mzlm_leave_packagedto.fromDate.Month;
                application.fromDayId = mzlm_leave_packagedto.fromDate.Day;
                application.fromYear = mzlm_leave_packagedto.fromDate.Year;
                application.toMonthId = mzlm_leave_packagedto.toDate.Month;
                application.toYear = mzlm_leave_packagedto.toDate.Year;
                application.toDayId = mzlm_leave_packagedto.toDate.Day;
                application.fromEngDate = mzlm_leave_packagedto.fromEngDate;
                application.toEngDate = mzlm_leave_packagedto.toEngDate;
            }
            _context.mzlm_leave_package_logs.Add(new mzlm_leave_package_logs
            {
                packageId = mzlm_leave_packagedto.id,
                createdOn = current,
                remark = mzlm_leave_packagedto.reason ?? "",
                createdBy = authUser.ItsId,
                stageId = mzlm_leave_packagedto.stageId
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mzlm_leave_packageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            mzlm_leave_package_update_notification(mzlm_leave_package_old.id, mzlm_leave_packagedto.reason);

            return NoContent();
        }

        // POST: api/LeavePackage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<mzlm_leave_package>> Postmzlm_leave_package(mzlm_leave_package_dto mzlm_leave_package)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_package == null)
            {
                return Problem("Entity set 'mzdb_context.mzlm_leave_package'  is null.");
            }
            mzlm_leave_package mzlm_leave_package_db = _mapper.Map<mzlm_leave_package>(mzlm_leave_package);
            _context.mzlm_leave_package.Add(mzlm_leave_package_db);
            try
            {
                if (!string.IsNullOrEmpty(mzlm_leave_package.UploadedFileBase64))
                {
                    byte[] fileBytes = Convert.FromBase64String(mzlm_leave_package.UploadedFileBase64);
                    using (var stream = new MemoryStream(fileBytes))
                    {
                        string fileUrl = await _helperService.UploadFileToS3(stream, mzlm_leave_package.UploadedFileName, "LeaveApplication");
                        mzlm_leave_package_db.UploadedDocumentUrl = fileUrl;
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mzlm_leave_packageExists(mzlm_leave_package_db.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getmzlm_leave_package", new { id = mzlm_leave_package_db.id }, mzlm_leave_package_db);
        }

        // DELETE: api/LeavePackage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemzlm_leave_package(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_package == null)
            {
                return NotFound();
            }
            var mzlm_leave_package = await _context.mzlm_leave_package.FindAsync(id);
            if (mzlm_leave_package == null)
            {
                return NotFound();
            }

            _context.mzlm_leave_package.Remove(mzlm_leave_package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mzlm_leave_packageExists(int id)
        {
            return (_context.mzlm_leave_package?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private void mzlm_leave_package_update_notification(int mzlm_leave_package_id, string reason)
        {

            List<mzlm_leave_application> mzlm_leave_applications = _context.mzlm_leave_application
                    .Where(app => app.packageId == mzlm_leave_package_id)
                    .Include(app => app.package)
                    .Include(app => app.type)
                    .Include(app => app.category) // Include the Category navigation property
                    .Include(app => app.its) // Include the its navigation property
                        .ThenInclude(its => its.mauzeNavigation)
                            .ThenInclude(mauze => mauze.qism) // Continue including nested properties as necessary
                            .ThenInclude(qism => qism.its)
                            .ThenInclude(its => its.its)
                    .ToList();
            mzlm_leave_application mzlm_leave_application = mzlm_leave_applications.FirstOrDefault();

            string branchEmail = mzlm_leave_application.its.mauzeNavigation.qism.emailId;
            //string branchEmail = "hatimn219@gmail.com";
            //string applicantEmail = "hatimn219@gmail.com";

            if (mzlm_leave_application.stageId == 5 && mzlm_leave_application.appliedBy == "Branch")
            {

                string msg = @"<p><br />Salaam Jameel,</p>
                    <p>Please be informed that the Idara Office has approved your leave request and a notification has been sent to all individuals eligible for this leave.</p>
                    <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; Leave Management for details.</p>
                    <p>Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave Application Approved by Idara Admin", msg, branchEmail, "leave");

                foreach (mzlm_leave_application item in mzlm_leave_applications)
                {
                    string applicantEmail = string.IsNullOrEmpty(item.its.officialEmailAddress) ? item.its.emailAddress : item.its.officialEmailAddress;

                    if (string.IsNullOrEmpty(applicantEmail))
                    {
                        continue;
                    }

                    string msg2 = @"<p><br />Salaam Jameel,<br />" + item.its.fullName + @"<br />" + item.itsId + @"</p>
                        <p>Please be informed that the Idara Office has approved a leave requested by your Branch Admin.</p>
                        <p>Details:<br />Leave type: " + item.type.name + @"
                        <br />Leave category: " + item.category.name + @"
                        <br />From date: " + item.fromDayId + "/" + item.fromMonthId + "/" + item.fromYear + @"
                        <br />To date: " + item.toDayId + "/" + item.toMonthId + "/" + item.toYear + @"
                        <br />No of days: " + ((float)item.shiftCount / (float)2) + @"</p>
                        <p><br />Kindly check www.mahadalzahra.org &gt; HR login &gt; Leave Application for more details.</p>
                        <p>Wa al-Salaam.</p>";
                    _notificationService.SendStandardHTMLEmail("Leave Application Approved by Idara Admin", msg2, applicantEmail, "leave");

                    try
                    {
                        List<string> num = new List<string> { item.its.c_codeWhatsapp + item.its.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br />", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_package_update_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            if (mzlm_leave_application.stageId == 7 && mzlm_leave_application.appliedBy == "Branch")
            {
                string msg = @"<p><br />Salaam Jameel,<br />Please be informed that the Idara Office has <span style=""color:red"">rejected</span> your leave request.</p>
                    <p>Details:<br />Leave type: " + mzlm_leave_application.type.name + @"<br />Leave category: " + mzlm_leave_application.category.name + @"<br />From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"<br />To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"<br />No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"<br />Remark: " + reason + @"</p>
                    <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; Leave Management for more details.</p>
                    <p>Shukran,<br />Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave Application Request Rejected by Idara Admin", msg, branchEmail, "leave");
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br />", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_package_update_notification");
                }
                catch (Exception ex)
                {

                }
            }
        }

    }
}
