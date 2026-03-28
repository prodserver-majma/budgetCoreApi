using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MinEntryController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;
        private readonly NotificationService _notificationService;
        private readonly WhatsAppApiService _whatsAppApiService;
        private readonly string adminEmails = "admin@mahadalzahra.com, juzerdiwan@jameasaifiyah.edu";
        private readonly globalConstants _globalConstants;

        public MinEntryController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
            _notificationService = new NotificationService();
            _globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
        }

        [HttpGet]
        public async Task<ActionResult<List<azwaaj_minentry_dto>>> Getazwaaj_minentry([FromQuery] int? itsId)
        {
            if (_context.azwaaj_minentry == null)
            {
                return NotFound();
            }

            List<azwaaj_minentry> entries = _context.azwaaj_minentry
                    .Include(x => x.deptVenue)
            .Include(x => x.its)
            .Include(x => x.salaryType).ToList();

            List<azwaaj_minentry_dto> data = _mapper.Map<List<azwaaj_minentry_dto>>(entries);

            if (itsId != null)
            {
                data = data.Where(x => x.itsid == itsId).ToList();
            }


            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);

        }

        [HttpGet("entryList")]
        public async Task<ActionResult<List<azwaaj_minentry_dto>>> Getazwaaj_minentryList(
            [FromQuery] int qismId,
            [FromQuery] DateTime? date = null,
            [FromQuery] string deptVenues = null,
            [FromQuery] string salarytypes = null,
            [FromQuery] string employeeTypes = null
            )
        {
            string msg = "";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateTime curr = DateTime.UtcNow;

                List<int> dvs = _helperService.parseIds(deptVenues);
                List<int> salaryTypes = _helperService.parseIds(salarytypes);
                List<string> empTypes = _helperService.parseStrings(employeeTypes);

                //branch_user authU = _context.branch_user
                //                                    .Include(x => x.its)
                //                                .Include(x => x.deptVenue.Where(y => y.qismId == qismId))
                //                                .ThenInclude(x => x.employee_dept_salary)
                //                                .ThenInclude(x => x.its)
                //                                .ThenInclude(x => x.mzlm_leave_application)
                //                                .Include(x => x.deptVenue.Where(y => y.qismId == qismId))
                //                                .ThenInclude(x => x.employee_dept_salary)
                //                                .ThenInclude(x => x.its)
                //                                .ThenInclude(x => x.mauzeNavigation)
                //                                .Include(x => x.deptVenue.Where(y => y.qismId == qismId))
                //                                .ThenInclude(x => x.employee_dept_salary)
                //                                .ThenInclude(x => x.salaryType)
                //                                .Where(x => x.itsId == authUser.ItsId)
                //                                .FirstOrDefault();

                branch_user authU = _context.branch_user
                    .Include(x => x.its).ThenInclude(x => x.mauzeNavigation)
                                                .Include(x => x.deptVenue.Where(y => y.qismId == qismId))
                                                .Where(x => x.itsId == authUser.ItsId)
                                                .FirstOrDefault();

                msg = msg + "branch fetched -> ";
                if (authU == null)
                {
                    return BadRequest(new { message = "Branch User Not Found" });
                }

                List<dept_venue> depts = authU.deptVenue.ToList();
                if (deptVenues != null)
                {
                    depts = depts.Where(x => dvs.Contains(x.id)).ToList();
                }


                if (_context.azwaaj_minentry == null)
                {
                    return NotFound();
                }

                msg = msg + " before date condition -> ";

                venue m = _context.venue.Where(x => x.qismId == qismId && x.CampId.Length > 5).FirstOrDefault();

                TimeZoneInfo venueTimeZone = TimeZoneInfo.FindSystemTimeZoneById(authU.its?.mauzeNavigation?.CampId);
                DateTime venueLocalTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, venueTimeZone);
                DateOnly TimeZoneDate = DateOnly.FromDateTime(venueLocalTime);

                if (date == null || date?.Date == venueLocalTime.Date)
                {
                    date = venueLocalTime;

                    msg = msg + "current date -> ";

                    if (curr.DayOfWeek == 0)
                    {
                        return Ok();
                    }

                    CalenderModel calenderModel = _helperService.getHijriDate(date ?? venueLocalTime);
                    string hijriStr = calenderModel.hijDay + " " + calenderModel.hijMonthName + " " + calenderModel.hijYear;
                    List<azwaaj_minentry_dto> tobeAdded = new List<azwaaj_minentry_dto>();
                    List<int> OnleaveList = _globalConstants.onLeaveStages;
                    DateOnly dateOnly = DateOnly.FromDateTime(date ?? venueLocalTime);

                    IQueryable<azwaaj_minentry> entries = _context.azwaaj_minentry
                        .Where(x => x.date == dateOnly && x.its.employeeType != "Khidmatguzaar" && x.its.activeStatus == true && x.deptVenue.qismId == qismId);

                    IQueryable<employee_dept_salary> empDeptSalaries = _context.employee_dept_salary
                        .Include(x => x.salaryType)
                        .Include(x => x.its).ThenInclude(x => x.mauzeNavigation)
                        .Include(x => x.its)
                        .ThenInclude(x => x.mzlm_leave_application.Where(y => OnleaveList.Contains(y.stageId) && y.fromEngDate <= venueLocalTime && y.toEngDate >= curr))
                        .Where(x => x.its.employeeType != "Khidmatguzaar" && x.its.activeStatus == true && x.deptVenue.qismId == qismId);

                    if (deptVenues != null)
                    {
                        entries = entries.Where(x => dvs.Contains(x.deptVenueId ?? 0));
                        empDeptSalaries = empDeptSalaries.Where(x => dvs.Contains(x.deptVenueId));
                    }

                    if (salarytypes != null)
                    {
                        entries = entries.Where(x => salaryTypes.Contains(x.policyId ?? 0));
                        empDeptSalaries = empDeptSalaries.Where(x => salaryTypes.Contains(x.salaryTypeId));
                    }

                    if (employeeTypes != null)
                    {
                        entries = entries.Where(x => empTypes.Any(y => y == x.its.employeeType));
                        empDeptSalaries = empDeptSalaries.Where(x => empTypes.Any(y => y == x.its.employeeType));
                    }

                    List<employee_dept_salary> employee_dept_salaries = empDeptSalaries.ToList();
                    List<azwaaj_minentry> entriesList = entries.ToList();

                    foreach (dept_venue item in authU.deptVenue)
                    {
                        List<employee_dept_salary> empDeptSalariesList = employee_dept_salaries.Where(x => x.deptVenueId == item.id).ToList();

                        foreach (employee_dept_salary emp in empDeptSalariesList)
                        {
                            azwaaj_minentry entry = entriesList.Where(x => x.itsid == emp.itsId && x.policyId == emp.salaryTypeId && x.deptVenueId == item.id).FirstOrDefault();

                            List<mzlm_leave_application> leaveList = emp.its.mzlm_leave_application.ToList();
                            bool leave = leaveList.Count > 0;
                            if (entry == null)
                            {
                                tobeAdded.Add(new azwaaj_minentry_dto
                                {
                                    createdBy = authU.its.fullName,
                                    deptVenueId = item.id,
                                    date = TimeZoneDate,
                                    createdOn = venueLocalTime,
                                    policyId = emp.salaryTypeId,
                                    itsid = emp.itsId,
                                    min = emp.workingMin ?? 0,
                                    hijriDate = hijriStr,
                                    deptVenueName = item.deptName + "_" + item.venueName,
                                    isOnLeave = leave,
                                    policyName = emp.salaryType.Name,
                                    name = emp.its.fullName,
                                    qismId = item.qismId ?? 0,
                                    value = emp.workingMin,
                                    mz_idara = emp.its.mz_idara,
                                    mauze = emp.its.mauzeNavigation?.displayName ?? "N/A",
                                    designation = emp.its.designation,
                                    employeeType = emp.its.employeeType,
                                    mz_idaracolor = _helperService.stringToColorCode(emp.its.mz_idara ?? "Empty"),
                                    ispending = true,
                                    rate = emp.salaryAmount,
                                    allotedMin = emp.workingMin
                                });
                            }
                            else
                            {
                                azwaaj_minentry_dto item1 = _mapper.Map<azwaaj_minentry_dto>(entry);
                                item1.hijriDate = hijriStr;
                                item1.value = emp?.workingMin ?? 0;
                                item1.mz_idaracolor = _helperService.stringToColorCode(item1.mz_idara ?? "Empty");
                                item1.mindiff = (int)(item1.min - item1.value);
                                item1.ispending = false;
                                tobeAdded.Add(item1);
                            }
                        }

                    }

                    if (qismId != null)
                    {
                        tobeAdded = tobeAdded.Where(x => x.qismId == qismId).ToList();
                    }

                    tobeAdded = tobeAdded.OrderByDescending(x => x.min).ToList();

                    return tobeAdded;
                }
                else
                {
                    if (date?.Date > curr.Date)
                    {
                        return BadRequest(new { message = "Can only fetch MinEntry List of Past Dates" });
                    }

                    msg = msg + "Past Date entry -> ";

                    CalenderModel calenderModel = _helperService.getHijriDate(date ?? curr);
                    string hijriStr = calenderModel.hijDay + " " + calenderModel.hijMonthName + " " + calenderModel.hijYear;

                    var deptIds = depts.Select(d => d.id).ToList(); // Extract the IDs to a list if not already

                    DateOnly dateOnly = DateOnly.FromDateTime(date ?? venueLocalTime);
                    List<azwaaj_minentry_dto> data = new List<azwaaj_minentry_dto>();
                    var entries = _context.azwaaj_minentry
                        .Include(x => x.deptVenue)
                        .Include(x => x.its)
                        .Include(x => x.salaryType)
                        .Where(x => x.date == dateOnly && x.its.employeeType != "Khidmatguzaar" && x.deptVenue.qismId == qismId && deptIds.Contains(x.deptVenueId ?? 0));


                    var employee_dept_salaries = _context.employee_dept_salary
                        .Include(x => x.its)
                        .Include(x => x.salaryType)
                        .Where(x => x.its.employeeType != "Khidmatguzaar" && x.its.dojGregorian < venueLocalTime);

                    var missingEntries = from empSalary in _context.employee_dept_salary
                     .Include(x => x.its)
                     .ThenInclude(x => x.mauzeNavigation)
                     .Include(x => x.deptVenue)
                     .Include(x => x.salaryType)
                     .Where(x => x.its.employeeType != "Khidmatguzaar" && x.deptVenue.qismId == qismId && x.its.mauzeNavigation.qismId == qismId && deptIds.Contains(x.deptVenueId))
                                         join entry in _context.azwaaj_minentry
                                         .Include(x => x.deptVenue)
                                         .Include(x => x.its)
                                         .Include(x => x.salaryType)
                                         .Where(x => x.date == dateOnly && x.its.employeeType != "Khidmatguzaar" && x.deptVenue.qismId == qismId)
                                         on empSalary.itsId equals entry.itsid into entryGroup
                                         from subEntry in entryGroup.DefaultIfEmpty()
                                         where subEntry == null
                                         select empSalary;

                    msg = msg + "Employee dept salaries fetched -> ";

                    if (salarytypes != null)
                    {
                        entries = entries.Where(x => salaryTypes.Contains(x.policyId ?? 0));
                        employee_dept_salaries = employee_dept_salaries.Where(x => salaryTypes.Contains(x.salaryTypeId));
                    }

                    if (employeeTypes != null)
                    {
                        entries = entries.Where(x => empTypes.Any(y => y == x.its.employeeType));
                        employee_dept_salaries = employee_dept_salaries.Where(x => empTypes.Any(y => y == x.its.employeeType));
                    }

                    List<azwaaj_minentry> entriesList = entries.ToList();
                    //List<employee_dept_salary> employee_dept_salariesList = employee_dept_salaries.ToList();

                    //if (entriesList.Count == 0)
                    //{
                    //    return Ok();
                    //}

                    data = _mapper.Map<List<azwaaj_minentry_dto>>(entriesList);

                    msg = msg + "azwaaj_minentry_dto mapper done -> ";

                    foreach (azwaaj_minentry_dto item in data)
                    {
                        item.hijriDate = hijriStr;
                        //item.value = employee_dept_salariesList.Where(x => x.itsId == item.itsid && x.salaryTypeId == item.policyId && x.deptVenueId == item.deptVenueId).FirstOrDefault()?.workingMin ?? 0;
                        item.mz_idaracolor = "";
                        //item.mindiff = (int)(item.min - item.value);
                    }

                    List<employee_dept_salary> missingEntriesList = missingEntries.ToList();

                    foreach (employee_dept_salary item in missingEntriesList)
                    {
                        data.Add(new azwaaj_minentry_dto
                        {
                            createdBy = authU.its.fullName,
                            deptVenueId = item.deptVenueId,
                            date = dateOnly,
                            createdOn = venueLocalTime,
                            policyId = item.salaryTypeId,
                            itsid = item.itsId,
                            min = 0,
                            hijriDate = hijriStr,
                            deptVenueName = item.deptVenue.deptName + "_" + item.deptVenue.venueName,
                            isOnLeave = false,
                            policyName = item.salaryType.Name,
                            name = item.its.fullName,
                            qismId = item.deptVenue.qismId ?? 0,
                            value = item.workingMin,
                            mz_idara = item.its.mz_idara,
                            mauze = item.its.mauzeNavigation?.displayName ?? "N/A",
                            designation = item.its.designation,
                            employeeType = item.its.employeeType,
                            mz_idaracolor = "",
                            mindiff = -1 * (item.workingMin ?? 0),
                            allotedMin = item.workingMin,
                            rate = item.salaryAmount
                        });
                    }

                    data = data.OrderBy(x => x.mindiff).ToList();

                    return data;

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex + msg);
            }

        }

        // GET: api/azwaaj_minentry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<azwaaj_minentry>> Getazwaaj_minentry(int id)
        {
            if (_context.azwaaj_minentry == null)
            {
                return NotFound();
            }
            var azwaaj_minentry = await _context.azwaaj_minentry.FindAsync(id);

            if (azwaaj_minentry == null)
            {
                return NotFound();
            }

            return azwaaj_minentry;
        }

        // PUT: api/azwaaj_minentry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putazwaaj_minentry(int id, azwaaj_minentry azwaaj_minentry)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (id != azwaaj_minentry.id)
            {
                return BadRequest();
            }

            var existingEntry = await _context.azwaaj_minentry.AsNoTracking().FirstOrDefaultAsync(e => e.id == id);
            if (existingEntry == null)
            {
                return NotFound();
            }

            bool hasChanges = !string.Equals(existingEntry.min, azwaaj_minentry.min); // Compare relevant fields
            if (hasChanges)
            {
                // Set UpdatedBy and UpdatedOn only if changes are detected.
                azwaaj_minentry.createdBy = authUser.Name + " - backdated"; // Assuming you have a Username or similar property
                azwaaj_minentry.createdOn = DateTime.UtcNow; // Or your appropriate time zone
            }

            _context.Entry(azwaaj_minentry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!azwaaj_minentryExists(id))
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

        // POST: api/azwaaj_minentry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<azwaaj_minentry>> Postazwaaj_minentry(List<azwaaj_minentry_dto> azwaaj_minentry_dto)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.azwaaj_minentry == null)
            {
                return Problem("Entity set 'mzdb_context.azwaaj_minentry'  is null.");
            }

            List<azwaaj_minentry> azwaaj_minentry = _mapper.Map<List<azwaaj_minentry>>(azwaaj_minentry_dto);

            DateOnly? selectedDate = azwaaj_minentry.FirstOrDefault().date;

            List<azwaaj_minentry> existing = _context.azwaaj_minentry.Where(x => x.date == selectedDate).ToList();

            List<azwaaj_minentry> toUpdate = existing.Join(azwaaj_minentry, e => e.id, m => m.id, (e, m) => m).ToList();
            List<azwaaj_minentry> toAdd = azwaaj_minentry.Where(x => !toUpdate.Any(y => y.id == x.id)).ToList();

            foreach (azwaaj_minentry azwaaj in toAdd)
            {
                await _context.azwaaj_minentry.AddAsync(azwaaj);
            }

            foreach (azwaaj_minentry azwaaj in toUpdate)
            {
                var existingEntity = _context.azwaaj_minentry.Find(azwaaj.id);
                if (existingEntity != null)
                {
                    azwaaj.createdBy = authUser.Name;
                    azwaaj.createdOn = DateTime.Now;
                    _context.Entry(existingEntity).CurrentValues.SetValues(azwaaj);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return CreatedAtAction("Getazwaaj_minentry", azwaaj_minentry);
        }

        // DELETE: api/azwaaj_minentry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteazwaaj_minentry(int id)
        {
            if (_context.azwaaj_minentry == null)
            {
                return NotFound();
            }
            var azwaaj_minentry = await _context.azwaaj_minentry.FindAsync(id);
            if (azwaaj_minentry == null)
            {
                return NotFound();
            }

            _context.azwaaj_minentry.Remove(azwaaj_minentry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool azwaaj_minentryExists(int id)
        {
            return (_context.khidmat_guzaar?.Any(e => e.itsId == id)).GetValueOrDefault();
        }

        [Route("logs/{itsId}/{deptVenueId}")]
        [HttpGet]
        public async Task<ActionResult> logs(int itsId, int deptVenueId)
        {
            List<minEntryLogModel> minEntryLogModels = new List<minEntryLogModel>();
            khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == itsId).AsNoTracking().FirstOrDefault();
            dept_venue dv = _context.dept_venue.Where(x => x.id == deptVenueId).Include(x => x.venue).AsNoTracking().FirstOrDefault();

            minEntryLogReport minEntryReportModel = new minEntryLogReport()
            {
                name = kg.fullName,
                deptName = dv.deptName,
                venueName = dv.venue.displayName
            };

            List<azwaaj_minentry> logs = await _context.azwaaj_minentry.Where(x => x.itsid == itsId && x.deptVenueId == deptVenueId).Include(x => x.salaryType).AsNoTracking().ToListAsync();



            foreach (azwaaj_minentry log in logs)
            {
                minEntryLogModels.Add(new minEntryLogModel
                {
                    min = log.min ?? 0,
                    createdOn = log.createdOn ?? DateTime.UtcNow,
                    markedBy = log.createdBy,
                    itsId = log.itsid ?? 0,
                    salaryType = log.salaryType.Name,
                    date = log.date ?? DateOnly.FromDateTime(DateTime.UtcNow),
                });
            }

            minEntryLogModels = minEntryLogModels.OrderByDescending(x => x.date).ToList();

            minEntryReportModel.logs = minEntryLogModels;

            return Ok(minEntryReportModel);
        }


        [AllowAnonymous]
        [Route("minEntryReminder")]
        [HttpGet]
        public async Task<ActionResult> minEntryReminder()
        {
            string api = "minEntryReminder";
            string s = "";

            try
            {

                //List<int> allowedvid = _helperService.allowedIds(11, true);
                List<int> allowedvid = _helperService.allowedIds(19, false);
                if (allowedvid.Count == 0) { return Ok("No Venue Found"); }

                venue m = _context.venue.FirstOrDefault(x => x.qismId > 0 && allowedvid.Any(y => y == x.Id));
                TimeZoneInfo venueTimeZone = TimeZoneInfo.FindSystemTimeZoneById(m.CampId);
                DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, venueTimeZone);

                DateOnly dateOnly = DateOnly.FromDateTime(today);
                //List<int> allowedvid = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 117, 118, 119, 120, 121, 122, 123, 124, 125 };


                List<branch_user> users = await _context.branch_user.Where(x => x.platform_user_module.Any(y => y.moduleId == 59 || y.moduleId == 60 || y.moduleId == 61) && x.venues.Any(y => allowedvid.Any(z => z == y.Id)))
                    .Include(x => x.its)
                    .Include(x => x.venues)
                    .Include(x => x.deptVenue)
                    .Include(x => x.qism_al_tahfeez)
                    .ThenInclude(x => x.dept_venue)
                    .Include(x => x.platform_user_module)
                    .AsNoTracking()
                    .ToListAsync();


                List<qism_al_tahfeez> qisms = users.Select(x => x.qism_al_tahfeez).Distinct().ToList();

                List<azwaaj_minentry> minentry = await _context.azwaaj_minentry.Where(x => x.date == DateOnly.FromDateTime(today)).AsNoTracking().ToListAsync();

                List<employee_dept_salary> employee_dept_salaries = await _context.employee_dept_salary
                    .Include(x => x.its)
                    .Where(x => x.its.employeeType != "Khidmatguzaar").AsNoTracking().ToListAsync();



                foreach (qism_al_tahfeez qism in qisms)
                {
                    if (qism == null)
                    {
                        continue;
                    }

                    List<minEntryReminderModel> minEntryReminderModels = new List<minEntryReminderModel>();
                    List<branch_user> qism_users = users.Where(x => x.venues.Any(y => y.qismId == qism.id)).ToList();
                    List<dept_venue> qism_venues = qism_users.SelectMany(x => x.deptVenue).Distinct().ToList();

                    List<azwaaj_minentry> qism_entries = minentry.Where(x => qism.dept_venue.Any(y => y.id == x.deptVenueId)).ToList();
                    List<employee_dept_salary> qism_dept_Salaries = employee_dept_salaries.Where(x => qism.dept_venue.Any(y => y.id == x.deptVenueId)).ToList();
                    int srno = 0;
                    foreach (branch_user coordinator in qism_users)
                    {

                        if (qism.itsId == coordinator.itsId)
                        {
                            minEntryReminderModels.Add(new minEntryReminderModel
                            {
                                srno = ++srno,
                                itsId = coordinator.itsId,
                                name = coordinator.its.fullName,
                                Role = "Masul",
                                count = qism_dept_Salaries.Count() - qism_entries.Count(),
                            });
                            continue;
                        }

                        minEntryReminderModel minEntryReminderModel = new minEntryReminderModel
                        {
                            srno = ++srno,
                            itsId = coordinator.itsId,
                            name = coordinator.its.fullName,
                            Role = "Coordinator -",
                            count = 0,
                        };

                        //Policy 59 - Per Minute Policy 60 - Per Period Policy 61 - Fixed

                        if (coordinator.platform_user_module.Any(x => x.moduleId == 59))
                        {
                            minEntryReminderModel.count += qism_dept_Salaries.Where(x => x.salaryTypeId == 2 && coordinator.deptVenue.Any(y => y.id == x.deptVenueId)).Count() - qism_entries.Where(x => x.policyId == 2 && coordinator.deptVenue.Any(y => y.id == x.deptVenueId)).Count();
                            minEntryReminderModel.Role += " Per Minute";
                        }

                        if (coordinator.platform_user_module.Any(x => x.moduleId == 60))
                        {
                            minEntryReminderModel.count += qism_dept_Salaries.Where(x => x.salaryTypeId == 3 && coordinator.deptVenue.Any(y => y.id == x.deptVenueId)).Count() - qism_entries.Where(x => x.policyId == 3 && coordinator.deptVenue.Any(y => y.id == x.deptVenueId)).Count();
                            minEntryReminderModel.Role += " Per Period";
                        }

                        if (coordinator.platform_user_module.Any(x => x.moduleId == 61))
                        {
                            minEntryReminderModel.count += qism_dept_Salaries.Where(x => x.salaryTypeId == 1 && coordinator.deptVenue.Any(y => y.id == x.deptVenueId)).Count() - qism_entries.Where(x => x.policyId == 1 && coordinator.deptVenue.Any(y => y.id == x.deptVenueId)).Count();
                            minEntryReminderModel.Role += " Fixed";
                        }

                        string msg1 = @"<div style=""max-width: 600px; margin: 20px auto; padding: 20px; background-color: #f8f8f8; border: 1px solid #ddd; border-radius: 10px;"">
                                <p style=""color: #333;"">Salaam Jameel,</p>

                                <p>This email serves as a reminder to submit ‘Daily Minute Entries’ on <a href=""http://www.mahadalzahra.org"">www.mahadalzahra.org</a> under Branch Login &gt; HR &gt; Attendance &gt; Daily Minute Entry.</p>

                                <table style=""width: 100%; border-collapse: collapse; margin-top: 20px;"">
                                  <thead>
                                    <tr>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Sr</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">ITS</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Name</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Role</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Pending Entries</th>
                                    </tr>
                                  </thead>
                                  <tbody>
                                    <tr>
                                      <td style=""border: 1px solid #ddd; padding: 10px;""> 1 </td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.itsId + @"</td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.name + @"</td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.Role + @"</td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.count + @"</td>
                                    </tr>
                                  </tbody>
                                </table>

                                <p>Please be aware that if the pending entries are not completed before 11 PM (local time), the module will auto-submit the sheet, marking them absent for the day.</p>

                                <p>However, this can be rectified before 11:59 PM today or by back-dated entries procedure.</p>

                                <p>Shukran, <br> Wa al-Salaam.</p>
                              </div>";

                        if (minEntryReminderModel.count != 0)
                        {
                            _notificationService.SendStandardHTMLEmail("Daily Minute Entry Reminder", msg1, string.IsNullOrEmpty(coordinator.its.officialEmailAddress) ? coordinator.its.emailAddress : coordinator.its.officialEmailAddress, "attendance");
                            //_notificationService.SendStandardHTMLEmail("Daily Minute Entry Reminder", msg1, "hatimn219@gmail.com", "attendance");
                        }

                        minEntryReminderModels.Add(minEntryReminderModel);

                    }


                    string msg = @"
                                <h3 style=""color: #333;"">Salaam Jameel,</h3>

                                <p>This email serves as a reminder to submit ‘Daily Minute Entries’ on <a href=""http://www.mahadalzahra.org"">www.mahadalzahra.org</a> under Branch Login &gt; HR &gt; Attendance &gt; Daily Minute Entry.</p>

                                <table style=""width: 100%; border-collapse: collapse; margin-top: 20px;"">
                                  <thead>
                                    <tr>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Sr</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">ITS</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Name</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Role</th>
                                      <th style=""border: 1px solid #ddd; padding: 10px; text-align: left;"">Pending Entries</th>
                                    </tr>
                                  </thead>
                                  <tbody>";

                    foreach (minEntryReminderModel minEntryReminderModel in minEntryReminderModels)
                    {
                        msg += @"
                                    <tr>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.srno + @" </td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.itsId + @"</td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.name + @"</td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.Role + @"</td>
                                      <td style=""border: 1px solid #ddd; padding: 10px;"">" + minEntryReminderModel.count + @"</td>
                                    </tr>";
                    }
                    msg += @"
                                  </tbody>
                                </table>

                                <p>Please be aware that if the pending entries are not completed before 11 PM (local time), the module will auto-submit the sheet, marking them absent for the day.</p>

                                <p>However, this can be rectified before 11:59 PM today or by back-dated entries procedure.</p>

                                <p>Shukran, <br> Wa al-Salaam.</p>
                              ";

                    if (minEntryReminderModels.Any(x => x.count != 0))
                    {
                        _notificationService.SendStandardHTMLEmail("Daily Minute Entry Reminder", msg, qism.emailId, "attendance");
                        //_notificationService.SendStandardHTMLEmail("Daily Minute Entry Reminder", msg, "hatimn219@gmail.com", "attendance");
                    }

                }


                return Ok(s);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


        [AllowAnonymous]
        [Route("v2/missingAttendance")]
        [HttpGet]
        public async Task<ActionResult> missingAttendance()
        {
            try
            {
                List<int> onLeaveList = _globalConstants.onLeaveStages;

                venue m = _context.venue.FirstOrDefault(x => x.qismId > 0);
                TimeZoneInfo venueTimeZone = TimeZoneInfo.FindSystemTimeZoneById(m.CampId);
                DateTime yestarday = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-1), venueTimeZone);

                //DateOnly n = DateOnly.FromDateTime(yestarday);

                // Combine database query and in-memory filter to minimize data loading
                List<khidmat_guzaar> kgs = await _context.khidmat_guzaar
                    .Where(kg => kg.activeStatus == true &&
                                 kg.employee_salary.isMahadSalary == true && kg.mauze > 0 &&
                                 (kg.CreatedOn > yestarday || kg.UpdatedOn > yestarday))
                    .Include(kg => kg.employee_dept_salary)
                    .ThenInclude(x => x.deptVenue)
                    .Include(kg => kg.employee_salary)
                    .Include(kg => kg.mauzeNavigation)
                    .Include(kg => kg.azwaaj_minentry)
                    .ThenInclude(kg => kg.deptVenue)
                    .ToListAsync();

                foreach (var kg in kgs)
                {

                    if (kg.azwaaj_minentry.Any(x => x.deptVenue.venueId == kg.mauze))
                    {
                        continue;
                    }
                    venueTimeZone = TimeZoneInfo.FindSystemTimeZoneById(kg.mauzeNavigation.CampId);
                    yestarday = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-1), venueTimeZone);

                    DateTime startDate = kg.dojGregorian ?? (kg.CreatedOn ?? DateTime.Now);

                    for (DateTime date = startDate; date.Date <= yestarday.Date; date = date.AddDays(1))
                    {
                        foreach (var eds in kg.employee_dept_salary)
                        {
                            var entry = new azwaaj_minentry()
                            {
                                itsid = kg.itsId,
                                deptVenueId = eds.deptVenueId,
                                policyId = eds.salaryTypeId,
                                min = eds.salaryTypeId == 1 ? eds.workingMin : 0,
                                allotedMin = eds.workingMin ?? 0,
                                rate = eds.salaryAmount,
                                createdOn = yestarday,
                                createdBy = "System",
                                date = DateOnly.FromDateTime(date),
                                description = "System Generated - Back Dated entry (DOJ)",
                                isOnLeave = false // Assuming 'leave' logic is handled elsewhere
                            };
                            kg.azwaaj_minentry.Add(entry);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return Ok("success");
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error message
                // Implement logging as per your application's logging strategy
                // Example: _logger.LogError(ex, "Error in missingAttendance API");

                return BadRequest(ex);
            }
        }


        [AllowAnonymous]
        [Route("minEntryJob")]
        [HttpGet]
        public async Task<ActionResult> minEntryJob()
        {
            string api = "minEntryJob";
            string s = "";

            try
            {
                List<int> allowedvid = _helperService.allowedIds(23, false);
                if (allowedvid.Count == 0)
                {
                    return Ok("No Venue Found");
                }

                venue v = _context.venue.FirstOrDefault(x => x.qismId > 0 && allowedvid.Any(y => y == x.Id));
                TimeZoneInfo venueTimeZone = TimeZoneInfo.FindSystemTimeZoneById(v.CampId);
                DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, venueTimeZone);

                DateOnly dateOnly = DateOnly.FromDateTime(today);
                //List<azwaaj_minentry> minentry = await _context.azwaaj_minentry.Where(x => x.date == dateOnly).AsNoTracking().ToListAsync();


                List<khidmat_guzaar> kgs = await _context.khidmat_guzaar.Where(x => x.activeStatus == true && allowedvid.Any(y => y == (x.mauze ?? 0)))
                    .Include(x => x.employee_dept_salary)
                    .Include(x => x.azwaaj_minentry.Where(y => y.date == dateOnly))
                    .Include(x => x.mzlm_leave_application)
                    .AsNoTracking()
                    .ToListAsync();
                //List<khidmat_guzaar> kgs = _context.khidmat_guzaar.Where(x => x.itsId==30389002).ToList();

                //List<azwaaj_minentry> minentry = _context.azwaaj_minentry.Where(x => x.date == dateOnly).ToList();
                List<int> OnleaveList = _globalConstants.onLeaveStages;


                foreach (khidmat_guzaar m in kgs)
                {

                    List<employee_dept_salary> depts = m.employee_dept_salary.Where(x => x.hasSalary == true).ToList();
                    //List<employee_dept_salary> depts = m.employee_dept_salary.Where(x => x.hasSalary == true && (x.salaryTypeId == 1)).ToList(); // testing
                    //List<employee_dept_salary> depts = m.employee_dept_salary.Where(x => x.hasSalary == true && (x.salaryTypeId == 2 || x.salaryTypeId == 3) && x.deptVenueId != 17).ToList(); // do not include jamea
                    foreach (var n in depts)
                    {
                        if (m.azwaaj_minentry.Any(x => x.itsid == m.itsId && x.deptVenueId == n.deptVenueId && x.policyId == n.salaryTypeId))
                        {
                            continue;
                        }

                        List<mzlm_leave_application> leaveList = m.mzlm_leave_application.Where(x => x.fromEngDate <= today && x.toEngDate >= today && OnleaveList.Any(y => y == x.stageId)).ToList();
                        bool leave = leaveList.Count > 0;

                        await _context.azwaaj_minentry.AddAsync(new azwaaj_minentry
                        {
                            itsid = m.itsId,
                            createdBy = "Auto-System",
                            createdOn = today,
                            date = dateOnly,
                            deptVenueId = n.deptVenueId,
                            policyId = n.salaryTypeId,
                            description = "genrated by system to maintain records",
                            min = n.salaryTypeId == 1 ? n.workingMin : 0,
                            isOnLeave = false,
                            allotedMin = n.workingMin ?? 0,
                            rate = n.salaryAmount
                        });

                    }
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    return BadRequest(ex);

                }


                return Ok(s);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }


        }


        public struct minEntryReportModel
        {
            public int srno { get; set; }
            public int itsId { get; set; }
            public string name { get; set; }
            public int allotedMin { get; set; }
            public int MinEntry { get; set; }
            public int diffrence { get; set; }
        }

        [AllowAnonymous]
        [Route("minEntryReport")]
        [HttpGet]
        public async Task<ActionResult> minEntryReport()
        {
            string api = "minEntryJob";
            string s = "";

            try
            {
                List<int> allowedvid = _helperService.allowedIds(23, true);
                //List<int> allowedvid = _helperService.allowedIds(12, false);
                if (allowedvid.Count == 0) { return Ok("No Venue Found"); }
                venue m = _context.venue.FirstOrDefault(x => x.qismId > 0 && allowedvid.Any(y => y == x.Id));
                TimeZoneInfo venueTimeZone = TimeZoneInfo.FindSystemTimeZoneById(m.CampId);
                DateTime today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, venueTimeZone);

                DateOnly dateOnly = DateOnly.FromDateTime(today);

                List<branch_user> users = await _context.branch_user.Where(x => x.platform_user_module.Any(y => y.moduleId == 59 || y.moduleId == 60 || y.moduleId == 61) && x.venues.Any(y => allowedvid.Any(z => z == y.Id)))
                   .Include(x => x.its)
                   .Include(x => x.venues)
                   .Include(x => x.deptVenue)
                   .Include(x => x.qism_al_tahfeez)
                   .ThenInclude(x => x.dept_venue)
                   .Include(x => x.platform_user_module)
                   .AsNoTracking()
                   .ToListAsync();

                List<qism_al_tahfeez> qisms = users.Select(x => x.qism_al_tahfeez).Distinct().ToList();

                List<azwaaj_minentry> minentry = await _context.azwaaj_minentry.Where(x => x.date == dateOnly && x.its.employeeType != "Khidmatguzaar").ToListAsync();

                List<employee_dept_salary> employee_dept_salaries = await _context.employee_dept_salary
                    .Include(x => x.its)
                    .Where(x => x.its.employeeType != "Khidmatguzaar").AsNoTracking().ToListAsync();

                foreach (qism_al_tahfeez qism in qisms)
                {
                    if (qism == null)
                    {
                        continue;
                    }

                    List<branch_user> qism_users = users.Where(x => x.venues.Any(y => y.qismId == qism.id)).ToList();
                    List<dept_venue> qism_venues = qism_users.SelectMany(x => x.deptVenue).Distinct().ToList();

                    List<azwaaj_minentry> qism_entries = minentry.Where(x => qism.dept_venue.Any(y => y.id == x.deptVenueId)).ToList();
                    List<employee_dept_salary> qism_dept_Salaries = employee_dept_salaries.Where(x => qism.dept_venue.Any(y => y.id == x.deptVenueId)).ToList();

                    foreach (branch_user coordinator in qism_users)
                    {

                        List<minEntryReportModel> minEntryReportModels = new List<minEntryReportModel>();
                        int srno = 0;

                        if (coordinator.platform_user_module.Any(x => x.moduleId == 59))
                        {
                            List<employee_dept_salary> eds = qism_dept_Salaries.Where(x => coordinator.deptVenue.Any(y => y.id == x.deptVenueId) && x.salaryTypeId == 2).ToList();

                            foreach (employee_dept_salary empDeptSal in eds)
                            {
                                azwaaj_minentry azMin = qism_entries.Where(x => x.itsid == empDeptSal.itsId && x.deptVenueId == empDeptSal.deptVenueId && x.policyId == 2).FirstOrDefault();
                                minEntryReportModel minEntryReportModel = new minEntryReportModel
                                {
                                    srno = ++srno,
                                    itsId = empDeptSal.itsId,
                                    name = empDeptSal.its.fullName,
                                    allotedMin = empDeptSal.workingMin ?? 0,
                                    MinEntry = azMin?.min ?? (empDeptSal.workingMin ?? 0)
                                };

                                minEntryReportModel.diffrence = minEntryReportModel.MinEntry - minEntryReportModel.allotedMin;

                                minEntryReportModels.Add(minEntryReportModel);
                            }

                        }

                        if (coordinator.platform_user_module.Any(x => x.moduleId == 60))
                        {
                            List<employee_dept_salary> eds = qism_dept_Salaries.Where(x => coordinator.deptVenue.Any(y => y.id == x.deptVenueId) && x.salaryTypeId == 3).ToList();

                            foreach (employee_dept_salary empDeptSal in eds)
                            {
                                azwaaj_minentry azMin = qism_entries.Where(x => x.itsid == empDeptSal.itsId && x.deptVenueId == empDeptSal.deptVenueId && x.policyId == 3).FirstOrDefault();
                                minEntryReportModel minEntryReportModel = new minEntryReportModel
                                {
                                    srno = ++srno,
                                    itsId = empDeptSal.itsId,
                                    name = empDeptSal.its.fullName,
                                    allotedMin = empDeptSal.workingMin ?? 0,
                                    MinEntry = azMin?.min ?? (empDeptSal.workingMin ?? 0)
                                };

                                minEntryReportModel.diffrence = minEntryReportModel.MinEntry - minEntryReportModel.allotedMin;

                                minEntryReportModels.Add(minEntryReportModel);
                            }
                        }

                        if (coordinator.platform_user_module.Any(x => x.moduleId == 61))
                        {
                            List<employee_dept_salary> eds = qism_dept_Salaries.Where(x => coordinator.deptVenue.Any(y => y.id == x.deptVenueId) && x.salaryTypeId == 1).ToList();

                            foreach (employee_dept_salary empDeptSal in eds)
                            {
                                azwaaj_minentry azMin = qism_entries.Where(x => x.itsid == empDeptSal.itsId && x.deptVenueId == empDeptSal.deptVenueId && x.policyId == 1).FirstOrDefault();
                                minEntryReportModel minEntryReportModel = new minEntryReportModel
                                {
                                    srno = ++srno,
                                    itsId = empDeptSal.itsId,
                                    name = empDeptSal.its.fullName,
                                    allotedMin = empDeptSal.workingMin ?? 0,
                                    MinEntry = azMin?.min ?? (empDeptSal.workingMin ?? 0)
                                };

                                minEntryReportModel.diffrence = minEntryReportModel.MinEntry - minEntryReportModel.allotedMin;

                                minEntryReportModels.Add(minEntryReportModel);
                            }
                        }

                        string msg = @"
                                        <p style = ""color: #333;"" > Salaam Jameel,</p>

                                        <p > Attached is the report of 'Daily Minute Entries' as of " + today.ToString("dd/MM/yyyy") + @", 11:30 PM(local time) for your reference:</p>

                                        <table style = ""width: 100%; border-collapse: collapse; margin-top: 20px;"">
                                          <thead>
                                            <tr>
                                              <th style = ""border: 1px solid #ddd; padding: 10px; text-align: left;""> Sr </th>
                                              <th style = ""border: 1px solid #ddd; padding: 10px; text-align: left;""> ITS </th>
                                              <th style = ""border: 1px solid #ddd; padding: 10px; text-align: left;""> Name </th>
                                              <th style = ""border: 1px solid #ddd; padding: 10px; text-align: left;""> Allocated minutes </th>
                                              <th style = ""border: 1px solid #ddd; padding: 10px; text-align: left;""> Minutes Entry </th>
                                              <th style = ""border: 1px solid #ddd; padding: 10px; text-align: left;""> Difference </th>
                                            </tr>
                                          </thead>
                                          <tbody>";
                        foreach (minEntryReportModel minEntryReportModel1 in minEntryReportModels)
                        {

                            msg += @"<tr>
                                    <td style = ""border: 1px solid #ddd; padding: 10px;"">" + minEntryReportModel1.srno + @" </td>
                                    <td style = ""border: 1px solid #ddd; padding: 10px;"">" + minEntryReportModel1.itsId + @" </td>
                                    <td style = ""border: 1px solid #ddd; padding: 10px;"">" + minEntryReportModel1.name + @" </td>
                                    <td style = ""border: 1px solid #ddd; padding: 10px;""> " + minEntryReportModel1.allotedMin + @"</td>
                                    <td style = ""border: 1px solid #ddd; padding: 10px;""> " + minEntryReportModel1.MinEntry + @"</td>
                                    <td style = ""border: 1px solid #ddd; padding: 10px;""> " + minEntryReportModel1.diffrence + @"</td>
                                </tr>";
                        }

                        msg += @"
                                  </tbody>
                                </table>

                                <p> Kindly be informed that the above table sorting is based on the 'Difference' column in descending order.</p>

                                <p> Shukran, <br> Wa al-Salaam.</p>
                              ";

                        _notificationService.SendStandardHTMLEmail("Daily Minute Entry Report", msg, string.IsNullOrEmpty(coordinator.its.officialEmailAddress) ? coordinator.its.emailAddress : coordinator.its.officialEmailAddress, "attendance");
                        //_notificationService.SendStandardHTMLEmail("Daily Minute Entry Report", msg, "hatimn219@gmail.com", "attendance");

                    }

                }


                return Ok(s);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }


        }
    }

    public class minEntryReminderModel
    {
        public int srno { get; set; }
        public int itsId { get; set; }
        public string name { get; set; }
        public string Role { get; set; }
        public int count { get; set; }
    }
    public class minEntryLogReport
    {
        public List<minEntryLogModel> logs { get; set; }
        public string venueName { get; set; }
        public string name { get; set; }
        public string deptName { get; set; }
    }
    public class minEntryLogModel
    {
        public int srno { get; set; }
        public int itsId { get; set; }
        public int min { get; set; }
        public string markedBy { get; set; }
        public DateOnly date { get; set; }
        public DateTime createdOn { get; set; }

        public string salaryType { get; set; }
    }
}
