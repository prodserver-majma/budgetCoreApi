using Abp.Extensions;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using mahadalzahrawebapi.Api.v1;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Design.Internal;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Org.BouncyCastle.Crypto;
using System.Data.Entity.Validation;
using System.Reflection;
using System.Security.Cryptography;
using System.ServiceModel.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mahadalzahrawebapi.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;

        public EmployeeController(mzdbContext context, IMapper mapper, TokenService tokenService)
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

        // GET: v2
        [HttpGet]
        public IActionResult Getemployee()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            List<khidmat_guzaar> kgdb = _context
                .khidmat_guzaar.Include(kg => kg.employee_bank_details)
                .Include(kg => kg.employee_academic_details)
                .ToList();
            List<khidmat_guzaarDTO> kgs = _mapper.Map<List<khidmat_guzaarDTO>>(kgdb);

            //return await _context.employee.ToListAsync();
            return Ok(kgs);
        }

        [HttpGet("GetAuthUser")]
        public async Task<ActionResult<object>> GetEmpAuthUser()
        {
            branch_user y = new branch_user();
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                UserModel z = new UserModel();

                if (authUser.loginName == "Branch_Login_Angular")
                {
                    y = _context
                        .branch_user.Where(x => x.itsId == authUser.ItsId)
                        .Include(x => x.its)
                        .Include(x => x.platform_user_module)
                        .ThenInclude(x => x.qism)
                        .FirstOrDefault();

                    z = new UserModel
                    {
                        emailId = y.emailId,
                        name = authUser.Name,
                        id = y.itsId,
                        itsId = y.itsId,
                        mobile = y.its.mobileNo,
                        modules = y.platform_user_module.ToList().Select(x => x.moduleId).ToList(),
                        password = y.password,
                        userName = y.its.fullName
                    };

                    z.qism = y
                        .platform_user_module.GroupBy(x => x.qismId)
                        .Select(x => x.FirstOrDefault()?.qism ?? new qism_al_tahfeez())
                        .ToList()
                        .Select(x => new QismModel
                        {
                            emailId = x.emailId,
                            id = x.id,
                            name = x.name,
                            modules = y
                                .platform_user_module?.Where(k =>
                                    k.userId == z.itsId && k.qismId == x.id
                                )
                                .Select(k => k.moduleId)
                                .ToList()
                        })
                        .ToList();
                }

                if (authUser.loginName == "HR_Login_Angular")
                {
                    var result = new { itsId = authUser.ItsId, name = authUser.Name, };

                    return Ok(result);
                }

                return Ok(z);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // GET: v2/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBasicDetailsModel>> Getkhidmat_guzaar(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .khidmat_guzaar.Include(x => x.mauzeNavigation)
                .Include(x => x.employee_khidmat_details)
                .Where(x => x.itsId == id)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeeBasicDetailsModel>(khidmat_guzaar);
        }

        [HttpGet("selfBasicDetails")]
        public async Task<ActionResult<EmployeeBasicDetailsModel>> GetSelfBasicDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .khidmat_guzaar.Include(x => x.mauzeNavigation)
                .Include(x => x.employee_khidmat_details)
                .Where(x => x.itsId == authUser.ItsId)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeeBasicDetailsModel>(khidmat_guzaar);
        }

        [HttpPut("updateSelfBasicDetails")]
        public async Task<IActionResult> PutupdateSelfBasicDetails(EmployeeBasicDetailsModel kg)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            khidmat_guzaar khidmat_guzaar = _mapper.Map<khidmat_guzaar>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!khidmat_guzaarExists(authUser.ItsId))
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

        [HttpGet("selfAcademicDetails")]
        public async Task<ActionResult<EmployeeAcademicDetailsModel>> GetSelfAcademicDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            int academicYear = Int32.Parse(
                _context
                    .global_constant.Where(x => x.key == "trainingAcedemicYear")
                    .FirstOrDefault()
                    .value
            );

            training_class tc = _context
                .training_class_student.Where(x =>
                    x.studentITS == authUser.ItsId && x.academicYear == academicYear
                )
                .Include(x => x._class)
                .Select(x => x._class)
                .FirstOrDefault();

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .employee_academic_details.Where(x => x.itsId == authUser.ItsId)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<EmployeeAcademicDetailsModel>(khidmat_guzaar);

            result.trainingClass = tc?.className;

            return result;
        }

        [HttpPut("updateSelfAcademicDetails")]
        public async Task<IActionResult> PutupdateSelfAcademicDetails(
            EmployeeAcademicDetailsModel kg
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_academic_details khidmat_guzaar = _mapper.Map<employee_academic_details>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!academicDetailsExists(authUser.ItsId))
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

        [HttpPost("academicdetails")]
        public async Task<ActionResult<employee_academic_details>> createAcademicDetails(
            EmployeeAcademicDetailsModel employeeAcademicDetails
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.employee_bank_details == null)
            {
                return Problem("Entity set 'MzApiDb_context.employee'  is null.");
            }

            employee_academic_details ead = _mapper.Map<employee_academic_details>(
                employeeAcademicDetails
            );

            _context.employee_academic_details.Add(ead);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetEmployee_Bank_Details",
                new { id = employeeAcademicDetails.id },
                ead
            );
        }

        [Route("faculty/addmoze")]
        [HttpPost]
        public async Task<IActionResult> addMoze(wafdulhuffaz_khidmat_mawaze moze)
        {
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                moze.itsId = authUser.ItsId;

                _context.wafdulhuffaz_khidmat_mawaze.Add(moze);
                _context.SaveChanges();

                return Ok("Moze succesfully Added");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Exception" });
            }
        }

        [Route("faculty/deletemoze/{id}")]
        [HttpDelete]
        public async Task<IActionResult> deleteMoze(int id)
        {
            try
            {
                var delete = _context
                    .wafdulhuffaz_khidmat_mawaze.Where(x => x.id == id)
                    .FirstOrDefault();

                _context.wafdulhuffaz_khidmat_mawaze.Remove(delete);
                _context.SaveChanges();

                return Ok("Moze is succesfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Exception" });
            }
        }

        [Route("faculty/getkhidmatmawaze")]
        [HttpGet]
        public async Task<IActionResult> getKhidmatMawaze()
        {
            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<wafdulhuffaz_khidmat_mawaze> mawaze = new List<wafdulhuffaz_khidmat_mawaze>();
                List<wafdulhuffaz_khidmat_mawaze> mawaze2 = new List<wafdulhuffaz_khidmat_mawaze>();

                List<WafdMawazeModel> mawaze3 = new List<WafdMawazeModel>();
                List<WafdMawazeModel> mawaze4 = new List<WafdMawazeModel>();

                mawaze = _context
                    .wafdulhuffaz_khidmat_mawaze.Where(x => x.itsId == authUser.ItsId)
                    .OrderByDescending(x => x.hijriYear)
                    .ThenBy(x => x.khidmatMainType)
                    .ToList();
                mawaze2 = mawaze
                    .GroupBy(x => x.khidmatMainType)
                    .Select(x => x.FirstOrDefault())
                    .ToList();
                foreach (var i in mawaze2.Where(x => x.khidmatSubType != null).ToList())
                {
                    var listmawaze = mawaze
                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                        .GroupBy(x => x.khidmatSubType)
                        .Select(x => x.FirstOrDefault())
                        .ToList();
                    var count1 = mawaze
                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                        .ToList()
                        .Count;
                    int c = 1;
                    foreach (var j in listmawaze)
                    {
                        bool d = false;
                        if (c == 1)
                        {
                            d = true;
                        }
                        var count = mawaze
                            .Where(x =>
                                x.khidmatMainType.Equals(i.khidmatMainType)
                                && x.khidmatSubType.Equals(j.khidmatSubType)
                            )
                            .ToList()
                            .Count;
                        mawaze3.Add(
                            new WafdMawazeModel
                            {
                                colSpan = 1,
                                display_subType = true,
                                rowSpan = listmawaze.Count,
                                khidmatMainType = i.khidmatMainType,
                                khidmatSubType = j.khidmatSubType == null ? "-" : j.khidmatSubType,
                                mainTypeCount = count1,
                                subTypeCount = count,
                                display = d
                            }
                        );
                        c = c + 1;
                    }
                }
                foreach (var i in mawaze)
                {
                    mawaze4.Add(
                        new WafdMawazeModel
                        {
                            id = i.id,
                            hijriYear = i.hijriYear,
                            mozeName = i.mozeName,
                            colSpan = 1,
                            display_subType = true,
                            rowSpan = 1,
                            khidmatMainType = i.khidmatMainType,
                            khidmatSubType = i.khidmatSubType == null ? "-" : i.khidmatSubType,
                            mainTypeCount = 0,
                            subTypeCount = 0,
                            display = true
                        }
                    );
                }

                return Ok(new { mawaze = mawaze4, mawaze3 = mawaze3 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error" });
            }
        }

        [Route("campfasal/getkutub")]
        [HttpGet]
        public async Task<IActionResult> getAllKutub()
        {
            string api = "api/campfasal/getkutub";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                List<campfasal_kutub> c = _context.campfasal_kutub.ToList();

                return Ok(c);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private bool academicDetailsExists(int id)
        {
            return (
                _context.employee_academic_details?.Any(e => e.itsId == id)
            ).GetValueOrDefault();
        }

        [HttpGet("selfPassportDetails")]
        public async Task<ActionResult<EmployeePassportDetailsModel>> GetSelfPassportDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .employee_passport_details.Where(x => x.itsId == authUser.ItsId)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeePassportDetailsModel>(khidmat_guzaar);
        }

        [HttpPut("updateSelfPassportDetails")]
        public async Task<IActionResult> PutupdateSelfPassportDetails(
            EmployeePassportDetailsModel kg
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_academic_details khidmat_guzaar = _mapper.Map<employee_academic_details>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!passportDetailsExists(authUser.ItsId))
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

        [HttpPost("passportdetails")]
        public async Task<ActionResult<employee_passport_details>> createPassportDetails(
            EmployeePassportDetailsModel employee_Bank_Details
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.employee_bank_details == null)
            {
                return Problem("Entity set 'MzApiDb_context.employee'  is null.");
            }

            try
            {
                byte[] binaryData1 = Convert.FromBase64String(
                    employee_Bank_Details.passportCopy ?? ""
                );
                MemoryStream stream1 = new MemoryStream(binaryData1);
                if (!string.IsNullOrEmpty(employee_Bank_Details.passportCopy))
                {
                    employee_Bank_Details.passportCopy = await _helperService.UploadFileToS3(
                        stream1,
                        employee_Bank_Details.itsId
                            + employee_Bank_Details.passportName
                            + employee_Bank_Details.dateOfExpiry
                            + employee_Bank_Details.passportCopyFileName,
                        "uploads/passports"
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            employee_passport_details emd = _mapper.Map<employee_passport_details>(
                employee_Bank_Details
            );

            _context.employee_passport_details.Add(emd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetEmployee_Bank_Details",
                new { id = employee_Bank_Details.id },
                emd
            );
        }

        private bool passportDetailsExists(int id)
        {
            return (
                _context.employee_passport_details?.Any(e => e.itsId == id)
            ).GetValueOrDefault();
        }

        [HttpGet("selfKhidmatDetails")]
        public async Task<ActionResult<EmployeeKidmatDetailsModel>> GetSelfKhidmatDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .employee_khidmat_details.Where(x => x.itsId == authUser.ItsId)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeeKidmatDetailsModel>(khidmat_guzaar);
        }

        [HttpPut("updateSelfKhidmatDetails")]
        public async Task<IActionResult> PutupdateSelfkhidmatDetails(EmployeeKidmatDetailsModel kg)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_khidmat_details khidmat_guzaar = _mapper.Map<employee_khidmat_details>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!khidmatDetailsExists(authUser.ItsId))
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

        [HttpPost("khidmatdetails")]
        public async Task<ActionResult<employee_khidmat_details>> createKhidmatDetails(
            EmployeeKidmatDetailsModel employeeKidmatDetails
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.employee_khidmat_details == null)
            {
                return Problem("Entity set 'MzApiDb_context.employee'  is null.");
            }

            employee_khidmat_details emd = _mapper.Map<employee_khidmat_details>(
                employeeKidmatDetails
            );

            _context.employee_khidmat_details.Add(emd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetEmployee_Bank_Details",
                new { id = employeeKidmatDetails.id },
                emd
            );
        }

        private bool khidmatDetailsExists(int id)
        {
            return (_context.employee_khidmat_details?.Any(e => e.itsId == id)).GetValueOrDefault();
        }

        [HttpGet("selfBankDetails")]
        public async Task<ActionResult<List<EmployeeBankDetailsModel>>> GetSelfBankDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .employee_bank_details.Where(x => x.itsId == authUser.ItsId)
                .ToListAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<EmployeeBankDetailsModel>>(khidmat_guzaar);
        }

        [HttpPut("updateSelfBankDetails/{id}")]
        public async Task<IActionResult> PutupdateSelfBankDetails(EmployeeBankDetailsModel kg)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_bank_details khidmat_guzaar = _mapper.Map<employee_bank_details>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bankDetailsExists(kg.id))
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

        [HttpPost("bankdetails")]
        public async Task<ActionResult<employee_bank_details>> createBankDetails(
            EmployeeBankDetailsModel employee_Bank_Details
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.employee_bank_details == null)
            {
                return Problem("Entity set 'MzApiDb_context.employee'  is null.");
            }

            try
            {
                byte[] binaryData1 = Convert.FromBase64String(
                    employee_Bank_Details.chequeAttachment ?? ""
                );
                MemoryStream stream1 = new MemoryStream(binaryData1);
                byte[] binaryData2 = Convert.FromBase64String(
                    employee_Bank_Details.panCardAttachment ?? ""
                );
                MemoryStream stream2 = new MemoryStream(binaryData2);
                if (!string.IsNullOrEmpty(employee_Bank_Details.chequeAttachment))
                {
                    employee_Bank_Details.chequeAttachment = await _helperService.UploadFileToS3(
                        stream1,
                        employee_Bank_Details.chequeAttachmentFileName,
                        "uploads/cheque"
                    );
                }
                if (!string.IsNullOrEmpty(employee_Bank_Details.panCardAttachment))
                {
                    employee_Bank_Details.panCardAttachment = await _helperService.UploadFileToS3(
                        stream2,
                        employee_Bank_Details.panCardAttachmentFileName,
                        "uploads/pancard"
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            employee_bank_details emd = _mapper.Map<employee_bank_details>(employee_Bank_Details);

            _context.employee_bank_details.Add(emd);
            await _context.SaveChangesAsync();

            return Ok(emd);
        }

        [HttpDelete("bankdetails/{id}")]
        public async Task<IActionResult> DeleteBankDetails(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.employee_bank_details == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context.employee_bank_details.FindAsync(id);
            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            _context.employee_bank_details.Remove(khidmat_guzaar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("setDefaultBank/{id}")]
        public async Task<IActionResult> SetDefaultBank(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_bank_details defaultBank = await _context
                .employee_bank_details.Where(x => x.id == id)
                .FirstOrDefaultAsync();

            if (defaultBank == null)
            {
                return NoContent();
            }

            if (defaultBank.isDefault == true)
            {
                return NoContent();
            }

            List<employee_bank_details> banks = await _context
                .employee_bank_details.Where(x => x.itsId == defaultBank.itsId)
                .ToListAsync();
            defaultBank.isDefault = true;
            foreach (var bank in banks)
            {
                if (bank.id != id)
                {
                    bank.isDefault = false;
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool bankDetailsExists(int id)
        {
            return (_context.employee_bank_details?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpGet("selfSalaryDetails")]
        public async Task<ActionResult<EmployeeSalaryDetailsModel>> GetSelfSalaryDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .employee_salary.Where(x => x.itsId == authUser.ItsId)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeeSalaryDetailsModel>(khidmat_guzaar);
        }

        [HttpPut("updateSelfSalaryDetails")]
        public async Task<IActionResult> PutupdateSelfSalaryDetails(EmployeeSalaryDetailsModel kg)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_salary khidmat_guzaar = _mapper.Map<employee_salary>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!salaryDetailsExists(authUser.ItsId))
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

        private bool salaryDetailsExists(int id)
        {
            return (_context.employee_salary?.Any(e => e.itsId == id)).GetValueOrDefault();
        }

        [HttpGet("selfAssessmentDetails")]
        public async Task<ActionResult<EmployeeSelfAssessmentModel>> GetSelfAssessmentDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .kg_self_assessment.Where(x => x.itsId == authUser.ItsId)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeeSelfAssessmentModel>(khidmat_guzaar);
        }

        [HttpPut("updateSelfAssessmentDetails")]
        public async Task<IActionResult> PutupdateSelfAssessmentDetails(
            EmployeeSelfAssessmentModel kg
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            kg_self_assessment khidmat_guzaar = _mapper.Map<kg_self_assessment>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!selfAssessmentDetailsExists(authUser.ItsId))
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

        private bool selfAssessmentDetailsExists(int id)
        {
            return (_context.kg_self_assessment?.Any(e => e.itsId == id)).GetValueOrDefault();
        }

        [HttpGet("selfDeptSalaryDetails")]
        public async Task<
            ActionResult<List<EmployeeDeptSalaryModel>>
        > GetSelfEmployeeSalaryDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .employee_dept_salary.Where(x => x.itsId == authUser.ItsId)
                .ToListAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<EmployeeDeptSalaryModel>>(khidmat_guzaar);
        }

        [HttpPut("updateSelfEmployeeSalaryDetails")]
        public async Task<IActionResult> PutupdateSelEmployeeSalaryDetails(
            EmployeeDeptSalaryModel kg
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_dept_salary khidmat_guzaar = _mapper.Map<employee_dept_salary>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!selfDeptSalaryDetailsExists(kg.itsId, kg.deptVenueId))
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

        private bool selfDeptSalaryDetailsExists(int id, int deptId)
        {
            return (
                _context.employee_dept_salary?.Any(e => e.itsId == id && e.deptVenueId == deptId)
            ).GetValueOrDefault();
        }

        [HttpGet("selfFamilyDetails")]
        public async Task<ActionResult<EmployeeFamilyDetailsModel>> GetSelfFamilyDetails()
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context
                .employee_family_details.Where(x => x.itsId == authUser.ItsId)
                .FirstOrDefaultAsync();

            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            return _mapper.Map<EmployeeFamilyDetailsModel>(khidmat_guzaar);
        }

        [HttpPut("updateSelfFamilyDetails")]
        public async Task<IActionResult> PutupdateSelfFamilyDetails(EmployeeSelfAssessmentModel kg)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            employee_family_details khidmat_guzaar = _mapper.Map<employee_family_details>(kg);

            if (authUser.ItsId != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!selfFamilyDetailsExists(authUser.ItsId))
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

        private bool selfFamilyDetailsExists(int id)
        {
            return (_context.employee_family_details?.Any(e => e.itsId == id)).GetValueOrDefault();
        }

        // PUT: v2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putkhidmat_guzaar(int id, khidmat_guzaar khidmat_guzaar)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (id != khidmat_guzaar.itsId)
            {
                return BadRequest();
            }

            _context.Entry(khidmat_guzaar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!khidmat_guzaarExists(id))
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

        // POST: v2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<khidmat_guzaar>> Postkhidmat_guzaar(
            khidmat_guzaar khidmat_guzaar
        )
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return Problem("Entity set 'MzApiDb_context.employee'  is null.");
            }
            _context.khidmat_guzaar.Add(khidmat_guzaar);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "Getkhidmat_guzaar",
                new { id = khidmat_guzaar.itsId },
                khidmat_guzaar
            );
        }

        // DELETE: v2/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletekhidmat_guzaar(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.khidmat_guzaar == null)
            {
                return NotFound();
            }
            var khidmat_guzaar = await _context.khidmat_guzaar.FindAsync(id);
            if (khidmat_guzaar == null)
            {
                return NotFound();
            }

            _context.khidmat_guzaar.Remove(khidmat_guzaar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool khidmat_guzaarExists(int id)
        {
            return (_context.khidmat_guzaar?.Any(e => e.itsId == id)).GetValueOrDefault();
        }

        [Route("v2/getallemployees")]
        [HttpGet]
        public async Task<IActionResult> getAllEmployees(
            [FromQuery] int qismId,
            [FromQuery] string employeeTypes,
            [FromQuery] string workTypes = "Fixed,Time-based,Fixed and Time-based,None"
        )
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<string> employeeType = _helperService.parseStrings(employeeTypes);
            List<string> workType = _helperService.parseStrings(workTypes);
            try
            {
                branch_user b = await _context
                    .branch_user.Where(x =>
                        x.itsId == authUser.ItsId && x.deptVenue.Any(y => y.qismId == qismId)
                    )
                    .Include(x => x.deptVenue.Where(x => x.qismId == qismId))
                    .Include(x => x.venues.Where(x => x.qismId == qismId))
                    .FirstOrDefaultAsync();
                if (b == null)
                {
                    return BadRequest(
                        "You do not have access to any program from this Qism al-Tahfeez"
                    );
                }

                var kg2 = _context
                    .khidmat_guzaar.Where(kg =>
                        kg.mauzeNavigation.qismId == qismId
                        && b.venues.Contains(kg.mauzeNavigation)
                        && employeeType.Contains(kg.employeeType)
                        && workType.Contains(kg.workType)
                        && (
                            kg.employee_dept_salary.Count == 0
                            || kg.employee_dept_salary.Any(z => b.deptVenue.Contains(z.deptVenue))
                        )
                    )
                    .Include(its => its.employee_academic_details)
                    .Include(its => its.mauzeNavigation)
                    .Include(its => its.employee_bank_details)
                    .Include(its => its.employee_dept_salary)
                    .ThenInclude(x => x.salaryType)
                    .Include(its => its.employee_dept_salary)
                    .ThenInclude(x => x.deptVenue)
                    .Include(its => its.employee_khidmat_details)
                    .Include(its => its.employee_passport_details)
                    .Include(its => its.employee_salary)
                    .ToList();

                List<EmployeeModel> kh = new List<EmployeeModel>();
                int its = 0;
                try
                {
                    foreach (var item in kg2)
                    {
                        its = item.itsId;
                        kh.Add(Translator.khtoModel(item, false));
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                kh.Sort(
                    (x, y) =>
                    {
                        // Sort by activeStatus in descending order
                        int activeStatusComparison = (y.basicDetails.activeStatus ?? 0).CompareTo(
                            x.basicDetails.activeStatus ?? 0
                        );
                        if (activeStatusComparison != 0)
                            return activeStatusComparison;

                        // Sort by mz_idara based on idaraSortingOrder
                        int indexX = _globalConstants.idaraSortingOrder.IndexOf(
                            x.basicDetails.mz_idara
                        );
                        int indexY = _globalConstants.idaraSortingOrder.IndexOf(
                            y.basicDetails.mz_idara
                        );
                        if (indexX == -1)
                            indexX = int.MaxValue;
                        if (indexY == -1)
                            indexY = int.MaxValue;
                        if (indexX != indexY)
                        {
                            return indexX.CompareTo(indexY);
                        }

                        // Sort by batchId in descending order
                        int batchIdComparison = (y.academicDetails?.batchId ?? 0).CompareTo(
                            x.academicDetails?.batchId ?? 0
                        );
                        if (batchIdComparison != 0)
                        {
                            return batchIdComparison;
                        }

                        // Sort by category in ascending order
                        int categoryComparison = string.Compare(
                            x.academicDetails?.category,
                            y.academicDetails?.category,
                            StringComparison.Ordinal
                        );
                        if (categoryComparison != 0)
                        {
                            return categoryComparison;
                        }

                        // Sort by farigDarajah in descending order
                        int farigDarajahComparison = (
                            y.academicDetails?.farigDarajah ?? 0
                        ).CompareTo(x.academicDetails?.farigDarajah ?? 0);
                        if (farigDarajahComparison != 0)
                        {
                            return farigDarajahComparison;
                        }

                        // Sort by age in descending order
                        return (y.basicDetails.age ?? 0).CompareTo(x.basicDetails.age ?? 0);
                    }
                );

                return Ok(kh);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            // return Ok(ServiceFactory.GetUserService().getAllUsers());
        }

        [Route("v2/getallemployees/admin")]
        [HttpGet]
        public async Task<IActionResult> getAllEmployeesforadmin(
            [FromQuery] string employeeTypes,
            [FromQuery] string workTypes = "Fixed,Time-based,Fixed and Time-based,None"
        )
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<string> employeeType = _helperService.parseStrings(employeeTypes);
            List<string> workType = _helperService.parseStrings(workTypes);
            try
            {
                var kg2 = _context
                    .khidmat_guzaar.Where(kg =>
                        employeeType.Contains(kg.employeeType) && workType.Contains(kg.workType)
                    )
                    .Include(its => its.employee_academic_details)
                    .Include(its => its.mauzeNavigation)
                    .Include(its => its.employee_bank_details)
                    .Include(its => its.employee_dept_salary)
                    .ThenInclude(x => x.salaryType)
                    .Include(its => its.employee_dept_salary)
                    .ThenInclude(x => x.deptVenue)
                    .Include(its => its.qualification)
                    .Include(its => its.employee_e_attendence)
                    .Include(its => its.employee_khidmat_details)
                    .Include(its => its.employee_passport_details)
                    .Include(its => its.employee_salary)
                    .Include(its => its.employee_family_details)
                    .ToList();
                List<int> itsId = kg2.Select(x => x.itsId).ToList();
                List<kg_faimalydetails_its> kg_Faimalydetails = _context
                    .kg_faimalydetails_its.Where(x => itsId.Contains(x.hofItsId ?? 0))
                    .ToList();
                // List<EmployeeModel> kh = kg2.Select(x => Translator.khtoModel(x, false)).ToList();

                List<EmployeeModel> kh = new List<EmployeeModel>();

                foreach (var item in kg2)
                {
                    EmployeeModel emp = Translator.khtoModel(item, false);
                    int acdYr = _helperService.getAcedemicYear(DateTime.Now).acedemicYear;
                    int khidmatDuration = acdYr - (emp.khidmatDetails?.khidmatYear ?? acdYr);

                    if (
                        emp.basicDetails?.employeeType == "Khidmatguzaar"
                        && emp.extraDetails != null
                    )
                    {
                        emp.extraDetails.khidmatDuration = khidmatDuration;
                    }

                    // emp.extraDetails = new EmployeeExtraDetailsModel();
                    var familyList = kg_Faimalydetails
                        .Where(x => x.hofItsId == item.itsId)
                        .ToList();
                    emp.extraDetails.spouseITS =
                        familyList
                            .Where(x => x.relation == "Wife")
                            .Select(x => x.itsId)
                            .FirstOrDefault() ?? 0;
                    emp.extraDetails.spouseName =
                        familyList
                            .Where(x => x.relation == "Wife")
                            .Select(x => x.name)
                            .FirstOrDefault() ?? "";
                    emp.extraDetails.childCount = familyList
                        .Where(x => x.relation == "Son" || x.relation == "Daughter")
                        .Count();

                    kh.Add(emp);
                }

                kh.Sort(
                    (x, y) =>
                    {
                        // Sort by activeStatus in descending order
                        int activeStatusComparison = (y.basicDetails.activeStatus ?? 0).CompareTo(
                            x.basicDetails.activeStatus ?? 0
                        );
                        if (activeStatusComparison != 0)
                        {
                            return activeStatusComparison;
                        }

                        // Sort by mz_idara based on idaraSortingOrder
                        int indexX = _globalConstants.idaraSortingOrder.IndexOf(
                            x.basicDetails.mz_idara
                        );
                        int indexY = _globalConstants.idaraSortingOrder.IndexOf(
                            y.basicDetails.mz_idara
                        );
                        if (indexX == -1)
                        {
                            indexX = int.MaxValue;
                        }
                        if (indexY == -1)
                        {
                            indexY = int.MaxValue;
                        }
                        if (indexX != indexY)
                        {
                            return indexX.CompareTo(indexY);
                        }

                        // Sort by batchId in descending order
                        int batchIdComparison = (y.academicDetails?.batchId ?? 0).CompareTo(
                            x.academicDetails?.batchId ?? 0
                        );
                        if (batchIdComparison != 0)
                        {
                            return batchIdComparison;
                        }

                        // Sort by category in ascending order
                        int categoryComparison = string.Compare(
                            x.academicDetails?.category,
                            y.academicDetails?.category,
                            StringComparison.Ordinal
                        );
                        if (categoryComparison != 0)
                        {
                            return categoryComparison;
                        }

                        // Sort by farigDarajah in descending order
                        int farigDarajahComparison = (
                            y.academicDetails?.farigDarajah ?? 0
                        ).CompareTo(x.academicDetails?.farigDarajah ?? 0);
                        if (farigDarajahComparison != 0)
                        {
                            return farigDarajahComparison;
                        }

                        // Sort by age in descending order
                        return (y.basicDetails.age ?? 0).CompareTo(x.basicDetails.age ?? 0);
                    }
                );

                return Ok(kh);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            // return Ok(ServiceFactory.GetUserService().getAllUsers());
        }

        [Route("v3/getallemployees/admin")]
        [HttpGet]
        public async Task<IActionResult> getAllEmployeesadmin()
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
           
            try
            {
                var users = _context.user.ToList();
                var kgs = _context.khidmat_guzaar.Where(x => x.activeStatus == true).ToList();

                List<EmployeeBasicDetailsModel> kh = new List<EmployeeBasicDetailsModel>();

                foreach (var kg in kgs)
                {
                    var user = users.Where(x => x.ItsId == kg.itsId).FirstOrDefault();

                    if(user != null)
                    {
                        var userss = _mapper.Map<EmployeeBasicDetailsModel>(kg);

                        kh.Add(userss);
                    }
                    
                    
                }                

                return Ok(kh);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            // return Ok(ServiceFactory.GetUserService().getAllUsers());
        }

        [Route("v3/addemployeebulk")]
        [HttpPost]
        public async Task<IActionResult> addEmployeeBulkV3(List<EmployeeModel> emps)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string error = "";

            try
            {
                var authkg = await _context
                    .khidmat_guzaar.AsNoTracking()
                    .Where(x => x.itsId == authUser.ItsId)
                    .Include(x => x.employee_dept_salary)
                    .Include(x => x.branch_user)
                    .ThenInclude(user => user.deptVenue)
                    .FirstOrDefaultAsync();

                List<int> itsIds = emps.Select(x => x.basicDetails.itsId).ToList();

                var khidmat_Guzaars = await _context
                    .khidmat_guzaar.AsNoTracking()
                    .Where(x => itsIds.Contains(x.itsId))
                    .Include(x => x.employee_dept_salary)
                    .Include(x => x.employee_bank_details)
                    .Include(x => x.employee_academic_details)
                    .Include(x => x.employee_salary)
                    .Include(x => x.mauzeNavigation)
                    .Include(x => x.wafdprofile_maqaraat_teacher)
                    .ToListAsync();

                foreach (var emp in emps)
                {
                    if (emp.basicDetails?.itsId == 0)
                    {
                        var counter =
                            await _context
                                .current_counter.Where(x => x.name == "nonMuminITS")
                                .FirstOrDefaultAsync() ?? new current_counter();

                        emp.basicDetails.itsId = counter.count++;
                        _context.Entry(counter).State = EntityState.Modified;
                    }

                    var existingKg = khidmat_Guzaars.FirstOrDefault(x =>
                        x.itsId == emp.basicDetails.itsId
                    );

                    if (existingKg != null)
                    {
                        // Track changes in the main entity
                        await _helperService.LogChanges<khidmat_guzaar>(
                            existingKg,
                            _mapper.Map<khidmat_guzaar>(emp.basicDetails),
                            authUser.ItsId,
                            emp.basicDetails?.itsId.ToString()
                        );
                        _mapper.Map(emp.basicDetails, existingKg);
                        existingKg.UpdatedOn = DateTime.Now;
                        _context.Entry(existingKg).State = EntityState.Modified;

                        // Clear and re-add related entities for `employee_bank_details`
                        var existingBankDetails = _context
                            .employee_bank_details.Where(x => x.itsId == emp.basicDetails.itsId)
                            .ToList();
                        _context.employee_bank_details.RemoveRange(existingBankDetails);

                        foreach (var bankDetail in emp.bankDetails)
                        {
                            var bankDetailEntity = _mapper.Map<employee_bank_details>(bankDetail);
                            bankDetailEntity.itsId = emp.basicDetails.itsId;
                            await _helperService.LogChanges<employee_bank_details>(
                                new employee_bank_details(),
                                bankDetailEntity,
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            await _context.employee_bank_details.AddAsync(bankDetailEntity);
                        }

                        // Clear and re-add related entities for `employee_dept_salary`
                        var existingDeptSalaries = _context
                            .employee_dept_salary.Where(x => x.itsId == emp.basicDetails.itsId)
                            .ToList();
                        _context.employee_dept_salary.RemoveRange(existingDeptSalaries);

                        foreach (var deptSalary in emp.deptSalaries)
                        {
                            var deptSalaryEntity = _mapper.Map<employee_dept_salary>(deptSalary);
                            deptSalaryEntity.itsId = emp.basicDetails.itsId;
                            await _helperService.LogChanges<employee_dept_salary>(
                                new employee_dept_salary(),
                                deptSalaryEntity,
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            await _context.employee_dept_salary.AddAsync(deptSalaryEntity);
                        }

                        // Handle academic details
                        var academicEntity = await _context
                            .employee_academic_details.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.itsId == emp.basicDetails.itsId);

                        if (academicEntity != null)
                        {
                            await _helperService.LogChanges<employee_academic_details>(
                                academicEntity,
                                _mapper.Map<employee_academic_details>(emp.academicDetails),
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            _context.employee_academic_details.Remove(academicEntity);
                        }

                        if (emp.academicDetails != null)
                        {
                            var newAcademicEntity = _mapper.Map<employee_academic_details>(
                                emp.academicDetails
                            );
                            newAcademicEntity.itsId = emp.basicDetails.itsId;
                            await _context.employee_academic_details.AddAsync(newAcademicEntity);
                        }


                        // Handle salary details
                        var salaryEntity = await _context
                            .employee_salary.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.itsId == emp.basicDetails.itsId);

                        if (salaryEntity != null)
                        {
                            await _helperService.LogChanges<employee_salary>(
                                salaryEntity,
                                _mapper.Map<employee_salary>(emp.employeeSalary),
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            _context.employee_salary.Remove(salaryEntity);
                        }

                        var newSalaryEntity = _mapper.Map<employee_salary>(emp.employeeSalary);
                        newSalaryEntity.itsId = emp.basicDetails.itsId;
                        await _context.employee_salary.AddAsync(newSalaryEntity);
                    }
                    else
                    {
                        // New `khidmat_guzaar` entry
                        var newKg = Translator.modelToKg(emp);
                        await _helperService.LogChanges<khidmat_guzaar>(
                            new khidmat_guzaar(),
                            newKg,
                            authUser.ItsId,
                            emp.basicDetails.itsId.ToString()
                        );
                        await _context.khidmat_guzaar.AddAsync(newKg);
                        if (emp.bankDetails == null)
                        {
                            emp.bankDetails = new List<EmployeeBankDetailsModel>();
                        }
                        foreach (var bankDetail in emp.bankDetails)
                        {
                            var bankDetailEntity = _mapper.Map<employee_bank_details>(bankDetail);
                            bankDetailEntity.itsId = emp.basicDetails.itsId;
                            await _helperService.LogChanges<employee_bank_details>(
                                new employee_bank_details(),
                                bankDetailEntity,
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            await _context.employee_bank_details.AddAsync(bankDetailEntity);
                        }

                        if (emp.deptSalaries == null)
                        {
                            emp.deptSalaries = new List<EmployeeDeptSalaryModel>();
                        }
                        foreach (var deptSalary in emp.deptSalaries)
                        {
                            var deptSalaryEntity = _mapper.Map<employee_dept_salary>(deptSalary);
                            deptSalaryEntity.itsId = emp.basicDetails.itsId;
                            await _helperService.LogChanges<employee_dept_salary>(
                                new employee_dept_salary(),
                                deptSalaryEntity,
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            await _context.employee_dept_salary.AddAsync(deptSalaryEntity);
                        }

                        if (emp.academicDetails != null)
                        {
                            var academicEntity = _mapper.Map<employee_academic_details>(
                                emp.academicDetails
                            );
                            academicEntity.itsId = emp.basicDetails.itsId;
                            await _helperService.LogChanges<employee_academic_details>(
                                new employee_academic_details(),
                                academicEntity,
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            await _context.employee_academic_details.AddAsync(academicEntity);
                        }

                        if (emp.employeeSalary == null)
                        {
                            emp.employeeSalary = new EmployeeSalaryDetailsModel();
                        }
                        var salaryEntity = _mapper.Map<employee_salary>(emp.employeeSalary);
                        salaryEntity.itsId = emp.basicDetails.itsId;
                        await _helperService.LogChanges<employee_salary>(
                            new employee_salary(),
                            salaryEntity,
                            authUser.ItsId,
                            emp.basicDetails.itsId.ToString()
                        );
                        await _context.employee_salary.AddAsync(salaryEntity);
                    }
                }

                await _context.SaveChangesAsync(); // Consolidated SaveChangesAsync() after all changes

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency conflict occurred. Please retry.");
            }
            catch (DbEntityValidationException dbEx)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage.AppendLine(
                            $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}"
                        );
                    }
                }
                return BadRequest(errorMessage.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("hk/addemployeebulk")]
        [HttpPost]
        public async Task<IActionResult> addEmployeesBulk(List<EmployeeModel> emps)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<int> itsIds = emps.Select(x => x.basicDetails.itsId).ToList();

            List<int?> venue = emps.Select(x => x.basicDetails.mauze).ToList();

            List<khidmat_guzaar>? kg = _context.khidmat_guzaar.Where(x => itsIds.Contains(x.itsId)).ToList();
            
            List<user> users = _context.user.Where(x => itsIds.Contains(x.ItsId)).ToList();            

            List<registrationform_dropdown_set> rds = _context.registrationform_dropdown_set.ToList();

            List<user_deptvenue> udvss = _context.user_deptvenue.Where(x => itsIds.Contains(x.itsId)).ToList();

            foreach (var emp in emps)
            {
                var basics = emp.basicDetails;

                var isExistingKg = kg.FirstOrDefault(x => x.itsId == basics.itsId);

                if(isExistingKg == null)
                {
                    await _helperService.LogChanges<khidmat_guzaar>(
                        new khidmat_guzaar(),
                        _mapper.Map<khidmat_guzaar>(basics),
                        authUser.ItsId,
                        basics.itsId.ToString()
                    );

                    await _context.khidmat_guzaar.AddAsync(Translator.modelToKg(emp));
                    await _context.SaveChangesAsync();                    
                }
                else
                {
                    var updatedKg = Translator.modelToKg(emp);

                    _context.Entry(isExistingKg).CurrentValues.SetValues(updatedKg);
                }

                var isExistingUser = users.FirstOrDefault(x => x.ItsId == basics.itsId);

                if (isExistingUser == null)
                {
                    _context.user.Add(new user
                    {
                        Username = basics.fullName,
                        ItsId = basics.itsId,
                        Password = basics.itsId.ToString(),
                        DID = 0,
                        EmailId = basics.emailAddress,
                        Mobile = basics.mobileNo,
                        roleId = basics.roleId,
                        Accesslevel = "",
                        loginStatus = ""
                    });
                }
                else
                {
                    isExistingUser.Username = basics.fullName;
                    isExistingUser.EmailId = basics.emailAddress;
                    isExistingUser.Mobile = basics.mobileNo;
                    isExistingUser.roleId = basics.roleId;
                }
                await _context.SaveChangesAsync();

                //foreach(var mauze in basics.mawaze)
                //{
                //    foreach (var pset in basics.psetId)
                //    {
                //        user_deptvenue udv = new user_deptvenue();

                //        var ps = rds.FirstOrDefault(x => x.subprogramId == pset && x.venueId == mauze);

                //        udv.deptVenueId = (int)ps.deptVenueId;
                //        udv.psetId = ps.id;
                //        udv.itsId = basics.itsId;

                //        await _context.user_deptvenue.AddAsync(udv);
                //        _context.SaveChangesAsync();
                //    }
                //}
            }
            return Ok();
        }

        [Route("hk/assignUserDept")]
        [HttpPost]
        public async Task<IActionResult> assignUserDept(List<userDept> models)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<int> itsIds = models.Select(x => x.itsId).ToList();

            List<user> usrs = _context.user.Where(x => itsIds.Contains(x.ItsId)).ToList();

            List<registrationform_dropdown_set> rdss = _context.registrationform_dropdown_set.ToList();

            List<user_deptvenue> udvss = _context.user_deptvenue.ToList();

            List<student_registration_rights> srrss = _context.student_registration_rights.ToList();

            try
            {
                foreach (var model in models)
                {
                    foreach (var venue in model.venueIds)
                    {
                        List<registrationform_dropdown_set> rds = rdss.Where(x => x.venueId == venue).ToList();

                        foreach (var clsId in model.classId)
                        {
                            registrationform_dropdown_set rd = rds.FirstOrDefault(x => x.subprogramId == clsId);

                            user_deptvenue udvs = udvss.FirstOrDefault(x => x.itsId == model.itsId && x.deptVenueId == rd.deptVenueId && x.psetId == rd.id);

                            if (udvs == null)
                            {
                                user_deptvenue udv = new user_deptvenue();

                                udv.deptVenueId = (int)rd.deptVenueId;
                                udv.psetId = rd.id;
                                udv.itsId = model.itsId;

                                _context.user_deptvenue.Add(udv);

                            }

                            student_registration_rights srrs = _context.student_registration_rights.Where(x => x.itsId == model.itsId && x.programSetId == rd.id).FirstOrDefault();
                            if (srrs == null)
                            {
                                student_registration_rights srr = new student_registration_rights();

                                srr.itsId = model.itsId;
                                srr.programSetId = rd.id;

                                _context.student_registration_rights.Add(srr);

                            }


                        }
                    }

                }
                _context.SaveChanges();

                return Ok(new {message = "Assigned Successfully."});
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.ToString() });
            }
            

        }

        [Route("deleteUserDeptMapping/{id}")]
        [HttpGet]
        public async Task<IActionResult> deleteUserDeptMapping(int id)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            user_deptvenue udvs = _context.user_deptvenue.FirstOrDefault(x => x.id == id);

            student_registration_rights srrs = _context.student_registration_rights.FirstOrDefault(x => x.itsId == udvs.itsId && x.programSetId == udvs.psetId);

            if(udvs != null)
            {
                _context.user_deptvenue.Remove(udvs);
                _context.SaveChanges();
            }

            if (srrs != null)
            {
                _context.student_registration_rights.Remove(srrs);
                _context.SaveChanges();
            }
            return Ok(new {message = "Deleted Succefully." });
        }

        [Route("v2/addemployeebulk")]
        [HttpPost]
        public async Task<IActionResult> addEmployeeBulk(List<EmployeeModel> emps)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            string error = "";
            string api = "v2/addemployee";

            //try
            //{
                khidmat_guzaar? authkg = await _context
                    .khidmat_guzaar.Where(x => x.itsId == authUser.ItsId)
                    .Include(x => x.employee_dept_salary)
                    .Include(x => x.branch_user)
                    .ThenInclude(user => user.deptVenue)
                    .FirstOrDefaultAsync();

                List<int> itsIds = emps.Select(x => x.basicDetails.itsId).ToList();

                List<khidmat_guzaar> khidmat_Guzaars = await _context
                    .khidmat_guzaar.Where(x => itsIds.Contains(x.itsId))
                    .Include(x => x.employee_dept_salary)
                    .Include(x => x.employee_bank_details)
                    .Include(x => x.employee_academic_details)
                    .Include(x => x.employee_salary)
                    .Include(x => x.mauzeNavigation)
                    .Include(x => x.wafdprofile_maqaraat_teacher)
                    .ToListAsync();

                employee_dept_salary? default_emp_dept_salary = null;

                if (authkg != null && authkg.branch_user?.deptVenue?.Count == 1)
                {
                    default_emp_dept_salary = new employee_dept_salary
                    {
                        deptVenue =
                            authkg.branch_user.deptVenue.FirstOrDefault() ?? new dept_venue(),
                        deptVenueId = authkg.branch_user.deptVenue.FirstOrDefault()?.id ?? 0,
                        hasSalary = true,
                        salaryTypeId = 1,
                        isHijriSalary = false,
                        salaryAmount = 0,
                        workingMin = 480
                    };
                }

                foreach (EmployeeModel emp in emps)
                {
                    if (emp.bankDetails == null)
                    {
                        emp.bankDetails = new List<EmployeeBankDetailsModel>();
                    }

                    if (emp.deptSalaries == null)
                    {
                        emp.deptSalaries = new List<EmployeeDeptSalaryModel>();
                    }

                    if (emp.employeeSalary == null)
                    {
                        emp.employeeSalary = new EmployeeSalaryDetailsModel();
                    }

                    if (emp.academicDetails == null)
                    {
                        emp.academicDetails = new EmployeeAcademicDetailsModel();
                    }

                    if (emp.basicDetails?.itsId == 0)
                    {
                        if (emp.basicDetails.isMumin != false)
                        {
                            error += "Its id not found in given employee,";
                            continue;
                        }

                        current_counter c =
                            await _context
                                .current_counter.Where(x => x.name == "nonMuminITS")
                                .FirstOrDefaultAsync() ?? new current_counter();
                        emp.basicDetails.itsId = c.count;

                        c.count++;
                        await _context.SaveChangesAsync();
                    }

                    khidmat_guzaar? kgs = khidmat_Guzaars
                        .Where(x => x.itsId == emp.basicDetails.itsId)
                        .FirstOrDefault();

                    if (kgs != null)
                    {
                        var users = _context.user.Where(x => x.ItsId == emp.basicDetails.itsId).FirstOrDefault();

                        if (users == null)
                        {
                            _context.user.Add(new user
                            {
                                Username = emp.basicDetails.fullName,
                                ItsId = emp.basicDetails.itsId,
                                Password = emp.basicDetails.itsId.ToString(),
                                Accesslevel = "",
                                DID = 0,
                                EmailId = emp.basicDetails.emailAddress,
                                Mobile = emp.basicDetails.mobileNo,
                                roleId = emp.basicDetails.roleId,
                                loginStatus = "",
                            });
                        }
                        else
                        {
                            users.Username = emp.basicDetails.fullName;
                            users.EmailId = emp.basicDetails.emailAddress;
                            users.Mobile = emp.basicDetails.mobileNo;
                            users.roleId = emp.basicDetails.roleId;
                        }
                        _context.SaveChanges();
                        //cache.DeleteItem("getAllEmployeeData" + kgs.employeeType + ":" + authUser.qismId);

                        await _helperService.LogChanges<khidmat_guzaar>(
                            kgs,
                            _mapper.Map<khidmat_guzaar>(emp.basicDetails),
                            authUser.ItsId,
                            emp.basicDetails?.itsId.ToString()
                        );
                        try
                        {
                            byte[] binaryData = Convert.FromBase64String(
                                emp.basicDetails?.photoBase64 ?? ""
                            );
                            MemoryStream stream = new MemoryStream(binaryData);
                            emp.basicDetails.photo = await _helperService.UploadFileToS3(
                                stream,
                                emp.basicDetails.itsId.ToString() + ".jpg",
                                "uploads/Its_Photos"
                            );
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                        kgs.activeStatus = emp.basicDetails?.activeStatus == 1 ? true : false;
                        kgs.bloodGroup = emp.basicDetails?.bloodGroup;
                        kgs.age = emp.basicDetails?.age;
                        kgs.currentAddress = emp.basicDetails?.currentAddress;
                        kgs.c_codeMobile = emp.basicDetails?.c_codeMobile;
                        kgs.c_codeWhatsapp = emp.basicDetails?.c_codeWhatsapp;
                        kgs.dawat_title = emp.basicDetails?.dawat_title;
                        kgs.dobGregorian = emp.basicDetails?.dobGregorian;
                        kgs.dobHijri = emp.basicDetails?.dobHijri;
                        kgs.dojGregorian = emp.basicDetails?.dojGregorian;
                        kgs.dojHijri = emp.basicDetails?.dojHijri;
                        kgs.domicileAddressParents = emp.basicDetails?.domicileAddressParents;
                        kgs.domicileParent = emp.basicDetails?.domicileParent;
                        kgs.emailAddress = emp.basicDetails?.emailAddress;
                        kgs.employeeType = emp.basicDetails?.employeeType;
                        kgs.fullName = emp.basicDetails?.fullName;
                        kgs.fullNameArabic = emp.basicDetails?.fullNameArabic;
                        kgs.haddiyatYear = emp.basicDetails?.haddiyatYear;
                        kgs.its_idaras = emp.basicDetails?.its_idaras;
                        kgs.its_preferredIdara = emp.basicDetails?.its_preferredIdara;
                        kgs.jamaat = emp.basicDetails?.jamaat;

                        kgs.jamiat = emp.basicDetails?.jamiat;
                        kgs.mafsuhiyatYear = emp.basicDetails?.mafsuhiyatYear;
                        kgs.maritalStatus = emp.basicDetails?.maritalStatus;
                        kgs.mobileNo = emp.basicDetails?.mobileNo;
                        kgs.muqamArabic = emp.basicDetails?.muqamArabic;
                        kgs.muqam = emp.basicDetails?.muqam;
                        kgs.mz_idara = emp.basicDetails?.mz_idara;
                        kgs.nationality = emp.basicDetails?.nationality;
                        kgs.officialEmailAddress = emp.basicDetails?.officialEmailAddress;
                        kgs.personalHouseAddress = emp.basicDetails?.personalHouseAddress;
                        kgs.personalHouseArea = emp.basicDetails?.personalHouseArea;
                        kgs.personalHouseStatus = emp.basicDetails?.personalHouseStatus;
                        kgs.personalHouseType = emp.basicDetails?.personalHouseType;
                        if (emp.basicDetails?.photoBase64?.Length < 10000)
                        {
                            kgs.photoBase64 = emp.basicDetails?.photoBase64;
                        }
                        kgs.photo = emp.basicDetails?.photo;

                        kgs.UpdatedOn = DateTime.Now;
                        kgs.RfId = emp.basicDetails?.RfId ?? "";

                        kgs.watan = emp.basicDetails?.watan;
                        kgs.watanAdress = emp.basicDetails?.watanAdress;
                        kgs.watanArabic = emp.basicDetails?.watanArabic;
                        kgs.whatsappNo = emp.basicDetails?.whatsappNo;
                        kgs.mauze = emp.basicDetails?.mauze;
                        kgs.gender = emp.basicDetails?.gender;
                        kgs.workType = emp.basicDetails?.workType;
                        kgs.designation = emp.basicDetails?.designation;
                        kgs.salaryCalender = emp.basicDetails?.salaryCalender;

                        await _context.SaveChangesAsync();

                        if (emp.extraDetails?.mauzeChanged == true)
                        {
                            emp.deptSalaries = new List<EmployeeDeptSalaryModel>();

                            emp.employeeSalary.grossSalary = 0;
                            emp.employeeSalary.isMahadSalary = false;
                            emp.employeeSalary.arrears = 0;
                            emp.employeeSalary.qardanHasanahNonRefundable = 0;
                            emp.employeeSalary.qardanHasanahRefundable = 0;
                            emp.employeeSalary.withHoldings = 0;
                            emp.employeeSalary.currency = null;
                            emp.employeeSalary.sabeel = 0;
                            emp.employeeSalary.marafiqKhairiyah = 0;
                            emp.employeeSalary.mumbaiAllowance = 0;
                            emp.employeeSalary.fmbAllowance = 0;
                            emp.employeeSalary.marriageAllowance = 0;
                            emp.employeeSalary.rentAllowance = 0;
                            emp.employeeSalary.conveyanceAllowance = 0;

                            emp.academicDetails.maqaraatTeacherIts = null;

                            List<employee_academic_details> academicData = _context
                                .employee_academic_details.Where(x =>
                                    x.maqaraatTeacherIts == kgs.itsId
                                )
                                .ToList();
                            foreach (var item in academicData)
                            {
                                item.maqaraatTeacherIts = null;
                            }

                            wafdprofile_maqaraat_teacher wpmt =
                                kgs.wafdprofile_maqaraat_teacher.FirstOrDefault();

                            if (wpmt != null)
                            {
                                _context.wafdprofile_maqaraat_teacher.Remove(wpmt);
                            }
                        }

                        employee_academic_details? ead = await _context
                            .employee_academic_details.Where(x => x.itsId == kgs.itsId)
                            .FirstOrDefaultAsync();
                        if (ead != null)
                        {
                            kgs.employee_academic_details.its = null;
                            await _helperService.LogChanges<employee_academic_details>(
                                kgs.employee_academic_details,
                                _mapper.Map<employee_academic_details>(emp.academicDetails),
                                authUser.ItsId,
                                emp.basicDetails?.itsId.ToString() ?? ""
                            );
                            _context.employee_academic_details.Remove(ead);
                        }

                        if (emp?.academicDetails != null)
                        {
                            emp.academicDetails.itsId = emp.basicDetails.itsId;
                            await _context.employee_academic_details.AddAsync(
                                Translator.modelToAd(emp)
                            );
                        }

                        await _helperService.LogChanges<employee_bank_details>(
                            kgs.employee_bank_details.ToList(),
                            _mapper.Map<List<employee_bank_details>>(emp.bankDetails),
                            authUser.ItsId,
                            emp.basicDetails.itsId.ToString()
                        );

                        emp.bankDetails.ForEach(async x =>
                        {
                            try
                            {
                                byte[] binaryData1 = Convert.FromBase64String(
                                    x.chequeAttachment ?? ""
                                );
                                MemoryStream stream1 = new MemoryStream(binaryData1);
                                byte[] binaryData2 = Convert.FromBase64String(
                                    x.panCardAttachment ?? ""
                                );
                                MemoryStream stream2 = new MemoryStream(binaryData2);
                                if (!string.IsNullOrEmpty(x.chequeAttachment))
                                {
                                    x.chequeAttachment = await _helperService.UploadFileToS3(
                                        stream1,
                                        x.chequeAttachmentFileName,
                                        "uploads/cheque"
                                    );
                                }
                                if (!string.IsNullOrEmpty(x.panCardAttachment))
                                {
                                    x.panCardAttachment = await _helperService.UploadFileToS3(
                                        stream2,
                                        x.panCardAttachmentFileName,
                                        "uploads/pancard"
                                    );
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        });

                        List<employee_bank_details> ebdExisting = await _context
                            .employee_bank_details.Where(x => x.itsId == kgs.itsId)
                            .ToListAsync();

                        ebdExisting.ForEach(x =>
                        {
                            _context.employee_bank_details.Remove(x);
                        });

                        List<employee_bank_details> ebds = Translator.modelToBd(emp);

                        ebds.ForEach(async x =>
                        {
                            x.itsId = emp.basicDetails.itsId;
                            if (x.chequeAttachment != null && x.chequeAttachment?.Length > 250)
                            {
                                x.chequeAttachment = await _helperService.UploadFileToS3(
                                    new MemoryStream(Convert.FromBase64String(x.chequeAttachment)),
                                    x.itsId + "-" + "cancelChq",
                                    "uploads/cheque"
                                );
                            }

                            if (x.panCardAttachment != null && x.panCardAttachment?.Length > 250)
                            {
                                x.panCardAttachment = await _helperService.UploadFileToS3(
                                    new MemoryStream(Convert.FromBase64String(x.panCardAttachment)),
                                    x.itsId + "-" + "panCard",
                                    "uploads/pancard"
                                );
                            }

                            await _context.employee_bank_details.AddAsync(x);
                        });

                        employee_salary? esd = await _context
                            .employee_salary.Where(x => x.itsId == kgs.itsId)
                            .FirstOrDefaultAsync();
                        if (esd != null)
                        {
                            kgs.employee_salary.its = null;
                            await _helperService.LogChanges<employee_salary>(
                                kgs.employee_salary,
                                _mapper.Map<employee_salary>(emp.employeeSalary),
                                authUser.ItsId,
                                emp.basicDetails.itsId.ToString()
                            );
                            _context.employee_salary.Remove(esd);
                        }
                        if (emp?.employeeSalary != null)
                        {
                            emp.employeeSalary.itsId = emp.basicDetails.itsId;
                            emp.employeeSalary.currency = string.IsNullOrEmpty(
                                emp.employeeSalary.currency
                            )
                                ? kgs.mauzeNavigation.currency
                                : emp.employeeSalary.currency;
                            await _context.employee_salary.AddAsync(Translator.modelToSd(emp));
                        }
                        try
                        {
                            // await _helperService.LogChanges<employee_dept_salary>(kgs.employee_dept_salary.ToList(), _mapper.Map<List<employee_dept_salary>>(emp.deptSalaries), authUser.ItsId, emp.basicDetails.itsId.ToString());

                            List<employee_dept_salary> edsExisting = await _context
                                .employee_dept_salary.Where(x => x.itsId == kgs.itsId)
                                .ToListAsync();
                            edsExisting.ForEach(x => _context.employee_dept_salary.Remove(x));

                            await _context.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                        if (
                            default_emp_dept_salary != null
                            && (
                                emp.basicDetails.workType == "Fixed"
                                || emp.basicDetails.workType == "Fixed and Time-based"
                            )
                            && emp.deptSalaries.Count() == 0
                        )
                        {
                            //default_emp_dept_salary.itsId = emp.basicDetails.itsId;
                            emp.deptSalaries.Add(
                                new EmployeeDeptSalaryModel
                                {
                                    itsId = emp.basicDetails.itsId,
                                    deptVenueId = default_emp_dept_salary.deptVenueId,
                                    hasSalary = true,
                                    salaryTypeId = 1,
                                    isHijriSalary =
                                        emp.basicDetails.employeeType == "Khidmatguzaar",
                                    salaryAmount = emp.employeeSalary.grossSalary,
                                    workingMin = 480,
                                    workingDays = 30
                                }
                            );
                            await _context.SaveChangesAsync();
                        }

                        if (
                            (
                                emp.basicDetails.workType == "Fixed"
                                || emp.basicDetails.workType == "Fixed and Time-based"
                            )
                            && emp?.employeeSalary?.grossSalary != 0
                            && emp.deptSalaries.Count >= 1
                        )
                        {
                            if (emp.deptSalaries.Count > 0)
                            {
                                emp.deptSalaries.Last().salaryAmount =
                                    emp.employeeSalary.grossSalary;
                                emp.deptSalaries = _salaryService.costPerDept(
                                    emp.deptSalaries,
                                    true,
                                    emp.employeeSalary.grossSalary
                                );
                            }
                        }

                        List<employee_dept_salary> edsl2 = Translator.modelToLDS(emp);

                        edsl2.ForEach(async x =>
                        {
                            x.deptVenue = null;
                            x.salaryType = null;
                            x.itsId = emp.basicDetails.itsId;
                            await _context.employee_dept_salary.AddAsync(
                                new employee_dept_salary
                                {
                                    itsId = emp.basicDetails.itsId,
                                    deptVenueId = x.deptVenueId,
                                    hasSalary = x.hasSalary,
                                    salaryTypeId = x.salaryTypeId,
                                    isHijriSalary = x.isHijriSalary,
                                    salaryAmount = x.salaryAmount,
                                    workingMin = x.workingMin,
                                    workingDays = x.workingDays,
                                }
                            );
                        });

                        await _context.SaveChangesAsync();
                        //return Ok(emp.basicDetails.itsId);
                        continue;
                    }

                    #region newKG


                    //New Kg
                    emp.basicDetails.CreatedBy = authUser.Name;
                    emp.basicDetails.CreatedOn = DateTime.Now;
                    emp.basicDetails.activeStatus = 1;

                    await _helperService.LogChanges<khidmat_guzaar>(
                        new khidmat_guzaar(),
                        _mapper.Map<khidmat_guzaar>(emp.basicDetails),
                        authUser.ItsId,
                        emp.basicDetails.itsId.ToString()
                    );

                    if (!string.IsNullOrEmpty(emp.basicDetails.photoBase64))
                    {
                        try
                        {
                            byte[] binaryData = Convert.FromBase64String(
                                emp.basicDetails.photoBase64 ?? ""
                            );
                            MemoryStream stream = new MemoryStream(binaryData);
                            string respon = await _helperService.UploadFileToS3(
                                stream,
                                emp.basicDetails.itsId.ToString() + ".jpg",
                                "uploads/Its_Photos"
                            );
                            emp.basicDetails.photo = respon;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }

                    List<employee_dept_salary> edslist = new List<employee_dept_salary>();
                    foreach (EmployeeDeptSalaryModel ed in emp.deptSalaries)
                    {
                        employee_dept_salary tempEds = _mapper.Map<employee_dept_salary>(ed);
                        await _helperService.LogChanges<employee_dept_salary>(
                            new employee_dept_salary(),
                            tempEds,
                            authUser.ItsId,
                            emp.basicDetails.itsId.ToString()
                        );
                        edslist.Add(tempEds);
                    }

                    List<employee_bank_details> ebdslist = new List<employee_bank_details>();
                    foreach (EmployeeBankDetailsModel ed in emp.bankDetails)
                    {
                        employee_bank_details tempEds = _mapper.Map<employee_bank_details>(ed);
                        await _helperService.LogChanges<employee_bank_details>(
                            new employee_bank_details(),
                            tempEds,
                            authUser.ItsId,
                            emp.basicDetails.itsId.ToString()
                        );
                        ebdslist.Add(tempEds);
                    }

                    await _helperService.LogChanges<employee_academic_details>(
                        new employee_academic_details(),
                        _mapper.Map<employee_academic_details>(emp.academicDetails),
                        authUser.ItsId,
                        emp.basicDetails.itsId.ToString()
                    );

                    await _helperService.LogChanges<employee_salary>(
                        new employee_salary(),
                        _mapper.Map<employee_salary>(emp.employeeSalary),
                        authUser.ItsId,
                        emp.basicDetails.itsId.ToString()
                    );

                    await _context.khidmat_guzaar.AddAsync(Translator.modelToKg(emp));
                    await _context.SaveChangesAsync();

                    //_helperService.LogChanges<khidmat_guzaar>(new khidmat_guzaar(), _mapper.Map<khidmat_guzaar>(emp.basicDetails), authUser.ItsId, emp.basicDetails.itsId.ToString());

                    emp.academicDetails.itsId = emp.basicDetails.itsId;
                    await _context.employee_academic_details.AddAsync(Translator.modelToAd(emp));
                    await _context.SaveChangesAsync();

                    if (
                        (
                            emp.basicDetails.workType == "Fixed"
                            || emp.basicDetails.workType == "Fixed and Time-based"
                        )
                        && emp.employeeSalary.grossSalary != 0
                        && emp.deptSalaries.Count >= 1
                    )
                    {
                        emp.deptSalaries.Last().salaryAmount = emp.employeeSalary.grossSalary;
                        emp.deptSalaries = _salaryService.costPerDept(
                            emp.deptSalaries,
                            true,
                            emp.employeeSalary.grossSalary
                        );
                    }

                    List<employee_dept_salary> edsl = Translator.modelToLDS(emp);

                    foreach (employee_dept_salary em in edsl)
                    {
                        em.deptVenue = null;
                        em.salaryType = null;
                        em.itsId = emp.basicDetails.itsId;
                        if (em.hasSalary == null)
                        {
                            em.hasSalary = false;
                        }
                        await _context.employee_dept_salary.AddAsync(em);
                        await _context.SaveChangesAsync();
                    }

                    if (
                        default_emp_dept_salary != null
                        && (
                            emp.basicDetails.workType == "Fixed"
                            || emp.basicDetails.workType == "Fixed and Time-based"
                        )
                    )
                    {
                        //default_emp_dept_salary.itsId = emp.basicDetails.itsId;
                        _context.employee_dept_salary.AddAsync(
                            new employee_dept_salary
                            {
                                itsId = emp.basicDetails.itsId,
                                deptVenueId = default_emp_dept_salary.deptVenueId,
                                hasSalary = true,
                                salaryTypeId = 1,
                                isHijriSalary = emp.basicDetails.employeeType == "Khidmatguzaar",
                                salaryAmount = 0,
                                workingMin = 480,
                                workingDays = 30
                            }
                        );
                        await _context.SaveChangesAsync();
                        

                        continue;
                    }
                    #endregion
                    var user = _context.user.Where(x => x.ItsId == emp.basicDetails.itsId).FirstOrDefault();

                    if (user == null)
                    {
                        _context.user.Add(new user
                        {
                            Username = emp.basicDetails.fullName,
                            ItsId = emp.basicDetails.itsId,
                            Password = emp.basicDetails.itsId.ToString(),
                            DID = 0,
                            EmailId = emp.basicDetails.emailAddress,
                            Mobile = emp.basicDetails.mobileNo,
                            roleId = emp.basicDetails.roleId,
                        });
                    }
                    else
                    {
                        user.Username = emp.basicDetails.fullName;

                        user.EmailId = emp.basicDetails.emailAddress;
                        user.Mobile = emp.basicDetails.mobileNo;
                        user.roleId = emp.basicDetails.roleId;
                    }
                    _context.SaveChanges();


                }

                return Ok();
            }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        StringBuilder errorMessage = new StringBuilder();

        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                error +=
        //                    $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
        //                errorMessage.AppendLine(error);
        //            }
        //        }

        //        // Log the error message or return it as part of an API response
        //        // For example, you could log it:
        //        // System.Diagnostics.Trace.TraceError(errorMessage.ToString());


        //        // Or return it in an API response:
        //        // return BadRequest(errorMessage.ToString());

        //        return BadRequest(errorMessage.ToString() + dbEx); // Or handle the exception as per your requirement
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // private async Task<IActionResult> UpdateEntityAsync(int id, EntityDto updatedEntity)
        // {
        //     // Fetch the entity from the database
        //     var entity = await _context.Entities.FindAsync(id);

        //     if (entity == null)
        //     {
        //         // Return NotFound if the entity doesn't exist
        //         return NotFound();
        //     }

        //     // Map updatedEntity to the fetched entity
        //     entity.Property1 = updatedEntity.Property1;
        //     entity.Property2 = updatedEntity.Property2;
        //     // Add more mappings as needed

        //     // Set a retry counter for handling concurrency conflicts
        //     const int maxRetryCount = 3;
        //     int retryCount = 0;

        //     bool saveFailed;
        //     do
        //     {
        //         saveFailed = false;
        //         try
        //         {
        //             // Save the changes to the database asynchronously
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException ex)
        //         {
        //             saveFailed = true;

        //             // Retrieve the affected entity entry
        //             var entry = ex.Entries.Single();

        //             // Get the current database values for the affected entity
        //             var databaseEntity = entry.GetDatabaseValues();
        //             if (databaseEntity == null)
        //             {
        //                 // The entity has been deleted by another process
        //                 return NotFound();
        //             }

        //             // Optional: Reload the entity with the latest data from the database
        //             entry.OriginalValues.SetValues(databaseEntity);

        //             retryCount++;
        //             if (retryCount > maxRetryCount)
        //             {
        //                 // If retries exceed the limit, rethrow the exception or handle accordingly
        //                 throw;
        //             }
        //         }

        //     } while (saveFailed);  // Retry until saving is successful or retry limit is reached

        //     // Return success response (e.g., NoContent)
        //     return NoContent();
        // }

        [Route("v2/distributedeptsalary")]
        [HttpPost]
        public async Task<IActionResult> distributeDeptSalary(List<EmployeeDeptSalaryModel> edsl)
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string api = "/v2/distributedeptsalary";
            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);


            try
            {
                var i = _salaryService.costPerDept(edsl, true);
                return Ok(i);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("hk/getemployeesfromitsbulk")]
        [HttpGet]
        public async Task<IActionResult> getEmployeesFromItsBulk(
            [FromQuery] string itsIds
        )
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<int> allits = _helperService.parseIds(itsIds);

            List<user> users = _context.user.Where(x => allits.Contains(x.ItsId)).ToList();

            List<venue> venues = _context.venue.ToList();

            List<registrationform_dropdown_set> rdss = _context.registrationform_dropdown_set.ToList();

            List<user_deptvenue> udvss = _context.user_deptvenue.Where(x => allits.Contains(x.itsId)).ToList();

            List<EmployeeBasicDetailsModel> employees = new List<EmployeeBasicDetailsModel>();

            foreach (var usr in users)
            {
                List<user_deptvenue> udvs = udvss.Where(x => x.itsId == usr.ItsId).ToList();

                List<int> psets = new List<int>();
                foreach (var udv in udvs)
                {
                    psets.Add(udv.psetId);
                }
                
                EmployeeBasicDetailsModel employee = new EmployeeBasicDetailsModel
                {
                    itsId = usr.ItsId,
                    fullName = usr.Username,
                    mauze = usr.mauze,
                    roleId = usr.roleId,
                    psetId = psets
                };

                employees.Add(employee);
            }
            return Ok(employees);
        }

        [Route("v2/getemployeefromitsbulk")]
        [HttpGet]
        public async Task<IActionResult> getEmployeeFromItsBulk(
            [FromQuery] string itsIds,
            [FromQuery] string userType = "qism"
        )
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string api = "v2/getemployeefromits/{itsId}";
            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                List<int> allits = _helperService.parseIds(itsIds);
                //var userData = _context.user.ToList();
                var kgsQuery = _context
                    .khidmat_guzaar.Include(x => x.employee_dept_salary)
                    .Include(x => x.mauzeNavigation)
                    .Include(x => x.employee_bank_details)
                    .Include(x => x.employee_academic_details)
                    .Include(x => x.employee_family_details)
                    .Include(x => x.employee_passport_details)
                    .Include(x => x.employee_salary)
                    .Where(x => allits.Contains(x.itsId));

                List<khidmat_guzaar> kgs = kgsQuery.ToList();

                List<EmployeeModel> employees = new List<EmployeeModel>();

                List<nisaab_alumni> nisaab_Alumnis = new List<nisaab_alumni>();

                List<user_deptvenue> udvss = _context.user_deptvenue.Where(x => allits.Contains(x.itsId)).ToList();

                List<registrationform_dropdown_set> rdss = _context.registrationform_dropdown_set.ToList();

                List<registrationform_subprograms> rfss = _context.registrationform_subprograms.ToList();

                if (kgs.Count > 0)
                {
                    nisaab_Alumnis = _context
                        .nisaab_alumni.Where(x => allits.Contains(x.itsId))
                        .ToList();
                }

                //List<mawaze> mawze = new List<mawaze>();
                foreach (int itsId in allits)
                {
                    khidmat_guzaar kh = kgs.Where(x => x.itsId == itsId).FirstOrDefault();
                    EmployeeModel kg = new EmployeeModel();

                    ItsUser user = new ItsUser();
                    JHSAcademicData jHSAcademicData = new JHSAcademicData();

                    var userdata = _context.user.Where(x => x.ItsId == itsId).FirstOrDefault();
                    int? roleId = 0;
                    if (userdata != null)
                    {
                        roleId = userdata.roleId;
                    }

                    try
                    {
                        if (kh != null)
                        {
                            kg = _mapper.Map<EmployeeModel>(kh);

                            // FIX: Ensure lists are initialized
                            kg.basicDetails.mawazes ??= new List<mawaze>();
                            kg.basicDetails.psetId ??= new List<int>();

                            kg.extraDetails = new EmployeeExtraDetailsModel();

                            kg.extraDetails.status =
                                kg.basicDetails.activeStatus == 1 ? "Existing" : "InActive";

                            //var udvsss = udvss.Where(x => x.itsId == itsId).ToList();

                            //foreach (var udv in udvsss)
                            //{
                            //    kg.basicDetails.psetId.Add(udv.psetId);
                            //}

                            List<mawaze> khMawaze = new List<mawaze>();
                            var udvs_kh = udvss.Where(x => x.itsId == kg.basicDetails.itsId).ToList();

                            foreach (var udv in udvs_kh)
                            {
                                var rds = rdss.FirstOrDefault(x => x.id == udv.psetId);
                                var rfs = rfss.FirstOrDefault(x => x.id == rds.subprogramId);

                                classes cls = new classes
                                {
                                    id = rfs.id,
                                    name = rfs.name
                                };

                                var existingVenue = khMawaze.FirstOrDefault(x => x.id == rds.venueId);

                                if (existingVenue != null)
                                {
                                    existingVenue.classes.Add(cls);
                                }
                                else
                                {
                                    khMawaze.Add(new mawaze
                                    {
                                        id = (int)rds.venueId,
                                        classes = new List<classes> { cls }
                                    });
                                }
                            }

                            kg.basicDetails.mawazes.AddRange(khMawaze);

                            if (
                                kg.basicDetails.employeeType == "Khidmatguzaar"
                                && userType != "admin"
                            )
                            {
                                continue;
                            }
                            kg.basicDetails.roleId = roleId;
                            employees.Add(kg);
                            continue;
                        }
                        else
                        {
                            user.ItsId = itsId;
                            user.status = 0;
                            //return Ok(0);
                            //user = await _itsService.GetItsUser(itsId);
                            //jHSAcademicData = await _jhsService.GetJHSAcademicData(itsId);
                            //user = ;
                        }
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.ToString());
                    }

                    kg.basicDetails = new EmployeeBasicDetailsModel();
                    kg.academicDetails = new EmployeeAcademicDetailsModel();
                    kg.bankDetails = new List<EmployeeBankDetailsModel>();
                    kg.employeeSalary = new EmployeeSalaryDetailsModel() { itsId = user.ItsId };
                    kg.deptSalaries = new List<EmployeeDeptSalaryModel>();
                    kg.familyDetails = new EmployeeFamilyDetailsModel();
                    kg.extraDetails = new EmployeeExtraDetailsModel();
                    kg.extraDetails.status = "New";

                    kg.select = false;
                    kg.basicDetails.roleId = roleId;
                    kg.basicDetails.itsId = user.ItsId;
                    kg.basicDetails.age = user.Age;
                    kg.basicDetails.bloodGroup = user.BloodGroup;
                    kg.basicDetails.mobileNo = user.MobileNo;
                    kg.basicDetails.emailAddress = user.EmailId;
                    kg.basicDetails.dawat_title = user.Title;
                    kg.basicDetails.currentAddress = user.Address;
                    kg.basicDetails.dobGregorian = user.Dob.ToString("d");
                    kg.basicDetails.dobHijri = user.DOB_Hijri;
                    kg.basicDetails.fullName = user.Name;
                    kg.basicDetails.nationality = user.Nationality;
                    kg.basicDetails.fullNameArabic = user.Arabic_FullName;
                    kg.basicDetails.isMumin = true;
                    kg.basicDetails.gender = user.Gender;
                    if (user.Photo != null)
                    {
                        kg.basicDetails.photoBase64 = Convert.ToBase64String(
                            user.Photo,
                            0,
                            user.Photo.Length
                        );
                    }
                    kg.basicDetails.watan = user.Vatan;
                    kg.basicDetails.watanArabic = user.Vatan_Arabic;
                    kg.basicDetails.maritalStatus = user.MaritalStatus;
                    kg.basicDetails.muqam = user.Maqaam;
                    kg.basicDetails.muqamArabic = user.Jamaat_Arabic;
                    if (user.haddiyatYear != "" && user.haddiyatYear != null)
                    {
                        kg.basicDetails.haddiyatYear = Int32.Parse(user.haddiyatYear);
                    }
                    if (user.mafsuhiyatYear != "" && user.mafsuhiyatYear != null)
                    {
                        kg.basicDetails.mafsuhiyatYear = Int32.Parse(user.mafsuhiyatYear);
                    }
                    kg.basicDetails.its_preferredIdara = user.Idara;
                    kg.basicDetails.jamaat = user.Jamaat;
                    kg.basicDetails.jamiat = user.Jamiat;
                    kg.basicDetails.photo = "/uploads/Its_Photos/" + user.ItsId + ".jpeg";


                    List<mawaze> mawze = new List<mawaze>();
                    var udvs = udvss.Where(x => x.itsId == kg.basicDetails.itsId).ToList();

                    if(udvs.Count() > 0)
                    {
                        foreach (var udv in udvs)
                        {
                            var rds = rdss.FirstOrDefault(x => x.id == udv.psetId);
                            var rfs = rfss.FirstOrDefault(x => x.id == rds.subprogramId);

                            classes cls = new classes
                            {
                                id = rfs.id,
                                name = rfs.name
                            };

                            var isExistingVenue = mawze.FirstOrDefault(x => x.id == rds.venueId);

                            if (isExistingVenue != null)
                            {
                                isExistingVenue.classes.Add(cls);
                            }
                            else
                            {
                                mawaze mauz = new mawaze
                                {
                                    id = (int)rds.venueId,
                                    classes = new List<classes> { cls }
                                };

                                mawze.Add(mauz);
                            }
                        }

                        kg.basicDetails.mawazes.AddRange(mawze);
                    }
                    //kg.basicDetails.mauze = kh?.mauze ?? 0;



                    kg.academicDetails.itsId = user.ItsId;
                    if (user.hifzYear != "" && user.hifzYear != null)
                    {
                        kg.academicDetails.hifzSanadYear = Int32.Parse(user.hifzYear);
                    }

                    kg.academicDetails.hifzStatus = user.hifzStatus;

                    if (jHSAcademicData != null)
                    {
                        kg.academicDetails.category = "Aljamea-tus-Saifiyah";
                        kg.academicDetails.farigDarajah = jHSAcademicData.farighDarajah;
                        kg.academicDetails.farigYear = jHSAcademicData.farighYear;
                        kg.academicDetails.aljameaDegree = jHSAcademicData.jameaDegree;
                    }
                    else
                    {
                        nisaab_alumni alum = nisaab_Alumnis
                            .Where(x => x.itsId == itsId)
                            .FirstOrDefault();
                        if (alum != null)
                        {
                            kg.academicDetails.category = "Nisaab Mahad al-Zahra";
                            kg.academicDetails.farigDarajah = alum.farigDarajah;
                            kg.academicDetails.farigYear = alum.farigYear;
                            kg.academicDetails.aljameaDegree = alum.degree;
                        }
                    }

                    kg.academicDetails.batchId = _helperService.getWafdCurrentClass(
                        kg.academicDetails.farigYear,
                        kg.academicDetails.farigDarajah
                    );
                    kg.basicDetails.batchid = kg.academicDetails.batchId;
                    kg.basicDetails.status = user.status ?? 1;

                    if (user.Photo != null)
                    {
                        await _helperService.SaveITSImage(user.Photo, itsId);
                    }
                    employees.Add(kg);
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("getfaculty/nameid/{qismId}")]
        [HttpGet]
        public async Task<IActionResult> getAllFacultyNamesIts([FromRoute] int qismId)
        {
            string api = "get/all/user";

            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            List<UserModel> huffazModel = null;
            List<dynamic> result = new List<dynamic>();
            //huffazModel = cache.GetItem<List<UserModel>>("getAllQismEmployes" + qismId);
            //if (huffazModel != null)
            //{
            //    huffazModel.ForEach(x => result.Add(new
            //    {
            //        name = x.userName + " (" + x.itsId + ")",
            //        its = x.itsId
            //    }));
            //    return Ok(result);
            //}
            List<khidmat_guzaar> kg = _context
                .khidmat_guzaar.Where(x =>
                    (x.mauze != null && x.mauzeNavigation.qismId == qismId)
                    && x.activeStatus == true
                )
                .OrderByDescending(x => x.employee_academic_details.batchId ?? 0)
                .ToList();
            kg.ForEach(x =>
                result.Add(new { name = x.fullName + " (" + x.itsId + ")", its = x.itsId })
            );
            return Ok(result);
        }

        [Route("v2/getemptymodel")]
        [HttpGet]
        public async Task<IActionResult> getEmptyModel()
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string api = "v2/getemptymodel";
            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                EmployeeModel kg = new EmployeeModel();

                kg.basicDetails = new EmployeeBasicDetailsModel();
                kg.academicDetails = new EmployeeAcademicDetailsModel();
                kg.bankDetails = new List<EmployeeBankDetailsModel>();
                kg.employeeSalary = new EmployeeSalaryDetailsModel();
                kg.deptSalaries = new List<EmployeeDeptSalaryModel>();

                return Ok(kg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("addnonmumin/saveimage/{itsId}")]
        [HttpPost]
        public async Task<IActionResult> SaveImage(int itsId)
        {
            var query = _context.khidmat_guzaar.FirstOrDefault(x => x.itsId == itsId);

            if (query == null)
            {
                return NotFound("User not found.");
            }

            try
            {
                var files = Request.Form.Files;
                if (files.Count <= 0)
                {
                    return BadRequest(new { message = "No files selected." });
                }

                var postedFile = files[0];
                var extension = Path.GetExtension(postedFile.FileName);

                using (var stream = new MemoryStream())
                {
                    await postedFile.CopyToAsync(stream);
                    string fileUrl = await _helperService.UploadFileToS3(
                        stream,
                        query.itsId.ToString() + extension,
                        "uploads/Its_Photos"
                    );
                    query.photo = fileUrl;
                    _context.SaveChanges();

                    return Ok(fileUrl);
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                return StatusCode(500, "Internal server error: " + ex);
            }
        }

        [Route("v2/getallstaffattendace")]
        [HttpGet]
        public async Task<IActionResult> getStaffAttendence()
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string api = "v2/getallstaffattendace";
            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                EmployeeModel kg = new EmployeeModel();

                return Ok(kg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("v2/getfaculty/nameid")]
        [HttpGet]
        public async Task<IActionResult> getAllFacultyNamesIts()
        {
            List<EmployeeModel> huffazModel = null;
            List<dynamic> result = new List<dynamic>();
            //huffazModel = cache.GetItem<List<EmployeeModel>>("getAllEmployeeDataFaculty");
            //if (huffazModel != null)
            //{
            //    huffazModel.ForEach(x => result.Add(new
            //    {
            //        name = x.basicDetails.fullName + " (" + x.basicDetails.itsId + ")",
            //        its = x.basicDetails.itsId
            //    }));
            //    return Ok(result);
            //}

            List<khidmat_guzaar> kg = _context
                .khidmat_guzaar.Where(x =>
                    x.employeeType == "Khidmatguzaar" && x.activeStatus == true
                )
                .ToList();
            kg.ForEach(x =>
                result.Add(new { name = x.fullName + " (" + x.itsId + ")", its = x.itsId })
            );
            return Ok(result);
        }

        [Route("v2/inactivatehr")]
        [HttpGet]
        public async Task<IActionResult> inactivateHr(
            [FromQuery] int itsId,
            [FromQuery] int? mauzeId,
            [FromQuery] DateTime dateOfExit
        )
        {
            var token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            string api = "v2/inactivatehr";
            //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

            try
            {
                DateTime date = DateTime.UtcNow;

                khidmat_guzaar kg = _context
                    .khidmat_guzaar.Where(x => x.itsId == itsId)
                    .Include(x => x.employee_dept_salary)
                    .Include(x => x.employee_salary)
                    .Include(x => x.employee_academic_details)
                    .Include(x => x.wafdprofile_maqaraat_teacher)
                    .Include(x => x.mzlm_leave_application)
                    .Include(x => x.mauzeNavigation)
                    .FirstOrDefault();

                khidmat_guzaar empModel = _mapper.Map<khidmat_guzaar>(kg);

                if (kg == null)
                {
                    return BadRequest(new { message = "Employee not found" });
                }

                kg.activeStatus = false;
                kg.doeGregorian = dateOfExit;
                kg.dojGregorian = null;
                kg.workType = null;

                List<azwaaj_minentry> attendance = _context
                    .azwaaj_minentry.Where(x =>
                        x.itsid == itsId && x.date > DateOnly.FromDateTime(dateOfExit)
                    )
                    .ToList();

                _context.azwaaj_minentry.RemoveRange(attendance);

                if (mauzeId != null)
                {
                    kg.mauze = mauzeId;
                }
                else
                {
                    kg.mauze = null;
                }

                if (kg.employee_salary != null)
                {
                    kg.employee_salary.grossSalary = 0;
                    kg.employee_salary.isMahadSalary = false;
                    kg.employee_salary.isHijriAllowence = false;
                    kg.employee_salary.arrears = 0;
                    kg.employee_salary.qardanHasanahNonRefundable = 0;
                    kg.employee_salary.qardanHasanahRefundable = 0;
                    kg.employee_salary.withHoldings = 0;
                    kg.employee_salary.currency = null;
                    kg.employee_salary.sabeel = 0;
                    kg.employee_salary.marafiqKhairiyah = 0;
                    kg.employee_salary.mumbaiAllowance = 0;
                    kg.employee_salary.fmbAllowance = 0;
                    kg.employee_salary.marriageAllowance = 0;
                    kg.employee_salary.conveyanceAllowance = 0;
                    kg.employee_salary.rentAllowance = 0;
                }

                kg.mzlm_leave_application.ToList()
                    .ForEach(x =>
                    {
                        if (x.fromEngDate > date)
                        {
                            _context.mzlm_leave_application.Remove(x);
                        }
                    });
                if (kg.employee_academic_details != null)
                {
                    kg.employee_academic_details.maqaraatTeacherIts = null;
                }

                List<employee_academic_details> academicData = _context
                    .employee_academic_details.Where(x => x.maqaraatTeacherIts == kg.itsId)
                    .ToList();
                foreach (var item in academicData)
                {
                    item.maqaraatTeacherIts = null;
                }

                wafdprofile_maqaraat_teacher wpmt =
                    kg.wafdprofile_maqaraat_teacher.FirstOrDefault();

                if (wpmt != null)
                {
                    _context.wafdprofile_maqaraat_teacher.Remove(wpmt);
                }

                kg.employee_dept_salary.ToList()
                    .ForEach(x =>
                    {
                        kg.employee_dept_salary.Remove(x);
                    });

                var basicdetailslogs = await _helperService.LogChanges<khidmat_guzaar>(
                    kg,
                    empModel,
                    authUser.ItsId,
                    empModel.itsId.ToString()
                );

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("v2/testMahadFamilyApi")]
        [HttpGet]
        public async Task<IActionResult> testItsMahadApi([FromQuery] int itsId)
        {
            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                string api = "v2/testMahadFamilyApi";
                //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

                return Ok(await _itsService.GetFaimalyDetail(itsId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("v2/testfamilytreeApi")]
        [HttpGet]
        public async Task<IActionResult> testfamilytreeApi([FromQuery] int itsId)
        {
            try
            {
                var token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                string api = "v2/testfamilytreeApi";
                //ServiceFactory.GetHelperService().//Add_ApiLogs(api, authUser, Request);

                return Ok(await _itsService.GetFamilyMembers(itsId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getFamilyMembers")]
        [HttpGet]
        public async Task<IActionResult> getFamilyMembers([FromQuery] int? itsId)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            try
            {
                if (itsId == null)
                {
                    itsId = authUser.ItsId;
                }

                List<kg_faimalydetails_its> family = _context
                    .kg_faimalydetails_its.Where(x => x.hofItsId == itsId)
                    .ToList();
                return Ok(_mapper.Map<List<EmployeeFamilyMembersModel>>(family));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("kgfamilyDataUpdate")]
        [HttpGet]
        public async Task<IActionResult> getkgfamilyMemberUpdate()
        {
            try
            {
                List<int> test = new List<int>
                {
                    20359681,
                    20359198,
                    60474952,
                    40421014,
                    20359731,
                    30405507,
                    40443738,
                    40402017,
                    30369641,
                    30443617,
                    40405550,
                    30472653,
                    50484534,
                    30489490,
                    50459038,
                    30351321,
                    60460733,
                    60430123,
                    30389013,
                    60456491,
                    30380155,
                    30376961,
                    30308003,
                    30706898,
                    30901898,
                    30359038,
                    30384792,
                    30350126,
                    30445950,
                    30805544,
                    40494610,
                    50432622,
                    30436672,
                    30399004,
                    30705954,
                    60422753,
                    30356156,
                    30702162,
                    30380645,
                    50408326,
                    30472383,
                    30359535,
                    30906039,
                    30367107,
                    30399163,
                    40476149,
                    30390267,
                    30383247,
                    30328203,
                    30353485,
                    40401137,
                    30479003,
                    30400518,
                    30328427,
                    30704936,
                    30350555,
                    60428004,
                    30389598,
                    30503085,
                    40801358,
                    30487895,
                    30416508,
                    40444605,
                    30482734,
                    30703400,
                    30909926
                };

                List<khidmat_guzaar> kgs = _context
                    .khidmat_guzaar.Where(x =>
                        x.activeStatus == true && x.employeeType == "Khidmatguzaar"
                    )
                    .AsNoTracking()
                    .ToList();
                kgs = kgs.Distinct().ToList();
                List<int> hofIts = kgs.Select(x => x.itsId).ToList();
                List<kg_faimalydetails_its> familyDetails = _context
                    .kg_faimalydetails_its.Where(x => hofIts.Contains(x.hofItsId ?? 0))
                    .ToList();
                List<List<ItsFamilyModel>> familyList = new List<List<ItsFamilyModel>>();
                foreach (var kg in kgs)
                {
                    var family = await _itsService.GetFamilyMembers(kg.itsId);
                    familyList.Add(family);
                    List<int> toRemove = familyDetails
                        .Where(x => x.hofItsId == kg.itsId && !family.Any(f => f.ItsId == x.itsId))
                        .Select(x => x.itsId ?? 0)
                        .ToList();
                    toRemove = toRemove.Distinct().ToList();
                    foreach (int itsId in toRemove)
                    {
                        kg_faimalydetails_its toRemoveMember = familyDetails
                            .Where(x => x.itsId == itsId && x.hofItsId == kg.itsId)
                            .FirstOrDefault();
                        if (toRemoveMember != null)
                        {
                            _context.kg_faimalydetails_its.Remove(toRemoveMember);
                            familyDetails.Remove(toRemoveMember);
                        }
                    }
                    _context.SaveChanges();
                    foreach (ItsFamilyModel f in family)
                    {
                        if (familyDetails.All(x => x.itsId != f.ItsId || x.hofItsId != kg.itsId))
                        {
                            try
                            {
                                _context.kg_faimalydetails_its.Add(
                                    new kg_faimalydetails_its
                                    {
                                        hofItsId = kg.itsId,
                                        itsId = f.ItsId,
                                        name = f.Name,
                                        relation = f.Relation,
                                        jamaat = f.Jamaat,
                                        age = f.Age.ToString()
                                    }
                                );
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        _context.SaveChanges();
                    }
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    MaxDepth = 64 // Increase depth if necessary
                };
                return new JsonResult(familyList, options);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [AllowAnonymous]
        [Route("kgfamilyDataItsUpdate")]
        [HttpGet]
        public async Task<IActionResult> getkgfamilyDataItsUpdate()
        {
            List<kg_faimalydetails_its> famalyData = _context
                .kg_faimalydetails_its.Where(x => x.idara == null)
                .ToList();
            foreach (kg_faimalydetails_its member in famalyData)
            {
                ItsUser user = await _itsService.GetItsUser(member.itsId ?? 0);
                if (user != null)
                {
                    member.name = user.Name;
                    member.jamaat = user.Jamaat;
                    member.age = user.Age.ToString();
                    member.dob = user.Dob;
                    member.occupation = user.Occupation;
                    member.nationality = user.Nationality;
                    member.bloodGroup = user.BloodGroup;
                    member.hifzStatus = user.hifzStatus;
                    member.idara = user.Idara;
                }
                _context.SaveChanges();
            }
            return Ok();
        }

        [AllowAnonymous]
        [Route("wafd_ul_huffaz/update/ITSDATA")]
        [HttpGet]
        public async Task<IActionResult> UpdateItsData_Huffaz([FromQuery] string? itsIds = null)
        {
            string api = "wafd_ul_huffaz/update/ITSDATA";
            // //// Add_ApiLogs(api);

            List<EmployeeBasicDetailsModel> huffazModels = new List<EmployeeBasicDetailsModel>();
            try
            {
                string arabicName = "";

                List<khidmat_guzaar> hgm = new List<khidmat_guzaar>();

                if (!string.IsNullOrEmpty(itsIds))
                {
                    List<int> reqIts = _helperService.parseIds(itsIds);

                    hgm = await _context
                        .khidmat_guzaar.Where(kg =>
                            kg.isMumin == true && reqIts.Any(x => x == kg.itsId)
                        )
                        .Include(its => its.employee_academic_details)
                        .Include(its => its.mauzeNavigation)
                        .Include(its => its.employee_bank_details)
                        .Include(its => its.employee_dept_salary)
                        .ThenInclude(x => x.salaryType)
                        .Include(its => its.employee_dept_salary)
                        .ThenInclude(x => x.deptVenue)
                        .Include(its => its.employee_e_attendence)
                        .Include(its => its.employee_khidmat_details)
                        .Include(its => its.employee_passport_details)
                        .Include(its => its.employee_salary)
                        .ToListAsync();
                }
                else
                {
                    hgm = await _context
                        .khidmat_guzaar.Where(kg => kg.isMumin == true)
                        .Include(its => its.employee_academic_details)
                        .Include(its => its.mauzeNavigation)
                        .Include(its => its.employee_bank_details)
                        .Include(its => its.employee_dept_salary)
                        .ThenInclude(x => x.salaryType)
                        .Include(its => its.employee_dept_salary)
                        .ThenInclude(x => x.deptVenue)
                        .Include(its => its.employee_e_attendence)
                        .Include(its => its.employee_khidmat_details)
                        .Include(its => its.employee_passport_details)
                        .Include(its => its.employee_salary)
                        .ToListAsync();
                }

                foreach (khidmat_guzaar huffaz1 in hgm)
                {
                    //cache.DeleteItem("getEmployeeData" + huffaz1.itsId);
                    //cache.DeleteItem("getAllEmployeeData" + huffaz1.employeeType);
                    try
                    {
                        EmployeeModel huffaz = Translator.khtoModel(huffaz1);

                        ItsUser user = await _itsService.GetItsUser(huffaz1.itsId);

                        JHSAcademicData jHSAcademicData = await _jhsService.GetJHSAcademicData(
                            huffaz1.itsId
                        );
                        ItsUser user_wife = new ItsUser();

                        // log.DebugFormat("ItsData == " + user);
                        if (user == null)
                        {
                            huffazModels.Add(
                                new EmployeeBasicDetailsModel
                                {
                                    itsId = huffaz1.itsId,
                                    fullName = huffaz1.fullName
                                }
                            );
                            continue;
                        }
                        else
                        {
                            arabicName = user.Arabic_FullName;
                            if (huffaz.academicDetails != null)
                            {
                                huffaz.academicDetails.hifzStatus = user.hifzStatus;
                                huffaz.academicDetails.hifzSanadYear = Convert.ToInt32(
                                    user.hifzYear
                                );
                            }
                            else
                            {
                                _context.employee_academic_details.Add(
                                    new employee_academic_details
                                    {
                                        itsId = huffaz1.itsId,
                                        hifzStatus = user.hifzStatus,
                                        hifzSanadYear = Convert.ToInt32(user.hifzYear)
                                    }
                                );
                            }

                            huffaz1.dobGregorian = user.Dob.ToString("dd/MM/yyyy");
                            if (!string.IsNullOrEmpty(user.haddiyatYear))
                            {
                                huffaz1.haddiyatYear = Convert.ToInt32(user.haddiyatYear);
                            }
                            if (!string.IsNullOrEmpty(user.mafsuhiyatYear))
                            {
                                huffaz1.mafsuhiyatYear = Convert.ToInt32(user.mafsuhiyatYear);
                            }

                            huffaz1.nationality = user.Nationality;

                            huffaz1.jamiat = user.Jamiat;
                            huffaz1.dawat_title = user.Title;
                            huffaz1.muqam = user.Maqaam;
                            huffaz1.maritalStatus = user.MaritalStatus;
                            huffaz1.its_preferredIdara = user.Idara;
                            huffaz1.jamaat = user.Jamaat;
                            huffaz1.bloodGroup = user.BloodGroup;
                            huffaz1.watanArabic = user.Vatan_Arabic;
                            huffaz1.watan = user.Vatan;
                            huffaz1.dobHijri = user.DOB_Hijri;
                            huffaz1.mobileNo = user.MobileNo;
                            huffaz1.emailAddress = user.EmailId;
                            huffaz1.age = user.Age;
                            huffaz1.fullNameArabic = arabicName;
                            huffaz1.fullName = user.Name;
                            huffaz1.photoBase64 = Convert.ToBase64String(
                                user.Photo,
                                0,
                                user.Photo.Length
                            );

                            await _helperService.LogChanges<khidmat_guzaar>(
                                _mapper.Map<khidmat_guzaar>(huffaz.basicDetails),
                                huffaz1,
                                1,
                                huffaz1.itsId.ToString()
                            );
                            try
                            {
                                byte[] binaryData = Convert.FromBase64String(
                                    huffaz1.photoBase64 ?? ""
                                );
                                MemoryStream stream = new MemoryStream(binaryData);
                                huffaz1.photo = await _helperService.UploadFileToS3(
                                    stream,
                                    huffaz1.itsId.ToString() + ".jpg",
                                    "uploads/Its_Photos"
                                );
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }

                            await _context.SaveChangesAsync();
                        }

                        await _helperService.SaveITSImage(user.Photo, huffaz1.itsId);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + huffazModels.ToString());
            }
            return Ok();
        }

        [Route("wafdalhuffaz/getExportKgModelWithOutPhoto")]
        [HttpPost]
        public async Task<IActionResult> ExportKgModelWithOutPhoto(List<EmployeeModel> model)
        {
            List<wafd_ul_huffaz_Model> wafd = new List<wafd_ul_huffaz_Model>();
            model = model.Where(x => x.select == true).ToList();
            model.ForEach(x => wafd.Add(khToWafdModel(x)));
            return ExportBasicWithOutPhoto(wafd);
        }

        [Route("wafdalhuffaz/getExportKgModelWithPhoto")]
        [HttpPost]
        public async Task<IActionResult> ExportKgModelWithPhoto(List<EmployeeModel> model)
        {
            List<wafd_ul_huffaz_Model> wafd = new List<wafd_ul_huffaz_Model>();
            model = model.Where(x => x.select == true).ToList();
            model.ForEach(x => wafd.Add(khToWafdModel(x)));
            return ExportBasicWithPhoto(wafd);
        }

        [AllowAnonymous]
        [Route("datarequest")]
        [HttpGet]
        public async Task<IActionResult> getEmployeeDataRequest(
            [FromQuery] string secret,
            [FromQuery] string employeeType = "Khidmatguzaar"
        )
        {
            if (secret != _globalConstants.attalimSecret)
            {
                return BadRequest(new { message = "Invalid Secret" });
            }

            int hijriYear = _helperService.getTodayHijriDate().hijYear;

            List<dataRequestModel> kgs = await _context
                .khidmat_guzaar.Include(x => x.employee_academic_details)
                .Include(x => x.employee_khidmat_details)
                .Include(x => x.employee_salary)
                .Include(x => x.mauzeNavigation)
                .ThenInclude(x => x.qism)
                .Where(x => x.activeStatus == true && x.employeeType == employeeType)
                .Select(x => new dataRequestModel
                {
                    age = (x.age ?? 0).ToString(),
                    Designation = x.designation,
                    current_wazifa = x.employee_salary.grossSalary.ToString(),
                    Email = x.emailAddress,
                    Full_name = x.fullName,
                    ITS = x.itsId.ToString(),
                    Jamaat = x.jamaat,
                    Mobile = x.mobileNo,
                    Gender = x.gender,
                    farig_daraja = (x.employee_academic_details.farigDarajah ?? 0).ToString(),
                    hifz_status = x.employee_academic_details.hifzStatus,
                    idara = x.its_idaras,
                    jamaatid = x.jamaat,
                    jamea_sanad = x.employee_academic_details.aljameaDegree,
                    Jamiyat = x.jamiat,
                    Official_Email = x.officialEmailAddress,
                    watan = x.watan,
                    Tile = x.dawat_title,
                    Schoolname = null,
                    Whatsapp = x.whatsappNo,
                    Current_mauze_muddat = null,
                    Total_khidmat_muddat = null,
                    jamiyat_type = x.jamiat,
                    Misaal_name = null
                })
                .ToListAsync();

            return Ok(kgs);
        }

        [AllowAnonymous]
        [Route("updateAlumniDataFromITS")]
        [HttpGet]
        public async Task<IActionResult> updateAlumniDataFromITS()
        {
            List<nisaab_alumni> alumni = _context.nisaab_alumni.Include(x => x.its).ToList();

            // Update each data twice a week while the API runs daily
            var alumCount = alumni.Count;
            int dayOfWeek = (int)DateTime.Now.DayOfWeek;
            int interval = alumCount / 3; // Divide alumni into three groups

            // Calculate the start index based on the current day of the week
            int start = interval * (dayOfWeek % 3);

            // Get the subset of alumni to update
            var alumniToUpdate = alumni.Skip(start).Take(interval).ToList();

            foreach (nisaab_alumni alum in alumniToUpdate)
            {
                ItsUser user = await _itsService.GetItsUser(alum.itsId);
                if (user != null)
                {
                    alum.its.nameEng = user.Name;
                    alum.its.jamaat = user.Jamaat;
                    alum.its.age = user.Age;
                    alum.its.dobGregorian = user.Dob.ToString("yyyy-MM-dd");
                    alum.its.idara = user.Idara;
                    alum.its.jamiat = user.Jamiat;
                    alum.its.maqaam = user.Maqaam;
                    alum.its.address = user.Address;
                    alum.its.studentMobile = user.MobileNo;
                    alum.its.studentEmail = user.EmailId;
                    alum.its.nameArabic = user.Arabic_FullName;
                    alum.its.dobHijri = user.DOB_Hijri;

                    _context.SaveChanges();
                }
            }
            return Ok();
        }

        private wafd_ul_huffaz_Model khToWafdModel(EmployeeModel huffaz)
        {
            EmployeeModel w = huffaz;

            kg_identitycards aadhaar = _context
                .kg_identitycards.Where(x =>
                    x.itsId == huffaz.basicDetails.itsId && x.cardType == "Aadhaar Card"
                )
                .FirstOrDefault();

            if (w == null)
            {
                return new wafd_ul_huffaz_Model();
            }

            List<wafd_mahad_past_mawaze> pastMawazeList = _context
                .wafd_mahad_past_mawaze.Where(x => x.itsIs == huffaz.basicDetails.itsId)
                .ToList()
                .GroupBy(x => x.mauze)
                .Select(x => x.FirstOrDefault())
                .OrderByDescending(x => x.fromYear)
                .ToList();
            List<wafd_otheridara_mawaze> otherIdaraMawazeList = _context
                .wafd_otheridara_mawaze.Where(x => x.itsId == huffaz.basicDetails.itsId)
                .OrderByDescending(x => x.fromYear)
                .ToList();

            venue d = new venue();

            int? did = w
                .deptSalaries.OrderByDescending(x => x.salaryAmount)
                .FirstOrDefault()
                ?.dept_venue.venueId;
            if (did != null)
            {
                d = _context.venue.FirstOrDefault(x => x.Id == (did ?? 0));
            }

            string hifzstatus = "";

            hifzstatus =
                w.academicDetails.hifzStatus
                + "@"
                + (
                    (w.academicDetails.hifzSanadYear ?? 0) == 0
                        ? "[N/A]"
                        : w.academicDetails.hifzSanadYear.ToString()
                );

            string darajah = "NA";

            if (w.academicDetails.wafdClassId != null)
            {
                darajah = (
                    _context
                        .nisaab_classes.Where(x => x.id == w.academicDetails.wafdClassId)
                        .FirstOrDefault()
                        .std
                ).ToString();
            }

            wafdprofile_qualification_new education = _context
                .wafdprofile_qualification_new.Where(x => x.itsid == huffaz.basicDetails.itsId)
                .OrderByDescending(x => x.year)
                .FirstOrDefault();

            wafd_ul_huffaz_Model wafdM = new wafd_ul_huffaz_Model();
            wafdM.select = w.select == true ? true : false;
            wafdM.khidmatDuration = (
                _helperService.getTodayHijriDate().hijYear - w.khidmatDetails?.khidmatYear
            ).ToString();
            wafdM.tayeenDuration = (
                _helperService.getTodayHijriDate().hijYear - w.khidmatDetails?.tayeenYear ?? 0
            ).ToString();
            wafdM.khidmatMonth = w.khidmatDetails?.khidmatMonth.ToString();
            wafdM.khidmatMauzeHouseStatus =
                w.khidmatDetails?.khidmatMauzeHouseStatus == null
                || w.khidmatDetails?.khidmatMauzeHouseStatus == ""
                    ? "[N/A]"
                    : w.khidmatDetails.khidmatMauzeHouseStatus;
            wafdM.khidmatYear = w.khidmatDetails?.khidmatYear ?? 0;
            if (w.basicDetails.employeeType == "Visiting Faculty")
            {
                List<int> khMins = _context
                    .azwaaj_minentry.Where(x => x.itsid == w.basicDetails.itsId && x.policyId == 2)
                    .GroupBy(x => x.date)
                    .Select(x => x.Sum(y => (y.min ?? 0)))
                    .ToList();
                List<int> khMins2 = khMins.Where(x => x > 60).ToList();
                int years = khMins2.Count() / 275;
                wafdM.khidmatDuration =
                    years.ToString() + " year(s), " + khMins2.Count().ToString() + " day(s), ";
            }
            wafdM.mahad_khidmatYear =
                w.khidmatDetails?.mahad_khidmatYear == null
                    ? 0
                    : w.khidmatDetails.mahad_khidmatYear;
            wafdM.tayeenMonth = w.khidmatDetails?.tayeenMonth;
            wafdM.tayeenYear =
                w.khidmatDetails?.tayeenYear == null ? 0 : w.khidmatDetails.tayeenYear;
            wafdM.totalKhidmatYear =
                _helperService.getTodayHijriDate().hijYear - w.khidmatDetails.khidmatYear;
            wafdM.totalTayeenYear =
                (_helperService.getTodayHijriDate().hijYear - w.khidmatDetails.tayeenYear) ?? 0;
            wafdM.khdimatMauzeHouseType =
                w.khidmatDetails.khdimatMauzeHouseType == null
                || w.khidmatDetails.khdimatMauzeHouseType == ""
                    ? "[N/A]"
                    : w.khidmatDetails.khdimatMauzeHouseType;

            wafdM.aboutYourSelf = w.selfAssessment?.aboutYourSelf;
            wafdM.personalityType = w.selfAssessment?.personalitytype;
            wafdM.personalityReport = w.selfAssessment?.personalityReport;
            wafdM.strenght = w.selfAssessment?.strength;
            wafdM.weakness = w.selfAssessment?.weakness;
            wafdM.roleModel = w.selfAssessment?.roleModel;
            wafdM.changeAboutYourself = w.selfAssessment?.changeAboutYourself;
            wafdM.alternativeCareerPath = w.selfAssessment?.alternativeCareerPath;
            wafdM.longTermGoal = w.selfAssessment?.longTermGoal;

            if (aadhaar != null)
            {
                wafdM.aadhaarCardName = aadhaar.nameOnCard;
                wafdM.aadhaarCardNo = aadhaar.cardNumber;
            }

            foreach (wafd_mahad_past_mawaze pm in pastMawazeList)
            {
                wafdM.past_Moze_1 +=
                    pm.fromYear?.ToString() + "-" + pm.toYear?.ToString() + " " + pm.mauze + " | ";
            }

            foreach (wafd_otheridara_mawaze om in otherIdaraMawazeList)
            {
                wafdM.past_Moze_2 +=
                    om.fromYear?.ToString()
                    + "-"
                    + om.toYear?.ToString()
                    + " "
                    + om.khidmatNature
                    + ": "
                    + om.mauze
                    + " | ";
            }

            wafdM.id = w.basicDetails.id;
            wafdM.photo = w.basicDetails.photo;
            wafdM.itsId = w.basicDetails.itsId;
            wafdM.moze = d.displayName;
            //wafdM.krNo = w.basicDetails.krNo;
            wafdM.krNo = "";
            wafdM.currentAcedemicYear = _helperService.getTodayHijriDate().hijYear;
            wafdM.mafsuhiyatYear = w.basicDetails.mafsuhiyatYear;
            wafdM.fullNameArabic = w.basicDetails.fullNameArabic;
            wafdM.maritalStatus = w.basicDetails.maritalStatus;
            wafdM.fullName = w.basicDetails.fullName;
            wafdM.dob = w.basicDetails.dobGregorian;
            wafdM.dobArabic = w.basicDetails.dobHijri;
            wafdM.watanArabic = w.basicDetails.watanArabic;
            wafdM.khidmatMauzeAddress = w.basicDetails.currentAddress;
            wafdM.watan = w.basicDetails.watan;
            //wafdM.jaman = w.basicDetails.jaman;
            wafdM.jaman = "";
            wafdM.mobileNo = w.basicDetails.mobileNo;
            wafdM.whatsappNo = w.basicDetails.whatsappNo?.ToString();
            wafdM.emailAddress = w.basicDetails.emailAddress;
            wafdM.muqaam = w.basicDetails.muqam;
            wafdM.bloodGroup = w.basicDetails.bloodGroup;
            wafdM.activeStatus = w.basicDetails.activeStatus == 1 ? true : false;
            wafdM.muqaamArabic = w.basicDetails.muqamArabic;
            wafdM.nationality =
                w.basicDetails.nationality == null || w.basicDetails.nationality == ""
                    ? "[N/A]"
                    : w.basicDetails.nationality;
            DateTime dob = new DateTime();
            try
            {
                dob = DateTime.Parse(w.basicDetails.dobGregorian);
            }
            catch (Exception e)
            {
                dob = DateTime.Now;
            }
            int calcAge = _helperService.CalculateAge(dob);
            wafdM.age =
                w.basicDetails.age == null ? _helperService.CalculateAge(dob) : w.basicDetails.age;
            wafdM.jamaat = w.basicDetails.jamaat;
            wafdM.jamiat = w.basicDetails.jamiat;
            wafdM.its_preferredIdara = w.basicDetails.its_preferredIdara;
            wafdM.title =
                w.basicDetails.dawat_title == null || w.basicDetails.dawat_title == ""
                    ? "[N/A]"
                    : w.basicDetails.dawat_title;
            wafdM.panCard = w.bankDetails.FirstOrDefault().panCard;
            wafdM.mz_idara = w.basicDetails.mz_idara;
            wafdM.maqaam = w.basicDetails.muqam;
            wafdM.officialEmailAddress = w.basicDetails.officialEmailAddress;
            wafdM.haddiyatYear = w.basicDetails.haddiyatYear;
            wafdM.domacileParents = w.basicDetails.domicileParent;
            wafdM.domacileParentsAddress = w.basicDetails.domicileAddressParents;
            wafdM.watanAddress = w.basicDetails.watanAdress;
            wafdM.personalHouseAddress =
                w.basicDetails.personalHouseAddress == null
                || w.basicDetails.personalHouseAddress == ""
                    ? "[N/A]"
                    : w.basicDetails.personalHouseAddress;
            wafdM.its_idaras = w.basicDetails.its_idaras;
            wafdM.dojGregorian = w.basicDetails.dojGregorian;
            wafdM.c_codeWhatsapp = w.basicDetails.c_codeWhatsapp;
            wafdM.activeStatusString = w.basicDetails.activeStatus == 1 ? "1" : "0";

            wafdM.ifsc = w.bankDetails.FirstOrDefault().ifsc;
            wafdM.bankName = w.bankDetails.FirstOrDefault().bankName;
            wafdM.bankAccountName = w.bankDetails.FirstOrDefault().bankAccountName;
            wafdM.bankAccountNumber = w.bankDetails.FirstOrDefault().bankAccountNumber;
            wafdM.bankBranch = w.bankDetails.FirstOrDefault().bankBranch;
            wafdM.accountType = w.bankDetails.FirstOrDefault().bankAccountType;
            wafdM.chequeAttachment = w.bankDetails.FirstOrDefault().chequeAttachment;

            wafdM.farigDarajah =
                w.academicDetails?.farigDarajah == null ? 0 : w.academicDetails.farigDarajah;
            wafdM.fariqYear = w.academicDetails?.farigYear;
            wafdM.hifzSanadYear =
                w.academicDetails?.hifzSanadYear == null ? 0 : w.academicDetails.hifzSanadYear;
            wafdM.trNo = w.academicDetails?.trNo;
            wafdM.wafd_TrainingMasool = w.academicDetails?.wafdTrainingMasoolIts;
            wafdM.alJameaDegree =
                w.academicDetails?.aljameaDegree == null || w.academicDetails.aljameaDegree == ""
                    ? "[N/A]"
                    : w.academicDetails.aljameaDegree;
            wafdM.category =
                w.academicDetails.category == null || w.academicDetails.category == ""
                    ? "[N/A]"
                    : w.academicDetails.category;

            wafdM.passportPlaceOfBirth = w.passportDetails?.passportPlaceOfBirth;
            wafdM.passportName = w.passportDetails?.passportName;
            wafdM.passportNumber = w.passportDetails?.passportNo;
            wafdM.dobPassport = w.passportDetails?.dobPassport;
            wafdM.dateOfIssue = w.passportDetails?.dateOfIssue;
            wafdM.dateOfExpiry = w.passportDetails?.dateOfExpiry;
            wafdM.placeOfIssue = w.passportDetails?.placeOfIssue;
            wafdM.passportcopy = w.passportDetails?.passportCopy;

            wafdM.currency = d.currency;

            wafdM.qismTahfeez = d.qismTahfeez;
            wafdM.currentMoze = d.displayName;
            wafdM.venueId = (int)d.Id;

            wafdM.wazifa =
                w.employeeSalary == null ? 0 : _salaryService.netSalary(w.employeeSalary);
            wafdM.grossSalary = w.employeeSalary?.grossSalary;
            wafdM.rentAllowance = w.employeeSalary?.rentAllowance;
            wafdM.marriageAllowance = w.employeeSalary?.marriageAllowance;
            wafdM.mumbaiAllowance = w.employeeSalary?.mumbaiAllowance;
            wafdM.conveyanceAllowance = w.employeeSalary?.conveyanceAllowance;
            wafdM.professionTax = w.employeeSalary?.professionTax;
            wafdM.tds = w.employeeSalary?.tds;
            wafdM.qardanHasanah = w.employeeSalary?.qardanHasanah;
            wafdM.marafiqKhairiyah = w.employeeSalary?.marafiqKhairiyah;
            wafdM.sabeel = w.employeeSalary?.sabeel;
            wafdM.bqhs = w.employeeSalary?.bqhs;

            wafdM.isMahadWazifa = w.deptSalaries.FirstOrDefault()?.hasSalary;
            wafdM.salaryTag = w.deptSalaries.FirstOrDefault()?.deptVenueId;
            wafdM.deptVenueName = w.deptSalaries.FirstOrDefault()?.dept_venue.venueName;
            wafdM.deptVenueId = w.deptSalaries.FirstOrDefault()?.deptVenueId;
            wafdM.venueProgram = w.deptSalaries.FirstOrDefault()?.dept_venue.venueName;

            wafdM.personalHouseArea =
                huffaz.basicDetails.personalHouseArea == null
                || huffaz.basicDetails.personalHouseArea == ""
                    ? "[N/A]"
                    : huffaz.basicDetails.personalHouseArea;
            wafdM.personalHouseType =
                huffaz.basicDetails.personalHouseType == null
                || huffaz.basicDetails.personalHouseType == ""
                    ? "[N/A]"
                    : huffaz.basicDetails.personalHouseType;
            wafdM.personalHouseStatus =
                huffaz.basicDetails.personalHouseStatus == null
                || huffaz.basicDetails.personalHouseStatus == ""
                    ? "[N/A]"
                    : huffaz.basicDetails.personalHouseStatus;
            wafdM.batchId = w.academicDetails.batchId ?? 0;
            wafdM.ho = d.ho;
            //wafdM.workTypeId = w.basicDetails.workTypeId ?? 0;
            wafdM.workTypeId = w.basicDetails.workType;
            wafdM.hifzStatusModified = hifzstatus;
            wafdM.currentDarajah = darajah;

            wafdM.isHijriAllowance = w.employeeSalary.isHijriAllowence == true ? "Yes" : "No";
            wafdM.isMahadSalary = w.employeeSalary.isMahadSalary == true ? "Yes" : "No";
            if (education != null)
            {
                wafdM.educationalQualification = education.degree;
            }

            return wafdM;
        }

        private IActionResult ExportBasicWithOutPhoto(List<wafd_ul_huffaz_Model> model)
        {
            try
            {
                //  AuthUser authUser = ServiceFactory.GetAuthService().GetAuthUser(HttpContext.Current.User);

                List<R_22_1> Report = new List<R_22_1>();

                List<wafd_ul_huffaz_Model> w = model.Where(x => x.select == true).ToList();

                int currentAcedemicYear = _helperService
                    .getAcedemicYear(DateTime.Today)
                    .acedemicYear;

                int c = 0;
                foreach (var i in w)
                {
                    c = c + 1;
                    dept_venue md = _context
                        .dept_venue.Where(x => x.id == i.deptVenueId)
                        .FirstOrDefault();
                    int vv = i.venueId ?? 0;
                    venue m = _context.venue.Where(x => x.Id == vv).FirstOrDefault();

                    khidmat_guzaar kg = _context
                        .khidmat_guzaar.Where(x =>
                            x.itsId == i.itsId && x.employeeType == "Khidmatguzaar"
                        )
                        .FirstOrDefault();
                    if (kg == null)
                    {
                        continue;
                    }
                    string process = "";
                    string dv = "";
                    string venue = "";

                    process = "Qism";
                    if (md != null)
                    {
                        dv = md.deptName + "_" + md.venueName;
                    }
                    if (m != null)
                    {
                        venue = m.displayName;
                    }
                    int dara = 0;

                    try
                    {
                        if (i.mz_idara.Equals("Wafd al-Huffaz"))
                        {
                            dara = _helperService.getWafdCurrentClass(i.fariqYear, i.farigDarajah);
                        }
                    }
                    catch (Exception ex) { }
                    string email = i.emailAddress;
                    if (i.officialEmailAddress != null)
                    {
                        email = i.officialEmailAddress;
                    }

                    var t = kg.employee_academic_details;

                    var a = new R_22_1
                    {
                        hifzStatusModified = t.hifzStatus + "@" + i.hifzSanadYear,
                        hifzSanadyear = i.hifzSanadYear?.ToString(),
                        hifzStatus = t?.hifzStatus,
                        age = i.age?.ToString(),
                        aljameaDegree = i.alJameaDegree,
                        farigYear = i.fariqYear?.ToString(),
                        fullNameArabic = i.fullNameArabic,
                        mz_idara = i.mz_idara,
                        preferredIdara = i.its_preferredIdara,
                        primaryEmailAddress = email,
                        darajah = dara,
                        itsId = i.itsId,
                        fullName = i.fullName,
                        whatsappNumber = i.c_codeWhatsapp + (i.whatsappNo?.ToString()),
                        mobileNumber = i.mobileNo.ToString(),
                        moze = venue,
                        srNo = c,
                        category = i.category,
                        batchId = i.batchId,
                        farigDarajah = i.farigDarajah ?? 0,
                        qismTahfeez = m?.qismTahfeez
                    };
                    Report.Add(a);
                }

                return Ok(Report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult ExportBasicWithPhoto(List<wafd_ul_huffaz_Model> model)
        {
            try
            {
                List<R_22> Report = new List<R_22>();

                List<wafd_ul_huffaz_Model> w = model.Where(x => x.select == true).ToList();
                int c = 0;

                int currentAcedemicYear = _helperService
                    .getAcedemicYear(DateTime.Today)
                    .acedemicYear;

                foreach (var i in w)
                {
                    c = c + 1;
                    dept_venue md = _context
                        .dept_venue.Where(x => x.id == i.deptVenueId)
                        .FirstOrDefault();
                    int vv = i.venueId ?? 0;
                    venue m = _context.venue.Where(x => x.Id == vv).FirstOrDefault();
                    khidmat_guzaar kg = _context
                        .khidmat_guzaar.Where(x =>
                            x.itsId == i.itsId && x.employeeType == "Khidmatguzaar"
                        )
                        .FirstOrDefault();

                    if (kg == null)
                    {
                        continue;
                    }

                    string process = "";
                    string dv = "";
                    string venue = "";

                    process = "Qism";

                    if (md != null)
                    {
                        dv = md.deptName + "_" + md.venueName;
                    }
                    if (m != null)
                    {
                        venue = m.displayName;
                    }
                    int dara = 0;

                    try
                    {
                        if (i.mz_idara.Equals("Wafd al-Huffaz"))
                        {
                            dara = _helperService.getWafdCurrentClass(i.fariqYear, i.farigDarajah);
                        }
                    }
                    catch (Exception ex) { }
                    string email = i.emailAddress;
                    if (i.officialEmailAddress != null || i.officialEmailAddress != "")
                    {
                        email = i.officialEmailAddress;
                    }
                    var t = kg.employee_academic_details;
                    var a = new R_22
                    {
                        hifzStatusModified = t.hifzStatus + "@" + i.hifzSanadYear,
                        hifzSanadyear = i.hifzSanadYear?.ToString(),
                        hifzStatus = t?.hifzStatus,
                        primaryEmailAddress = email,
                        farigYear = i.fariqYear?.ToString(),
                        mz_idara = i.mz_idara,
                        fullNameArabic = i.fullNameArabic,
                        age = i.age?.ToString(),
                        aljameaDegree = i.alJameaDegree,
                        preferredIdara = i.its_preferredIdara,
                        photo2 = kg.photoBase64,
                        darajah = dara,
                        itsId = i.itsId,
                        fullName = i.fullName,
                        whatsappNumber = i.c_codeWhatsapp + (i.whatsappNo?.ToString()),
                        mobileNumber = i.mobileNo.ToString(),
                        moze = venue,
                        srNo = c,
                        category = i.category,
                        batchId = i.batchId,
                        farigDarajah = i.farigDarajah ?? 0,
                        qismTahfeez = m?.qismTahfeez
                    };
                    Report.Add(a);
                }

                return Ok(Report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getkhidmatmawazenew/{itsId}")]
        [HttpGet]
        public async Task<IActionResult> getKhidmatMawaze_New(int itsId)
        {
            try
            {
                List<wafdulhuffaz_khidmat_mawaze> mawaze = new List<wafdulhuffaz_khidmat_mawaze>();
                List<wafdulhuffaz_khidmat_mawaze> mawaze2 = new List<wafdulhuffaz_khidmat_mawaze>();
                List<WafdMawazeModel> mawaze3 = new List<WafdMawazeModel>();
                List<WafdMawazeModel> mawaze4 = new List<WafdMawazeModel>();

                mawaze = _context
                    .wafdulhuffaz_khidmat_mawaze.Where(x => x.itsId == itsId)
                    .OrderByDescending(x => x.hijriYear)
                    .ThenBy(x => x.khidmatMainType)
                    .ToList();
                mawaze2 = mawaze
                    .GroupBy(x => x.khidmatMainType)
                    .Select(x => x.FirstOrDefault())
                    .ToList();
                foreach (var i in mawaze2.Where(x => x.khidmatSubType != null).ToList())
                {
                    var listmawaze = mawaze
                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                        .GroupBy(x => x.khidmatSubType)
                        .Select(x => x.FirstOrDefault())
                        .ToList();
                    var count1 = mawaze
                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                        .ToList()
                        .Count;
                    int c = 1;
                    foreach (var j in listmawaze)
                    {
                        bool d = false;
                        if (c == 1)
                        {
                            d = true;
                        }
                        var count = mawaze
                            .Where(x =>
                                x.khidmatMainType.Equals(i.khidmatMainType)
                                && x.khidmatSubType.Equals(j.khidmatSubType)
                            )
                            .ToList()
                            .Count;
                        mawaze3.Add(
                            new WafdMawazeModel
                            {
                                colSpan = 1,
                                display_subType = true,
                                rowSpan = listmawaze.Count,
                                khidmatMainType = i.khidmatMainType,
                                khidmatSubType = j.khidmatSubType,
                                mainTypeCount = count1,
                                subTypeCount = count,
                                display = d
                            }
                        );
                        c = c + 1;
                    }
                }
                foreach (var i in mawaze2.Where(x => x.khidmatSubType == null).ToList())
                {
                    var count1 = mawaze
                        .Where(x => x.khidmatMainType.Equals(i.khidmatMainType))
                        .ToList()
                        .Count;

                    mawaze3.Add(
                        new WafdMawazeModel
                        {
                            colSpan = 2,
                            display_subType = false,
                            rowSpan = 1,
                            khidmatMainType = i.khidmatMainType,
                            khidmatSubType = "-",
                            mainTypeCount = count1,
                            subTypeCount = 1,
                            display = true
                        }
                    );
                }

                foreach (var i in mawaze.Where(x => x.khidmatSubType == null).ToList())
                {
                    mawaze4.Add(
                        new WafdMawazeModel
                        {
                            id = i.id,
                            hijriYear = i.hijriYear,
                            mozeName = i.mozeName,
                            colSpan = 2,
                            display_subType = false,
                            rowSpan = 1,
                            khidmatMainType = i.khidmatMainType,
                            khidmatSubType = "-",
                            mainTypeCount = 0,
                            subTypeCount = 0,
                            display = true
                        }
                    );
                }
                foreach (var i in mawaze.Where(x => x.khidmatSubType != null).ToList())
                {
                    mawaze4.Add(
                        new WafdMawazeModel
                        {
                            id = i.id,
                            hijriYear = i.hijriYear,
                            mozeName = i.mozeName,
                            colSpan = 1,
                            display_subType = true,
                            rowSpan = 1,
                            khidmatMainType = i.khidmatMainType,
                            khidmatSubType = i.khidmatSubType,
                            mainTypeCount = 0,
                            subTypeCount = 0,
                            display = true
                        }
                    );
                }
                return Ok(new { mawaze = mawaze4, mawaze3 = mawaze3 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error" });
            }
        }
    }
}
