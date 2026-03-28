using Abp.Webhooks;
using AutoMapper;
using Castle.MicroKernel.Registration;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static mahadalzahrawebapi.Controllers.ItemController;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public ItemController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
        }

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById(
            "Asia/Kolkata"
        );
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Route("getallbaseitem")]
        [HttpGet]
        public async Task<IActionResult> getNewAllBaseItem()
        {
            string api = "getallbaseitem";
            //// Add_ApiLogs(api);

            try
            {
                List<expenseHead> eh = new List<expenseHead>();
                List<mz_expense_procurement_baseitem> items = _context
                    .mz_expense_procurement_baseitem.OrderBy(x => x.name)
                    .ToList();
                items.ForEach(x =>
                {
                    eh.Add(
                        new expenseHead
                        {
                            id = x.id,
                            isCapital = x.isCapital,
                            name = x.name,
                            status = x.status ?? false,
                        }
                    );
                });
                return Ok(eh);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("getallitem")]
        [HttpGet]
        public async Task<IActionResult> getNewAllItem()
        {
            string api = "getallitem";
            try
            {
                List<mz_expense_procurement_item> items = _context
                    .mz_expense_procurement_item.OrderBy(x => x.name)
                    .ToList();
                List<item> i = new List<item>();
                items.ForEach(x =>
                {
                    i.Add(
                        new item
                        {
                            id = x.id,
                            name = x.name,
                            type = x.type,
                            uom = x.uom,
                        }
                    );
                });
                return Ok(i);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("activebaseitem/{id}")]
        [HttpGet]
        public async Task<ActionResult> activebaseitem(int id)
        {
            string api = "activebaseitem/{id}";
            //// Add_ApiLogs(api);

            try
            {
                mz_expense_procurement_baseitem i = _context
                    .mz_expense_procurement_baseitem.Where(x => x.id == id)
                    .FirstOrDefault();

                i.status = true;
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("INactivebaseitem/{id}")]
        [HttpGet]
        public async Task<ActionResult> INactivebaseitem(int id)
        {
            string api = "INactivebaseitem/{id}";
            //// Add_ApiLogs(api);
            {
                try
                {
                    mz_expense_procurement_baseitem i = _context
                        .mz_expense_procurement_baseitem.Where(x => x.id == id)
                        .FirstOrDefault();

                    i.status = false;
                    List<dept_venue_baseitem> dvbi = _context
                        .dept_venue_baseitem.Where(x => x.baseItemId == id)
                        .ToList();
                    List<user_dept_venue_baseitem> udvbi = _context
                        .user_dept_venue_baseitem.Where(x => x.baseItemId == id)
                        .ToList();

                    foreach (var j in dvbi)
                    {
                        _context.dept_venue_baseitem.Remove(j);
                    }
                    foreach (var j in udvbi)
                    {
                        _context.user_dept_venue_baseitem.Remove(j);
                    }

                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        [Route("deptvenuebaseitemmapping/{deptvenueid}/{baseitemId}")]
        [HttpGet]
        public async Task<ActionResult> deptvenuebaseitemmapping(int deptvenueid, int baseitemId)
        {
            string api = "deptvenuebaseitemmapping/{deptvenueid}/{baseitemId}";
            //// Add_ApiLogs(api);

            try
            {
                mz_expense_procurement_baseitem i1 = _context
                    .mz_expense_procurement_baseitem.Where(x => x.id == baseitemId)
                    .FirstOrDefault();
                dept_venue i2 = _context
                    .dept_venue.Where(x => x.id == deptvenueid)
                    .FirstOrDefault();

                if (i1 != null && i2 != null)
                {
                    dept_venue_baseitem i = new dept_venue_baseitem
                    {
                        baseItemId = baseitemId,
                        deptVenueId = deptvenueid
                    };
                    _context.dept_venue_baseitem.Add(i);
                    _context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("getitembaseitem")]
        [HttpGet]
        public async Task<ActionResult> getNewItemBaseItem()
        {
            string api = "getitembaseitem";
            //// Add_ApiLogs(api);

            List<BillManagementModel> models = new List<BillManagementModel>();

            List<mz_expense_procurement_baseitem> bi = _context
                .mz_expense_procurement_baseitem.Include(x => x.item)
                .ToList();
            bi.ForEach(x =>
            {
                x.item.ToList()
                    .ForEach(y =>
                    {
                        models.Add(
                            new BillManagementModel
                            {
                                itemName = y.name,
                                baseItemName = x.name,
                                itemId = y.id,
                                baseItemId = x.id
                            }
                        );
                    });
            });

            return Ok(models);
        }

        [Route("additem_baseitem")]
        [HttpPost]
        public async Task<ActionResult> addItem_BaseItem(BillManagementModel item)
        {
            string api = "additem_baseitem";
            try
            {
                mz_expense_procurement_baseitem bi = _context
                    .mz_expense_procurement_baseitem.Where(x => x.id == item.baseItemId)
                    .Include(x => x.item)
                    .FirstOrDefault();
                mz_expense_procurement_item i = _context
                    .mz_expense_procurement_item.Where(x => x.id == item.itemId)
                    .FirstOrDefault();

                //mz_expense_procurement_item_baseitem i_bi = _context.mz_expense_procurement_item_baseitem.Where(x => x.itemId == item.itemId && x.baseItemId == item.baseItemId).FirstOrDefault();

                if (!bi.item.Contains(i))
                {
                    _context.Database.ExecuteSqlRaw("INSERT INTO mz_expense_procurement_item_baseitem (itemId, baseItemId) VALUES ({0}, {1})", item.itemId, item.baseItemId);
                    //bi.item.Add(i);
                    //_context.SaveChanges();
                }
                else
                {
                    return BadRequest(new { message = "this ITEM is already added in the base item " + bi.name });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("addbaseitem")]
        [HttpPost]
        public async Task<ActionResult> addBaseItem(BillManagementModel item)
        {
            string api = "addbaseitem";
            try
            {
                if (item.itemName == "New")
                {
                    _context.mz_expense_procurement_baseitem.Add(
                        new mz_expense_procurement_baseitem
                        {
                            name = item.baseItemName,
                            isCapital = item.isCapex ?? false
                        }
                    );

                    List<dept_venue> dv = _context.dept_venue.ToList();

                    int BudgetFinancialYear = _globalConstants.currentBudgetYear;
                    foreach (var i in dv)
                    {
                        dept_venue_baseitem dvbi = _context
                            .dept_venue_baseitem.Where(x =>
                                x.deptVenueId == i.id && x.baseItemId == item.baseItemId
                            )
                            .FirstOrDefault();

                        if (dvbi == null)
                        {
                            _context.dept_venue_baseitem.Add(
                                new dept_venue_baseitem
                                {
                                    baseItemId = item.baseItemId,
                                    deptVenueId = i.id
                                }
                            );

                            mz_expense_sanctioned_budget b = _context
                                .mz_expense_sanctioned_budget.Where(x =>
                                    x.baseItemId == item.baseItemId
                                    && x.deptVenueId == i.id
                                    && x.financialYear == BudgetFinancialYear
                                )
                                .FirstOrDefault();
                            if (b == null)
                            {
                                mz_expense_sanctioned_budget b1 = new mz_expense_sanctioned_budget
                                {
                                    baseItemId = item.baseItemId,
                                    deptVenueId = i.id,
                                    sanctioned_amount = 0,
                                    user_arazAmount = 0,
                                    admin_arazAmount = 0,
                                    financialYear = BudgetFinancialYear,
                                    updatedOn = indianTime,
                                    updatedBy = "System"
                                };
                                _context.mz_expense_sanctioned_budget.Add(b1);
                            }
                        }
                    }
                }
                else
                {
                    mz_expense_procurement_baseitem bi = _context
                        .mz_expense_procurement_baseitem.Where(x => x.id == item.baseItemId)
                        .FirstOrDefault();

                    bi.name = item.baseItemName;
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("additem")]
        [HttpPost]
        public async Task<ActionResult> addItem(BillManagementModel item)
        {
            string api = "additem";
            //// Add_ApiLogs(api);


            try
            {
                if (item.baseItemName == "New")
                {
                    _context.mz_expense_procurement_item.Add(
                        new mz_expense_procurement_item { name = item.itemName, uom = item.remarks }
                    );
                }
                else
                {
                    mz_expense_procurement_item bi = _context
                        .mz_expense_procurement_item.Where(x => x.id == item.itemId)
                        .FirstOrDefault();

                    bi.name = item.itemName;
                    bi.uom = item.remarks;
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Exception" });
            }
        }

        //[Route("api/getUserAccess")]
        //[HttpGet]
        //public async Task<ActionResult> GetUserAccess()
        //{
        //    string api = "api/getUserAccess";
        //    //// Add_ApiLogs(api);

        //    AuthUser authUser = ServiceFactory.GetAuthService().GetAuthUser(HttpContext.Current.User);
        //    if (authUser.AccessLevel == "Admin" || authUser.ItsId == 50409944) { return Ok(true); }
        //    else { return Ok(false); }

        //}

        [Route("old/getallbaseitem")]
        [HttpGet]
        public async Task<ActionResult> getAllBaseItem()
        {
            string api = "getallbaseitem";
            //// Add_ApiLogs(api);

            List<DeptVenueModel> l = (
                from dv in _context.dept_venue
                where dv.status == "active"
                join dvbi2 in (
                    from dvbi in _context.dept_venue_baseitem
                    join bi in _context.mz_expense_procurement_baseitem
                        on dvbi.baseItemId equals bi.id
                    select new BaseItemModel
                    {
                        deptId = dvbi.deptVenueId,
                        id = bi.id,
                        name = bi.name,
                        status = bi.status ?? false,
                        deptMappingId = dvbi.id
                    }
                )
                    on dv.id equals dvbi2.deptId
                    into big
                select new DeptVenueModel
                {
                    id = dv.id,
                    name = dv.deptName + "/" + dv.venueName,
                    baseItems = big
                }
            ).ToList();

            return Ok(l);
        }

        // ***** Get All Item List *************
        [Route("items/{itsId}/{deptVenueId}")]
        [HttpGet]
        public async Task<ActionResult> GetAllItems(int itsId, int deptVenueId)
        {
            string api = "api/items/{itsId}/{deptVenueId}";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<ItemRequisitionModel> items = new List<ItemRequisitionModel>();

                if (deptVenueId == 500)
                {
                    List<user_deptvenue> udv = _context
                        .user_deptvenue.Where(x => x.itsId == itsId)
                        .ToList();
                    List<user_dept_venue_baseitem> udvbi = new List<user_dept_venue_baseitem>();
                    List<mz_expense_procurement_baseitem> bi =
                        new List<mz_expense_procurement_baseitem>();
                    List<DeptVenueRightModel> dvrmodel = new List<DeptVenueRightModel>();

                    foreach (var i in udv)
                    {
                        List<user_dept_venue_baseitem> ud = _context
                            .user_dept_venue_baseitem.Where(x =>
                                x.itsId == itsId && x.dept_venueId == i.deptVenueId
                            )
                            .ToList();
                        foreach (var j in ud)
                        {
                            udvbi.Add(j);
                        }
                    }

                    foreach (var i in udvbi)
                    {
                        bi.Add(
                            _context
                                .mz_expense_procurement_baseitem.Where(x => x.id == i.baseItemId)
                                .Include(x => x.item)
                                .FirstOrDefault()
                        );

                        dept_venue dvx = _context
                            .dept_venue.Where(x => x.id == i.dept_venueId)
                            .FirstOrDefault();
                        dvrmodel.Add(
                            new DeptVenueRightModel
                            {
                                id = dvx.id,
                                deptId = dvx.deptId,
                                venueId = dvx.venueId,
                                deptName = dvx.deptName,
                                venueName = dvx.venueName,
                                right = true
                            }
                        );
                    }

                    bi.ForEach(x =>
                    {
                        x.item.ToList()
                            .ForEach(y =>
                            {
                                items.Add(
                                    new ItemRequisitionModel
                                    {
                                        baseItemName = x.name,
                                        itemName = y.name,
                                        baseItemId = x.id,
                                        type = y.type,
                                        description = "",
                                        uom = y.uom,
                                        status = (x.status ?? false) ? "active" : "inactive",
                                        deptVenue = dvrmodel,
                                        itemId = y.id
                                    }
                                );
                            });
                    });
                }
                else
                {
                    List<user_dept_venue_baseitem> udvbi = _context
                        .user_dept_venue_baseitem.Where(x =>
                            x.itsId == itsId && x.dept_venueId == deptVenueId
                        )
                        .ToList();

                    foreach (var i in udvbi)
                    {
                        List<mz_expense_procurement_baseitem> it = _context
                            .mz_expense_procurement_baseitem.Where(x => x.id == i.baseItemId)
                            .Include(x => x.item)
                            .ToList();

                        it.ForEach(x =>
                        {
                            x.item.ToList()
                                .ForEach(y =>
                                {
                                    items.Add(
                                        new ItemRequisitionModel
                                        {
                                            baseItemName = x.name,
                                            itemName = y.name,
                                            baseItemId = x.id,
                                            type = y.type,
                                            description = "",
                                            uom = y.uom,
                                            status = (x.status ?? false) ? "active" : "inactive",
                                            deptVenue = null,
                                            itemId = y.id
                                        }
                                    );
                                });
                        });
                    }
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("deleteitembaseitemmapping/{baseitemId}/{itemId}")]
        [HttpDelete]
        public async Task<ActionResult> deleteItemBaseItemmapping(int baseitemId, int itemId)
        {
            string api = "deleteitembaseitemmapping/{baseitemId}/{itemId}";
            //// Add_ApiLogs(api);


            try
            {
                mz_expense_procurement_baseitem bi = _context
                    .mz_expense_procurement_baseitem.Where(x => x.id == baseitemId)
                    .Include(x => x.item)
                    .FirstOrDefault();
                mz_expense_procurement_item i = _context
                    .mz_expense_procurement_item.Where(x => x.id == itemId)
                    .FirstOrDefault();
                bi.item.Remove(i);
                _context.SaveChanges();

                return Ok("succesfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Exception" });
            }
        }

        // HusainKH
        [Route("deleteuserbaseitemmapping/{baseItemId}")]
        //[HttpDelete]
        [HttpGet]
        public async Task<ActionResult> deleteuserbaseitemmapping(int baseItemId)
        {
            try
            {
                user_dept_venue_baseitem udb = _context.user_dept_venue_baseitem.FirstOrDefault(x => x.id == baseItemId);
                //System.Diagnostics.Debug.WriteLine($"UDB: {System.Text.Json.JsonSerializer.Serialize(udb)}");

                if (udb != null)
                {
                    _context.user_dept_venue_baseitem.Remove(udb);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("No record found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("getbaseitemsforvenue/{deptVenueId}")]
        [HttpGet]
        public async Task<ActionResult> getItemBaseItem(int deptVenueId)
        {
            string api = "getitembaseitem";
            //// Add_ApiLogs(api);

            List<BillManagementModel> models = new List<BillManagementModel>();

            List<mz_expense_procurement_baseitem> bi = _context
                .mz_expense_procurement_baseitem.Include(x => x.item)
                .ToList();
            bi.ForEach(x =>
            {
                x.item.ToList()
                    .ForEach(y =>
                    {
                        models.Add(
                            new BillManagementModel
                            {
                                id = 0,
                                itemName = y.name,
                                baseItemName = x.name,
                            }
                        );
                    });
            });

            return Ok(models);
        }

        [Route("getalldeptvenuebaseitem")]
        [HttpGet]
        public async Task<ActionResult> getAllDeptVenueBaseItem()
        {
            string api = "getalldeptvenuebaseitem";

            try
            {
                //List<DeptVenueModel> deptVenueModels = _context
                //    .dept_venue.Include(x => x.dept_venue_baseitem)
                //    .ThenInclude(x => x.baseItem)
                //    .Include(x => x.registrationform_dropdown_set)
                //    .Where(x => x.status == "active")
                //    .Select(x => new DeptVenueModel
                //    {
                //        id = x.id,
                //        name = x.venueName + "/" + x.deptName + "/" + x.,
                //        baseItems = x.dept_venue_baseitem.Select(y => new BaseItemModel
                //        {
                //            id = y.baseItemId ?? 0,
                //            name = y.baseItem.name,
                //            status = y.baseItem.status ?? false,
                //        })
                //    })
                //    .ToList();

                var deptVenueModels = from deptVenue in _context.dept_venue
                                                       join deptVenueBaseitem in _context.dept_venue_baseitem on deptVenue.id equals deptVenueBaseitem.deptVenueId
                                                       join baseitems in _context.mz_expense_procurement_baseitem on deptVenueBaseitem.baseItemId equals baseitems.id
                                                       join rds in _context.registrationform_dropdown_set on deptVenue.id equals rds.deptVenueId
                                                       join sbs in _context.registrationform_subprograms on rds.subprogramId equals sbs.id
                                                       where deptVenue.status == "active"
                                                        group baseitems by new
                                                        {
                                                            deptVenue.id,
                                                            deptVenue.venueName,
                                                            deptVenue.deptName,
                                                            SubProgramName = sbs.name
                                                        } into g
                                                        select
                                                        new DeptVenueModel
                                                       {
                                                           id = g.Key.id,
                                                           name = g.Key.venueName + "/" + g.Key.deptName + "/" + g.Key.SubProgramName,
                                                            baseItems = g.Select(b => new BaseItemModel
                                                            {
                                                                id = b.id,
                                                                name = b.name,
                                                                status = b.status ?? false
                                                            }).ToList()

                                                        };

                //new BaseItemModel
                //{
                //    id = deptVenueBaseitem.baseItemId ?? 0,
                //    name = baseitems.name,
                //    status = baseitems.status ?? false,
                //}

                var deptVenues = deptVenueModels.ToList();
                return Ok(deptVenues);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("adddeptvenuebaseitem")]
        [HttpPost]
        public async Task<ActionResult> adddeptvenuebaseitem(BaseItemModel baseItem)
        {
            string api = "adddeptvenuebaseitem";
            //// Add_ApiLogs(api);

            try
            {
                List<int> biIds = _helperService.parseIds(baseItem.name);

                List<dept_venue_baseitem> l = (
                    from dvbi in _context.dept_venue_baseitem
                    where
                        dvbi.deptVenueId == baseItem.deptId
                        && dvbi.psetId == baseItem.psetId 
                        && dvbi.baseItemId != null
                        && biIds.Contains(dvbi.baseItemId ?? 0)
                    select dvbi
                ).ToList();
                if (l.Count > 0)
                {
                    throw new Exception(
                        "one or more baseItems are already tagged with the given expense head. only add new base items to the expense head"
                    );
                }

                foreach (int bid in biIds)
                {
                    _context.dept_venue_baseitem.Add(
                        new dept_venue_baseitem
                        {
                            deptVenueId = baseItem.deptId ?? 0,
                            baseItemId = bid,
                            psetId = baseItem.psetId
                        }
                    );
                }

                _context.SaveChanges();

                return Ok("Successfully added baseitems to expense head");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("deletemapping/deptvenue/{dpvId}")]
        [HttpDelete]
        public async Task<ActionResult> removeDeptVenueMapping(int dpvId)
        {
            string api = "deletemapping/deptvenue/{dpvId}";
            //// Add_ApiLogs(api);

            try
            {
                List<dept_venue_baseitem> l = _context
                    .dept_venue_baseitem.Where(x => x.deptVenueId == dpvId)
                    .ToList();

                List<user_dept_venue_baseitem> toDelete = _context
                    .user_dept_venue_baseitem.Where(x => x.dept_venueId == dpvId)
                    .ToList();

                foreach (dept_venue_baseitem j in l)
                {
                    _context.dept_venue_baseitem.Remove(j);
                }

                foreach (user_dept_venue_baseitem i in toDelete)
                {
                    _context.user_dept_venue_baseitem.Remove(i);
                }
                _context.SaveChanges();

                return Ok("Successfully deleted all department baseitem mapping");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("deletemapping/baseitem/{mapId}")]
        [HttpDelete]
        public async Task<ActionResult> removeBaseItemMapping(int mapId)
        {
            string api = "deletemapping/baseitem/{mapId}";
            //// Add_ApiLogs(api);

            try
            {


                dept_venue_baseitem? d = _context
                    .dept_venue_baseitem.Where(x => x.id == mapId)
                    .FirstOrDefault();

                List<user_dept_venue_baseitem> toDelete = _context.user_dept_venue_baseitem
                    .Where(x => x.baseItemId == d.baseItemId && x.dept_venueId == d.deptVenueId)
                    .ToList();

                foreach (user_dept_venue_baseitem i in toDelete)
                {
                    _context.user_dept_venue_baseitem.Remove(i);
                }
                _context.dept_venue_baseitem.Remove(d);
                _context.SaveChanges();
                return Ok("Successfully deleted department baseitem");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[AcceptVerbs("GET")]
        [Route("deleteitems/{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteItem(int id)
        {
            return Ok("Deleted");
        }

        [Route("getuserdept")]
        [HttpGet]

        public async Task<IActionResult> getuserdept()
        {
            List<user_deptvenue> user_depts = _context.user_deptvenue.ToList();
            List<user> users = _context.user.ToList();
            List<dept_venue> dept_Venues = _context.dept_venue.ToList();
            List<registrationform_dropdown_set> rds = _context.registrationform_dropdown_set.ToList();
            List<registrationform_subprograms> rdsp = _context.registrationform_subprograms.ToList();

            try
            {
                var users_data = new List<userDatanewDto>();

                foreach (var usr in users)
                {
                    var user_data = new userDatanewDto
                    {
                        itsId = usr.ItsId,
                        Username = usr.Username,
                        schools = new List<schools>()
                    };

                    var users_details = user_depts.Where(x => x.itsId == usr.ItsId);
                    foreach (var user_det in users_details)
                    {
                        var dept = dept_Venues.FirstOrDefault(x => x.id == user_det.deptVenueId);

                            var registration_details = rds.FirstOrDefault(x => x.deptVenueId == dept.id);
                            var classDetails = rdsp.FirstOrDefault(x => x.id == registration_details.subprogramId);

                            var school = new schools
                            {
                                deptId = dept.id,
                                schoolName = dept.venueName,
                                section = dept.deptName,
                                classId = classDetails.id,
                                className = classDetails.name,
                                psetId = registration_details.id,
                                deptName = dept.venueName + "_" + dept.deptName + "_" + classDetails.name
                            };

                            user_data.schools.Add(school);
                    }
                    users_data.Add(user_data);
                }
                return Ok(users_data);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"Error: {e}" });
            }
        }
        public class userDataDto
        {
            public int? itsId { get; set; }
            public string? Username { get; set; }
            public int? its_count { get; set; }
            public int? dept_venueId { get; set; }
            public int? baseItemId { get; set; }
            public string? deptName { get; set; }
            public string? venueName { get; set; }
            public string? baseItem { get; set; }

            public string? deptVenueName { get; set; }
            public int? count_deptvenue { get; set; }
        }

        public class userDatanewDto
        {
            public int? itsId { get; set; }
            public string? Username { get; set; }
            public string? roleName { get; set; }
            public List<schools> schools { get; set; }
            public List<baseItems>? baseItems { get; set; }
            public List<itemData>? items { get; set; }
        }

        public class schools
        {
            public int id { get; set; }
            public int deptId { get; set; }
            public string schoolName { get; set; }
            public string section { get; set; }
            public string deptName { get; set; }
            public int classId {get;set; }
            public int psetId { get; set; }
            public string className { get; set; }
            public List<baseItems> baseitems { get; set; }

        }

        public class baseItems
        {
            public int bId { get; set; }
            public string bName { get; set; }
            //public List<itemData> items { get; set; }
        }

        public class itemData
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        [Route("getitemusers")]
        [HttpGet]
        public IActionResult getitemusers()
        {
            var result = new List<userDataDto>();
            var userbaseitem = _context.user_dept_venue_baseitem.ToList();

            var allusers = _context.user.ToList();
            var alldepts = _context.dept_venue.ToList();

            //return Ok(userbaseitem);
            foreach (var item in userbaseitem)
            {
                var user = allusers.FirstOrDefault(x => x.ItsId == item.itsId);
                var dept = alldepts.FirstOrDefault(x => x.id == item.dept_venueId);
                //return Ok(user.ItsId);
                if (user != null && dept != null)
                {
                    if(!result.Any(x => x.itsId == item.itsId && x.deptName == dept.deptName + "_" + dept.venueName))
                    result.Add(new userDataDto
                    {
                        itsId = item.itsId,
                        Username = user.Username,
                        deptName = dept.deptName + "_" + dept.venueName
                    });
                }
            }
            return Ok(result);
        }

        [Route("getallitemassociation")]
        [HttpGet]
        public IActionResult getAllItemAssociation()
        {
            var result = new List<userDatanewDto>();

            List<user> users = _context.user.ToList();

            List<role> roles = _context.role.ToList();

            List<dept_venue> dept_venues = _context.dept_venue.Include(x => x.venue).ToList();

            List<registrationform_dropdown_set> incomeDetails = _context.registrationform_dropdown_set.ToList();

            List<registrationform_subprograms> classDetails = _context.registrationform_subprograms.ToList();

            List<user_deptvenue> user_depts = _context.user_deptvenue.ToList();

            List<user_dept_venue_baseitem> user_dept_baseitems = _context.user_dept_venue_baseitem.ToList();

            List<mz_expense_procurement_baseitem> baseItems = _context.mz_expense_procurement_baseitem.Where(x => x.status == true).Include(x => x.item).ToList();


            foreach (var user in users)
            {
                var useDept = user_depts.FirstOrDefault(x => x.itsId == user.ItsId);
                var role = roles.FirstOrDefault(x => x.roleId == user.roleId);
                if(useDept != null)
                {
                    var userData = new userDatanewDto
                    {
                        itsId = useDept.itsId,
                        Username = user.Username,
                        roleName = role.roleName,
                        schools = new List<schools>(),
                        baseItems = new List<baseItems>()
                    };

                    var userDepts = user_depts.Where(x => x.itsId == user.ItsId).ToList();
                    //var bitem = new List<baseItems>();
                    foreach (var bi in baseItems)
                    {
                        var bitem = new baseItems
                        {
                            bId = bi.id,
                            bName = bi.name
                        };

                        userData.baseItems.Add(bitem);
                    }

                    foreach (var userDept in userDepts)
                    {
                        var dept = dept_venues.FirstOrDefault(x => x.id == userDept.deptVenueId);

                        var classDetail = incomeDetails.FirstOrDefault(x => x.id == userDept.psetId);

                        var classDet = classDetails.FirstOrDefault(x => x.id == classDetail.subprogramId);

                        var user_baseitem = user_dept_baseitems.Where(x => x.itsId == userDept.itsId && x.dept_venueId == dept.id && x.psetId == userDept.psetId).ToList();

                        var bItems = new List<baseItems>();
                        foreach (var userBase in user_baseitem)
                        {
                            var baseItem = baseItems.FirstOrDefault(x => x.id == userBase.baseItemId);

                            //List<itemData> items = new List<itemData>();
                            //baseItem.item.ToList().ForEach(x =>
                            //items.Add(new itemData
                            //{
                            //    id = x.id,
                            //    name = x.name
                            //})
                            //);

                            if(baseItem != null)
                            {
                                bItems.Add(new baseItems
                                {
                                    bId = userBase.id,
                                    bName = baseItem.name,
                                    //items = items

                                });
                            }
                            
                        }

                        var school = new schools
                        {
                            id = useDept.id,
                            deptId = dept.id,
                            schoolName = dept.venueName,
                            section = dept.deptName,
                            classId = classDet.id,
                            psetId = userDept.psetId,
                            className = classDet.name,
                            deptName = classDet.name + "_" + dept.venueName + "_" + dept.deptName,
                            baseitems = bItems
                        };

                        userData.schools.Add(school);

                        userData.schools = userData.schools.OrderBy(x => x.classId).ToList();

                    }

                    result.Add(userData);
                }
                
            }
            return Ok(result);

            //    var result = new List<userDataDto>();
            //    var userbaseitem = _context.user_dept_venue_baseitem.OrderBy(x => x.itsId).ThenBy(x => x.dept_venueId).ToList();
            //    var itsCount = userbaseitem.GroupBy(x => x.itsId).ToDictionary(g => g.Key, g => g.Count());

            //    var allbases = _context.mz_expense_procurement_baseitem.ToList();
            //    var allusers = _context.user.ToList();
            //    var alldepts = _context.dept_venue.ToList();

            //    var i = 0;
            //    var dept_count = 0;
            //    var listdept = new List<(int itsId, int deptVenue, int count)>();

            //    int? currentitsId = null;
            //    int? currentdept = null;
            //    int count = 0;
            //    foreach (var item1 in userbaseitem)
            //    {
            //        if (item1.itsId == currentitsId && item1.dept_venueId == currentdept)
            //        {
            //            count++;
            //        }
            //        else
            //        {
            //            if (count > 0)
            //            {
            //                listdept.Add((currentitsId.Value, currentdept.Value, count));
            //            }
            //            currentitsId = item1.itsId;
            //            currentdept = item1.dept_venueId;
            //            count = 1;
            //        }
            //    }
            //    if (count > 0)
            //    {
            //        listdept.Add((currentitsId.Value, currentdept.Value, count));
            //    }
            //    foreach (var item in userbaseitem)
            //    {
            //        var baseitem = allbases.FirstOrDefault(x => x.id == item.baseItemId);
            //        var user = allusers.FirstOrDefault(x => x.ItsId == item.itsId);
            //        var dept_venue = alldepts.FirstOrDefault(x => x.id == item.dept_venueId);

            //        if (baseitem != null && user != null && dept_venue != null && item.itsId.HasValue && item.dept_venueId.HasValue)
            //        {
            //            var dto = new userDataDto
            //            {
            //                itsId = user.ItsId,
            //                its_count = itsCount[item.itsId.Value],
            //                Username = user.Username,
            //                dept_venueId = dept_venue.id,
            //                baseItemId = baseitem.id,
            //                deptName = dept_venue.deptName,
            //                venueName = dept_venue.venueName,
            //                baseItem = baseitem.name,
            //                deptVenueName = dept_venue.deptName + "_" + dept_venue.venueName
            //            };

            //            var match = listdept.FirstOrDefault(g => g.itsId == item.itsId && g.deptVenue == item.dept_venueId);
            //            if (match != default)
            //            {
            //                dto.count_deptvenue = match.count;
            //            }
            //            result.Add(dto);
            //        }
            //    }

            //    return Ok(result);
        }

        [Route("adduserinsbaseItem")]
        [HttpPost]

        public async Task<ActionResult> adduserinsbaseItem(userBaseItem items)
        {
            
            List<registrationform_dropdown_set> deptIds = _context.registrationform_dropdown_set.Where(x => items.psetId.Contains(x.id)).ToList();
            try
            {
                foreach(var deptId in deptIds)
                {
                    foreach(var bItem in items.baseItemId)
                    {
                        var userCheck = _context.user_dept_venue_baseitem.FirstOrDefault(x => x.itsId == items.itsId && x.dept_venueId == deptId.deptVenueId && x.baseItemId == bItem && x.psetId == deptId.id);

                        if (userCheck != null)
                        {
                            continue;
                        }
                        _context.user_dept_venue_baseitem.Add(new user_dept_venue_baseitem
                        {
                            itsId = items.itsId,
                            dept_venueId = deptId.deptVenueId,
                            baseItemId = bItem,
                            psetId = deptId.id
                        });

                        _context.SaveChanges();
                    }
                    
                }
                return Ok(new {message = "Baseitems assigned to user"});

            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
    public class item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uom { get; set; }



    }

    public class expenseHead
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public bool isCapital { get; set; }


    }

    public class userBaseItem
    {
        public int itsId { get; set; }
        public List<int>? psetId { get; set; }
        public List<int>? baseItemId { get; set; }
    }
}
