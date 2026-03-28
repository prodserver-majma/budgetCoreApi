using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Migrations;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeeCategoryController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public FeeCategoryController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        [Route("addfeecategory/{name}")]
        [HttpPost]
        public async Task<ActionResult> AddFeeCategory(string name, FeeCategoryModel data)
        {
            string api = "addfeecategory/{name}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                mz_student_feecategory f = _context.mz_student_feecategory.Where(x => x.categoryName.ToLower() == name.ToLower()).FirstOrDefault();

                if (f != null)
                {
                    return BadRequest(new { message = "Fee Category already present" });
                }
                else
                {
                    _context.mz_student_feecategory.Add(new mz_student_feecategory { categoryName = name });
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }


        }

        [Route("addfeecategorypset")]
        [HttpPost]
        public async Task<ActionResult> AddFeeCategory_Pset(incomeEstimate model)
        {            
            string api = "addfeecategorypset";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                int financialYear = Int32.Parse(
                _context
                    .global_constant.Where(x => x.key == "budgetFinancialYear")
                    .FirstOrDefault()
                    .value
            );

                model.income.currency = "INR";

                mz_student_feecategory_pset f = _context.mz_student_feecategory_pset.Where(x => x.fcId == model.income.categoryId && x.psetId == model.income.psetId).FirstOrDefault();

                if (f != null)
                {
                    return BadRequest(new { message = "Selected bindings already exists!" });
                }
                else
                { 
                    var fp = new mz_student_feecategory_pset { psetId = model.income.psetId, amount = model.income.amount, currency = model.income.currency, fcId = model.income.categoryId, frequency = model.income.frequency };
                    _context.mz_student_feecategory_pset.Add(fp);
                    _context.SaveChanges();

                    System.Diagnostics.Debug.WriteLine($"This is FP: {fp.id}");

                    _context.mz_expense_estimate_student.Add(new mz_expense_estimate_student { sfcp_id = fp.id, psetId = model.income.psetId, fcId = model.income.categoryId, createdBy = authUser.Name, stage = "Initiated", financialYear = financialYear, createdOn = DateTime.Now });

                    foreach (var item1 in model.month)
                    {
                        _context.mz_expense_estimate_student_monthly.Add(new mz_expense_estimate_student_monthly
                        {
                            estimate_student_id = fp.id,
                            psetId = model.income.psetId ?? 0,
                            month = item1.month,
                            student_count = item1.student_count,
                            fees_per_student = Convert.ToInt32(item1.fees_per_student)
                        });

                        _context.mz_student_feecategory_pset_monthly.Add(new mz_student_feecategory_pset_monthly
                        {
                            student_feecategory_pset_id = fp.id,
                            psetId = model.income.psetId ?? 0,
                            month = item1.month,
                            student_count = item1.student_count,
                            fees_per_student = Convert.ToInt32(item1.fees_per_student)
                        });
                    }

                    _context.SaveChanges();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getallfeecategory")]
        [HttpGet]
        public async Task<ActionResult> getAllFeeCategory()
        {
            string api = "getallfeecategory";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<FeeCategoryModel> model = new List<FeeCategoryModel>();


                List<mz_student_feecategory> fc = _context.mz_student_feecategory.ToList();

                foreach (var i in fc)
                {
                    model.Add(new FeeCategoryModel { categoryId = i.id, categoryName = i.categoryName });
                }


                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("getallfeecategory/{psetId}")]
        [HttpGet]
        public async Task<ActionResult> getAllFeeCategory(int psetId)
        {
            string api = "getallfeecategory/{psetId}";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
                List<FeeCategoryModel> model = new List<FeeCategoryModel>();


                List<mz_student_feecategory_pset> fc = _context.mz_student_feecategory_pset.Where(x => x.psetId == psetId).ToList();
                List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> categoryDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> amountDD = new List<dropdown_dataset_options>();

                foreach (var i in fc)
                {

                    mz_student_feecategory f = _context.mz_student_feecategory.Where(x => x.id == i.fcId).FirstOrDefault();


                    registrationform_dropdown_set set = _context.registrationform_dropdown_set.Where(x => x.id == i.psetId).FirstOrDefault();
                    registrationform_programs p = _context.registrationform_programs.Where(x => x.id == set.programId).FirstOrDefault();
                    registrationform_subprograms sp = _context.registrationform_subprograms.Where(x => x.id == set.subprogramId).FirstOrDefault();
                    venue v = _context.venue.Where(x => x.Id == set.venueId).FirstOrDefault();


                    model.Add(new FeeCategoryModel { id = i.id, categoryId = i.fcId ?? 0, categoryName = f.categoryName, psetId = i.psetId ?? 0, amount = i.amount ?? 0, currency = i.currency, psetName = p.name + "_" + sp.name + "_" + v?.displayName });
                }


                programDD = model.OrderBy(x => x.psetName).GroupBy(x => x.psetName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().psetName?.ToString() }).ToList();
                categoryDD = model.OrderBy(x => x.categoryName).GroupBy(x => x.categoryName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().categoryName?.ToString() }).ToList();
                amountDD = model.OrderBy(x => x.amount).GroupBy(x => x.amount).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().amount.ToString() }).ToList();

                return Ok(new { data = model, programDD = programDD, categoryDD = categoryDD, amountDD = amountDD });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("getallfeecategorypset")]
        [HttpGet]
        public async Task<ActionResult> getAllFeeCategoryPset()
        {
            string api = "getallfeecategorypset";
            //// Add_ApiLogs(api);


            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<FeeCategoryModel> model = new List<FeeCategoryModel>();

                List<student_registration_rights> pset = _context.student_registration_rights.Where(x => x.itsId == authUser.ItsId).ToList();

                //List<mz_student_feecategory_pset> fc = _context.mz_student_feecategory_pset.ToList();
                List<dropdown_dataset_options> programDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> categoryDD = new List<dropdown_dataset_options>();
                List<dropdown_dataset_options> amountDD = new List<dropdown_dataset_options>();

                foreach (var i in pset)
                {
                    var fc = _context.mz_student_feecategory_pset.Where(x => x.psetId == i.programSetId).ToList();
                    
                    
                    if (fc != null)
                    {
                        foreach (var fcs in fc)
                        {
                            mz_student_feecategory f = _context.mz_student_feecategory.Where(x => x.id == fcs.fcId).FirstOrDefault();
                            if (fcs == null)
                            {
                                f = new mz_student_feecategory();
                            }
                            registrationform_dropdown_set set = _context.registrationform_dropdown_set.Where(x => x.id == fcs.psetId).FirstOrDefault();
                            if (set == null)
                            {
                                set = new registrationform_dropdown_set();
                            }
                            registrationform_programs p = _context.registrationform_programs.Where(x => x.id == set.programId).FirstOrDefault();
                            if (p == null)
                            {
                                p = new registrationform_programs();
                            }
                            registrationform_subprograms sp = _context.registrationform_subprograms.Where(x => x.id == set.subprogramId).FirstOrDefault();
                            if (sp == null)
                            {
                                sp = new registrationform_subprograms();
                            }
                            venue v = _context.venue.Where(x => x.Id == set.venueId).FirstOrDefault();
                            if(v == null)
                            {
                                v = new venue();
                            }
                            if (f != null)
                            {
                                model.Add(new FeeCategoryModel { id = fcs.id, categoryId = fcs.fcId ?? 0, categoryName = f?.categoryName, psetId = fcs.psetId ?? 0, amount = fcs.amount ?? 0, frequency = fcs.frequency, currency = fcs.currency, psetName = p.name + "_" + sp.name + "_" + v?.displayName });
                            }
                        }
                    }

                    //mz_student_feecategory f = _context.mz_student_feecategory.Where(x => x.id == fc.fcId).FirstOrDefault();

                    //if (fc == null)
                    //{
                    //    f = new mz_student_feecategory();
                    //}

                    //registrationform_dropdown_set set = _context.registrationform_dropdown_set.Where(x => x.id == fc.psetId).FirstOrDefault();
                    //if (set == null)
                    //{
                    //    set = new registrationform_dropdown_set();
                    //}
                    //;
                    //registrationform_programs p = _context.registrationform_programs.Where(x => x.id == set.programId).FirstOrDefault();
                    //if (p == null)
                    //{
                    //    p = new registrationform_programs();
                    //}
                    //;
                    //registrationform_subprograms sp = _context.registrationform_subprograms.Where(x => x.id == set.subprogramId).FirstOrDefault();
                    //if (sp == null)
                    //{
                    //    sp = new registrationform_subprograms();
                    //}
                    //;
                    //venue v = _context.venue.Where(x => x.Id == set.venueId).FirstOrDefault();
                    //if (v == null)
                    //{
                    //    v = new venue();
                    //}
                    //;

                    //if (f != null)
                    //{
                    //    model.Add(new FeeCategoryModel { id = fc.id, categoryId = fc.fcId ?? 0, categoryName = f?.categoryName, psetId = fc.psetId ?? 0, amount = fc.amount ?? 0, frequency = fc.frequency, currency = fc.currency, psetName = p.name + "_" + sp.name + "_" + v?.displayName });
                    //}
                }


                //foreach (var i in fc)
                //{
                //    mz_student_feecategory f = _context.mz_student_feecategory.Where(x => x.id == i.fcId).FirstOrDefault();


                //    registrationform_dropdown_set set = _context.registrationform_dropdown_set.Where(x => x.id == i.psetId).FirstOrDefault();
                //    if (set == null)
                //    {
                //        set = new registrationform_dropdown_set();
                //    };
                //    registrationform_programs p = _context.registrationform_programs.Where(x => x.id == set.programId).FirstOrDefault();
                //    if (p == null)
                //    {
                //        p = new registrationform_programs();
                //    };
                //    registrationform_subprograms sp = _context.registrationform_subprograms.Where(x => x.id == set.subprogramId).FirstOrDefault();
                //    if (sp == null)
                //    {
                //        sp = new registrationform_subprograms();
                //    };
                //    venue v = _context.venue.Where(x => x.Id == set.venueId).FirstOrDefault();
                //    if (v == null)
                //    {
                //        v = new venue();
                //    };

                //    model.Add(new FeeCategoryModel { id = i.id, categoryId = i.fcId ?? 0, categoryName = f.categoryName, psetId = i.psetId ?? 0, amount = i.amount ?? 0, frequency = i.frequency, currency = i.currency, psetName = p.name + "_" + sp.name + "_" + v?.displayName });
                //}


                programDD = model.OrderBy(x => x.psetName).GroupBy(x => x.psetName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().psetName?.ToString() }).ToList();
                categoryDD = model.OrderBy(x => x.categoryName).GroupBy(x => x.categoryName).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault().categoryName?.ToString() }).ToList();
                amountDD = model.OrderBy(x => x.amount).GroupBy(x => x.amount).Select(x => new dropdown_dataset_options { name = x.FirstOrDefault()?.amount.ToString() }).ToList();

                return Ok(new { data = model, programDD = programDD, categoryDD = categoryDD, amountDD = amountDD });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [Route("EditFeecategoryPset/{id}/{amount}/{frequency}")]
        [HttpPut]
        public async Task<ActionResult> EditFeecategoryPset(int id, int amount, string frequency, FeeCategoryModel model)
        {
            string api = "EditFeecategoryPset/{id}/{amount}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {

                mz_student_feecategory_pset f = _context.mz_student_feecategory_pset.Where(x => x.id == id).FirstOrDefault();

                f.amount = amount;
                f.frequency = frequency;

                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [Route("Activestatus/{ItsId}/{reason}")]
        [HttpPut]
        public async Task<ActionResult> ChangeActiveStatus(int ItsId, string reason)
        {
            string api = "Activestatus/{ItsId}/{reason}";
            //// Add_ApiLogs(api);

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                mz_student student = _context.mz_student.FirstOrDefault(x => x.itsID == ItsId);


                if (student.activeStatus == true)
                {
                    student.activeStatus = false;
                }
                else
                {
                    student.activeStatus = true;

                }

                //if (student.activeStatus == true)
                //{
                //    mz_student_logs l = new mz_student_logs { createdBy = authUser.Name, createdOn = indianTime, typeId = 1,studentId=student.mz_id,logId= Convert.ToInt32(authUser.logId), };
                //    context.mz_student_logs.Add(l);
                //}
                //else
                //{
                //    mz_student_logs l = new mz_student_logs { createdBy = authUser.Name, createdOn = indianTime, typeId = 2, studentId = student.mz_id, logId = Convert.ToInt32(authUser.logId), };
                //    context.mz_student_logs.Add(l);
                //}

                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        [Route("EditFeecategory/{id}/{category}")]
        [HttpPut]
        public async Task<ActionResult> EditFeecategory(int id, string category, FeeCategoryModel model)
        {
            string api = "EditFeecategory/{id}/{category}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {


                mz_student_feecategory f = _context.mz_student_feecategory.Where(x => x.id == id).FirstOrDefault();

                f.categoryName = category;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }


        }


        [Route("deletefeecategoryPset/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete_FeeCategory_Pset(int id)
        {
            string api = "deletefeecategoryPset/{id}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                mz_student_feecategory_pset f = _context.mz_student_feecategory_pset.Where(x => x.id == id).FirstOrDefault();
                mz_expense_estimate_student es = _context.mz_expense_estimate_student.Where(x => x.sfcp_id == f.id).FirstOrDefault();
                List<mz_expense_estimate_student_monthly> esms = _context.mz_expense_estimate_student_monthly.Where(x => x.estimate_student_id == f.id).ToList();

                System.Diagnostics.Debug.WriteLine($"This is ES: {esms}");
                _context.mz_student_feecategory_pset.Remove(f);
                _context.mz_expense_estimate_student.Remove(es);

                foreach(var esm in esms)
                {
                    _context.mz_expense_estimate_student_monthly.Remove(esm);
                }
                

                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }


        }



    }
}
