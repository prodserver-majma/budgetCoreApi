using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings.Finance;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalGrantController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private static readonly string WafdAlHuffaz = "Wafd al-Huffaz";
        private static readonly string MahadAlZahra_KHDGZ = "Mahad al-Zahra KHDGZ";
        private static readonly string Jamea_KG = "Jamea KG";
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        public MedicalGrantController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        // log4net.ILog log = // log4net.LogManager.GetLogger(typeof(EmailController));


        [Route("submitbills")]
        [HttpPost]
        public async Task<IActionResult> SubmitBills(FacultyMedicalEnayatBillSubmit i)
        {
            string api = "submitbills";
            //// Add_ApiLogs(api);

            int? id = null;
            try
            {

                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Today);

                DateTime from = new DateTime(2023, 12, 1);
                DateTime to = new DateTime(2024, 4, 30);
                if (i.billDate < from && i.billDate > to)
                {
                    return BadRequest(new { message = "You can only submit the bills between 1st Dec 2023 to 30th April 2024" });
                }

                enayat_medical_billentry b = _context.enayat_medical_billentry.Where(x => x.billPeriodId == _globalConstants.Current_Medical_Period && x.aplicantItsId == i.aplicantItsId && x.status.Equals("CLEARED")).FirstOrDefault();

                if (b != null)
                {
                    throw new Exception("your bills has already cleared by surat office");
                }

                khidmat_guzaar khadim = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId && x.mz_idara.Equals(MahadAlZahra_KHDGZ)).FirstOrDefault();

                if (khadim == null)
                {
                    throw new Exception("you are not authorized for this program");
                }

                if (i.billRelation == 1)
                {
                    i.relationItsId = authUser.ItsId;
                }

                // currency_converter c = _context.currency_converter.Where(x => x.id == i.currency).FirstOrDefault();
                var bill = new enayat_medical_billentry
                {
                    billPeriod = _globalConstants.Current_Medical_Period_Name,
                    billPeriodId = i.billPeriod,
                    requestFor = i.billRequestFor,
                    entryDate = DateTime.Today,
                    billType = i.billType,
                    billDate = i.billDate,
                    billFrom = i.billFrom,
                    amount = i.billAmount,
                    illness = i.illness,
                    relationType = _globalConstants.relations[i.billRelation ?? 0],
                    relationTypeId = i.billRelation,
                    aplicantItsId = authUser.ItsId,
                    status = "PENDING",
                    currencySymbol = i.currencysymbol,
                    billNumber = i.billNumber,
                    relationItsId = i.relationItsId
                };

                _context.enayat_medical_billentry.Add(bill);
                _context.SaveChanges();

                id = bill.id;

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Exception" });
            }

        }


        [Route("medicalbillattachments/{uniqueId}")]
        [HttpPost]
        public async Task<IActionResult> MedicalBillAttachment(int uniqueId)
        {
            string api = "api//fitnessattachments/{uniqueId}";
            //// Add_ApiLogs(api);

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            var bill = _context.enayat_medical_billentry.Where(x => x.id == uniqueId).FirstOrDefault();

            HttpResponseMessage result = null;

            //var httpRequest = HttpContext.Current.Request;

            //if (httpRequest.Files.Count > 0)
            //{
            //    string filePath = "";
            //    string path = "";
            //    string filePath_1 = "";
            //    string path_1 = "";

            //    var docfiles = new List<string>();
            //    foreach (string file in httpRequest.Files)
            //    {
            //        var postedFile = httpRequest.Files[file];
            //        var e = _context.contenttypewithextention_data.Where(x => x.contentType.Equals(postedFile.ContentType)).FirstOrDefault();
            //        if (e == null)
            //        {
            //            return BadRequest( new { message = "No extention found");
            //        }
            //        var extension = e.extention;



            //        path = "/" + Settings.Instance.UploadFolder + "/medicalBills/" + uniqueId + extension;

            //        filePath = HttpContext.Current.Server.MapPath("~/" + path);

            //        postedFile.SaveAs(filePath);

            //        path_1 = "/" + Settings.Instance.AssetFolder + "/medicalBills/" + uniqueId + extension;

            //        filePath_1 = HttpContext.Current.Server.MapPath("~/" + path_1);

            //        postedFile.SaveAs(filePath_1);


            //        bill.attachment = path;

            //        //docfiles.Add(filePath);
            //        // result = Request.CreateResponse(HttpStatusCode.Created, filePath);
            //    }

            //    try
            //    {
            //        _context.SaveChanges();
            //    }
            //    catch (DbEntityValidationException ex)
            //    {
            //        foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //        {
            //            foreach (var validationError in entityValidationErrors.ValidationErrors)
            //            {
            //                // log.Debug("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //            }
            //        }
            //        BadRequest(ex.ToString() + " check log ");
            //    }
            //    catch (Exception ex)
            //    {
            //        // log.Debug(ex.ToString());
            //        return BadRequest(ex.ToString() + " check log ");
            //    }

            //    return Ok("Bill Successfully submitted"); ;
            //}
            //else
            //{
            //    return Ok();

            //    //result = Request.CreateResponse(HttpStatusCode.BadRequest);
            //}

            return Ok("Module not active - S3 implementation required");

        }
    }
}
