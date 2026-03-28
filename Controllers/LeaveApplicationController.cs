using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Hangfire;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApplicationController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly HelperService _helperService;
        private readonly NotificationService _notificationService;
        private readonly globalConstants globalConstants;
        private readonly WhatsAppApiService _whatsAppApiService;

        public LeaveApplicationController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _helperService = new HelperService(context);
            _tokenService = tokenService;
            _notificationService = new NotificationService();
            _whatsAppApiService = new WhatsAppApiService(context);
            globalConstants = new globalConstants();
        }

        // GET: api/LeaveApplication
        [HttpGet]
        public async Task<ActionResult<List<mzlm_leave_application_dto>>> Getmzlm_leave_application(
            [FromQuery] int? itsId,
            [FromQuery] int? qismId,
            [FromQuery] string? stageIds,
            [FromQuery] string? typeIds,
            [FromQuery] bool? isdeductable,
            [FromQuery] string? appliedbys,
            [FromQuery] string? approvalStages,
            [FromQuery] int? academicYear
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (academicYear == null)
            {
                academicYear = globalConstants.currentAcademicYear;
            }
            var applications = _context.mzlm_leave_application
                .Include(la => la.category)
                .Include(la => la.type)
                .Include(la => la.stage)
                .Include(x => x.its)
                .ThenInclude(x => x.mauzeNavigation)
                .ThenInclude(x => x.qism)
                .Where(x => x.hijrAcademicYear == academicYear);

            if (applications == null || !applications.Any())
            {
                return NotFound();
            }

            if (itsId != null)
            {
                applications = applications.Where(x => x.itsId == itsId && (x.appliedBy == "Individual" || (x.appliedBy == "Admin" && x.stageId == 1) || (x.appliedBy == "Branch" && x.stageId == 5)));
            }
            if (qismId != null)
            {
                applications = applications.Where(x => x.its.mauzeNavigation.qismId == qismId);
            }
            if (typeIds != null)
            {
                List<int> typeList = _helperService.parseIds(typeIds);
                applications = applications.Where(x => typeList.Any(y => y == x.typeId));
            }
            if (isdeductable != null)
            {
                applications = applications.Where(x => x.stage.isDeductable == isdeductable);
            }
            if (stageIds != null)
            {
                List<int> stageList = _helperService.parseIds(stageIds);
                applications = applications.Where(x => stageList.Any(y => y == x.stageId));
            }
            if (appliedbys != null)
            {
                List<string> appliedbyList = _helperService.parseStrings(appliedbys);
                applications = applications.Where(x => appliedbyList.Any(y => y == x.appliedBy));
            }
            if (approvalStages != null)
            {
                List<string> approvalStageList = _helperService.parseStrings(approvalStages);
                applications = applications.Where(x => approvalStageList.Any(y => y == x.type.approvalFlow));
            }

            List<mzlm_leave_application> applicationsList = await applications.ToListAsync();

            List<mzlm_leave_application_dto> mzlm_Leave_Application_Dtos = _mapper.Map<List<mzlm_leave_application_dto>>(applicationsList);

            mzlm_Leave_Application_Dtos = mzlm_Leave_Application_Dtos
                                            .Where(x => x.remainingDays >= 0)
                                            .OrderBy(x => x.remainingDays)
                                            .Concat(mzlm_Leave_Application_Dtos
                                                .Where(x => x.remainingDays < 0)
                                                .OrderByDescending(x => x.remainingDays))
                                            .ToList();

            mzlm_Leave_Application_Dtos = mzlm_Leave_Application_Dtos.OrderByDescending(x => x.onLeave).ToList();

            return mzlm_Leave_Application_Dtos;
        }

        // GET: api/LeaveApplication/package/5
        [HttpGet("package/{id}")]
        public async Task<ActionResult<List<mzlm_leave_application_dto>>> Getmzlm_leave_applicationByPackage(int id, [FromQuery] int? qismId)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_application == null)
            {
                return NotFound();
            }
            var mzlm_leave_application = await _context.mzlm_leave_application
                .Include(x => x.mzlm_leave_logs)
                .Include(x => x.category)
                .Include(x => x.type)
                .Include(x => x.stage)
                .Include(x => x.its)
                .ThenInclude(x => x.mauzeNavigation)
                .ThenInclude(x => x.qism)
                .Where(x => x.packageId == id).ToListAsync();

            if (mzlm_leave_application == null)
            {
                return NotFound();
            }
            mzlm_leave_application = mzlm_leave_application.OrderByDescending(x => x.its.age).ToList();

            List<mzlm_leave_application_dto> leaveApplicationsDto = _mapper.Map<List<mzlm_leave_application_dto>>(mzlm_leave_application);
            if (qismId != null)
            {
                leaveApplicationsDto = leaveApplicationsDto.Where(x => x.qismId == qismId).ToList();
            }

            return leaveApplicationsDto;
        }

        // GET: api/LeaveApplication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mzlm_leave_application>> Getmzlm_leave_application(int id)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_application == null)
            {
                return NotFound();
            }
            var mzlm_leave_application = await _context.mzlm_leave_application.FindAsync(id);

            if (mzlm_leave_application == null)
            {
                return NotFound();
            }

            return mzlm_leave_application;
        }

        // PUT: api/LeaveApplication/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmzlm_leave_application(int id, mzlm_leave_application_dto mzlm_leave_applicationdto)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateTime current = DateTime.UtcNow;
            if (id != mzlm_leave_applicationdto.id)
            {
                return BadRequest();
            }

            // Fetch the existing leave application from the database
            mzlm_leave_application existingApplication = await _context.mzlm_leave_application.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            if (existingApplication == null)
            {
                return NotFound();
            }

            // Switch block to restrict modification of stage based on conditions
            switch (existingApplication.stageId)
            {
                case 1: // Assigned by Idara Admin
                    if (!(mzlm_leave_applicationdto.stageId == 2 || mzlm_leave_applicationdto.stageId == 16))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 2: // Cancelled by Idara Admin
                    return BadRequest(new { message = "Invalid stage transition." });
                case 3: // Pending with Idara Admin
                    if (!(mzlm_leave_applicationdto.stageId == 5 || mzlm_leave_applicationdto.stageId == 7 || mzlm_leave_applicationdto.stageId == 13))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 4: // Pending with Branch Admin
                    if (!(mzlm_leave_applicationdto.stageId == 3 || mzlm_leave_applicationdto.stageId == 8 || mzlm_leave_applicationdto.stageId == 16 || mzlm_leave_applicationdto.stageId == 13))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 5: // Approved by Idara Admin
                    if (!(mzlm_leave_applicationdto.stageId == 13 || mzlm_leave_applicationdto.stageId == 15 || mzlm_leave_applicationdto.stageId == 11))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 6: // Auto Approved
                    if (!(mzlm_leave_applicationdto.stageId == 13 || mzlm_leave_applicationdto.stageId == 15 || mzlm_leave_applicationdto.stageId == 11))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 9: // Cancellation approved by Idara Admin
                    return BadRequest(new { message = "Invalid stage transition." });
                case 10: // Cancellation approved by Branch Admin
                    if (!(mzlm_leave_applicationdto.stageId == 16 || mzlm_leave_applicationdto.stageId == 17))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 15: // Leave expired
                    return BadRequest(new { message = "Invalid stage transition." });
                case 16: // Cancelled by Idara Admin (duplicate case)
                    return BadRequest(new { message = "Invalid stage transition." });
                case 17: // Cancelled by Branch Admin
                    return BadRequest(new { message = "Invalid stage transition." });
                case 11: // Cancellation requested by Branch Admin
                    if (!(mzlm_leave_applicationdto.stageId == 10 || mzlm_leave_applicationdto.stageId == 16))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 12: // Leave requested by Branch Admin
                    if (!(mzlm_leave_applicationdto.stageId == 5 || mzlm_leave_applicationdto.stageId == 7 || mzlm_leave_applicationdto.stageId == 21 || mzlm_leave_applicationdto.stageId == 16))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 13: // Cancellation requested by User
                    if (!(mzlm_leave_applicationdto.stageId == 10 || mzlm_leave_applicationdto.stageId == 9 || mzlm_leave_applicationdto.stageId == 16 || mzlm_leave_applicationdto.stageId == 17))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 14: // Leave requested
                    if (!(mzlm_leave_applicationdto.stageId == 4 || mzlm_leave_applicationdto.stageId == 8 || mzlm_leave_applicationdto.stageId == 16))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 18: // Approved by Branch Admin
                    if (!(mzlm_leave_applicationdto.stageId == 11 || mzlm_leave_applicationdto.stageId == 13 || mzlm_leave_applicationdto.stageId == 16))
                    {
                        return BadRequest(new { message = "Invalid stage transition." });
                    }
                    break;
                case 19: // Cancellation rejected by Branch Admin
                    return BadRequest(new { message = "Invalid stage transition." });
                case 20: // Cancellation rejected by Idara Admin
                    return BadRequest(new { message = "Invalid stage transition." });
                default:
                    return BadRequest(new { message = "Invalid stage transition." });
            }

            mzlm_leave_application mzlm_leave_application = _mapper.Map<mzlm_leave_application>(mzlm_leave_applicationdto);

            _context.mzlm_leave_logs.Add(new mzlm_leave_logs
            {
                leaveId = mzlm_leave_application.id,
                createdOn = current,
                remark = mzlm_leave_applicationdto.reason ?? "",
                createdBy = authUser.ItsId,
                stageId = mzlm_leave_applicationdto.stageId
            });

            mzlm_leave_application.updatedOn = current;
            _context.Entry(mzlm_leave_application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mzlm_leave_applicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            mzlm_leave_individual_Application_update_notification(mzlm_leave_application.id);

            return NoContent();
        }


        // POST: api/LeaveApplication
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<mzlm_leave_application>>> Postmzlm_leave_application(mzlm_leave_application_dto mzlm_leave_application_dto)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_application == null)
            {
                return Problem("Entity set 'mzdb_context.mzlm_leave_application'  is null.");
            }

            DateTime current = DateTime.UtcNow;

            mzlm_leave_application mzlm_leave_application = _mapper.Map<mzlm_leave_application>(mzlm_leave_application_dto);
            mzlm_leave_application.hijrAcademicYear = _helperService.getAcedemicYear(mzlm_leave_application.fromEngDate).acedemicYear;

            khidmat_guzaar? its = _context.khidmat_guzaar.Where(x => x.itsId == mzlm_leave_application.itsId).FirstOrDefault();

            if (its != null)
            {
                mzlm_leave_application.venueId = its.mauze ?? 0;
            }

            if (mzlm_leave_application.createdOn != null)
            {
                mzlm_leave_application.createdOn = current;
            }

            if (mzlm_leave_application.fromEngDate < current.AddDays(-1))
            {
                return BadRequest(new { message = "From Date which has been selected has already passed! Cannot apply" });
            }

            mzlm_leave_application.createdBy = authUser.ItsId;

            if (!string.IsNullOrEmpty(mzlm_leave_application_dto.UploadedFileBase64))
            {
                byte[] fileBytes = Convert.FromBase64String(mzlm_leave_application_dto.UploadedFileBase64);
                using (var stream = new MemoryStream(fileBytes))
                {
                    string fileUrl = await _helperService.UploadFileToS3(stream, mzlm_leave_application_dto.UploadedFileName, "LeaveApplication");
                    mzlm_leave_application.UploadedDocumentUrl = fileUrl;
                }
            }

            if (mzlm_leave_application_dto.leaveBulkAssignation == null)
            {
                mzlm_leave_application.mzlm_leave_logs.Add(new mzlm_leave_logs
                {
                    createdOn = mzlm_leave_application.createdOn,
                    remark = mzlm_leave_application_dto.purpose ?? "",
                    createdBy = authUser.ItsId,
                    stageId = mzlm_leave_application_dto.stageId
                });
                _context.mzlm_leave_application.Add(mzlm_leave_application);
            }
            else
            {
                List<khidmat_guzaar> kgs = _context.khidmat_guzaar.ToList();
                List<int> itsIds = new List<int>();

                itsIds = _helperService.parseItsId(mzlm_leave_application_dto.leaveBulkAssignation.itsCsv ?? "");

                mzlm_leave_package pck = _context.mzlm_leave_package.Add(new mzlm_leave_package
                {
                    name = "",
                    purpose = mzlm_leave_application_dto.purpose ?? "",
                    stageId = mzlm_leave_application_dto.stageId,
                    leaveBulkAssignationJson = JsonSerializer.Serialize(mzlm_leave_application_dto.leaveBulkAssignation),

                }).Entity;

                pck.mzlm_leave_package_logs.Add(new mzlm_leave_package_logs
                {
                    createdOn = current,
                    remark = mzlm_leave_application_dto.purpose ?? "",
                    createdBy = authUser.ItsId,
                    stageId = mzlm_leave_application_dto.stageId
                });

                if (mzlm_leave_application_dto.leaveBulkAssignation.mauze.Count() > 0)
                {
                    if (mzlm_leave_application_dto.leaveBulkAssignation.mzCategory.Count() > 0)
                    {
                        List<int> temp = kgs
                            .Where(x =>
                            x.activeStatus == true &&
                            mzlm_leave_application_dto.leaveBulkAssignation.mauze.Any(y => y == (x.mauze ?? 0)) &&
                            mzlm_leave_application_dto.leaveBulkAssignation.mzCategory.Any(y => y == x.mz_idara))
                            .Select(x => x.itsId).ToList();
                        itsIds.AddRange(temp);
                    }
                    else
                    {
                        List<int> temp = kgs
                            .Where(x =>
                            x.activeStatus == true &&
                            mzlm_leave_application_dto.leaveBulkAssignation.mauze.Any(y => y == (x.mauze ?? 0)))
                            .Select(x => x.itsId).ToList();
                        itsIds.AddRange(temp
                            );
                    }
                }
                else if (mzlm_leave_application_dto.leaveBulkAssignation.mzCategory.Count() > 0)
                {
                    List<int> temp = kgs
                            .Where(x =>
                            x.activeStatus == true &&
                            mzlm_leave_application_dto.leaveBulkAssignation.mzCategory.Any(y => y == x.mz_idara))
                            .Select(x => x.itsId).ToList();
                    itsIds.AddRange(temp);
                }

                foreach (int itsId in itsIds)
                {
                    khidmat_guzaar kg = kgs.Where(x => x.itsId == itsId).FirstOrDefault();
                    mzlm_leave_application temp = new mzlm_leave_application();
                    temp = _mapper.Map<mzlm_leave_application>(mzlm_leave_application);
                    temp.itsId = itsId;
                    temp.its = kg;
                    temp.package = pck;
                    temp.venueId = kg.mauze ?? 0;
                    mzlm_leave_application = _context.mzlm_leave_application.Add(temp).Entity;
                }


            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (mzlm_leave_applicationExists(mzlm_leave_application.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return base.BadRequest(e.ToString());
            }

            if (mzlm_leave_application_dto.leaveBulkAssignation != null)
            {
                if (mzlm_leave_application_dto.notifyIndividual == true)
                {

                    mzlm_leave_individual_notification_package(mzlm_leave_application.packageId ?? 0);
                }

                if (mzlm_leave_application_dto.notifyBranch == true)
                {
                    mzlm_leave_branch_notification_package(mzlm_leave_application.packageId ?? 0, mzlm_leave_application_dto);
                }

                if (mzlm_leave_application_dto.appliedBy == "Branch")
                {
                    mzlm_leave_branch_application_notification(mzlm_leave_application.packageId ?? 0);
                }
            }
            else
            {

                // Assuming you've saved the application and have its ID:
                var applicationId = mzlm_leave_application.id;

                // Now, to get the application with its navigation properties:
                mzlm_leave_application applicationWithNavProps = await _context.mzlm_leave_application
                    .Where(app => app.id == applicationId)
                    .Include(app => app.category) // Include the Category navigation property
                    .Include(app => app.type) // Include the Category navigation property
                    .Include(app => app.its) // Include the its navigation property
                        .ThenInclude(its => its.mauzeNavigation)
                            .ThenInclude(mauze => mauze.qism) // Continue including nested properties as necessary
                            .ThenInclude(qism => qism.its)
                            .ThenInclude(its => its.its)
                    .FirstOrDefaultAsync();

                mzlm_leave_individual_Application_notification(applicationWithNavProps);

            }
            return CreatedAtAction("Getmzlm_leave_application", new { id = mzlm_leave_application.id }, _mapper.Map<mzlm_leave_application_dto>(mzlm_leave_application));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file provided." });

            var bucketName = "your-s3-bucket-name";
            var keyName = $"{Guid.NewGuid()}_{file.FileName}";
            var client = new AmazonS3Client(); // Configure AWS credentials

            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var uploadRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    InputStream = newMemoryStream,
                    ContentType = file.ContentType
                };

                var response = await client.PutObjectAsync(uploadRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Url = $"https://{bucketName}.s3.amazonaws.com/{keyName}" });
                }
                else
                {
                    return StatusCode((int)response.HttpStatusCode, "Error uploading file.");
                }
            }
        }


        // DELETE: api/LeaveApplication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemzlm_leave_application(int id)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_application == null)
            {
                return NotFound();
            }
            var mzlm_leave_application = await _context.mzlm_leave_application.FindAsync(id);
            if (mzlm_leave_application == null)
            {
                return NotFound();
            }

            mzlm_leave_individual_Application_delete_notification(id);

            _context.mzlm_leave_application.Remove(mzlm_leave_application);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mzlm_leave_applicationExists(int id)
        {
            return (_context.mzlm_leave_application?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private void mzlm_leave_individual_Application_delete_notification(int mzlm_leave_application_id)
        {
            mzlm_leave_application mzlm_leave_application = _context.mzlm_leave_application
                    .Where(app => app.id == mzlm_leave_application_id)
                    .Include(app => app.package)
                    .Include(app => app.type)
                    .Include(app => app.category) // Include the Category navigation property
                    .Include(app => app.its) // Include the its navigation property
                        .ThenInclude(its => its.mauzeNavigation)
                            .ThenInclude(mauze => mauze.qism) // Continue including nested properties as necessary
                                .ThenInclude(qism => qism.its)
                                .ThenInclude(its => its.its)
                    .FirstOrDefault();
            string applicantEmail = string.IsNullOrEmpty(mzlm_leave_application.its.officialEmailAddress) ? mzlm_leave_application.its.emailAddress : mzlm_leave_application.its.officialEmailAddress;
            string branchEmail = mzlm_leave_application.its.mauzeNavigation.qism.emailId;

            string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the following khidmatguzaar has cancelled a leave before final approval.</p>
                    <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                    <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                    <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                    <br/>Leave type: " + mzlm_leave_application.type.name + @"
                    <br/>Leave category: " + mzlm_leave_application.category.name + @"
                    <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                    <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                    <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                    <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                    <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; HR  &gt; Leave Management for details.</p>
                    <p>Shukran,<br/>Wa al-Salaam.</p>";
            _notificationService.SendStandardHTMLEmail("Leave canceled before Approval", msg, branchEmail, "leave");

            try
            {
                List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n"), "mzlm_leave_individual_Application_delete_notification");
            }
            catch (Exception ex)
            {

            }
        }

        private void mzlm_leave_individual_Application_update_notification(int mzlm_leave_application_id)
        {
            mzlm_leave_application mzlm_leave_application = _context.mzlm_leave_application
                    .Where(app => app.id == mzlm_leave_application_id)
                    .Include(app => app.package)
                    .Include(app => app.type)
                    .Include(app => app.mzlm_leave_logs)
                    .Include(app => app.category) // Include the Category navigation property
                    .Include(app => app.its) // Include the its navigation property
                        .ThenInclude(its => its.mauzeNavigation)
                            .ThenInclude(mauze => mauze.qism) // Continue including nested properties as necessary
                            .ThenInclude(qism => qism.its)
                            .ThenInclude(its => its.its)
                    .AsNoTracking()
                    .FirstOrDefault();
            string applicantEmail = string.IsNullOrEmpty(mzlm_leave_application.its.officialEmailAddress) ? mzlm_leave_application.its.emailAddress : mzlm_leave_application.its.officialEmailAddress;
            string branchEmail = mzlm_leave_application.its.mauzeNavigation?.qism?.emailId;

            if (mzlm_leave_application.stageId == 3 && mzlm_leave_application.appliedBy == "Individual")
            {
                if (mzlm_leave_application.type.approvalFlow.Contains("Admin"))
                {
                    string msg2 = @"<p>Salaam Jameel,<br/>Please be informed that the Branch Admin has <span style=""color:green"">accepted</span> a leave request and it has been forwarded to you for further approval.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                        <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                        <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                        <br/>Leave type: " + mzlm_leave_application.type.name + @"
                        <br/>Leave category: " + mzlm_leave_application.category.name + @"
                        <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                        <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                        <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                        <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                        <p>Please review the leave request on www.mahadalzahra.org &gt; Admin login &gt; HR  &gt; Leave Management at your earliest convenience.</p>
                        <p>Shukran,<br/>Wa al-Salaam.</p>";
                    _notificationService.SendStandardHTMLEmail("Leave Application - Idara Admin Approval Pending", msg2, globalConstants.adminLeaveEmails, "leave");
                    try
                    {
                        List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("<span style=\"color:green\">", ""), "mzlm_leave_individual_Application_update_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the Branch Admin has <span style=""color:green"">accepted</span> your leave request and forwarded to the Idara Office for further approval.</p>
                            <p>Kindly check www.mahadalzahra.org &gt; HR login &gt; Leave Application for details.</p>
                            <p>Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave Application Approved by Branch Admin", msg, applicantEmail, "leave");
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.c_codeWhatsapp + mzlm_leave_application.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_update_notification");
                }
                catch (Exception ex)
                {

                }
            }

            if (mzlm_leave_application.stageId == 8 && mzlm_leave_application.appliedBy == "Individual")
            {
                if (mzlm_leave_application.type.approvalFlow.Contains("Admin"))
                {
                    string msg2 = @"<p><br/>Salaam Jameel,<br/>Please be informed that the Branch Admin has <span style=""color:red"">rejected</span> a leave request.</p>
                        <p>Details:
                        <br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                        <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                        <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                        <br/>Leave type: " + mzlm_leave_application.type.name + @"
                        <br/>Leave category: " + mzlm_leave_application.category.name + @"
                        <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                        <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                        <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                        <br/>Purpose: " + mzlm_leave_application.purpose + @"
                        <br/>Remark: " + mzlm_leave_application.mzlm_leave_logs.Where(x => x.stageId == 8).FirstOrDefault()?.remark + @"</p>
                        <p>Kindly check www.mahadalzahra.org &gt; Admin login &gt; HR  &gt; Leave Management for details.</p>
                        <p>Shukran,<br/>Wa al-Salaam.</p>";
                    _notificationService.SendStandardHTMLEmail("Leave Application - Branch Admin Rejected a request", msg2, globalConstants.adminLeaveEmails, "leave");
                    try
                    {
                        List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("<span style=\"color:red\">", "").Replace("</span>", ""), "mzlm_leave_individual_Application_update_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the Branch Admin has <span style=""color:red"">rejected</span> your leave request.</p>
                    <p>Kindly check www.mahadalzahra.org &gt; HR login &gt; Leave Application for details.</p>
                    <p>Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave Application Rejected by Branch Admin", msg, applicantEmail, "leave");
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.c_codeWhatsapp + mzlm_leave_application.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("<span style=\"color:red\">", "").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_update_notification");
                }
                catch (Exception ex)
                {

                }
            }

            if (mzlm_leave_application.stageId == 7 && mzlm_leave_application.appliedBy == "Individual")
            {
                if (mzlm_leave_application.type.approvalFlow.Contains("Branch"))
                {
                    string msg2 = @"<p><br/>Salaam Jameel,<br/>Please be informed that the Idara Admin has <span style=""color:red"">rejected</span> a leave request.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                        <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                        <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                        <br/>Leave type: " + mzlm_leave_application.type.name + @"
                        <br/>Leave category: " + mzlm_leave_application.category.name + @"
                        <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                        <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                        <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                        <br/>Purpose: " + mzlm_leave_application.purpose + @"
                        <br/>Remark: " + mzlm_leave_application.mzlm_leave_logs.Where(x => x.stageId == 7).FirstOrDefault()?.remark + @"</p>
                        <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; HR  &gt; Leave Management for details.</p>
                        <p>Shukran,<br/>Wa al-Salaam.</p>";
                    if (!string.IsNullOrEmpty(branchEmail))
                    {
                        _notificationService.SendStandardHTMLEmail("Leave Application - Idara Admin Rejected a request", msg2, branchEmail, "leave");
                    }
                    try
                    {
                        List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("<span style=\"color:red\">", "").Replace("</span>", ""), "mzlm_leave_individual_Application_update_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the Idara Admin has <span style=""color:red"">rejected</span> your leave request.</p>
                    <p>Kindly check www.mahadalzahra.org &gt; HR login &gt; Leave Application for details.</p>
                    <p>Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave Application Rejected by Idara Admin", msg, applicantEmail, "leave");
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.c_codeWhatsapp + mzlm_leave_application.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("<span style=\"color:red\">", "").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_update_notification");
                }
                catch (Exception ex)
                {

                }
            }

            if (mzlm_leave_application.stageId == 5 && mzlm_leave_application.appliedBy == "Individual")
            {
                if (mzlm_leave_application.type.approvalFlow.Contains("Branch"))
                {
                    string msg2 = @"<p><br/>Salaam Jameel,<br/>Please be informed that the Idara Office has approved a leave request.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                        <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                        <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                        <br/>Leave type: " + mzlm_leave_application.type.name + @"
                        <br/>Leave category: " + mzlm_leave_application.category.name + @"
                        <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                        <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                        <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                        <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                        <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; HR  &gt; Leave Management for details.</p>
                        <p>Shukran,<br/>Wa al-Salaam.</p>";
                    if (!string.IsNullOrEmpty(branchEmail))
                    {
                        _notificationService.SendStandardHTMLEmail("Leave Application - Idara Admin has approved a request", msg2, branchEmail, "leave");
                    }
                    try
                    {
                        List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n"), "mzlm_leave_individual_Application_update_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                string msg = @"<p><br/>Salaam Jameel,<br/>" + mzlm_leave_application.its.fullName + @"<br/>" + mzlm_leave_application.itsId + @"</p>
                    <p>Please be informed that the Idara Office has approved your leave request.</p>
                    <p>Kindly check www.mahadalzahra.org &gt; HR login &gt; Leave Application for details.</p>
                    <p>Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave Application Approved by Idara Admin", msg, applicantEmail, "leave");
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.c_codeWhatsapp + mzlm_leave_application.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_update_notification");
                }
                catch (Exception ex)
                {

                }
            }

            if (mzlm_leave_application.stageId == 13 && mzlm_leave_application.appliedBy == "Individual")
            {
                string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the following khidmatguzaar has requested to cancel a leave.</p>
                    <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                    <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                    <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                    <br/>Leave type: " + mzlm_leave_application.type.name + @"
                    <br/>Leave category: " + mzlm_leave_application.category.name + @"
                    <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                    <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                    <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                    <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                    <p>Please review the request on www.mahadalzahra.org &gt; Branch login &gt; HR  &gt; Leave Management at your earliest convenience.</p>
                    <p>Shukran,<br/>Wa al-Salaam.</p>";
                if (!string.IsNullOrEmpty(branchEmail))
                {
                    _notificationService.SendStandardHTMLEmail("Leave cancelation request", msg, branchEmail, "leave");
                }
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_update_notification");
                }
                catch (Exception ex)
                {

                }
            }
            if (mzlm_leave_application.stageId == 10 && mzlm_leave_application.appliedBy == "Individual")
            {
                string msg2 = @"<p><br/>Salaam Jameel,<br/>Please be informed that the following khidmatguzaar has requested to cancel a leave.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                        <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                        <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                        <br/>Leave type: " + mzlm_leave_application.type.name + @"
                        <br/>Leave category: " + mzlm_leave_application.category.name + @"
                        <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                        <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                        <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                        <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                        <p>Please review the request on www.mahadalzahra.org &gt; Admin login &gt; HR  &gt; Leave Management at your earliest convenience.</p>
                        <p>Shukran,<br/>Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave cancelation request", msg2, globalConstants.adminLeaveEmails, "leave");
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n"), "mzlm_leave_individual_Application_update_notification");
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void mzlm_leave_individual_Application_notification(mzlm_leave_application mzlm_leave_application)
        {
            TimeSpan difference = mzlm_leave_application.fromEngDate - DateTime.UtcNow;
            string branchEmail = mzlm_leave_application.its.mauzeNavigation.qism.emailId;


            if (mzlm_leave_application.type.approvalFlow.Contains("Branch"))
            {
                string msg = @"<p>Salaam Jameel,
                    <br/>A new leave application has been received.</p>
                    <p>Details:
                    <br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                    <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                    <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                    <br/>Leave type: " + mzlm_leave_application.type.name + @"
                    <br/>Leave category: " + mzlm_leave_application.category.name + @"
                    <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                    <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                    <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                    <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                    <p>Please review the leave request on www.mahadalzahra.org &gt; Branch login &gt; HR  &gt; Leave Management at your earliest convenience.</p>
                    <p>Note: The leave will be auto escalated after 24 hours of application for further approval. In few cases, it may auto escalate after 12 hours.</p>
                    <p>Shukran,<br/>Wa al-Salaam.</p>";
                _notificationService.SendStandardHTMLEmail("Leave Application Request", msg, mzlm_leave_application.its.mauzeNavigation.qism.emailId, "leave");
                try
                {
                    List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                    _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_notification");
                }
                catch (Exception ex)
                {

                }

            }

            if (mzlm_leave_application.type.approvalFlow == "None")
            {
                if (mzlm_leave_application.category.notifyTo.Contains("Admin"))
                {
                    string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the following khidmatguzaar has taken an emergency leave.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                        <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                        <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                        <br/>Leave type: " + mzlm_leave_application.type.name + @"
                        <br/>Leave category: " + mzlm_leave_application.category.name + @"
                        <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                        <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                        <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                        <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                        <p>Kindly check www.mahadalzahra.org &gt; Admin login &gt; HR  &gt; Leave Management for details.</p>
                        <p>Shukran,<br/>Wa al-Salaam.</p>";

                    _notificationService.SendStandardHTMLEmail("Leave Application Request", msg, globalConstants.adminLeaveEmails, "leave");
                    try
                    {
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(globalConstants.adminWhatsapp, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (mzlm_leave_application.category.notifyTo.Contains("Branch"))
                {
                    string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the following khidmatguzaar has taken an emergency leave.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + mzlm_leave_application.its.fullName + @"
                        <br/>Khidmatguzaar ITS: " + mzlm_leave_application.itsId + @"
                        <br/>Khidmat Mauze: " + mzlm_leave_application.its.mauzeNavigation.displayName + @"
                        <br/>Leave type: " + mzlm_leave_application.type.name + @"
                        <br/>Leave category: " + mzlm_leave_application.category.name + @"
                        <br/>From date: " + mzlm_leave_application.fromDayId + "/" + mzlm_leave_application.fromMonthId + "/" + mzlm_leave_application.fromYear + @"
                        <br/>To date: " + mzlm_leave_application.toDayId + "/" + mzlm_leave_application.toMonthId + "/" + mzlm_leave_application.toYear + @"
                        <br/>No of days: " + ((float)mzlm_leave_application.shiftCount / (float)2) + @"
                        <br/>Purpose: " + mzlm_leave_application.purpose + @"</p>
                        <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; HR  &gt; Leave Management for details.</p>
                        <p>Shukran,<br/>Wa al-Salaam.</p>";
                    if (globalConstants.alternateEmails.Where(y => y.id == mzlm_leave_application.its.mauzeNavigation.qismId).Count() != 0)
                    {
                        _notificationService.SendStandardHTMLEmail("Leave Application Request", msg, branchEmail, "leave", null, globalConstants.alternateEmails.Where(y => y.id == mzlm_leave_application.its.mauzeNavigation.qismId).FirstOrDefault().name);
                    }
                    else
                    {
                        _notificationService.SendStandardHTMLEmail("Leave Application Request", msg, branchEmail, "leave");
                    }

                    try
                    {
                        List<string> num = new List<string> { mzlm_leave_application.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + mzlm_leave_application.its.mauzeNavigation.qism.its.its.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }

        }

        private void mzlm_leave_individual_notification_package(int id)
        {
            if (id != 0)
            {
                mzlm_leave_package pkg = _context.mzlm_leave_package
                                    .Where(x => x.id == id)
                                    .Include(pkg => pkg.mzlm_leave_application)
                                        .ThenInclude(app => app.its) // Assuming 'its' is a navigation property within mzlm_leave_application
                                        .ThenInclude(its => its.mauzeNavigation)
                                            .ThenInclude(mauze => mauze.qism) // Adjust these ThenInclude chains based on your actual navigation properties
                                    .FirstOrDefault();
                List<mzlm_leave_application> applications = _context.mzlm_leave_application.Where(x => x.packageId == id)
                .Include(x => x.category)
                .Include(x => x.type)
                .Include(x => x.its)
                  .ThenInclude(x => x.mauzeNavigation)
                    .ThenInclude(x => x.qism)
                .ToList();

                foreach (mzlm_leave_application x in applications)
                {
                    string msg = @"<p><br/>Salaam Jameel,<br/>" + x.its.fullName + @"<br/>" + x.itsId + @"</p>
                        <p>Please be informed that the Idara Office has assigned a new leave on the module.</p>
                        <p>Details:<br/>Leave type: " + x.type.name + @"
                        <br/>Leave category: " + x.category.name + @"
                        <br/>From date: " + x.fromDayId + "/" + x.fromMonthId + "/" + x.fromYear + @"
                        <br/>To date: " + x.toDayId + "/" + x.toMonthId + "/" + x.toYear + @"
                        <br/>No of days: " + ((float)x.shiftCount / (float)2) + @"
                        <br/>Purpose: " + x.purpose + @"</p>
                        <p><br/>Kindly check www.mahadalzahra.org &gt; HR login &gt; Leave Application for more details.</p>
                        <p>Wa al-Salaam.</p>";
                    _notificationService.SendStandardHTMLEmail("Leave Assigned By Idara Admin", msg, x.its.officialEmailAddress != null ? x.its.officialEmailAddress : x.its.emailAddress, "leave");

                    try
                    {
                        List<string> num = new List<string> { x.its.c_codeWhatsapp + x.its.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_notification_package");
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }

        private void mzlm_leave_branch_notification_package(int id, mzlm_leave_application_dto mzlm_leave_applicationdto)
        {
            if (id != 0)
            {
                mzlm_leave_package pkg = _context.mzlm_leave_package
                                    .Where(x => x.id == id)
                                    .Include(pkg => pkg.mzlm_leave_application)
                                        .ThenInclude(app => app.type)
                                    .Include(pkg => pkg.mzlm_leave_application)
                                        .ThenInclude(app => app.category)
                                    .Include(pkg => pkg.mzlm_leave_application)
                                        .ThenInclude(app => app.its) // Assuming 'its' is a navigation property within mzlm_leave_application
                                        .ThenInclude(its => its.mauzeNavigation)
                                            .ThenInclude(mauze => mauze.qism) // Adjust these ThenInclude chains based on your actual navigation properties
                                            .ThenInclude(qism => qism.its)
                                            .ThenInclude(its => its.its)
                                    .FirstOrDefault();
                List<mzlm_leave_application> applications = pkg.mzlm_leave_application.ToList();
                List<qism_al_tahfeez> qisms = pkg.mzlm_leave_application.GroupBy(x => x.its.mauzeNavigation.qismId).Select(x => x.First().its.mauzeNavigation.qism).ToList();

                foreach (qism_al_tahfeez x in qisms)
                {
                    int count = 1;
                    // List<khidmat_guzaar> kgs = applications.Where(x => x.its.mauzeNavigation.qismId == x.id).Select(x => x.its).ToList();

                    string msg = @"<p><br/>Salaam Jameel,<br/>
                        <p>Please be informed that the Idara Office has assigned a new leave on the module.</p>
                        <p>Details:<br/>Leave type: " + applications.First().type.name + @"
                        <br/>Leave category: " + applications.First().category.name + @"
                        <br/>From date: " + applications.First().fromDayId + "/" + applications.First().fromMonthId + "/" + applications.First().fromYear + @"
                        <br/>To date: " + applications.First().toDayId + "/" + applications.First().toMonthId + "/" + applications.First().toYear + @"
                        <br/>No of days: " + ((float)applications.First().shiftCount / (float)2) + @"
                        <br/>Leave applicable for: " + mzlm_leave_applicationdto.leaveBulkAssignation?.mauze?.ToString() + mzlm_leave_applicationdto.leaveBulkAssignation?.mzCategory?.ToString() + mzlm_leave_applicationdto.leaveBulkAssignation?.itsCsv?.ToString() + @"</p>
                        <p><br/>Kindly check www.mahadalzahra.org &gt; HRlogin &gt; Leave Application for more details.</p>
                        <p>Wa al-Salaam.</p>";

                    _notificationService.SendStandardHTMLEmail("Leave Assigned By Idara Admin", msg, x.its?.emailId ?? "error@mahadalzahra.com", "leave");
                    try
                    {
                        List<string> num = new List<string> { x.its?.its?.c_codeWhatsapp + x.its?.its?.whatsappNo };
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_individual_Application_update_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }

        private void mzlm_leave_branch_application_notification(int id)
        {
            if (id != 0)
            {
                mzlm_leave_package pkg = _context.mzlm_leave_package
                                    .Where(x => x.id == id)
                                    .Include(pkg => pkg.mzlm_leave_application)
                                        .ThenInclude(app => app.type)
                                    .Include(pkg => pkg.mzlm_leave_application)
                                        .ThenInclude(app => app.category)
                                    .Include(pkg => pkg.mzlm_leave_application)
                                        .ThenInclude(app => app.its) // Assuming 'its' is a navigation property within mzlm_leave_application
                                        .ThenInclude(its => its.mauzeNavigation)
                                            .ThenInclude(mauze => mauze.qism) // Adjust these ThenInclude chains based on your actual navigation properties
                                    .FirstOrDefault();
                List<mzlm_leave_application> applications = pkg.mzlm_leave_application.ToList();
                List<qism_al_tahfeez> qisms = pkg.mzlm_leave_application.GroupBy(x => x.its.mauzeNavigation.qismId).Select(x => x.First().its.mauzeNavigation.qism).ToList();

                foreach (qism_al_tahfeez x in qisms)
                {
                    int count = 1;
                    List<khidmat_guzaar> kgs = applications.Where(x => x.its.mauzeNavigation.qismId == x.id).Select(x => x.its).ToList();

                    string msg = @"<p><br/>Salaam Jameel,<br/>Please be informed that the Branch Admin has applied a leave.</p>
                    <p>Details:<br/>Branch name: " + x.name + @"<br/>Leave type: " + applications.First().type.name + @"<br/>Leave category: " + applications.First().category.name + @"<br/>From date: " + applications.First().fromDayId + "/" + applications.First().fromMonthId + "/" + applications.First().fromYear + @"<br/>To date: " + applications.First().toDayId + "/" + applications.First().toMonthId + "/" + applications.First().toYear + @"<br/>No of days: " + ((float)applications.First().shiftCount / (float)2) + @"<br/>Total individuals:</p>
                    <p>Kindly check www.mahadalzahra.org &gt; Admin login &gt; Leave Management for more details and approve/reject as appropriate.</p>
                    <p>Shukran,<br/>Wa al-Salaam.</p>";
                    _notificationService.SendStandardHTMLEmail("Leave Application Applied by Branch Admin", msg, globalConstants.adminLeaveEmails, "leave");
                    try
                    {
                        _whatsAppApiService.sendStarMarketingGeneralWhatsapp(globalConstants.adminWhatsapp, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "mzlm_leave_branch_application_notification");
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }

        [AllowAnonymous]
        [HttpGet("autoEscalateLeave")]
        public async Task<ActionResult> autoEscalateLeave()
        {
            DateTime curr = DateTime.UtcNow;

            hijri_calender hijriDay = _context.hijri_calender.Where(x => x.english_day == curr.Day && x.english_month == curr.Month && x.english_year == curr.Year).FirstOrDefault();

            DateTime currHij = new DateTime(hijriDay.hijri_year ?? 1, hijriDay.hijri_month ?? 1, hijriDay.hijri_day ?? 1, curr.Hour, curr.Minute, curr.Second);

            if (_context.mzlm_leave_application == null)
            {
                return NotFound();
            }

            IQueryable<mzlm_leave_application> mzlm_leave_application = _context.mzlm_leave_application
                .Include(x => x.mzlm_leave_logs)
                .Include(x => x.category)
                .Include(x => x.type)
                .Include(x => x.its)
                .ThenInclude(x => x.mauzeNavigation)
                .ThenInclude(x => x.qism)
                .ThenInclude(x => x.its)
                .ThenInclude(x => x.its);

            List<int> approvedIds = globalConstants.onLeaveStages;

            List<mzlm_leave_application> reminderNotification = await mzlm_leave_application.Where(x => approvedIds.Any(y => y == x.stageId) && x.fromEngDate < curr.AddHours(24) && x.fromEngDate > curr.AddHours(16)).ToListAsync();
            reminderNotification.ForEach(x =>
            {
                string applicantEmail = string.IsNullOrEmpty(x.its.officialEmailAddress) ? x.its.emailAddress : x.its.officialEmailAddress;

                string msg = @"<p>Salaam Jameel,<br/>" + x.its.fullName + @"<br/>" + x.itsId + @"</p>
                    <p>This email serves as a reminder that your scheduled leave will commence starting tomorrow.</p>
                    <p>Details:<br/>Leave type: " + x.type.name + @"<br/>Leave category: " + x.category.name + @"<br/>From date: " + x.fromDayId + "/" + x.fromMonthId + "/" + x.fromYear + @"<br/>To date: " + x.toDayId + "/" + x.toMonthId + "/" + x.toYear + @"<br/>No of days: " + ((float)x.shiftCount / (float)2) + @"</p>
                    <p>If there are alterations to the plan, please promptly submit a leave cancellation request for the module.</p>
                    <p>To request a cancellation, kindly visit www.mahadalzahra.org &gt; HR login &gt; Leave Application.</p>
                    <p>Wa al-Salaam.<p/>";

                List<string> num = new List<string> { x.its.c_codeWhatsapp + x.its.whatsappNo };
                List<string> num2 = new List<string> { x.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + x.its.mauzeNavigation.qism.its.its.whatsappNo };
                string branchEmail = x.its.mauzeNavigation.qism.emailId;
                string msg2 = @"<p>Salaam Jameel,<br/>This email serves as a reminder notifying you that the designated khidmatguzaar will be on leave starting from tomorrow.</p>
                    <p>Details:<br/>Khidmatguzaar name: " + x.its.fullName + @"<br/>Khidmatguzaar ITS: " + x.itsId + @"<br/>Khidmat Mauze: " + x.its.mauzeNavigation.displayName + @"<br/>Leave type: " + x.type.name + @"<br/>Leave category: " + x.category.name + @"<br/>From date: " + x.fromDayId + "/" + x.fromMonthId + "/" + x.fromYear + @"<br/>To date: " + x.toDayId + "/" + x.toMonthId + "/" + x.toYear + @"<br/>No of days: " + ((float)x.shiftCount / (float)2) + @"</p>
                    <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; Leave Management for more details.</p>
                    <p>Shukran,<br/>Wa al-Salaam.<p/>";

                try
                {
                    BackgroundJob.Enqueue(() => _notificationService.SendStandardHTMLEmail("Leave Tommorow", msg, applicantEmail, "leave", null, null));
                    BackgroundJob.Enqueue(() => _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", "").Replace("&gt;", ">"), "autoEscalateLeave", "System", "System"));
                    if (globalConstants.alternateEmails.Where(y => y.id == x.its.mauzeNavigation.qismId).Count() != 0)
                    {
                        BackgroundJob.Enqueue(() => _notificationService.SendStandardHTMLEmail("On Leave Tommorow", msg2, branchEmail, "leave", null, globalConstants.alternateEmails.Where(y => y.id == x.its.mauzeNavigation.qismId).FirstOrDefault().name));
                    }
                    else
                    {
                        BackgroundJob.Enqueue(() => _notificationService.SendStandardHTMLEmail("On Leave Tommorow", msg2, branchEmail, "leave", null, null));
                    }
                    BackgroundJob.Enqueue(() => _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num2, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n").Replace("</span>", ""), "autoEscalateLeave", "System", "System"));
                }
                catch (Exception ex)
                {

                }
            });
            await _context.SaveChangesAsync();

            List<mzlm_leave_application> pendingAtBranch = await mzlm_leave_application.Where(x => x.stageId == 4 && x.createdOn.AddHours(24) < curr && x.type.approvalFlow == "Branch and Admin" && x.appliedBy == "Individual" && x.shiftCount <= 7).ToListAsync();
            pendingAtBranch.ForEach(x =>
            {
                x.stageId = 3;
                x.mzlm_leave_logs.Add(
                        new mzlm_leave_logs { stageId = 3, createdOn = curr, createdBy = 1, remark = "Auto Escalated after 24 hrs" }
                    );

                string branchEmail = x.its.mauzeNavigation.qism.emailId;
                string msg2 = @"<p>Salaam Jameel,<br/>Please be informed that the leave request below has been auto-escalated to the Idara Office for further approval as it remained unattended within the specified time frame.</p>
                    <p>Details:<br/>Khidmatguzaar name: " + x.its.fullName + @"<br/>Khidmatguzaar ITS: " + x.itsId + @"<br/>Khidmat Mauze: " + x.its.mauzeNavigation.displayName + @"<br/>Leave type: " + x.type.name + @"<br/>Leave category: " + x.category.name + @"<br/>From date: " + x.fromDayId + "/" + x.fromMonthId + "/" + x.fromYear + @"<br/>To date: " + x.toDayId + "/" + x.toMonthId + "/" + x.toYear + @"<br/>No of days: " + ((float)x.shiftCount / (float)2) + @"</p>
                    <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; Leave Management for more details.</p>
                    <p>Shukran,<br/>Wa al-Salaam.</p>";
                List<string> num = new List<string> { x.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + x.its.mauzeNavigation.qism.its.its.whatsappNo };
                try
                {
                    BackgroundJob.Enqueue(() => _notificationService.SendStandardHTMLEmail("Leave request auto-escalated", msg2, branchEmail, "leave", null, null));
                    BackgroundJob.Enqueue(() => _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n"), "autoEscalateLeave", "System", "System"));
                }
                catch (Exception ex)
                {

                }
            });
            await _context.SaveChangesAsync();

            List<mzlm_leave_application> cancelationRequstedbyUser = await mzlm_leave_application.Where(x => x.stageId == 13 && x.createdOn.AddHours(24) < curr && x.type.approvalFlow == "Branch and Admin" && x.appliedBy == "Individual").ToListAsync();
            cancelationRequstedbyUser.ForEach(x =>
            {
                x.stageId = 10;
                x.mzlm_leave_logs.Add(
                        new mzlm_leave_logs { stageId = 10, createdOn = curr, createdBy = 1, remark = "Auto Escalated after 24 hrs" }
                    );
                string branchEmail = x.its.mauzeNavigation.qism.emailId;
                string msg2 = @"<p>Salaam Jameel,<br/>Please be informed that the leave request below has been auto-escalated to the Idara Office for further cancelation approval as it remained unattended within the specified time frame.</p>
                    <p>Details:<br/>Khidmatguzaar name: " + x.its.fullName + @"<br/>Khidmatguzaar ITS: " + x.itsId + @"<br/>Khidmat Mauze: " + x.its.mauzeNavigation.displayName + @"<br/>Leave type: " + x.type.name + @"<br/>Leave category: " + x.category.name + @"<br/>From date: " + x.fromDayId + "/" + x.fromMonthId + "/" + x.fromYear + @"<br/>To date: " + x.toDayId + "/" + x.toMonthId + "/" + x.toYear + @"<br/>No of days: " + ((float)x.shiftCount / (float)2) + @"</p>
                    <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; Leave Management for more details.</p>
                    <p>Shukran,<br/>Wa al-Salaam.</p>";
                List<string> num = new List<string> { x.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + x.its.mauzeNavigation.qism.its.its.whatsappNo };
                try
                {
                    BackgroundJob.Enqueue(() => _notificationService.SendStandardHTMLEmail("Leave cancelation request auto-escalated", msg2, branchEmail, "leave", null, null));
                    BackgroundJob.Enqueue(() => _whatsAppApiService.sendStarMarketingGeneralWhatsapp(num, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n"), "autoEscalateLeave", "System", "System"));
                }
                catch (Exception ex)
                {

                }
            });
            await _context.SaveChangesAsync();

            List<mzlm_leave_application> pendingWithIdarahAdmin = await mzlm_leave_application.Where(x => x.stageId == 3 && x.appliedBy == "Individual").ToListAsync();
            pendingWithIdarahAdmin.ForEach(x =>
            {
                DateTime refrence = new DateTime(x.fromYear, x.fromMonthId, x.fromDayId);

                if ((x.category.isHijri && currHij.AddHours(12) > refrence) || (!x.category.isHijri && currHij.AddHours(12) > refrence))
                {
                    x.stageId = 5;
                    x.mzlm_leave_logs.Add(
                            new mzlm_leave_logs { stageId = 5, createdOn = curr, createdBy = 1, remark = "Auto Escalated before 12 hours from requested time" }
                        );
                    string branchEmail = x.its.mauzeNavigation.qism.emailId;
                    string msg2 = @"<p>Salaam Jameel,<br/>Please be informed that the leave request below has been auto-approved as it remained unattended within the specified time frame.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + x.its.fullName + @"<br/>Khidmatguzaar ITS: " + x.itsId + @"<br/>Khidmat Mauze: " + x.its.mauzeNavigation.displayName + @"<br/>Leave type: " + x.type.name + @"<br/>Leave category: " + x.category.name + @"<br/>From date: " + x.fromDayId + "/" + x.fromMonthId + "/" + x.fromYear + @"<br/>To date: " + x.toDayId + "/" + x.toMonthId + "/" + x.toYear + @"<br/>No of days: " + ((float)x.shiftCount / (float)2) + @"</p>
                        <p>Kindly check www.mahadalzahra.org &gt; Admin login &gt; Leave Management for more details.</p>
                        <p>Shukran,<br/>Wa al-Salaam.<p/>";
                    List<string> num = new List<string> { x.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + x.its.mauzeNavigation.qism.its.its.whatsappNo };

                    try
                    {
                        BackgroundJob.Enqueue(() => _notificationService.SendStandardHTMLEmail("Leave request auto-approved", msg2, globalConstants.adminLeaveEmails, "leave", null, null));
                        BackgroundJob.Enqueue(() => _whatsAppApiService.sendStarMarketingGeneralWhatsapp(globalConstants.adminWhatsapp, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n"), "autoEscalateLeave", "System", "System"));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
            await _context.SaveChangesAsync();

            List<mzlm_leave_application> cencellationPendingWithIdarahAdmin = await mzlm_leave_application.Where(x => (x.stageId == 10 || x.stageId == 11) && x.createdOn.AddHours(24) > curr && x.appliedBy == "Individual").ToListAsync();
            cencellationPendingWithIdarahAdmin.ForEach(x =>
            {
                DateTime refrence = new DateTime(x.fromYear, x.fromMonthId, x.fromDayId);

                if ((x.category.isHijri && currHij.AddHours(12) > refrence) || (!x.category.isHijri && currHij.AddHours(12) > refrence))
                {
                    x.mzlm_leave_logs.Add(
                            new mzlm_leave_logs { stageId = 16, createdOn = curr, createdBy = 1, remark = "Auto Escalated before 12 hours from requested time" }
                        );
                    x.stageId = 16;

                    string branchEmail = x.its.mauzeNavigation.qism.emailId;
                    string msg2 = @"<p>Salaam Jameel,<br/>Please be informed that the leave request below has been auto-cancelled as it remained unattended within the specified time frame.</p>
                        <p>Details:<br/>Khidmatguzaar name: " + x.its.fullName + @"<br/>Khidmatguzaar ITS: " + x.itsId + @"<br/>Khidmat Mauze: " + x.its.mauzeNavigation.displayName + @"<br/>Leave type: " + x.type.name + @"<br/>Leave category: " + x.category.name + @"<br/>From date: " + x.fromDayId + "/" + x.fromMonthId + "/" + x.fromYear + @"<br/>To date: " + x.toDayId + "/" + x.toMonthId + "/" + x.toYear + @"<br/>No of days: " + ((float)x.shiftCount / (float)2) + @"</p>
                        <p>Kindly check www.mahadalzahra.org &gt; Admin login &gt; Leave Management for more details.</p>
                        <p>Shukran,<br/>Wa al-Salaam.<p/>";
                    List<string> num = new List<string> { x.its.mauzeNavigation.qism.its.its.c_codeWhatsapp + x.its.mauzeNavigation.qism.its.its.whatsappNo };

                    try
                    {
                        BackgroundJob.Enqueue(() => _notificationService.SendStandardHTMLEmail("Leave cancelation request auto-cancelled", msg2, globalConstants.adminLeaveEmails, "leave", null, null));
                        BackgroundJob.Enqueue(() => _whatsAppApiService.sendStarMarketingGeneralWhatsapp(globalConstants.adminWhatsapp, msg2.Replace("<br/>", "\n").Replace("<p>", "\n").Replace("</p>", "\n"), "autoEscalateLeave", "System", "System"));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });

            await _context.SaveChangesAsync();

            if (mzlm_leave_application == null)
            {
                return NotFound();
            }
            List<mzlm_leave_application_dto> leaveApplicationsDto = _mapper.Map<List<mzlm_leave_application_dto>>(mzlm_leave_application);


            return Ok("Success");
        }

        [AllowAnonymous]
        [HttpGet("tabadlaLeaveUpdate")]
        public async Task<ActionResult> autotabadlaLeaveUpdate()
        {
            DateTime curr = DateTime.UtcNow;

            IQueryable<edit_table_column_logs> edits = _context.edit_table_column_logs.Where(x => x.table_name == "khidmat_guzaar" && x.column_name == "mauze" && x.edit_date_time > curr.AddHours(-24));

            hijri_calender hijriDay = _context.hijri_calender.Where(x => x.english_day == curr.Day && x.english_month == curr.Month && x.english_year == curr.Year).FirstOrDefault();

            DateTime currHij = new DateTime(hijriDay.hijri_year ?? 1, hijriDay.hijri_month ?? 1, hijriDay.hijri_day ?? 1, curr.Hour, curr.Minute, curr.Second);

            if (_context.mzlm_leave_application == null)
            {
                return NotFound();
            }

            List<int> itsIds = edits.Select(x => int.Parse(x.table_primary_key_value)).ToList();

            IQueryable<mzlm_leave_application> mzlm_leave_applicationsToUpdate = _context.mzlm_leave_application
                .Include(x => x.mzlm_leave_logs)
                .Include(x => x.package)
                .Include(x => x.category)
                .Include(x => x.type)
                .Include(x => x.its)
                .ThenInclude(x => x.mauzeNavigation)
                .ThenInclude(x => x.qism)
                .ThenInclude(x => x.its)
                .ThenInclude(x => x.its)
                .Where(x => x.fromEngDate >= curr && !((x.stageId == 5 || x.stageId == 4) && x.appliedBy == "Individual") && itsIds.Contains(x.itsId));

            IQueryable<mzlm_leave_application> applicationToDelete = mzlm_leave_applicationsToUpdate.Where(x => x.appliedBy == "Branch");
            IQueryable<mzlm_leave_application> applicationsByAdmin = mzlm_leave_applicationsToUpdate.Where(x => x.appliedBy == "Admin");
            List<venue> venues = _context.venue.ToList();
            List<khidmat_guzaar> kgs = _context.khidmat_guzaar.Where(x => itsIds.Contains(x.itsId)).Include(x => x.mauzeNavigation).ToList();
            List<int> mauzeIds = kgs.Select(x => x.mauze ?? 0).ToList();

            //foreach(khidmat_guzaar kg in kgs)
            //{
            //    kg.
            //}

            List<int> approvedIds = globalConstants.onLeaveStages;



            //if (mzlm_leave_application == null)
            //{
            //    return NotFound();
            //}
            //List<mzlm_leave_application_dto> leaveApplicationsDto = _mapper.Map<List<mzlm_leave_application_dto>>(mzlm_leave_application);


            return Ok("Success");
        }


        //[AllowAnonymous]
        //[HttpGet("testEmail")]
        //public async Task<ActionResult> testEmail()
        //{
        //    string branchEmail = "hatimn219@gmail.com";
        //    string msg2 = @"<p>Salaam Jameel,<br/>This email serves as a reminder notifying you that the designated khidmatguzaar will be on leave starting from tomorrow.</p>
        //            <p>Details:<br/>Khidmatguzaar name: </p>
        //            <p>Kindly check www.mahadalzahra.org &gt; Branch login &gt; Leave Management for more details.</p>
        //            <p>Shukran,<br/>Wa al-Salaam.<p/>";
        //    if (globalConstants.alternateEmails.Where(y => y.id == 2).Count() != 0)
        //    {
        //        _notificationService.SendStandardHTMLEmail("On Leave Tommorow", msg2, branchEmail, "leave", null, globalConstants.alternateEmails.Where(y => y.id == 2).FirstOrDefault().name);
        //    }
        //    else
        //    {
        //        _notificationService.SendStandardHTMLEmail("On Leave Tommorow", msg2, branchEmail, "leave");
        //    }

        //    return Ok();
        //}

    }
}
