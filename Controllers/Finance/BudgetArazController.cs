using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Migrations;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using static mahadalzahrawebapi.Controllers.ItemController;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetArazController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly BudgetArazService _budgetArazService;

        public BudgetArazController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _budgetArazService = new BudgetArazService(context);
        }

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById(
            "Asia/Kolkata"
        );
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        private int FinancialYear = 2024;

        //CacheService cache = new CacheService();

        [Route("addsmartgoal")]
        [HttpPost]
        public async Task<IActionResult> getBaseItem(BudgetSmartGoal goal)
        {
            string api = "addsmartgoal";
            //// Add_ApiLogs(api);

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                global_constant financialYear = _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault();
                mz_expense_budget_smart_goals goals = new mz_expense_budget_smart_goals
                {
                    itsId = authUser.ItsId,
                    attainable = goal.attainable,
                    category = goal.category,
                    createdOn = DateTime.Now,
                    deptVenueId = goal.deptVenueId,
                    financialYear = Int32.Parse(financialYear.value),
                    measearable = goal.measurable,
                    relevant = goal.relevant,
                    specific = goal.specific,
                    stage = "Initiated",
                    timeStart = goal.time.FromDate,
                    timeEnd = goal.time.ToDate
                };
                _context.mz_expense_budget_smart_goals.Add(goals);
                _context.SaveChanges();

                return Ok(goal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getsmartgoals")]
        [HttpGet]
        public async Task<IActionResult> getsmartgoals()
        {
            string api = "addsmartgoal";
            //// Add_ApiLogs(api);

            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                global_constant financialYear = _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault();
                int fy = Int32.Parse(financialYear.value);

                List<mz_expense_budget_smart_goals> goals = _context
                    .mz_expense_budget_smart_goals.Where(x => x.financialYear == fy)
                    .Include(x => x.mz_expense_budget_smart_issue_logs)
                    .Include(x => x.deptVenue)
                    .ThenInclude(x => x.venue)
                    .ToList();

                List<user_deptvenue> dvs = _context
                    .user_deptvenue.Where(x => x.itsId == authUser.ItsId)
                    .ToList();
                List<BudgetSmartGoal> goal = new List<BudgetSmartGoal>();
                List<khidmat_guzaar> kg = _context
                    .khidmat_guzaar.Where(x =>
                        x.employeeType == "Khidmatguzaar" && x.activeStatus == true
                    )
                    .ToList();

                foreach (var j in dvs)
                {
                    foreach (
                        mz_expense_budget_smart_goals g in goals
                            .Where(x => x.deptVenueId == j.deptVenueId)
                            .ToList()
                    )
                    {
                        BudgetSmartGoal goalModel = new BudgetSmartGoal
                        {
                            attainable = g.attainable,
                            deptVenueId = g.deptVenueId,
                            deptVenueName =
                                g.deptVenue.deptName + "_" + g.deptVenue.venue.displayName,
                            category = g.category,
                            createdBy = kg.Where(x => x.itsId == g.itsId)
                                .FirstOrDefault()
                                ?.fullName,
                            measurable = g.measearable,
                            id = g.id,
                            itsId = g.itsId,
                            createdOn = g.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                            relevant = g.relevant,
                            specific = g.specific,
                            stage = g.stage,
                            timeFrom = g.timeStart?.ToString("dd/MM/yyyy"),
                            timeTo = g.timeEnd?.ToString("dd/MM/yyyy"),
                            isConcerning = false,
                            verified = true,
                            hasIssues = false,
                        };
                        goalModel.time = new DateRange
                        {
                            FromDate = g.timeStart,
                            ToDate = g.timeEnd,
                        };

                        if (g.mz_expense_budget_smart_issue_logs.Count > 0)
                        {
                            goalModel.hasIssues = true;
                        }
                        if (g.stage.Contains("Initia"))
                        {
                            goalModel.verified = false;
                            if (g.stage.Contains("concern"))
                            {
                                goalModel.isConcerning = true;
                            }
                        }
                        goal.Add(goalModel);
                    }
                }
                return Ok(goal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("verifysmartgoal")]
        [HttpPost]
        public async Task<IActionResult> verifysmartgoal(BudgetSmartGoal arazItem)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string api = "getbudgetarazsummamy";
            List<BudgetSmartGoal> arazItems = new List<BudgetSmartGoal>();
            arazItems.Add(arazItem);
            //// Add_ApiLogs(api);

            return Ok(_budgetArazService.verifyBudgetSmartGoal(authUser, arazItems));
        }

        public struct smartGoalVarify
        {
            public List<BudgetSmartGoal> arazItems;
        };

        [Route("verifysmartgoals")]
        [HttpPost]
        public async Task<IActionResult> verifySmartGoals(smartGoalVarify budgetVarification)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);

            return Ok(
                _budgetArazService.verifyBudgetSmartGoal(authUser, budgetVarification.arazItems)
            );
        }

        [Route("smartgoalissues")]
        [HttpPost]
        public async Task<IActionResult> smartGoalIssues(BudgetSmartGoal budgetVarification)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            string api = "smartgoalissues";
            //// Add_ApiLogs(api);

            string res = "";
            try
            {
                res = _budgetArazService.budgetSmartGoalIssue(authUser, budgetVarification, false);

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("budgetsmartgoalissue/{arazId}")]
        [HttpGet]
        public async Task<IActionResult> budgetSmartGoalIssues(int arazId)
        {
            List<Dictionary<String, String>> issues = new List<Dictionary<string, string>>();

            string api = "budgetitemissuelogs/{arazId}";

            List<mz_expense_budget_smart_issue_logs> logs = _context
                .mz_expense_budget_smart_issue_logs.Where(x => x.smartGoalId == arazId)
                .ToList();
            logs = logs.OrderByDescending(x => x.createdOn).ToList();
            int c = 0;
            foreach (mz_expense_budget_smart_issue_logs log in logs)
            {
                c++;
                Dictionary<string, string> temp = new Dictionary<string, string>();
                temp.Add("srno", c.ToString());
                temp.Add("remark", log.remark);
                temp.Add("user", log.createdBy);
                temp.Add("isConcerning", log.isConcerning.ToString());
                temp.Add("date", log.createdOn?.ToString("dd/M/yyyy hh:mm tt"));
                issues.Add(temp);
            }

            return Ok(issues);
        }

        [Route("getbaseItems/{deptVenueId}/{psetId}")]
        [HttpGet]
        public async Task<IActionResult> getBaseItem(int deptVenueId, int psetId)
        {
            string api = "getbaseItems/{deptVenueId}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<dropdown_dataset_header> baseitems = new List<dropdown_dataset_header>();

                List<user_dept_venue_baseitem> rights = _context
                    .user_dept_venue_baseitem.Where(x =>
                        x.itsId == authUser.ItsId && x.dept_venueId == deptVenueId && x.psetId == psetId
                    )   
                    .ToList();

                foreach (var i in rights)
                {
                    mz_expense_procurement_baseitem baseitem = _context
                        .mz_expense_procurement_baseitem.Where(x => x.id == i.baseItemId)
                        .FirstOrDefault();
                    if (baseitem != null)
                    {
                        baseitems.Add(
                            new dropdown_dataset_header
                            {
                                id = i.baseItemId ?? 0,
                                name = baseitem.name
                            }
                        );
                    }
                }

                return Ok(baseitems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getsanctionedBudget")]
        [HttpGet]
        public async Task<IActionResult> getSanctionedBudget()
        {
            string api = "getsanctionedBudget";
            //// Add_ApiLogs(api);
            int financialYear = indianTime.Year;
            int mnth = indianTime.Month;

            var monthCheck = mnth % 3;

            var start = 0;
            var end = 0;

            if (monthCheck == 0)
            {
                start = mnth - 2;
                end = mnth;
            }
            else if (monthCheck == 2)
            {
                start = mnth - 1;
                end = mnth + 1;
            }
            else
            {
                start = mnth;
                end = mnth + 2;
            }

            int currMonthSwch = switchMonth(end);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<BudgetArazExpenseModel> baseitems = new List<BudgetArazExpenseModel>();

                List<user_deptvenue> dvs = _context
                    .user_deptvenue.Where(x => x.itsId == authUser.ItsId)
                    .ToList();
                List<user_dept_venue_baseitem> rightsOfUser = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();
                List<mz_expense_procurement_baseitem> baseItems = _context
                    .mz_expense_procurement_baseitem.Where(x => !x.isIncome)
                    .ToList();

                List<mz_expense_budget_araz> arazs = _context
                    .mz_expense_budget_araz.Include(x => x.mz_expense_budget_araz_monthly)
                    .Where(x =>
                        x.stage == "Sanctioned" && x.financialYear == financialYear
                    )
                    .Include(x => x.deptVenue)
                    .Include(x => x.baseItem)
                    .Include(x => x.item)
                    .ToList();

                List<mz_expense_bill_master> bills = _context
                    .mz_expense_bill_master.Where(x =>
                        x.status != "Rejected" && x.financialYear == financialYear
                    )
                    .Include(x => x.mz_expense_bill_item)
                    .ToList();

                List<mz_expense_budget_araz_transfer_logs> transfer_Logs = _context
                    .mz_expense_budget_araz_transfer_logs.Where(x =>
                        x.fromAraz.financialYear == financialYear
                    )
                    .Include(x => x.fromAraz).ThenInclude(x => x.deptVenue)
                    .Include(x => x.fromAraz).ThenInclude(x => x.baseItem)
                    .Include(x => x.fromAraz).ThenInclude(x => x.item)
                    .Include(x => x.toAraz).ThenInclude(x => x.deptVenue)
                    .Include(x => x.toAraz).ThenInclude(x => x.baseItem)
                    .Include(x => x.toAraz).ThenInclude(x => x.item)
                    .ToList();

                List<registrationform_dropdown_set> registration_set = _context.registrationform_dropdown_set.Include(x => x.subprogram).ToList();

                //List<registrationform_subprograms> subprograms = _context.registrationform_subprograms.ToList();

                foreach (user_deptvenue j in dvs)
                {
                    List<user_dept_venue_baseitem> rights = rightsOfUser
                        .Where(x => x.dept_venueId == j.deptVenueId && x.psetId == j.psetId)
                        .ToList();

                    if (rights.Count == 0)
                    {
                        continue;
                    }

                    foreach (user_dept_venue_baseitem i in rights)
                    {
                        List<BudgetArazExpenseItemModel> items =
                            new List<BudgetArazExpenseItemModel>();
                        try
                        {
                            mz_expense_procurement_baseitem? bi = baseItems
                                .Where(x => x.id == i.baseItemId)
                                .FirstOrDefault();
                            if (bi == null)
                            {
                                continue;
                            }

                            SalaryService sService = new SalaryService();
                            List<mz_expense_budget_araz> arazList = arazs
                                .Where(x =>
                                    x.deptVenueId == (j.deptVenueId) && x.psetId == j.psetId
                                    && x.baseItemId == (i.baseItemId ?? 0)
                                )
                                .ToList();

                            long totalExpense = 0;
                            float sanctionedAmountt = 0;
                            int currSanctionAmount = 0;
                            int tranferedAmount = 0;

                            List<mz_expense_bill_master> dvbiBills = bills
                                .Where(x =>
                                    x.baseItemId == i.baseItemId && x.deptVenueId == j.deptVenueId && x.psetId == j.psetId
                                )
                                .ToList();

                            if (arazList.Count == 0)
                            {
                                continue;
                            }

                            foreach (mz_expense_budget_araz item in arazList)
                            {
                                List<mz_expense_bill_item> billItems = dvbiBills
                                    .SelectMany(bill => bill.mz_expense_bill_item)
                                    .Where(billItem => billItem.itemId == item.itemId)
                                    .ToList();
                                billItems.ForEach(x =>
                                {
                                    x.bill = new mz_expense_bill_master();
                                    x.item = new mz_expense_procurement_item();
                                });

                                List<mz_expense_budget_araz_transfer_logs> transferFromItem =
                                    transfer_Logs
                                    .Where(x => x.fromArazId == item.id)
                                    .ToList();
                                List<mz_expense_budget_araz_transfer_logs> transferToItem =
                                    transfer_Logs.Where(x => x.toArazId == item.id).ToList();

                                float arazQuantity = item.amountPerUom;
                                sanctionedAmountt += arazQuantity;

                                int currQarterAmount = item.mz_expense_budget_araz_monthly.Where(i => int.Parse(i.month_num.Replace("Month ", "")) >= 1 && int.Parse(i.month_num.Replace("Month ", "")) <= currMonthSwch)
                    .Sum(i => (int)(i.amount * i.quantity));
                                currSanctionAmount += currQarterAmount;

                                tranferedAmount += (item.transferedAmount ?? 0);
                                float? averagePerUnitCost = billItems.Sum(x => x.amountPerPc) / (billItems.Count == 0 ? 1 : billItems.Count);
                                items.Add(
                                    new BudgetArazExpenseItemModel
                                    {
                                        itemName = item.item.name,
                                        arazAmount = arazQuantity,
                                        currQuarterAmount = currQarterAmount,
                                        consumedBudget = item.consumedAmount ?? 0,
                                        arazId = item.id,
                                        itemId = item.itemId,
                                        transferedAmount = item.transferedAmount ?? 0,
                                        uom = item.uom,
                                        quantity = item.quantity,
                                        arazJustification = item.justification,
                                        arazPricePerQuantity = (int)(item.amountPerQuantity ?? 0),
                                        arazQuantity = item.quantity,
                                        averagePerUnitCost = averagePerUnitCost,
                                        billRemarks = billItems.Aggregate(
                                            "",
                                            (current, x) =>
                                                current
                                                + (string.IsNullOrEmpty(current) ? "" : ", ")
                                                + "("
                                                + x.billId
                                                + ") "
                                                + x.remarks
                                        ),
                                        consumedQuantity = billItems.Sum(x => x.quantity),
                                        billItems = billItems,
                                        transferedFromLog = transferFromItem.Aggregate(
                                            "",
                                            (current, ele) =>
                                                current
                                                + (string.IsNullOrEmpty(current) ? "" : ", ")
                                                + ele.fromAraz.deptVenue.deptName
                                                + "_"
                                                + ele.fromAraz.deptVenue.venueName
                                                + "/"
                                                + ele.fromAraz.baseItem.name
                                                + "/"
                                                + ele.fromAraz.item.name
                                                + " -> "
                                                + ele.toAraz.deptVenue.deptName
                                                + "_"
                                                + ele.toAraz.deptVenue.venueName
                                                + "/"
                                                + ele.toAraz.baseItem.name
                                                + "/"
                                                + ele.toAraz.item.name
                                                + "("
                                                + ele.amount
                                                + ")"
                                        ),
                                        transferedToLog = transferToItem.Aggregate(
                                            "",
                                            (current, ele) =>
                                                current
                                                + (string.IsNullOrEmpty(current) ? "" : ", ")
                                                + ele.fromAraz.deptVenue.deptName
                                                + "_"
                                                + ele.fromAraz.deptVenue.venueName
                                                + "/"
                                                + ele.fromAraz.baseItem.name
                                                + "/"
                                                + ele.fromAraz.item.name
                                                + " -> "
                                                + ele.toAraz.deptVenue.deptName
                                                + "_"
                                                + ele.toAraz.deptVenue.venueName
                                                + "/"
                                                + ele.toAraz.baseItem.name
                                                + "/"
                                                + ele.toAraz.item.name
                                                + "("
                                                + ele.amount
                                                + ")"
                                        )
                                    }
                                );
                            }

                            switch (i.baseItemId)
                            {
                                case 1:
                                    totalExpense = await _salaryService.getSalaryExpenseAmount(
                                        j.deptVenueId,
                                        FinancialYear,
                                        "Khidmatguzaar"
                                    );
                                    break;
                                case 51:
                                    totalExpense = await _salaryService.getSalaryExpenseAmount(
                                        j.deptVenueId,
                                        FinancialYear,
                                        "Visiting Faculty"
                                    );
                                    break;
                                case 50:
                                    totalExpense = await _salaryService.getSalaryExpenseAmount(
                                        j.deptVenueId,
                                        FinancialYear,
                                        "Staff"
                                    );
                                    break;
                                default:
                                    totalExpense = (long)arazList.Sum(x => (x.consumedAmount ?? 0f));
                                    break;
                            }

                            //mz_expense_sanctioned_budget baseitem = _context.mz_expense_sanctioned_budget.Where(x => x.baseItemId == i.baseItemId && x.deptVenueId == j.deptVenueId && x.financialYear == FinancialYear).FirstOrDefault();
                            var rds = registration_set.
                                Where(x => x.id == j.psetId).FirstOrDefault();
                            baseitems.Add(
                                new BudgetArazExpenseModel
                                {
                                    id = arazList[0].id,
                                    baseItemId = (i.baseItemId ?? 0),
                                    currQuarterSanctionAmount = currSanctionAmount,
                                    deptVenueId = (j.deptVenueId),
                                    financialYear = financialYear,
                                    totalAmount = sanctionedAmountt,
                                    deptVenueName =
                                    rds.subprogram.name
                                    + "_"
                                        + arazList[0].deptVenue.deptName
                                        + "_"
                                        + arazList[0].deptVenue.venueName,
                                    baseItemName = bi.name,
                                    totalExpense = totalExpense,
                                    transferredAmount = tranferedAmount,
                                    items = items,
                                    classId = rds.subprogram.id
                                }
                            );
                            Debug.WriteLine("This RAN");
                            var data1 = "";
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.ToString());
                        }
                    }
                }

                baseitems = baseitems.OrderBy(x => x.classId).ToList();

                return Ok(baseitems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getSpecificsanctionedBudget/{deptVenueId}/{psetId}/{baseItemId}/{financialYear}/{month}")]
        [HttpGet]
        public async Task<IActionResult> getSpecificSanctionedBudget(
            int deptVenueId,
            int psetId,
            int baseItemId,
            int financialYear,
            int month
        )
        {
            string api = "getSpecificsanctionedBudget/{deptVenueId}/{baseItemId}/{financialYear}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<BudgetArazExpenseModel> baseitems = new List<BudgetArazExpenseModel>();

                DateTime dt = DateTime.Now;
                // DateTime cutofftime = new DateTime(2025, 3, 25);
                // if (dt >= cutofftime && authUser.ItsId != 50423552)
                // {
                //     return BadRequest( new { message = "Bill entry will resume from 2nd April");
                // }

                dept_venue? dv = _context
                    .dept_venue.Where(x => x.id == deptVenueId)
                    .FirstOrDefault();
                mz_expense_procurement_baseitem? bi = _context
                    .mz_expense_procurement_baseitem.Include(x => x.dept_venue_baseitem).Where(x => x.id == baseItemId)
                    .FirstOrDefault();

                if (dv == null || bi == null)
                {
                    return BadRequest(new { message = "Invalid deptVenueId or baseItemId" });
                }

                float totalExpense = 0;

                //totalExpense = _budgetArazService.getBudgetExpenseAmountAll_New_BillEntry(deptVenueId, baseItemId, financialYear);

                // int financialYear2 = _helperService.GetFinancialYear(dt);
                int financialYear2 = financialYear;
                

                List<mz_expense_budget_araz> baseitem = _context
                    .mz_expense_budget_araz.Include(x => x.mz_expense_budget_araz_monthly)
                    .Where(x =>
                        x.baseItemId == baseItemId
                        && x.deptVenueId == deptVenueId
                        && x.psetId == psetId
                        && x.financialYear == financialYear2
                        && x.stage == "Sanctioned"
                    )
                    .ToList();
                //if (baseitem.Count < 1)
                //{
                //    return BadRequest(new { message = "Budget is not sanctioned" });
                //}

                //List<mz_expense_budget_araz_monthly> budgetMonthly = 
                //Debug.WriteLine($"item1: {System.Text.Json.JsonSerializer.Serialize(baseitem[0].mz_expense_budget_araz_monthly.Count)}");
                var monthNum = "";
                int total = 0;
                int quarter = 0;

                foreach (var bItem in baseitem)
                {
                    var monthCheck = month % 3;

                    var start = 0;
                    var end = 0;

                    if (monthCheck == 0)
                    {
                        start = month - 2;
                        end = month;
                    }   else if(monthCheck == 2)
                    {
                        start = month - 1;
                        end = month + 1;
                    }
                    else
                    {
                        start = month;
                        end = month + 2;
                    }

                    quarter = (int)Math.Ceiling((double)end / 3 - 1);

                    if(quarter == 4)
                    {

                    }

                    start = switchMonth(start);
                    end = switchMonth(end);

                    totalExpense = _budgetArazService
                        .getBudgetConsumedAmount(deptVenueId, psetId, baseItemId, financialYear2)
                        .Where(x => x.month >= 1 && x.month <= end).Sum(x => x.amount);

                    //totalExpense = _budgetArazService
                    //.getBudgetConsumedAmount(deptVenueId, psetId, baseItemId, financialYear2)
                    //.Sum(x => x.amount);                    

                    total += bItem.mz_expense_budget_araz_monthly.Where(x =>
                        {
                            int months = int.Parse(x.month_num.Replace("Month ", ""));
                            return months >= 1 && months <= end;
                        }
                        ).Sum(x => (int)x.amount * (int)x.quantity);


                    //foreach (var bMonth in bItem.mz_expense_bud get_araz_monthly)
                    //{

                    //}

                }
                 float totalSanctionedAmount = baseitem.Sum(x =>
                    (x.amountPerUom * x.quantity) + (x.transferedAmount ?? 0)
                );
                bool itemLockState = bi
                    .dept_venue_baseitem.Where(x => x.deptVenueId == deptVenueId)
                    .FirstOrDefault()?.hasItemBlock ?? true;

                baseitems.Add(
                    new BudgetArazExpenseModel
                    {
                        id = baseitem.First().id,
                        baseItemId = baseItemId,
                        deptVenueId = deptVenueId,
                        financialYear = financialYear,
                        totalAmount = total,
                        deptVenueName = dv.deptName + "_" + dv.venueName,
                        baseItemName = bi.name,
                        totalExpense = totalExpense,
                        itemLock = itemLockState,
                        quarter = quarter
                    }
                );

                return Ok(baseitems.First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        public int switchMonth(int month)
        {
            int monthNum = 0;

            switch (month)
            {
                case 4:
                    monthNum = 1;
                    break;
                case 5:
                    monthNum = 2;
                    break;
                case 6:
                    monthNum = 3;
                    break;
                case 7:
                    monthNum = 4;
                    break;
                case 8:
                    monthNum = 5;
                    break;
                case 9:
                    monthNum = 6;
                    break;
                case 10:
                    monthNum = 7;
                    break;
                case 11:
                    monthNum = 8;
                    break;
                case 12:
                    monthNum = 9;
                    break;
                case 1:
                    monthNum = 10;
                    break;
                case 2:
                    monthNum = 11;
                    break;
                case 3:
                    monthNum = 12;
                    break;
            }
            return monthNum;
        }

        [Route("getSanctionedBaseItems/{deptVenueId}/{psetId}/{financialYear}")]
        [HttpGet]
        public async Task<IActionResult> getSanctionedBaseItems(int deptVenueId, int psetId, int financialYear)
        {
            List<mz_expense_budget_araz> baseitem = _context
                    .mz_expense_budget_araz.Where(x =>
                        x.deptVenueId == deptVenueId
                        && x.psetId == psetId
                        && x.financialYear == financialYear
                        && x.stage == "Sanctioned"
                    )
                    .ToList();

            List<mz_expense_procurement_baseitem> bItems = _context.mz_expense_procurement_baseitem.ToList();
            List<BudgetArazExpenseModel> baseitems = new List<BudgetArazExpenseModel>();

            if(baseitem.Count > 0)
            {
                foreach (var bi in baseitem)
                {
                    mz_expense_procurement_baseitem bItem = bItems.Where(x => x.id == bi.baseItemId).FirstOrDefault();

                    if(!baseitems.Any(x => x.baseItemId == bi.baseItemId && x.deptVenueId == deptVenueId)){
                        baseitems.Add(
                        new BudgetArazExpenseModel
                        {
                            id = bi.id,
                            baseItemId = bi.baseItemId,
                            deptVenueId = deptVenueId,
                            financialYear = financialYear,
                            baseItemName = bItem.name
                        }
                    );
                    }
                    
                }
            }            

            return Ok(baseitems);
        }

        [Route(
            "getSpecificsanctionedBudgetItem/{deptVenueId}/{psetId}/{baseItemId}/{itemId}/{financialYear}/{month}"
        )]
        [HttpGet]
        public async Task<IActionResult> getSpecificSanctionedBudgetItem(
            int deptVenueId,
            int psetId,
            int baseItemId,
            int itemId,
            int financialYear,
            int month
        )
        {
            string api =
                "getSpecificsanctionedBudgetitem/{deptVenueId}/{baseItemId}/{financialYear}";
            //// Add_ApiLogs(api);
            DateTime dt = DateTime.Now;
            DateTime cutofftime = new DateTime(2026, 3, 25);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            if (dt >= cutofftime && authUser.ItsId != 50423552)
            {
                return BadRequest(new { message = "Bill entry will resume from 2nd April" });
            }

            try
            {
                List<BudgetArazExpenseModel> baseitems = new List<BudgetArazExpenseModel>();

                int financialYear2 = _helperService.GetFinancialYear(dt);

                dept_venue dv = _context
                    .dept_venue.Where(x => x.id == deptVenueId)
                    .FirstOrDefault();
                mz_expense_procurement_baseitem bi = _context
                    .mz_expense_procurement_baseitem.Where(x => x.id == baseItemId)
                    .FirstOrDefault();

                

                List<mz_expense_budget_araz> baseitem = _context
                    .mz_expense_budget_araz.Include(x => x.mz_expense_budget_araz_monthly).Where(x =>
                        x.baseItemId == baseItemId
                        && x.deptVenueId == deptVenueId
                        && x.psetId == psetId
                        && x.financialYear == financialYear2
                        && x.itemId == itemId
                        && x.stage == "Sanctioned"
                    )
                    .ToList();

                int totalSanctionedAmount = 0;
                var monthNum = "";
                double total = 0;

                float totalExpense = 0;
                foreach (var bItem in baseitem)
                {
                    var monthCheck = month % 3;

                    var start = 0;
                    var end = 0;

                    if (monthCheck == 0)
                    {
                        start = month - 2;
                        end = month;
                    }
                    else if (monthCheck == 2)
                    {
                        start = month - 1;
                        end = month + 1;
                    }
                    else
                    {
                        start = month;
                        end = month + 2;
                    }


                    start = switchMonth(start);
                    end = switchMonth(end);

                    
                    totalExpense = _budgetArazService
                        .getBudgetConsumedAmount(deptVenueId, psetId, baseItemId, financialYear2)
                        .Where(x => x.itemId == itemId &&  x.month >= 1 && x.month <= end).Sum(x => x.amount);


                    totalSanctionedAmount = bItem.mz_expense_budget_araz_monthly.Where(x =>
                    {
                        int months = int.Parse(x.month_num.Replace("Month ", ""));
                        return months >= 1 && months <= end;
                    }
                    ).Sum(x =>((int)x.amount * (int)x.quantity) + (x.transferredAmount));

                }

                //foreach (var items in baseitem)
                //{
                //    totalSanctionedAmount = items.mz_expense_budget_araz_monthly.Sum(x =>
                //        ((int)x.amount * (int)x.quantity) + (x.transferredAmount ?? 0));
                //}


                baseitems.Add(
                    new BudgetArazExpenseModel
                    {
                        id = baseitem.First().id,
                        baseItemId = baseItemId,
                        itemId = itemId,
                        deptVenueId = deptVenueId,
                        financialYear = financialYear,
                        totalAmount = totalSanctionedAmount,
                        deptVenueName = dv.deptName + "_" + dv.venueName,
                        baseItemName = bi.name,
                        totalExpense = totalExpense
                    }
                );

                return Ok(baseitems.First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getItems/{baseItemId}/{deptVenueId}/{psetId}")]
        [HttpGet]
        public async Task<IActionResult> getItems(int baseItemId, int deptVenueId, int psetId)
        {
            string api = "getItems/{baseItemId}/{deptVenueId}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                List<dropdown_dataset_header> baseitems = new List<dropdown_dataset_header>();

                //mz_expense_procurement_baseitem rights = _context.mz_expense_procurement_baseitem.Where(x => x.id == baseItemId).FirstOrDefault();

                global_constant financialYear = _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault();
                int fy = Int32.Parse(financialYear.value);

                List<mz_expense_budget_araz> items = _context
                    .mz_expense_budget_araz.Where(x =>
                        x.deptVenueId == deptVenueId
                        && x.baseItemId == baseItemId
                        && x.psetId == psetId
                        && x.financialYear == fy
                    ).ToList();


                items.ForEach(j =>
                {
                    mz_expense_procurement_item? item = _context
                        .mz_expense_procurement_item.Where(x => x.id == j.itemId)
                        .FirstOrDefault();

                    baseitems.Add(
                        new dropdown_dataset_header
                        {
                            id = item.id,
                            name = item.name + " ( " + item.uom + " )"
                        }
                    );
                });

                //rights.mz_expense_procurement_item.ToList().ForEach(i =>
                //{
                //    baseitems.Add(new dropdown_dataset_header { id = i.id, name = i.name + " ( " + i.uom + " )" });

                //});

                return Ok(baseitems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getItems/{baseItemId}")]
        [HttpGet]
        public async Task<IActionResult> getItems(int baseItemId)
        {
            string api = "getItems/{baseItemId}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                List<dropdown_dataset_header> baseitems = new List<dropdown_dataset_header>();
                mz_expense_procurement_baseitem? rights = _context
                    .mz_expense_procurement_baseitem.Include(x => x.item).Where(x => x.id == baseItemId)
                    .FirstOrDefault();

                rights
                    .item.ToList()
                    .ForEach(i =>
                    {
                        baseitems.Add(
                            new dropdown_dataset_header
                            {
                                id = i.id,
                                name = i.name + " ( " + i.uom + " )"
                            }
                        );
                    });

                return Ok(baseitems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getDataForEstimateIncome/{itsId}")]
        [HttpGet]
        public async Task<IActionResult> getDataForEstimateIncome(int itsId)
        {
            string api = "getDataForEstimateIncome/{itsId}";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<BudgetArazItem> model = new List<BudgetArazItem>();

                List<greg_months> greg_months = _context.greg_months.ToList();

                if (authUser.loginName.Contains("Branch"))
                {
                    DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                    int financialYear = Int32.Parse(
                        _context
                            .global_constant.Where(x => x.key == "budgetFinancialYear")
                            .FirstOrDefault()
                            .value
                    );
                    if (itsId == 500)
                    {
                        itsId = authUser.ItsId;
                    }

                    branch_user user = _context
                        .branch_user.Include(x => x.pset)
                        .Where(x => x.itsId == itsId)
                        .FirstOrDefault();

                    if (user == null)
                    {
                        return BadRequest(new { message = "User not found" });
                    }

                    List<registrationform_dropdown_set> psetRights = user.pset.ToList();

                    List<int> allowedPsets = psetRights.Select(x => x.id).ToList();

                    List<registrationform_dropdown_set> allPsets = _context
                        .registrationform_dropdown_set.Where(x => allowedPsets.Contains(x.id))
                        .Include(x => x.program)
                        .Include(x => x.subprogram)
                        .Include(x => x.venue)
                        .ToList();

                    List<mz_expense_estimate_student> allEstimateStudents = _context
                        .mz_expense_estimate_student.Where(x =>
                            allowedPsets.Contains(x.psetId ?? 0) && x.financialYear == financialYear
                        )
                        .Include(x => x.mz_expense_student_budget_issue_logs)
                        .ToList();

                    List<mz_student_feecategory_pset> feecategory_Psets = _context
                        .mz_student_feecategory_pset.Where(x =>
                            allowedPsets.Contains(x.psetId ?? 0)
                        )
                        .ToList();

                    List<mz_student_feecategory> feecategories =
                        _context.mz_student_feecategory.ToList();

                    foreach (var pr in psetRights)
                    {
                        List<mz_student_feecategory_pset> fc = feecategory_Psets
                            .Where(x => x.psetId == pr.id)
                            .ToList();
                        List<dropdown_dataset_options> programDD =
                            new List<dropdown_dataset_options>();
                        List<dropdown_dataset_options> categoryDD =
                            new List<dropdown_dataset_options>();
                        List<dropdown_dataset_options> amountDD =
                            new List<dropdown_dataset_options>();

                        foreach (var i in fc)
                        {
                            mz_student_feecategory f = feecategories
                                .Where(x => x.id == i.fcId)
                                .FirstOrDefault();

                            registrationform_dropdown_set set = allPsets
                                .Where(x => x.id == i.psetId)
                                .FirstOrDefault();
                            if (set == null)
                            {
                                set = new registrationform_dropdown_set();
                            }
                            ;
                            registrationform_programs p = set.program;
                            if (p == null)
                            {
                                p = new registrationform_programs();
                            }
                            registrationform_subprograms sp = set.subprogram;
                            if (sp == null)
                            {
                                sp = new registrationform_subprograms();
                            }
                            venue v = set.venue;
                            if (v == null)
                            {
                                v = new venue();
                            }

                            mz_expense_estimate_student es = allEstimateStudents
                                .Where(x => x.psetId == i.psetId && x.fcId == i.fcId)
                                .FirstOrDefault();

                            int studentCount = 0;
                            //if (es == null)
                            //{
                            //    es = new mz_expense_estimate_student
                            //    {
                            //        sfcp_id = i.id,
                            //        fcId = i.fcId,
                            //        createdBy = authUser.Name,
                            //        createdOn = DateTime.Now,
                            //        duration = 0,
                            //        feesAmount = i.amount,
                            //        financialYear = financialYear,
                            //        psetId = i.psetId,
                            //        stage = "Initiated",
                            //        studentCountPerMonth = 0,
                            //    };
                            //    _context.mz_expense_estimate_student.Add(es);
                            //    _context.SaveChanges();
                            //}
                            studentCount = es?.studentCountPerMonth ?? 0;

                            BudgetArazItem item = new BudgetArazItem
                            {
                                quantity = studentCount,
                                id = es.id,
                                srno = i.fcId ?? 0,
                                name = f?.categoryName,
                                itemId = i.psetId ?? 0,
                                total = i.amount ?? 0,
                                deptVenueName = p?.name + "_" + sp?.name + "_" + v?.displayName,
                                stage = es.stage,
                                perUnitAmt = es.duration,
                                description = es.remarks,
                                hasIssues = false,
                                isConcerning = false,
                                verified = true,
                                remark = es.remarks_admin,
                                isExpense = false,
                                createdBy = es.createdBy,
                                createdOn = es.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                            };

                            if (es.mz_expense_student_budget_issue_logs.Count > 0)
                            {
                                item.hasIssues = true;
                            }
                            if (es.stage.Contains("Initia"))
                            {
                                item.verified = false;
                                if (es.stage.Contains("concern"))
                                {
                                    item.isConcerning = true;
                                }
                            }
                            model.Add(item);
                        }
                    }
                    model.OrderBy(x => x.isConcerning);

                    return Ok(new { data = model });
                }

                if (authUser.loginName.Contains("Admin") || authUser.loginName.Contains("HR"))
                {
                    int financialYear = Int32.Parse(
                        _context
                            .global_constant.Where(x => x.key == "budgetFinancialYear")
                            .OrderByDescending(x => x.value)
                            .FirstOrDefault()                            
                            .value
                    );
                    if (itsId == 500)
                    {
                        itsId = authUser.ItsId;
                    }
                    List<student_registration_rights> psetRights = _context
                        .student_registration_rights.Where(x => x.itsId == itsId)
                        .ToList();

                    List<mz_student_feecategory_pset> fcps = _context.mz_student_feecategory_pset.ToList();

                    List<mz_student_feecategory> fcs = _context.mz_student_feecategory.ToList();

                    List<registrationform_dropdown_set> rdss = _context.registrationform_dropdown_set.ToList();

                    List<registrationform_subprograms> rfs = _context.registrationform_subprograms.ToList();

                    List<registrationform_programs> rfp = _context.registrationform_programs.ToList();

                    List<venue> venues = _context.venue.ToList();

                    List<mz_expense_estimate_student> esm = _context.mz_expense_estimate_student.Where(x => x.financialYear == financialYear).ToList();
                    List<mz_expense_estimate_student_monthly> estimateMonthly = _context.mz_expense_estimate_student_monthly.ToList();

                    foreach (var pr in psetRights)
                    {
                        List<mz_student_feecategory_pset> fc = fcps.Where(x => x.psetId == pr.programSetId).ToList();
                        List<dropdown_dataset_options> programDD =
                            new List<dropdown_dataset_options>();
                        List<dropdown_dataset_options> categoryDD =
                            new List<dropdown_dataset_options>();
                        List<dropdown_dataset_options> amountDD =
                            new List<dropdown_dataset_options>();

                        foreach (var i in fc)
                        {
                            mz_student_feecategory f = fcs.Where(x => x.id == i.fcId).FirstOrDefault();

                            registrationform_dropdown_set set = rdss.Where(x => x.id == i.psetId)
                                .FirstOrDefault();
                            if (set == null)
                            {
                                set = new registrationform_dropdown_set();
                            }
                            ;
                            registrationform_programs p = rfp.Where(x => x.id == set.programId)
                                .FirstOrDefault();
                            if (p == null)
                            {
                                p = new registrationform_programs();
                            }
                            registrationform_subprograms sp = rfs.Where(x => x.id == set.subprogramId)
                                .FirstOrDefault();
                            if (sp == null)
                            {
                                sp = new registrationform_subprograms();
                            }
                            venue v = venues.Where(x => x.Id == set.venueId)
                                .FirstOrDefault();
                            if (v == null)
                            {
                                v = new venue();
                            }
                            mz_expense_estimate_student es = esm.Where(x =>
                                    x.psetId == i.psetId
                                    && x.fcId == i.fcId
                                    && x.financialYear == financialYear
                                )
                                .FirstOrDefault();
                            int studentCount = 0;

                            if (es == null)
                            {
                                continue;
                                //int dur = 0;

                                //switch (i.frequency)
                                //{
                                //    case "monthly":
                                //        dur = 12;
                                //        break;
                                //    case "quarterly":
                                //        dur = 4;
                                //        break;
                                //    case "halfYearly":
                                //        dur = 2;
                                //        break;
                                //    case "yearly":
                                //        dur = 1;
                                //        break;
                                //    default:
                                //        break;
                                //}

                                //es = new mz_expense_estimate_student
                                //{
                                //    fcId = i.fcId,
                                //    createdBy = authUser.Name,
                                //    createdOn = DateTime.Now,
                                //    duration = dur,
                                //    feesAmount = i.amount,
                                //    financialYear = financialYear,
                                //    psetId = i.psetId,
                                //    stage = "Initiated",
                                //    studentCountPerMonth = 0,
                                //};
                                //_context.mz_expense_estimate_student.Add(es);
                                //_context.SaveChanges();
                            }
                            studentCount = es?.studentCountPerMonth ?? 0;

                            BudgetArazItem item = new BudgetArazItem
                            {
                                quantity = studentCount,
                                id = es.id,
                                srno = i.fcId ?? 0,
                                name = f?.categoryName,
                                frequency = i.frequency,
                                itemId = i.psetId ?? 0,
                                total = i.amount ?? 0,
                                deptVenueName = p?.name + "_" + sp?.name + "_" + v?.displayName,
                                stage = es.stage,
                                perUnitAmt = es.duration,
                                description = es.remarks,
                                hasIssues = false,
                                isConcerning = false,
                                verified = true,
                                remark = es.remarks_admin,
                                isExpense = false,
                                createdBy = es.createdBy,
                                createdOn = es.createdOn?.ToString("dd/MM/yyyy hh:mm tt"),
                            };

                            if (es.mz_expense_student_budget_issue_logs.Count > 0)
                            {
                                item.hasIssues = true;
                            }
                            if (es.stage.Contains("Initia"))
                            {
                                item.verified = false;
                                if (es.stage.Contains("concern"))
                                {
                                    item.isConcerning = true;
                                }
                            }

                            List<mz_expense_estimate_student_monthly> monthlyEstimate = estimateMonthly.Where(x => x.estimate_student_id == i.id).ToList();

                            foreach (var month in monthlyEstimate)
                            {
                                if(month.estimate_student_id == i.id)
                                {
                                    var greg_month = greg_months.Where(x => x.month_name == month.month).FirstOrDefault();

                                    item.incomeMonths.Add(new studentFeesMonthly
                                    {
                                        month = greg_month.month_name,
                                        student_count = month.student_count,
                                        fees_per_student = month.fees_per_student
                                    });
                                }
                                
                            }

                            model.Add(item);
                        }
                    }
                }
                return Ok(new { data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getdeptvenuebaseitemtruerightforbudgetaraz/expense/{itsId}/{deptVenueId}")]
        [HttpGet]
        public async Task<IActionResult> getDeptVenueBaseItemTrueRight_withBudget(
            int itsId,
            int deptVenueId
        )
        {
            string api = "getdeptvenuebaseitemtruerightforbudgetaraz/expense/{itsId}/{deptVenueId}";
            //// Add_ApiLogs(api);
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            List<dropdown_dataset_options> baseItemDD = new List<dropdown_dataset_options>();
            List<dropdown_dataset_options> deptVenueDD = new List<dropdown_dataset_options>();
            List<dropdown_dataset_options> itemDD = new List<dropdown_dataset_options>();

            global_constant fy = _context
                .global_constant.Where(x => x.key == "budgetFinancialYear")
                .OrderByDescending(x => x.value)
                .FirstOrDefault();
            if (fy == null)
            {
                fy = new global_constant { key = "budgetFinancialYear", value = "2024" };
            }
            if (itsId == 500)
            {
                itsId = authUser.ItsId;
            }

            var data = _budgetArazService.getDeptVenueBaseItemTrueRightForBudgetAraz_for_User(
                itsId,
                deptVenueId,
                Int32.Parse(fy.value)
            );

            baseItemDD = data.OrderBy(x => x.baseItemName)
                .GroupBy(x => x.baseItemName)
                .Select(x => new dropdown_dataset_options
                {
                    name = x.FirstOrDefault().baseItemName?.ToString()
                })
                .ToList();
            deptVenueDD = data.OrderBy(x => x.deptVenueName)
                .GroupBy(x => x.deptVenueName)
                .Select(x => new dropdown_dataset_options
                {
                    name = x.FirstOrDefault().deptVenueName?.ToString()
                })
                .ToList();
            itemDD = data.OrderBy(x => x.name)
                .GroupBy(x => x.name)
                .Select(x => new dropdown_dataset_options
                {
                    name = x.FirstOrDefault().name?.ToString()
                })
                .ToList();

            return Ok(
                new
                {
                    data = data,
                    baseItemDD = baseItemDD,
                    deptVenueDD = deptVenueDD,
                    itemDD = itemDD
                }
            );
        }

        [Route("getbudgetarazsummary/user")]
        [HttpGet]
        public async Task<IActionResult> getBudgetArazSummaryUser()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);
            var response = _budgetArazService.getBudgetArazSummary(authUser, false);
            return Ok(response);
        }

        [Route("getbudgetarazsummary/admin")]
        [HttpGet]
        public async Task<IActionResult> getBudgetArazSummaryAdmin()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            string api = "getbudgetarazsummamy";
            var response = _budgetArazService.getBudgetArazSummary(authUser, true);
            return Ok(response);
        }

        [Route("getbudgetarazsummaryexport/user")]
        [HttpGet]
        public async Task<IActionResult> getBudgetArazSummaryExportUser()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);
            ;
            return Ok(_budgetArazService.getBudgetArazSummaryExport(authUser, false));
        }

        [Route("verifyarazitem/toaudit")]
        [HttpPost]
        public async Task<IActionResult> verifyarazitem(BudgetArazItem arazItem)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            string api = "getbudgetarazsummamy";
            List<BudgetArazItem> arazItems = new List<BudgetArazItem>();
            arazItems.Add(arazItem);
            //// Add_ApiLogs(api);
            var response = _budgetArazService.verifyBudgetArazItem(authUser, arazItems);
            return Ok(new {message = response});
        }

        public struct arazItemVarify
        {
            public List<BudgetArazItem> arazItems { get; set; }
        };

        [Route("verifyarazitems")]
        [HttpPost]
        public async Task<IActionResult> verifyArazItems(arazItemVarify budgetVarification)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);
            ///

            return Ok(new {message = _budgetArazService.verifyBudgetArazItem(authUser, budgetVarification.arazItems) });
        }

        [Route("santionbudgetIds")]
        [HttpPost]
        public async Task<IActionResult> santionbudgetIds(budgetIds ids)
        {
            var budgetids = _context.mz_expense_budget_araz.Where(x => x.stage == "Audited").ToList();
            try
            {
                foreach (var id in ids.ids)
                {
                    var budgets = budgetids.Where(x => x.venueId == id).ToList();
                    if (budgets != null)
                    {
                        foreach (var budget in budgets)
                        {
                            if (budget != null && budget.stage == "Audited")
                            {
                                budget.stage = "Sanctioned";
                            }
                        }
                    }
                }

                _context.SaveChanges();
                return Ok(new { message = "Budget Sanctioned" });

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message.ToString() });
            }

        }

        [Route("santionbudget")]
        [HttpGet]
        public async Task<IActionResult> santionBudget()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);

            int financialYear = Int32.Parse(
                _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault()
                    .value
            );
            List<mz_expense_budget_araz> araz = _context
                    .mz_expense_budget_araz.Where(x => x.financialYear == financialYear)
                    .ToList();
            List<venue> venues = _context.venue.Where(x => x.ActiveStatus.Value).ToList();
            List<dept_venue> dvs = _context.dept_venue.ToList();

            List<string> error = new List<string>();
            List<string> success = new List<string>();
            foreach (var venue in venues)
            {
                dvs = dvs.Where(x => x.venueId == venue.Id).ToList();

                var dat = (from dv in _context.dept_venue
                           where dv.venueId == venue.Id
                           join arz in _context.mz_expense_budget_araz on dv.id equals arz.deptVenueId into gj
                           from sub in gj.DefaultIfEmpty()
                           where sub != null && sub.financialYear == financialYear
                           select sub
                           ).ToList();

                
                if(dat.Count > 0)
                {
                    if (dat.Any(x => x.stage != "Audited"))
                    {
                        string name = venue.CampVenue;

                        error.Add(name);
                    }
                    else
                    {
                        dat.ForEach(x => x.stage = "Sanctioned");
                        _context.SaveChanges();

                        string name = venue.CampVenue;
                        success.Add(name);
                    }
                }
                

                //foreach(var dv in dvs)
                //{


                //    if (araz.Any(x => x.stage != "Audited"))
                //    {
                //        return BadRequest(new { message = "Some araz are still in process cant proceed with budget sanction" });
                //    }

                //    araz.ForEach(x => x.stage = "Sanctioned");
                //    _context.SaveChanges();
                //}

            }

            Debug.WriteLine($"Success {success.Count}: {System.Text.Json.JsonSerializer.Serialize(success)}");
            Debug.WriteLine($"Error {error.Count}: {System.Text.Json.JsonSerializer.Serialize(error)}");


            if (success.Count > 0)
            {
                return Ok(new { message = "Budget Sanctioned for " + System.Text.Json.JsonSerializer.Serialize(success) });
            }
            else
            {
                return BadRequest(new { message = "Cannot sanction the budget as not all the items have been Audited." });
            }   

        }

        [Route("arazitemissues")]
        [HttpPost]
        public async Task<IActionResult> arazItemIssues(BudgetArazItem budgetVarification)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);
            string res = "";
            try
            {
                if (budgetVarification.isExpense)
                {
                    res = _budgetArazService.budgetArazItemIssue(
                        authUser,
                        budgetVarification,
                        false
                    );
                }
                else
                {
                    res = _budgetArazService.budgetArazIncomeItemIssue(
                        authUser,
                        budgetVarification,
                        false
                    );
                }

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("arazitemissues/admin")]
        [HttpPost]
        public async Task<IActionResult> arazItemIssuesByAdmin(BudgetArazItem budgetVarification)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);
            string res = "";
            try
            {
                if (budgetVarification.isExpense)
                {
                    res = _budgetArazService.budgetArazItemIssue(
                        authUser,
                        budgetVarification,
                        true
                    );
                }
                else
                {
                    res = _budgetArazService.budgetArazIncomeItemIssue(
                        authUser,
                        budgetVarification,
                        true
                    );
                }

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("updatearazItem")]
        [HttpPost]
        public async Task<IActionResult> updateArazItem(BudgetArazItem budgetVarification)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);
            string res = "";

            try
            {
                res = _budgetArazService.updateExpenseBudgetAraz(authUser, budgetVarification);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("budgetitemissuelogs/{arazId}/{isExpense}")]
        [HttpGet]
        public async Task<IActionResult> budgetItemIssues(int arazId, bool isExpense)
        {
            List<Dictionary<String, String>> issues = new List<Dictionary<string, string>>();

            string api = "budgetitemissuelogs/{arazId}";
            //// Add_ApiLogs(api);

            if (isExpense)
            {
                List<mz_expense_budget_issue_logs> logs = _context
                    .mz_expense_budget_issue_logs.Where(x => x.budgetArazId == arazId)
                    .ToList();
                logs = logs.OrderByDescending(x => x.createdOn).ToList();
                int c = 0;
                foreach (mz_expense_budget_issue_logs log in logs)
                {
                    c++;
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    temp.Add("srno", c.ToString());
                    temp.Add("remark", log.remark);
                    temp.Add("user", log.createdBy);
                    temp.Add("isConcerning", log.isConcerning.ToString());
                    temp.Add("date", log.createdOn?.ToString("dd/M/yyyy hh:mm tt"));
                    Dictionary<string, string> t2 = JsonConvert.DeserializeObject<
                        Dictionary<string, string>
                    >(log.arazState);
                    foreach (KeyValuePair<string, string> t1 in t2)
                    {
                        temp.Add(t1.Key, t1.Value);
                    }

                    issues.Add(temp);
                }
            }
            else
            {
                List<mz_expense_student_budget_issue_logs> logs = _context
                    .mz_expense_student_budget_issue_logs.Where(x => x.estimateStudentId == arazId)
                    .ToList();
                logs = logs.OrderByDescending(x => x.createdOn).ToList();
                int c = 0;
                foreach (mz_expense_student_budget_issue_logs log in logs)
                {
                    c++;
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    temp.Add("srno", c.ToString());
                    temp.Add("remark", log.remark);
                    temp.Add("user", log.createdBy);
                    temp.Add("isConcerning", log.isConcerning.ToString());
                    temp.Add("date", log.createdOn?.ToString("dd/M/yyyy hh:mm tt"));
                    Dictionary<string, string> t2 = JsonConvert.DeserializeObject<
                        Dictionary<string, string>
                    >(log.arazState);
                    foreach (KeyValuePair<string, string> t1 in t2)
                    {
                        temp.Add(t1.Key, t1.Value);
                    }

                    issues.Add(temp);
                }
            }

            return Ok(issues);
        }

        [Route("getbudgetarazsummaryexport/admin")]
        [HttpGet]
        public async Task<IActionResult> getBudgetArazSummaryExportAdmin()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);

            return Ok(_budgetArazService.getBudgetArazSummaryExport(authUser, true));
        }

        [Route("getbudgetarazdropdowns")]
        [HttpGet]
        public async Task<IActionResult> getBudgetArazDropdowns()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            user user = _context.user.FirstOrDefault(x => x.ItsId == authUser.ItsId);

            DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

            string api = "getbudgetarazsummamy";
            //// Add_ApiLogs(api);

            int financialYear = Int32.Parse(
                    _context
                        .global_constant.Where(x => x.key == "budgetFinancialYear")
                        .FirstOrDefault()
                        .value
            );

            List<BudgetArazDeptVenue> deptvenue = new List<BudgetArazDeptVenue>();

            List<mz_expense_budget_araz> budget = _context
                    .mz_expense_budget_araz.Where(x => x.financialYear == financialYear)
                    //.Take(100)
                    .ToList();

            List<registrationform_dropdown_set> rds = _context.registrationform_dropdown_set.AsNoTracking().ToList();

            List<registrationform_subprograms> rsb = _context.registrationform_subprograms.AsNoTracking().OrderByDescending(x => x.id).ToList();

            List<dept_venue> depts = _context.dept_venue.AsNoTracking().Include(x => x.venue).ToList();

            List<mz_expense_procurement_baseitem> baseitems = _context.mz_expense_procurement_baseitem.AsNoTracking().Include(x => x.item).ToList();

            
            //try
            //{

            var rdsDict = rds.ToDictionary(x => x.id);
            var rsbDict = rsb.ToDictionary(x => x.id);
            var deptDict = depts.ToDictionary(x => x.id);
            var baseItemDict = baseitems.ToDictionary(x => x.id);

            var budgetLookup = budget
                .Select(x => (x.deptVenueId, x.psetId, x.baseItemId, x.itemId))
                .ToHashSet();

            if (user.roleId == 4)
            {
                List<dept_venue_baseitem> dvbs = _context.dept_venue_baseitem.AsNoTracking().ToList();
                foreach (var dvb in dvbs)
                {
                    if (!deptDict.TryGetValue(dvb.deptVenueId ?? 0, out var dept))
                        continue;

                    if (!rdsDict.TryGetValue(dvb.psetId ?? 0, out var rd))
                        continue;

                    if (!rsbDict.TryGetValue(rd.subprogramId ?? 0, out var rs))
                        continue;

                    if (!baseItemDict.TryGetValue(dvb.baseItemId ?? 0, out var baseIt))
                        continue;

                    if (dvb == null)
                        continue;

                    //var dept = depts.FirstOrDefault(x => x.id == dvb.deptVenueId);
                    if (dept == null)
                        continue;

                    //var rd = rds.FirstOrDefault(x => x.id == dvb.psetId);
                    if (rd == null)
                        continue;

                    //var rs = rsb.FirstOrDefault(x => x.id == rd.subprogramId);
                    if (rs == null)
                        continue;

                    // 🔹 CHECK IF DEPT ALREADY EXISTS
                    var d = deptvenue.FirstOrDefault(x => x.pset == rd.id);

                    if (d == null)
                    {
                        d = new BudgetArazDeptVenue
                        {
                            id = dept.id,
                            name = dept.deptName + "_" + dept.venue.CampVenue + "_" + rs.name,
                            pset = rd.id,
                            venueId = dept.venueId ?? 0,
                            classId = rs.id,
                            expenseHeads = new List<BudgetArazExpenseHead>()
                        };

                        deptvenue.Add(d); // ✅ ADD ONLY ONCE
                    }

                    //var baseIt = baseitems.FirstOrDefault(x => x.id == dvb.baseItemId);
                    if (baseIt == null)
                        continue;

                    // 🔹 CHECK IF EXPENSE HEAD ALREADY EXISTS
                    var e = d.expenseHeads.FirstOrDefault(x => x.id == baseIt.id);

                    if (e == null)
                    {
                        e = new BudgetArazExpenseHead
                        {
                            id = baseIt.id,
                            name = baseIt.name,
                            items = new List<BudgetArazItem>()
                        };

                        d.expenseHeads.Add(e);
                    }

                    foreach (var z in baseIt.item)
                    {
                        // 🔹 PREVENT DUPLICATE ITEMS
                        bool itemAlreadyExists = e.items.Any(i => i.itemId == z.id);

                        bool alreadyBudgeted = budget.Any(l =>                            
                            l.psetId == dvb.psetId &&
                            l.baseItemId == dvb.baseItemId &&
                            l.itemId == z.id
                        );

                        if (!itemAlreadyExists && !alreadyBudgeted)
                        {
                            e.items.Add(new BudgetArazItem
                            {
                                itemId = z.id,
                                name = z.name + " (" + z.uom + ")"
                            });
                        }
                    }
                }
            }
            else
            {
                List<user_dept_venue_baseitem> udv = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();

                List<user_deptvenue> udvn = _context.user_deptvenue.Where(x => x.itsId == authUser.ItsId).ToList();

                
                    
                //baseitems = baseitems.Where(x => udv.Any(y => y.baseItemId == x.id)).ToList();

                //List<mz_expense_procurement_item_baseitem> items = _context.mz_expense_pro;

                foreach (var ud in udvn)
                {
                    var dpts = depts.FirstOrDefault(x => x.id == ud.deptVenueId);
                    if (dpts != null)
                    {
                        BudgetArazDeptVenue d = new BudgetArazDeptVenue();
                        d.expenseHeads = new List<BudgetArazExpenseHead>();
                        var rd = rds.FirstOrDefault(x => x.id == ud.psetId);

                        if (rsb != null)
                        {
                            var rs = rsb.FirstOrDefault(x => x.id == rd.subprogramId);

                            if (rs != null)
                            {
                                d.id = dpts.id;
                                d.name = dpts.deptName + "_" + dpts.venue.CampVenue + "_" + rs.name;
                                d.pset = rd.id;
                                d.venueId = dpts.venueId ?? 0;
                                d.classId = rs.id;



                                var udv1 = udv.Where(x => x.dept_venueId == ud.deptVenueId && x.psetId == ud.psetId);
                                foreach (var uds in udv1)
                                {
                                    var baseIt = baseitems.FirstOrDefault(x => x.id == uds.baseItemId);
                                    if (baseIt != null)
                                    {
                                        //System.Diagnostics.Debug.WriteLine($"!baseIt Name - : {baseIt.name}");

                                        BudgetArazExpenseHead e = new BudgetArazExpenseHead();
                                        e.items = new List<BudgetArazItem>();

                                        baseIt.item.ToList().ForEach(z =>
                                        {
                                            if (
                                                !budget.Any(l =>
                                                    l.deptVenueId == uds.dept_venueId
                                                    && l.psetId == uds.psetId
                                                    && l.baseItemId == uds.baseItemId
                                                    && l.itemId == z.id
                                                )
                                            )
                                            {
                                                BudgetArazItem item = new BudgetArazItem
                                                {
                                                    itemId = z.id,
                                                    name = z.name + " (" + z.uom + ")",
                                                };
                                                e.items.Add(item);
                                            }
                                        });

                                        //foreach (var itm in items1)
                                        //{                                            
                                        //    BudgetArazItem item = new BudgetArazItem
                                        //    {
                                        //        itemId = itm.id,
                                        //        name = itm.name,
                                        //    };

                                        //    e.items.Add(item);
                                        //}

                                        if (e.items.Count > 0)
                                        {
                                            e.id = baseIt.id;
                                            e.name = baseIt.name;
                                            d.expenseHeads.Add(e);
                                        }
                                    }
                                }
                                deptvenue.Add(d);
                            }
                        }
                    }
                }
            }


            deptvenue = deptvenue.OrderBy(x => x.classId).ToList();
            return Ok(deptvenue);
        }

        [Route("submit/budgetaraz")]
        [HttpPost]
        public async Task<IActionResult> submit_BudgetAraz(BudgetRequestModel model)
        {
            string api = "submit/budgetaraz";

            System.Diagnostics.Debug.WriteLine($"Model Count: {model.month.Count()}");
           
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                global_constant fy = _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault() ?? new global_constant { key = "budgetFinancialYear", value = "2024" };
                int financialYear = Int32.Parse(fy.value);
                mz_expense_budget_araz? u1 = _context
                    .mz_expense_budget_araz.Where(x =>
                        x.baseItemId == model.Expense.baseItemId
                        && x.deptVenueId == model.Expense.deptVenueId
                        && x.psetId == model.Expense.psetId
                        && x.financialYear == financialYear
                        && x.itemId == model.Expense.itemId
                    )
                    .FirstOrDefault();
                System.Diagnostics.Debug.WriteLine($"This is u1: {u1}");

                if (u1 == null)
                {

                    //var totalAmount = model.month.Sum(x => x.amount);
                    //var monthFilled = model.month.Where(x => x.amount != 0 && x.amount != null).Count();
                    var avgAmount = model.Expense.totalAmount / model.Expense.quantity;
                    System.Diagnostics.Debug.WriteLine($"This Ran");
                    mz_expense_procurement_item? item = _context
                        .mz_expense_procurement_item.Where(x => x.id == model.Expense.itemId)
                        .FirstOrDefault();
                    mz_expense_budget_araz u = new mz_expense_budget_araz
                    {
                        baseItemId = model.Expense.baseItemId,
                        deptVenueId = model.Expense.deptVenueId,
                        venueId = model.Expense.venueId,
                        psetId = model.Expense.psetId,
                        amountPerUom = model.Expense.totalAmount,
                        amountPerQuantity = avgAmount,
                        financialYear = financialYear,
                        createdOn = DateTime.Now,
                        createdBy = authUser.Name,
                        itemId = model.Expense.itemId,
                        justification = model.Expense.justification ?? "",
                        quantity = model.Expense.quantity,
                        uom = item?.uom ?? "",
                        stage = "Initiated",
                        transferedAmount = 0,
                        consumedQty = 0,
                        consumedAmount = 0
                    };

                    _context.mz_expense_budget_araz.Add(u);
                    _context.SaveChanges();

                    List<greg_months> gregMonths = _context.greg_months.ToList();

                    mz_expense_budget_araz_monthly dat = new mz_expense_budget_araz_monthly();
                    foreach (var item1 in model.month)
                    {
                        var greg_month = gregMonths.Where(x => x.month_name == item1.month).FirstOrDefault();

                        var expensemonth = new mz_expense_budget_araz_monthly
                        {
                            budget_araz_id = u.id,
                            deptVenueId = model.Expense.deptVenueId,
                            baseItemId = model.Expense.baseItemId,
                            itemId = model.Expense.itemId,
                            status = "Initiated",
                            created_on = DateTime.Now,
                            month_num = greg_month.slug,
                            amount = item1.amount ?? 0,
                            quantity = item1.quantity ?? 0
                        };
                        _context.mz_expense_budget_araz_monthly.Add(expensemonth);
                    }
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { message = ex.Message });
                        //return new Exception("Error saving monthly budget records: " + ex.Message);
                    }
                }
                else
                {
                    return BadRequest(new { message = "Already entered" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("updateVenueId")]
        [HttpGet]
        public async Task<IActionResult> updateVenueId()
        {
            var bdgts = _context.mz_expense_budget_araz.Where(x => x.venueId == null).ToList();
            var dept = _context.dept_venue.ToList();

            foreach (var bgt in bdgts)
            {
                var vId = dept.Where(x => x.id == bgt.deptVenueId).FirstOrDefault();
                bgt.venueId = vId.venueId;
            }
            _context.SaveChanges();

            return Ok();
        }

        [Route("getauditedvenues")]
        [HttpGet]
        public async Task<IActionResult> getauditedvenues()
        {
            var bdgts = _context.mz_expense_budget_araz.ToList();
            var venues = _context.venue.ToList();
            var res = new List<object>();
            string status = "";

            foreach (var venue in venues)
            {
                var bdgtSts = bdgts.Where(x => x.venueId == venue.Id).ToList();
                if (bdgtSts.Count > 0)
                {
                    var intialize = bdgtSts.Any(x => x.stage == "Initiated");


                    if (!intialize)
                    {
                        var audit = bdgtSts.Any(x => x.stage == "Audited");
                        if (audit)
                        {
                            var auditCount = bdgtSts.Count(x => x.stage == "Audited");

                            if (bdgtSts.Count == auditCount)
                            {
                                status = "Audited";
                            }
                            else
                            {
                                status = "Partly Audited";
                            }
                        }
                        else
                        {
                            var sanction = bdgtSts.Any(x => x.stage == "Sanctioned");
                            if (sanction)
                            {
                                var sanctionCount = bdgtSts.Count(x => x.stage == "Sanctioned");

                                if (bdgtSts.Count == sanctionCount)
                                {
                                    status = "Sanctioned";
                                }
                                else
                                {
                                    status = "Not Audited";
                                }
                            }
                        }
                    }
                    else
                    {
                        status = "Not Audited";
                    }

                }
                else
                {
                    status = "No Budget";
                }

                res.Add(new
                {
                    id = venue.Id,
                    name = venue.CampVenue,
                    status = status
                });

                //foreach (var dat in bdgtSts)
                //{
                //    res.Add(new
                //    {
                //        id = venue.Id,
                //        name = venue.CampVenue,
                //        status = dat.stage
                //    });
                //}



                //var vId = venues.Where(x => x.Id == bgt.Select(x => x.venueId).FirstOrDefault();
                //bgt.venueId = vId.venueId;
            }
            //_context.SaveChanges();

            return Ok(res);
        }

        [Route("submit/budgetarazsanctioned")]
        [HttpPost]
        public async Task<IActionResult> submit_BudgetArazSanctioned(BudgetArazExpenseModel model)
        {
            string api = "submit/budgetarazsanctioned";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                mz_expense_sanctioned_budget u1 = _context
                    .mz_expense_sanctioned_budget.Where(x =>
                        x.baseItemId == model.baseItemId
                        && x.deptVenueId == model.deptVenueId
                        && x.financialYear == FinancialYear
                    )
                    .FirstOrDefault();

                if (u1 == null)
                {
                    mz_expense_procurement_item item = _context
                        .mz_expense_procurement_item.Where(x => x.id == model.itemId)
                        .FirstOrDefault();
                    mz_expense_sanctioned_budget u = new mz_expense_sanctioned_budget
                    {
                        baseItemId = model.baseItemId,
                        deptVenueId = model.deptVenueId,
                        financialYear = model.financialYear,
                        sanctioned_amount = model.totalAmount,
                        updatedBy = authUser.Name,
                        updatedOn = indianTime
                    };

                    _context.mz_expense_sanctioned_budget.Add(u);
                }
                else
                {
                    u1.sanctioned_amount = model.totalAmount;
                    u1.updatedOn = indianTime;
                    u1.updatedBy = authUser.Name;
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("submit/estimateincome")]
        [HttpPost]
        public async Task<IActionResult> EstimateIncome(BudgetArazItem models)
        {
            string api = "submit/EstimateIncome";

            Debug.WriteLine(models.incomeMonths);
            
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                List<greg_months> greg_months = _context.greg_months.ToList();

                mz_expense_estimate_student? es = _context
                    .mz_expense_estimate_student.Where(x => x.id == models.id)
                    .FirstOrDefault();

                mz_student_feecategory_pset d = _context.mz_student_feecategory_pset.Where(x => x.fcId == es.fcId && x.psetId == es.psetId).FirstOrDefault();

                if (d != null)
                {
                    d.frequency = models.frequency;
                }
                _context.SaveChanges();

                int dur = 0;

                switch (models.frequency)
                {
                    case "monthly":
                        dur = 12;
                        break;
                    case "quarterly":
                        dur = 4;
                        break;
                    case "halfYearly":
                        dur = 2;
                        break;
                    case "yearly":
                        dur = 1;
                        break;
                    default:
                        break;
                }

                global_constant fy = _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault() ?? new global_constant { key = "budgetFinancialYear", value = "2024" };
                int financialYear = Int32.Parse(fy.value);
                if (es == null)
                {

                    var u = new mz_expense_estimate_student
                    {
                        feesAmount = models.total,
                        financialYear = financialYear,
                        psetId = models.itemId,
                        studentCountPerMonth = models.quantity,
                        createdBy = authUser.Name,
                        createdOn = indianTime,
                        duration = dur,
                    };
                    _context.mz_expense_estimate_student.Add(u);
                    _context.SaveChanges();
                }
                else
                {
                    es.studentCountPerMonth = models.quantity;
                    es.duration = dur;
                    es.remarks = models.description;
                    es.feesAmount = models.total;
                }

                List<mz_expense_estimate_student_monthly>? estimateMonthly = _context.mz_expense_estimate_student_monthly.Where(x => x.estimate_student_id == d.id).ToList();

                foreach (var esm in estimateMonthly)
                {
                    foreach (var item1 in models.incomeMonths)
                    {
                        var greg_month = greg_months.Where(x => x.month_name == item1.month).FirstOrDefault();
                        if (esm.month == greg_month.month_name)
                        {
                            esm.student_count = item1.student_count;
                            esm.fees_per_student = item1.fees_per_student;
                        }
                    }
                }

                List<mz_student_feecategory_pset_monthly>? studentFeeMonthly = _context.mz_student_feecategory_pset_monthly.Where(x => x.student_feecategory_pset_id == d.id).ToList();

                if(studentFeeMonthly.Count > 0)
                {
                    foreach (var sfm in studentFeeMonthly)
                    {
                        foreach (var item2 in models.incomeMonths)
                        {
                            var greg_month = greg_months.Where(x => x.month_name == item2.month).FirstOrDefault();

                            if (sfm.month == greg_month.slug)
                            {
                                sfm.student_count = item2.student_count;
                                sfm.fees_per_student = item2.fees_per_student;
                            }
                        }
                    }
                }
                
                _context.SaveChanges();
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("submit/budgetarazbyAdmin")]
        [HttpPost]
        public async Task<IActionResult> submit_BudgetAraz_ByAdmin(
            List<BudgetArazExpenseModel> model
        )
        {
            string api = "submit/budgetarazbyAdmin";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                global_constant fy = _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault();
                int financialYear = Int32.Parse(fy.value);
                if (model.Count > 50)
                {
                    return BadRequest(new { message = "max length is 50" });
                }

                foreach (var i in model)
                {
                    mz_expense_budget_araz u1 = _context
                        .mz_expense_budget_araz.Where(x =>
                            x.baseItemId == i.baseItemId
                            && x.deptVenueId == i.deptVenueId
                            && x.financialYear == FinancialYear
                            && x.itemId == i.itemId
                        )
                        .FirstOrDefault();

                    if (u1 != null)
                    {
                        u1.updatedOn = DateTime.Now;
                        u1.updatedBy = authUser.Name;
                        u1.remarks_admin = i.remarks_admin;
                    }
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("itemwisespends")]
        [HttpPost]
        public async Task<IActionResult> itemwisespends(filterItemwisespendReport filter)
        {
            string api = "itemwisespends";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);
                DateOnly fromDate = DateOnly.FromDateTime(
                    filter.billDates.FromDate ?? DateTime.MinValue
                );
                DateOnly toDate = DateOnly.FromDateTime(
                    filter.billDates.ToDate ?? DateTime.MinValue
                );
                List<BillManagementModel> model = new List<BillManagementModel>();

                List<user_dept_venue_baseitem> udvbi = _context
                    .user_dept_venue_baseitem.Where(x => x.itsId == authUser.ItsId)
                    .ToList();
                List<mz_expense_bill_master> allBills = _context
                    .mz_expense_bill_master.Where(x => x.status == "Paid" || x.status == "Approved")
                    .ToList();
                if (filter.billDates?.FromDate != null && filter.billDates?.ToDate != null)
                {
                    allBills = allBills
                        .Where(x => x.billDate > fromDate && x.billDate < toDate)
                        .ToList();
                }
                List<dept_venue> allDV = _context.dept_venue.ToList();
                if (filter.deptVenueIds.Count > 0)
                {
                    allDV = allDV.Where(x => filter.deptVenueIds.Contains(x.id)).ToList();
                }
                List<mz_expense_vendor_master> allVendors =
                    _context.mz_expense_vendor_master.ToList();
                List<mz_expense_procurement_baseitem> allBaseItems =
                    _context.mz_expense_procurement_baseitem.ToList();

                if (filter.baseItemIds.Count > 0)
                {
                    allBaseItems = allBaseItems
                        .Where(x => filter.baseItemIds.Contains(x.id))
                        .ToList();
                }

                List<BillItemListModel> allItems = _context
                    .mz_expense_bill_item.Join(
                        _context.mz_expense_procurement_item,
                        bi => bi.itemId,
                        pi => pi.id,
                        (bi, pi) =>
                            new BillItemListModel
                            {
                                itemId = pi.id,
                                itemName = pi.name,
                                remark = bi.remarks,
                                billId = bi.billId,
                                amountPerPc = bi.amountPerPc ?? 0,
                                quantity = bi.quantity ?? 0,
                                total = (bi.amountPerPc * bi.quantity) ?? 0
                            }
                    )
                    .ToList();

                if (filter.itemIds.Count > 0)
                {
                    allItems = allItems.Where(x => filter.itemIds.Contains(x.itemId)).ToList();
                }
                List<mz_expense_vendor_transaction> vendorTransactions =
                    _context.mz_expense_vendor_transaction.ToList();
                List<mz_expense_vendor_payment> vendorPayments =
                    _context.mz_expense_vendor_payment.ToList();
                udvbi = udvbi
                    .Where(x =>
                        allDV.Any(y => y.id == x.dept_venueId)
                        && allBaseItems.Any(y => y.id == x.baseItemId)
                    )
                    .ToList();
                foreach (var j in udvbi)
                {
                    List<mz_expense_bill_master> bills = allBills
                        .Where(x => x.deptVenueId == j.dept_venueId && x.baseItemId == j.baseItemId)
                        .ToList();

                    foreach (mz_expense_bill_master i in bills)
                    {
                        mz_expense_procurement_baseitem item = allBaseItems
                            .Where(x => x.id == i.baseItemId)
                            .FirstOrDefault();
                        dept_venue dv = allDV.Where(x => x.id == i.deptVenueId).FirstOrDefault();
                        mz_expense_vendor_master vendor = allVendors
                            .Where(x => x.id == i.vendorId)
                            .FirstOrDefault();
                        int itsid = Convert.ToInt32(i.createdBy);
                        user u = _context.user.Where(x => x.ItsId == itsid).FirstOrDefault();

                        var items = allItems.Where(x => x.billId == i.id).ToList();

                        mz_expense_vendor_transaction payment = vendorTransactions
                            .Where(x => x.billId == i.id && x.debit != null && x.paymentId != null)
                            .FirstOrDefault();
                        BillManagementModel m = new BillManagementModel();

                        foreach (var k in items)
                        {
                            if (payment != null)
                            {
                                mz_expense_vendor_payment actualPayment = vendorPayments
                                    .Where(x => x.id == payment.paymentId)
                                    .FirstOrDefault();
                                m.paymentDate = actualPayment.paymentDate ?? dateOnly;
                                m.paymentDateString = (
                                    actualPayment.paymentDate ?? dateOnly
                                ).ToString("dd-MM-yyyy");
                                m.txnId = payment.transactionId;
                            }

                            m.billDateString = (i.billDate ?? dateOnly).ToString("dd-MM-yyyy");
                            m.entryDateString = (i.createdOn ?? DateTime.Today).ToString(
                                "dd-MM-yyyy"
                            );
                            m.remarks = k.remark;
                            m.itemName = k.itemName;
                            m.isFundRequestedString =
                                (i.isFundRequested ?? false) ? "Requested" : "Not Requested";
                            m.isFundRequested = i.isFundRequested ?? false;
                            m.id = i.id;
                            m.baseItemId = i.baseItemId ?? 0;
                            m.billDate = i.billDate ?? dateOnly;
                            m.billNumber = i.billNo;
                            m.deptVenueId = i.deptVenueId ?? 0;
                            m.totalAmount = (int)k.total;
                            m.vendorId = i.vendorId ?? 0;
                            m.baseItemName = item?.name;
                            m.deptVenueName = dv.deptName + "_" + dv.venueName;
                            m.deptName = dv.deptName;
                            m.venueName = dv.venueName;
                            m.vendorName = vendor.name;
                            m.createdOn = i.createdOn ?? DateTime.Now;
                            m.createdBy = u?.Username;
                            m.paymentTo_AccName = i.paymentTo_AccName;
                            m.paymentTo_AccNum = i.paymentTo_AccNum;
                            m.paymentTo_BankName = i.paymentTo_BankName;
                            m.paymentTo_ifsc = i.paymentTo_ifsc;
                            m.isWaived = i.isWaived ?? false;
                            m.paymentMode_User = i.paymentMode_User;
                            m.status = i.status;
                            m.paymentMode_Admin = i.paymentMode_Admin;
                            m.gstAmount = i?.gstAmount;
                            m.gstPercentage = i.gstPercentage ?? 0;
                            m.isReconciled = i.isReconciled ?? false;
                            m.tdsAmount = i?.tdsAmount;
                            m.tdsApplicableAmount = i?.tdsApplicableAmount;
                            m.tdsPercentage = i?.tdsPercentage;
                            m.conveyanceAmount = i?.conveyanceAmount;
                            m.amountPerUom = k.amountPerPc;
                            m.quantity = k.quantity;
                            m.itemId = k.itemId;
                            model.Add(m);
                        }
                    }
                }
                model = model.OrderBy(x => x.id).ToList();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("transferlog")]
        [HttpPost]
        public async Task<IActionResult> BudgetTranferLog(BudgetTransferLogModel model)
        {
            string api = "transferlog";
            string step = "Step 0";
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                int financialYear = _helperService.GetFinancialYear(DateTime.Today);
                DateTime today = DateTime.Now;

                step = "Step 1";

                // Retrieve the "From" araz item
                mz_expense_budget_araz? fromAraz = await _context
                    .mz_expense_budget_araz
                    .Where(x =>
                        x.deptVenueId == model.from_deptvenueid &&
                        x.baseItemId == model.from_baseitem &&
                        x.itemId == model.from_itemid &&
                        x.stage == "Sanctioned" &&
                        x.financialYear == financialYear)
                    .FirstOrDefaultAsync();

                if (fromAraz == null)
                {
                    return BadRequest(new { message = "From item has not been sanctioned" });
                }

                if (((fromAraz.amountPerUom * fromAraz.quantity) - fromAraz.consumedAmount) +
                    (fromAraz.transferedAmount ?? 0) < model.Amount)
                {
                    return BadRequest(new { message = "Sufficient amount not available in selected From Item" });
                }

                if (fromAraz.transferedAmount != null)
                {
                    fromAraz.transferedAmount -= model.Amount;
                }
                else
                {
                    fromAraz.transferedAmount = 0 - model.Amount;
                }

                step = "Step 2";

                // Retrieve or create the "To" araz item
                mz_expense_budget_araz? toAraz = await _context
                    .mz_expense_budget_araz
                    .Where(x =>
                        x.deptVenueId == model.to_deptvenueid &&
                        x.baseItemId == model.to_baseitem &&
                        x.itemId == model.to_itemid &&
                        x.financialYear == financialYear)
                    .FirstOrDefaultAsync();

                if (toAraz == null)
                {
                    mz_expense_procurement_item? itemEntity = await _context
                        .mz_expense_procurement_item
                        .Where(x => x.id == model.to_itemid)
                        .FirstOrDefaultAsync();

                    toAraz = new mz_expense_budget_araz
                    {
                        deptVenueId = model.to_deptvenueid,
                        baseItemId = model.to_baseitem,
                        itemId = model.to_itemid,
                        quantity = model.to_quantity ?? 1,
                        amountPerUom = 0,
                        transferedAmount = model.Amount,
                        justification = "created using budget transfer",
                        createdBy = authUser.Name,
                        consumedAmount = 0,
                        consumedQty = 0,
                        createdOn = today,
                        stage = "Sanctioned",
                        financialYear = financialYear,
                        uom = itemEntity?.uom,
                    };
                    await _context.mz_expense_budget_araz.AddAsync(toAraz);

                    // Ensure the 'toAraz' gets saved to generate the ID
                    await _context.SaveChangesAsync();

                    step = "Step 3";
                }
                else if (toAraz.stage != "Sanctioned")
                {
                    toAraz.stage = "Sanctioned";
                    toAraz.updatedBy = authUser.Name;
                    toAraz.updatedOn = DateTime.Now;
                    toAraz.quantity = model.to_quantity ?? 1;
                    toAraz.amountPerUom = 0;
                    toAraz.transferedAmount = model.Amount;
                    toAraz.remarks_admin += " Sanctioned from budget transfer";

                    // Save the updated 'toAraz'
                    await _context.SaveChangesAsync();

                    step = "Step 4";
                }
                else
                {
                    toAraz.updatedBy = authUser.Name;
                    toAraz.updatedOn = DateTime.Now;
                    if (toAraz.amountPerUom == 0)
                    {
                        toAraz.quantity += model.to_quantity ?? 1;
                    }
                    toAraz.transferedAmount += model.Amount;

                    // Save the updated 'toAraz'
                    await _context.SaveChangesAsync();

                    step = "Step 5";
                }

                // Serialize the model for logging purposes
                string modelToString = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                step = "Step 6";

                // Create the transfer log
                mz_expense_budget_araz_transfer_logs tranferLog = new mz_expense_budget_araz_transfer_logs
                {
                    createdBy = authUser.Name,
                    createdOn = today,
                    fromArazId = fromAraz.id,
                    toArazId = toAraz.id,  // Ensure 'toArazId' is valid after saving the 'toAraz'
                    amount = model.Amount,
                    remarks = model.remarks,
                    trabferModel = modelToString
                };

                await _context.mz_expense_budget_araz_transfer_logs.AddAsync(tranferLog);

                // Save the transfer log
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString() + "error at -" + step);
            }
        }

        [Route("transferlog")]
        [HttpGet]
        public async Task<IActionResult> getBudgetTranferLogs()
        {
            string api = "transferlog";
            // Add_ApiLogs(api);
            string step = "Step 0";
            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                int financialYear = _helperService.GetFinancialYear(DateTime.Today);
                DateTime today = DateTime.Now;

                List<mz_expense_budget_araz_transfer_logs> budgetLogs = _context
                    .mz_expense_budget_araz_transfer_logs.Include(x => x.fromAraz)
                    .ThenInclude(x => x.deptVenue)
                    .Include(x => x.fromAraz)
                    .ThenInclude(x => x.baseItem)
                    .Include(x => x.fromAraz)
                    .ThenInclude(x => x.item)
                    .Include(x => x.toAraz)
                    .ThenInclude(x => x.deptVenue)
                    .Include(x => x.toAraz)
                    .ThenInclude(x => x.baseItem)
                    .Include(x => x.toAraz)
                    .ThenInclude(x => x.item)
                    .ToList();
                List<BudgetTransferLogModel> budgetLogsModel = new List<BudgetTransferLogModel>();

                foreach (mz_expense_budget_araz_transfer_logs log in budgetLogs)
                {
                    BudgetTransferLogModel temp = new BudgetTransferLogModel();

                    temp.Amount = log.amount;
                    temp.CreatedBy = log.createdBy;
                    temp.CreatedOn = log.createdOn;
                    temp.from_baseitem = log.fromAraz.baseItemId;
                    temp.from_deptvenueid = log.fromAraz.deptVenueId;
                    temp.from_itemid = log.fromAraz.itemId;
                    temp.from_baseitemName = log.fromAraz.baseItem.name;
                    temp.from_deptvenueName =
                        log.fromAraz.deptVenue.deptName + "_" + log.fromAraz.deptVenue.venueName;
                    temp.from_itemName = log.fromAraz.item.name;
                    temp.to_baseitem = log.toAraz.baseItemId;
                    temp.to_deptvenueid = log.toAraz.deptVenueId;
                    temp.to_itemid = log.toAraz.itemId;
                    temp.to_baseitemName = log.toAraz.baseItem.name;
                    temp.to_deptvenueName =
                        log.toAraz.deptVenue.deptName + "_" + log.toAraz.deptVenue.venueName;
                    temp.to_itemName = log.toAraz.item.name;
                    temp.remarks = log.remarks;

                    budgetLogsModel.Add(temp);
                }

                return Ok(budgetLogsModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString() + "error at -" + step);
            }
            //catch (EntityNotFoundException)
            //{
            //    return Conflict();
            //}
        }
        [Route("rectifyAmountsInBudgetArazTable")]
        [HttpGet]
        public async Task<IActionResult> rectifyAmountsInBudgetArazTable()
        {
            var budgetAraz = _context.mz_expense_budget_araz.ToList();
            var BudgetArazMonthly = _context.mz_expense_budget_araz_monthly.ToList();

            foreach (var budget in budgetAraz)
            {
                var totalQuantity = BudgetArazMonthly.Where(x => x.budget_araz_id == budget.id).Sum(x => x.quantity);
                var totalAmount = BudgetArazMonthly.Where(x => x.budget_araz_id == budget.id).Sum(x => x.amount * x.quantity);

                if(budget.amountPerUom != totalAmount) {
                    budget.amountPerUom = (float)totalAmount;
                    budget.quantity = (int)totalQuantity;
                    budget.amountPerQuantity = totalQuantity > 0
                        ? (float)totalAmount / (float)totalQuantity
                        : 0;
                }
                _context.SaveChanges();

                //_context.mz_expense_budget_araz.Add(budget);
            }


            return Ok();
        }
    }

    public class filterItemwisespendReport
    {
        public DateRange billDates { get; set; }
        public List<int> itemIds { get; set; }
        public List<int> baseItemIds { get; set; }
        public List<int> deptVenueIds { get; set; }
    }

    public class budgetIds
    {
        public List<int> ids { get; set; } = new List<int>();
    }
}
