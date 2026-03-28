using AutoMapper;
using mahadalzahrawebapi.GenericModels;
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
    public class LeaveSummaryController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly HelperService _helperService;
        private readonly TokenService _tokenService;
        private readonly globalConstants globalConstants;

        public LeaveSummaryController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _helperService = new HelperService(context);
            _tokenService = tokenService;
            globalConstants = new globalConstants();
        }

        // GET: api/LeaveSummary
        [HttpGet]
        public async Task<ActionResult<LeaveSummaryModel>> GetLeaveSummary(
    [FromQuery] int? its,
    [FromQuery] int? academicYear,
    [FromQuery] string? qismIds,
    [FromQuery] string? mzIdaras = "Mahad al-Zahra KHDGZ,Wafd al-Huffaz",
    [FromQuery] string? employeeTypes = "Khidmatguzaar"
)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (academicYear == null)
                academicYear = globalConstants.currentAcademicYear;

            // Parse filters
            var employeeTypeList = _helperService.parseStrings(employeeTypes);
            var qismList = qismIds != null ? _helperService.parseIds(qismIds) : new List<int>();
            var idaraList = mzIdaras != null ? _helperService.parseStrings(mzIdaras) : new List<string>();

            // Fetch base data
            var khidmatGuzaarQuery = _context.khidmat_guzaar.AsNoTracking()
                .Where(k => (k.activeStatus ?? false) && employeeTypeList.Contains(k.employeeType));

            if (qismList.Any())
                khidmatGuzaarQuery = khidmatGuzaarQuery.Where(k => qismList.Contains(k.mauzeNavigation.qismId ?? 0));

            if (idaraList.Any())
                khidmatGuzaarQuery = khidmatGuzaarQuery.Where(k => idaraList.Contains(k.mz_idara));

            if (its.HasValue)
                khidmatGuzaarQuery = khidmatGuzaarQuery.Where(k => k.itsId == its);

            var khidmatGuzaarList = await khidmatGuzaarQuery
                .Select(k => new
                {
                    k.itsId,
                    k.fullName,
                    k.mobileNo,
                    Email = string.IsNullOrEmpty(k.officialEmailAddress) ? k.emailAddress : k.officialEmailAddress,
                    k.mauzeNavigation.displayName,
                    k.mz_idara,
                    LeaveApplications = k.mzlm_leave_application
                        .Where(la => la.type.accessTo == "Individual")
                        .Select(la => new
                        {
                            la.typeId,
                            la.stage.isDeductable,
                            la.shiftCount,
                            la.stageId,
                            la.type.daysAllotted
                        }).ToList()
                }).ToListAsync();

            // Fetch leave types
            var leaveTypes = await _context.mzlm_leave_type
                .Where(lt => lt.accessTo == "Individual")
                .Select(lt => new leaveTypeHeader
                {
                    id = lt.id,
                    name = lt.name,
                    color = _helperService.stringToColorCode(lt.name),
                    daysAllotted = lt.daysAllotted
                })
                .ToListAsync();

            // Process leave data in memory
            var leaveSummary = new LeaveSummaryModel
            {
                colHeader = leaveTypes.OrderBy(lt => lt.name).ToList(),
                rowHeaders = new List<leaveApplicant>(),
                details = new List<LeaveApplicationData>()
            };

            foreach (var kg in khidmatGuzaarList)
            {
                // Group and calculate leave stats
                var leaveStats = kg.LeaveApplications
                    .GroupBy(la => la.typeId)
                    .Select(g => new leaveTypeStats
                    {
                        typeId = g.Key,
                        consumed = g.Where(x => x.isDeductable ?? true).Sum(x => x.shiftCount) / 2.0f,
                        applied = g.Sum(x => x.shiftCount) / 2.0f,
                        Cacelled = g.Where(x => x.stageId == 16 || x.stageId == 17).Sum(x => x.shiftCount) / 2.0f,
                        rejected = g.Where(x => x.stageId == 7 || x.stageId == 8).Sum(x => x.shiftCount) / 2.0f,
                        remaining = ((g.FirstOrDefault()?.daysAllotted ?? 0) * 2 -
                                     g.Where(x => x.isDeductable ?? true).Sum(x => x.shiftCount)) / 2.0f
                    }).ToList();

                // Create applicant and application data
                leaveSummary.rowHeaders.Add(new leaveApplicant
                {
                    name = kg.fullName,
                    itsId = kg.itsId,
                    email = kg.Email,
                    contactNum = kg.mobileNo,
                    mauze = kg.displayName,
                    mzIdara = kg.mz_idara,
                    totalConsumed = leaveStats.Sum(ls => ls.consumed)
                });

                leaveSummary.details.Add(new LeaveApplicationData
                {
                    itsId = kg.itsId,
                    data = leaveStats
                });
            }

            leaveSummary.rowHeaders = leaveSummary.rowHeaders.OrderByDescending(a => a.totalConsumed).ToList();
            return leaveSummary;
        }



        private bool mzlm_leave_typeExists(int id)
        {
            return (_context.mzlm_leave_type?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
