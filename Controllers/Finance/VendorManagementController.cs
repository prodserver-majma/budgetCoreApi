using Abp.Domain.Entities;
using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using mahadalzahrawedapi.Mappings.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using System;
using System.Runtime.InteropServices;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorManagementController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        private readonly FileExtensionContentTypeProvider _contentTypeProvider;
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        private readonly IWebHostEnvironment _env;

        public VendorManagementController(mzdbContext context, IMapper mapper, TokenService tokenService, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _contentTypeProvider = new FileExtensionContentTypeProvider();
            _env = env;
        }

        [Route("getAllVendors")]
        [HttpGet]
        public async Task<IActionResult> getAllVendors()
        {
            string api = "getAllVendors";
            //string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            //return Ok(token);
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                //return Ok(token);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                List<user_deptvenue> udvs = _context.user_deptvenue.ToList();

                var depts = _context.dept_venue.ToList();
                var groupedDepts = depts.GroupBy(x => x.venueId).ToList();

                //var rds = _context.registrationform_dropdown_set.ToList();

                //var sps = _context.registrationform_subprograms.ToList();


                List<mz_expense_vendor_master> vendors = _context.mz_expense_vendor_master.ToList();

                List<VendorMasterModel> entries = new List<VendorMasterModel>();
                foreach (var vendor in vendors)
                {
                    var ud = udvs.Where(x => x.itsId == authUser.ItsId).ToList();

                    foreach (var udv in ud)
                    {
                        var dpt = depts.Where(x => x.id == udv.deptVenueId).FirstOrDefault();

                        if(dpt.venueId == vendor.schoolId)
                        {
                            bool check = entries.Any(x => x.id == vendor.id);
                            if (!check)
                            {
                                VendorMasterModel model = _mapper.Map<VendorMasterModel>(vendor);

                                model.schoolClassName = dpt.venueName;
                                model.school = vendor.schoolId;
                                entries.Add(model);

                                if (model.panCardAttachment != null)
                                {
                                    var path = Path.Combine(_env.WebRootPath, "vendorPan", model.panCardAttachment);

                                    var fileCheck = System.IO.File.Exists(path);

                                    model.fileCheck = fileCheck;
                                }
                            }
                            
                        }
                        //if (udv.psetId == vendor.psetId && udv.deptVenueId == vendor.deptVenueId)
                        //{
                        //    bool check = entries.Any(x => x.id == vendor.id);
                        //    if (!check)
                        //    {                            
                        //        var dept = depts.Where(x => x.id == udv.deptVenueId).FirstOrDefault();

                        //        if (dept != null)
                        //        {
                        //            //var rd = rds.Where(x => x.id == udv.psetId).FirstOrDefault();

                        //            //if (rd != null)
                        //            //{
                        //                //var sp = sps.Where(x => x.id == rd.subprogramId).FirstOrDefault();

                        //                VendorMasterModel model = _mapper.Map<VendorMasterModel>(vendor);

                        //                model.schoolClassName = dept.venueName + "_" + dept.deptName;
                        //                model.pset = vendor.psetId;
                        //                entries.Add(model);

                        //                if (model.panCardAttachment != null)
                        //                {
                        //                    var path = Path.Combine(_env.WebRootPath, "vendorPan", model.panCardAttachment);

                        //                    var fileCheck = System.IO.File.Exists(path);

                        //                    model.fileCheck = fileCheck;
                        //                }

                        //            //}
                        //        }
                        //    }
                        //}
                    }
                }

                //var vendors = entry.Where(x => x.userItsId == authUser.ItsId).ToList();

                //var udvs = _context.user_deptvenue.ToList();

                //if(vendors.Count < 1)
                //{
                //    var userdv = udvs.Where(x => x.itsId == authUser.ItsId).ToList();
                //    System.Diagnostics.Debug.WriteLine($"This is deptVenues for Current User: {System.Text.Json.JsonSerializer.Serialize(userdv)}");

                //    foreach (var vndr in entry)
                //    {
                //        var udv = udvs.Where(x => x.itsId == vndr.userItsId).ToList();


                //        System.Diagnostics.Debug.WriteLine($"This is deptVenues for All User: {System.Text.Json.JsonSerializer.Serialize(udv)}");

                //    }
                //}

                return Ok(entries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("activeinactiveVendor/{id}")]
        [HttpGet]
        public async Task<IActionResult> activeInactiveVendor(int id)
        {
            string api = "activeinactiveVendor/{id}";
            //// Add_ApiLogs(api);

            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);



                mz_expense_vendor_master entry = _context.mz_expense_vendor_master.Where(x => x.id == id).FirstOrDefault();

                if (entry.status == true)
                {
                    entry.status = false;

                }
                else
                {
                    entry.status = true;
                }

                _context.SaveChanges();



                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("addVendor")]
        [HttpPost]
        public async Task<IActionResult> AddVendor(VendorMasterModel modelDto)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            var udv = _context.user_deptvenue.ToList();

            DateTime currentDateTime = DateTime.Now;

            try
              {
                //if (!string.IsNullOrEmpty(modelDto.accountNo))
                //{
                //    var checkId = "";
                //    mz_expense_vendor_master a;
                    
                //    //if (modelDto.gstNumber != null || modelDto.gstNumber != "")
                //    //{

                //    //}
                //    //var a = _context.mz_expense_vendor_master.Where(x => x.accountNo == modelDto.accountNo ).FirstOrDefault();
                    
                //}

                //mz_expense_vendor_master b;
                mz_expense_vendor_master a;

                List<string> msgs = new List<string>();

                mz_expense_vendor_master newVendor = new mz_expense_vendor_master();

                foreach (var ps in modelDto.schoolId)
                {
                    if (modelDto.gstNumber != null && modelDto.gstNumber != "")
                    {
                        a = _context.mz_expense_vendor_master.Where(x => x.gstNumber == modelDto.gstNumber && x.schoolId == ps).FirstOrDefault();
                    }
                    else if (modelDto.panCardNo != null && modelDto.panCardNo != "")
                    {
                        a = _context.mz_expense_vendor_master.Where(x => x.panCardNo == modelDto.panCardNo && x.schoolId == ps).FirstOrDefault();
                    }
                    else
                    {
                        a = _context.mz_expense_vendor_master.Where(x => x.panCardNo == modelDto.panCardNo && x.gstNumber == modelDto.gstNumber && x.schoolId == ps).FirstOrDefault();
                    }

                    if(a == null)
                    {
                        //var deptId = udv.Where(x => x.psetId == ps).FirstOrDefault();

                         newVendor = new mz_expense_vendor_master
                        {
                            //deptVenueId = deptId.deptVenueId,
                            //psetId = ps,
                            schoolId = ps,
                            name = modelDto.name,
                            phoneNo = modelDto.phoneNo,
                            mobileNo = modelDto.mobileNo,
                            whatsappNo = modelDto.whatsappNo,
                            address = modelDto.address,
                            state = modelDto.state,
                            city = modelDto.city,
                            ifscCode = modelDto.ifscCode,
                            bankName = modelDto.bankName,
                            accountNo = modelDto.accountNo,
                            accountName = modelDto.accountName,
                            panCardNo = modelDto.panCardNo,
                            //userItsId = authUser.ItsId,
                            createdOn = currentDateTime,
                            createdBy = Convert.ToString(authUser.ItsId),
                            status = modelDto.status,
                            type = modelDto.type,
                            email = modelDto.email,
                            gstNumber = modelDto.gstNumber
                        };

                        _context.mz_expense_vendor_master.Add(newVendor);
                        _context.SaveChanges();

                    }
                    else
                    {
                        msgs.Add("Vendor already exists for the selected class(es)");
                    }
                }

                Debug.WriteLine(newVendor);
                string vendorPan = newVendor.id + "_" + modelDto.panCardAttachmentFileName;

                try
                {
                    var uploadFolder = Path.Combine(_env.WebRootPath, "vendorPan");

                    Directory.CreateDirectory(uploadFolder);

                    byte[] fileBytes = Convert.FromBase64String(modelDto.panCardAttachment);


                    var fileName = Path.Combine(uploadFolder, vendorPan);

                    System.IO.File.WriteAllBytes(fileName, fileBytes);

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"This is upload debug: {e.Message}");
                }

                newVendor.panCardAttachment = vendorPan;
                _context.SaveChanges();

                if (modelDto.schoolId.Count() == msgs.Count())
                {
                    return BadRequest(new { message = msgs[0]});
                }
                //Debug.WriteLine($"Msg debug: {msgs.Count()}");

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [Route("editVendor")]
        [HttpPost]
        public async Task<IActionResult> EditVendor(VendorMasterModel modelDto)
        {
            string api = "editVendor";

            //mz_expense_vendor_master model = _mapper.Map<mz_expense_vendor_master>(modelDto);
            //var udv = _context.user_deptvenue.ToList();
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            DateTime currentDateTime = DateTime.Now;

            try
            {
                //foreach (var ps in modelDto.schoolId)
                //{
                    var a = _context.mz_expense_vendor_master.Where(x => x.id == modelDto.id).FirstOrDefault();

                    if (a == null)
                    {
                        return BadRequest(new { message = "Vendor Not Found" });
                    }

                    //var deptId = udv.Where(x => x.psetId == ps).FirstOrDefault();

                    a.accountName = modelDto.accountName;
                    a.accountNo = modelDto.accountNo;
                    a.address = modelDto.address;
                    a.bankName = modelDto.bankName;
                    a.city = modelDto.city;
                    a.ifscCode = modelDto.ifscCode.ToUpper();
                    a.mobileNo = modelDto.mobileNo;
                    a.name = modelDto.name;
                    a.panCardNo = modelDto.panCardNo;
                    a.gstNumber = modelDto.gstNumber;
                    a.phoneNo = modelDto.phoneNo;
                    a.state = modelDto.state;
                    a.status = modelDto.status;
                    a.whatsappNo = modelDto.whatsappNo;
                    //a.schoolId = modelDto.school;
                    //a.psetId = ps;
                    //a.deptVenueId = deptId.deptVenueId;
                    a.updatedOn = currentDateTime;
                    a.updatedBy = authUser.ItsId;
                //}

                if (modelDto.isPanCardAttachmentEdited == true)
                {
                    try
                    {
                        var uploadFolder = Path.Combine(_env.WebRootPath, "vendorPan");

                        Directory.CreateDirectory(uploadFolder);

                        byte[] fileBytes = Convert.FromBase64String(modelDto.panCardAttachment);

                        var fileName = Path.Combine(uploadFolder, modelDto.panCardAttachmentFileName);

                        System.IO.File.WriteAllBytes(fileName, fileBytes);

                    }

                    //try
                    //{
                    //    byte[] binaryData = Convert.FromBase64String(modelDto.panCardAttachment ?? "");
                    //    MemoryStream stream2 = new MemoryStream(binaryData);
                    //    if (!string.IsNullOrEmpty(modelDto.panCardAttachment))
                    //    {
                    //        a.panCardAttachment = await _helperService.UploadFileToS3(stream2, modelDto.panCardAttachmentFileName, "uploads/vendorPancard");
                    //    }
                    //}
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }


                _context.SaveChanges();


                return Ok(modelDto.id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [Route("vendor/getOnlinePaymentsUser")]
        [HttpGet]
        public async Task<IActionResult> getOnlinePaymentsUser()
        {
            string api = "vendor/getOnlinePaymentsUser";

            try
            {
                List<mz_expense_vendor_master> entries = new List<mz_expense_vendor_master>();
                List<mz_expense_online_payment_users> users = _context.mz_expense_online_payment_users.ToList();

                foreach (var i in users)
                {
                    entries.Add(new mz_expense_vendor_master
                    {
                        id = i.id,
                        accountName = i.accName,
                        accountNo = i.accNum,
                        bankName = i.bankName,
                        ifscCode = i.ifsc,
                        name = i.name
                    });
                }

                return Ok(entries);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
