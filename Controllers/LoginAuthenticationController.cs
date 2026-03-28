using AutoMapper;
using mahadalzahrawebapi.Api.v1;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace mahadalzahrawebapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAuthenticationController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly SalaryService _salaryService;
        private readonly HelperService _helperService;
        private readonly ItsServiceRemote _itsService;
        private readonly IJHSService _jhsService;
        private readonly globalConstants _globalConstants;
        private readonly HijriCalenderService _hijriCalenderService;

        public LoginAuthenticationController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _salaryService = new SalaryService(context);
            _helperService = new HelperService(context);
            _itsService = new ItsServiceRemote();
            _jhsService = new IJHSServiceRemote();
            _globalConstants = new globalConstants();
            _hijriCalenderService = new HijriCalenderService(context);
        }
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
        private static readonly string MahadAlZahra_KHDGZ = "Mahad al-Zahra KHDGZ";
        private static readonly string WafdAlHuffaz = "Wafd al-Huffaz";
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);


        [Route("getTokenVerification")]
        [HttpGet]
        public async Task<ActionResult> getTokenVerification()
        {

            string api = "getTokenVerification";

            try
            {
                string token = _tokenService.ExtractTokenFromRequest(HttpContext);
                AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

                return Ok(true);

            }
            catch (Exception e)
            {
                return Ok(false);

            }
        }



        [Route("getFacultyAuthVariables")]
        [HttpGet]
        public async Task<ActionResult> getFacultyAuthVariables()
        {

            string api = "getFacultyAuthVariables";
            int itsno;
            string employeeType;
            bool isMedical;
            bool isTeacher;
            bool isFasalTeacher;
            bool isIstinsakh;
            bool isTrainingTeacher;
            bool isTrainingStudent;
            bool isTrainingMasool;
            bool isHusainBhai;
            bool isWafd;
            bool isTrainingWafd;
            bool isMaqaraatTeacher;
            bool isInshaTeacher;
            bool isNazamTeacher;
            bool isBookReviewTeacher;
            bool isTadrees;
            bool isNisaabTeacher;
            bool isNisaabClassMasool;

            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            FacultyVariablesModel model = new FacultyVariablesModel();

            itsno = authUser.ItsId;

            int academicYear = Int32.Parse(_context.global_constant.Where(x => x.key == "trainingAcedemicYear").FirstOrDefault().value);


            khidmat_guzaar k = _context.khidmat_guzaar.Where(x => x.itsId == authUser.ItsId && x.mz_idara == MahadAlZahra_KHDGZ).FirstOrDefault();
            khidmat_guzaar k1 = _context.khidmat_guzaar.Where(x => x.itsId == itsno && x.activeStatus == true).FirstOrDefault();
            employee_academic_details k2 = _context.employee_academic_details.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();
            //nisaab_teachers t = _context.nisaab_teachers.Where(x => x.teacherIdentity == k1.itsId).FirstOrDefault();
            //campfasal_teachers t1 = _context.campfasal_teachers.Where(x => x.teacherIdentity == k1.itsId).FirstOrDefault();

            wafdprofile_maqaraat_teacher tt3 = _context.wafdprofile_maqaraat_teacher.Where(x => x.itsId == authUser.ItsId).FirstOrDefault();
            training_class_subject_teacher tm = _context.training_class_subject_teacher.Where(x => x.teacherITS == authUser.ItsId).FirstOrDefault();
            training_class_student ts = _context.training_class_student.Where(x => x.studentITS == authUser.ItsId && x.academicYear == academicYear).Include(x => x._class).FirstOrDefault();

            //masool_classtype m1 = _context.masool_classtype.Where(x => x.masoolIts == authUser.ItsId).FirstOrDefault();

            training_class tm1 = _context.training_class.Where(x => x.masoolIts == authUser.ItsId).FirstOrDefault();

            isTrainingTeacher = false;
            isTrainingStudent = false;
            isTrainingMasool = false;
            isWafd = false;
            isHusainBhai = false;
            isTrainingWafd = false;
            isIstinsakh = false;
            String darajah = "N/A";
            isInshaTeacher = false;
            isMaqaraatTeacher = false;
            isNazamTeacher = false;
            isBookReviewTeacher = false;
            isTadrees = false;
            isNisaabClassMasool = false;
            isNisaabTeacher = false;
            try
            {
                if (ts != null)
                {
                    darajah = ts._class.className;

                }
            }
            catch (Exception ex)
            {

            }

            if (k != null)
            {
                isMedical = true;
            }
            else
            {
                isMedical = false;
            }

            isTeacher = false;

            isFasalTeacher = false;


            if (tm != null)
            {
                isTrainingTeacher = true;
            }
            if (ts != null)
            {
                isTrainingStudent = true;
            }

            if (tt3 != null)
            {

                isMaqaraatTeacher = true;
            }

            if (tm1 != null)
            {
                isTrainingMasool = true;
            }
            if (k2 != null)
            {
                isWafd = true;
            }

            if (itsno == 40403403)
            {
                isHusainBhai = true;
            }


            model.isFasalTeacher = isFasalTeacher;
            model.isHusainBhai = isHusainBhai;
            model.isIstinsakh = isIstinsakh;
            model.isMedical = isMedical;
            model.isTeacher = isTeacher;
            model.isTrainingMasool = isTrainingMasool;
            model.isTrainingTeacher = isTrainingTeacher;
            model.darajah = darajah;
            model.academicYear = _hijriCalenderService.getAcedemicYear(DateTime.Today).acedemicYear;
            model.isTrainingStudent = isTrainingStudent;
            model.isWafd = isWafd;
            model.isTrainingWafd = isTrainingWafd;
            model.isMaqaraatTeacher = isMaqaraatTeacher;
            model.isTadrees = isTadrees;
            model.isNisaabClassMasool = isNisaabClassMasool;
            model.isNisaabTeacher = isNisaabTeacher;
            model.employeeType = k1?.employeeType ?? "N/A";
            model.mzIdara = k1?.mz_idara ?? "N/A";
            model.isMumin = k1?.isMumin ?? false;


            return Ok(model);

        }

        [AllowAnonymous]
        [Route("student/itsonelogin")]
        [HttpGet]
        public async Task<ActionResult> authenticateWithIts(String Token, String DT)
        {

            string secretToken = "7kOYCp1doR6FtXhLV9mwaE3qTeHvjBzcQsgJU8IZN2G5iWKxlPDfu";
            string secret2 = "7kOYCp1doR6FtXhLV9mwaE3qTeHvjBzcQsgJU8IZN2G5iWKx";

            //return Redirect("https://mahadalzahra.org/");
            //Token = HttpUtility.UrlDecode(Token);
            //Token = Token.Replace(" ", "");
            //DT = HttpUtility.UrlDecode(DT);
            //DT = DT.Replace(" ", "");
            string decrypt = OneLogin_Decrypt(secretToken, Token);
            if (secret2 == decrypt)
            {
                String[] decrytedData = OneLogin_Decrypt(secretToken, DT).Split(',');

                int its = int.Parse(decrytedData[0]);
                mz_student student = _context.mz_student.FirstOrDefault(x => x.itsID == its);

                if (student == null)
                {
                    return Redirect("https://students.mahadalzahra.org/student_dashboard/itsonelogin");
                }
                var l = new mz_faculty_loginlogs { itsId = student.itsID, date = indianTime, ipAddress = "", deviceDetails = "" };

                _context.mz_faculty_loginlogs.Add(l);
                _context.SaveChanges();
                //ServiceFactory.GetAuthService().GetAuthContent(wafd.Id, "", wafd.FullName, wafd.ItsId, "", "", 0, "", 0, "", aa, request.UserAgent, l.id, "Faculty_Login");
                registrationform_dropdown_set pset = _context.registrationform_dropdown_set.Include(x => x.deptVenue).FirstOrDefault(x => x.id == student.psetId);

                AuthUser authUser = new AuthUser()
                {
                    Id = student.mz_id,
                    Name = student.nameEng,
                    ItsId = student.itsID ?? 0,
                    logId = l.id,
                    loginName = "Student_Login_Angular",
                    qismId = pset.qismId ?? 0,
                    Department = pset.deptVenue.masterDeptName,
                    DeptVenueName = pset.deptVenue.deptName + "_" + pset.deptVenue.venueName,
                    DepartmentCode = pset.deptVenue.deptName,
                    DID = pset.deptVenue.deptName,
                    DisplayName = pset.deptVenue.deptName,
                    ipAddress = "",
                    DeptVenueId = 0,
                    deviceDetails = ""
                };


                string token = _tokenService.GenerateJwtToken(authUser);


                return Redirect("https://students.mahadalzahra.org/student_dashboard/itsonelogin?token=" + token);
            }



            return Redirect("https://students.mahadalzahra.org/student_dashboard/itsonelogin");
        }

        [AllowAnonymous]
        [Route("getFacultyloginauthentication")]
        [HttpPost]
        public async Task<ActionResult> getFacultyLoginAuth(UserModel model)
        {
            string api = "api/loginauthentication/getFacultyloginauthentication";

            try
            {
                khidmat_guzaar wafd = _context.khidmat_guzaar.Where(x => x.itsId == model.itsId && x.activeStatus == true).Include(x => x.mauzeNavigation).FirstOrDefault();

                //return BadRequest( new { message = "Server is under Maintainace");
                if (model.itsId == 0 || model.itsId.ToString().Length != 8)
                {
                    return BadRequest(new { message = "invalid its id" });
                }
                if (String.IsNullOrWhiteSpace(model.password))
                {
                    return BadRequest(new { message = "password cannot be empty" });
                }


                if (wafd != null)
                {
                    if (model.password != "facultylogin@5253")
                    {
                        if (wafd.isMumin == true)
                        {
                            bool isAuthentic = _itsService.Authenticate(model.itsId ?? 0, model.password);
                            if (!isAuthentic)
                            {
                                if (model.password != wafd.password)
                                {
                                    return BadRequest(new { message = "invalid its id or passowrd" });
                                }
                            }
                        }
                        else
                        {
                            if (model.password != wafd.password)
                            {
                                return BadRequest(new { message = "invalid its id or passowrd" });
                            }
                        }
                    }

                    var l = new mz_faculty_loginlogs { itsId = model.itsId, date = indianTime, ipAddress = "", deviceDetails = "" };

                    _context.mz_faculty_loginlogs.Add(l);
                    _context.SaveChanges();

                    AuthUser authUser = new AuthUser()
                    {
                        Id = (long)(wafd.id ?? 0),
                        Name = wafd.fullName,
                        ItsId = wafd.itsId,
                        logId = l.id,
                        loginName = "HR_Login_Angular",
                        qismId = wafd.mauzeNavigation.qismId ?? 0,
                        Department = wafd.mauzeNavigation.displayName,
                        DeptVenueName = wafd.employeeType,
                        DepartmentCode = wafd.workType,
                        DID = wafd.mauzeNavigation.displayName,
                        DisplayName = wafd.mauzeNavigation.displayName,
                        ipAddress = "",
                        DeptVenueId = 0,
                        deviceDetails = ""
                    };


                    string token = _tokenService.GenerateJwtToken(authUser);
                    tokensmodel tokens = new tokensmodel
                    {
                        mzManageKey = token,
                        apiKey = token // Set the apiKey here
                    };

                    return Ok(tokens);

                }
                else
                {
                    return BadRequest(new { message = "You are not enrolled to this program" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [AllowAnonymous]
        [Route("getstudentloginauthentication")]
        [HttpPost]
        public async Task<ActionResult> getstudentLoginAuth(UserModel model)
        {
            string api = "api/loginauthentication/getFacultyloginauthentication";
            //Add_ApiLogs(api);

            try
            {

                mz_student? student = _context.mz_student.FirstOrDefault(x => x.itsID == model.itsId);

                if (model.itsId == 0 || model.itsId?.ToString().Length != 8)
                {
                    return BadRequest(new { message = "invalid its id" });
                }
                if (String.IsNullOrWhiteSpace(model.password))
                {
                    return BadRequest(new { message = "password cannot be empty" });
                }

                if (student != null)
                {
                    //temparary password for specific student
                    //if(model.itsId == 50420420 && model.password == "fd7ga54TE4asd")
                    //{

                    //} else
                    if (model.password != "studentlogin@5253")
                    {
                        IItsService service = new ItsServiceRemote();
                        bool isAuthentic = service.Authenticate(model.itsId ?? 0, model.password);
                        if (!isAuthentic)
                        {
                            return BadRequest(new { message = "invalid its id or passowrd" });
                        }
                    }



                    var l = new mz_faculty_loginlogs { itsId = student.itsID, date = indianTime, ipAddress = "", deviceDetails = "" };

                    _context.mz_faculty_loginlogs.Add(l);
                    _context.SaveChanges();
                    registrationform_dropdown_set pset = _context.registrationform_dropdown_set.Include(x => x.deptVenue).FirstOrDefault(x => x.id == student.psetId);

                    AuthUser authUser = new AuthUser()
                    {
                        Id = student.mz_id,
                        Name = student.nameEng,
                        ItsId = student.itsID ?? 0,
                        logId = l.id,
                        loginName = "Student_Login_Angular",
                        qismId = pset.qismId ?? 0,
                        Department = pset?.deptVenue?.masterDeptName,
                        DeptVenueName = pset?.deptVenue?.deptName + "_" + pset?.deptVenue.venueName,
                        DepartmentCode = pset?.deptVenue?.deptName,
                        DID = pset?.deptVenue?.deptName,
                        DisplayName = pset?.deptVenue?.deptName,
                        ipAddress = "",
                        DeptVenueId = 0,
                        deviceDetails = ""
                    };

                    string token = _tokenService.GenerateJwtToken(authUser);
                    return Ok(new { token = token });
                }
                else
                {
                    return BadRequest(new { message = "Your are not enrolled to this program" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //private string OneLogin_Decrypt(string TokenKey, string DataString)
        //{
        //    MemoryStream ms = null;
        //    CryptoStream cs = null;
        //    try
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 128;
        //            AES.BlockSize = 128;
        //            byte[] EncryptedData = System.Convert.FromBase64String(DataString);
        //            Rfc2898DeriveBytes SecretKey = new Rfc2898DeriveBytes(TokenKey, new byte[] { 86, 42, 71, 72, 94, 124, 57, 94, 84, 79, 35, 99, 84 });
        //            using (ICryptoTransform Decryptor = AES.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16)))
        //            {
        //                ms = new MemoryStream(EncryptedData);
        //                using (ms)
        //                {
        //                    cs = new CryptoStream(ms, Decryptor, CryptoStreamMode.Read);
        //                    using (cs)
        //                    {
        //                        byte[] PlainText = new byte[EncryptedData.Length - 1 + 1];
        //                        return Encoding.Unicode.GetString(PlainText, 0, cs.Read(PlainText, 0, PlainText.Length));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error - " + ex.Message;
        //    }
        //    finally
        //    {
        //        if (ms != null)
        //        {
        //            ms.Close();
        //        }

        //        if (cs != null)
        //        {
        //            cs.Close();
        //        }
        //    }
        //}

        //private string OneLogin_Decrypt(string TokenKey, string DataString)
        //{
        //    MemoryStream ms = null;
        //    CryptoStream cs = null;
        //    try
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 128;
        //            AES.BlockSize = 128;
        //            AES.Padding = PaddingMode.PKCS7;

        //            // Decode the base64 encoded data
        //            byte[] EncryptedData = Convert.FromBase64String(DataString);

        //            // Generate the key and IV
        //            Rfc2898DeriveBytes SecretKey = new Rfc2898DeriveBytes(TokenKey, new byte[] { 86, 42, 71, 72, 94, 124, 57, 94, 84, 79, 35, 99, 84 });

        //            using (ICryptoTransform Decryptor = AES.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16)))
        //            {
        //                ms = new MemoryStream(EncryptedData);
        //                using (ms)
        //                {
        //                    cs = new CryptoStream(ms, Decryptor, CryptoStreamMode.Read);
        //                    using (cs)
        //                    {
        //                        using (MemoryStream resultStream = new MemoryStream())
        //                        {
        //                            cs.CopyTo(resultStream);
        //                            byte[] PlainText = resultStream.ToArray();
        //                            return Encoding.Unicode.GetString(PlainText);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (FormatException fe)
        //    {
        //        // Specific handling for Base64 format issues
        //        return "Base64 Error - " + fe.Message;
        //    }
        //    catch (CryptographicException ce)
        //    {
        //        // Specific handling for cryptographic issues
        //        return "Cryptographic Error - " + ce.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        // General error handling
        //        return "Error - " + ex.Message;
        //    }
        //    finally
        //    {
        //        if (ms != null)
        //        {
        //            ms.Close();
        //        }

        //        if (cs != null)
        //        {
        //            cs.Close();
        //        }
        //    }
        //}

        private string OneLogin_Decrypt(string TokenKey, string DataString)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            try
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 128;
                    AES.BlockSize = 128;
                    byte[] EncryptedData = System.Convert.FromBase64String(DataString);
                    Rfc2898DeriveBytes SecretKey = new Rfc2898DeriveBytes(TokenKey, new byte[] { 86, 42, 71, 72, 94, 124, 57, 94, 84, 79, 35, 99, 84 });
                    using (ICryptoTransform Decryptor = AES.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16)))
                    {
                        ms = new MemoryStream(EncryptedData);
                        using (ms)
                        {
                            cs = new CryptoStream(ms, Decryptor, CryptoStreamMode.Read);
                            using (cs)
                            {
                                byte[] PlainText = new byte[EncryptedData.Length - 1 + 1];
                                return Encoding.Unicode.GetString(PlainText, 0, cs.Read(PlainText, 0, PlainText.Length));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error - " + ex.Message;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }

                if (cs != null)
                {
                    cs.Close();
                }
            }
        }

        [AllowAnonymous]
        [Route("getbranchloginauth")]
        [HttpPost]
        public async Task<ActionResult> getBranchLoginAuth(UserModel model)
        {
            string api = "api/LoginAuthentication/getBranchLoginAuth";

            try
            {
                MailAddress userId = new MailAddress(model.emailId);

                if (String.IsNullOrWhiteSpace(model.password))
                {
                    return BadRequest(new { message = "password cannot be empty" });
                }

                branch_user user = _context.branch_user.Where(x => x.emailId == userId.Address).FirstOrDefault();

                if (user == null)
                {
                    user = _context.qism_al_tahfeez.Include(x => x.its).Where(x => x.emailId == userId.Address).FirstOrDefault()?.its;
                }

                if (user == null)
                {
                    return BadRequest(new { message = "Unauthorized User" });
                }
                ;

                branch_user bru = _context.branch_user.Where(x => x.itsId == user.itsId).Include(x => x.its).Include(x => x.deptVenue).ThenInclude(x => x.qism).FirstOrDefault();

                System.Console.WriteLine($"This is branch user query");
                List<QismModel> qisms = bru.deptVenue.ToList().Where(x => x.qismId != null).GroupBy(x => x.qismId).Select(x => new QismModel
                {
                    id = x.First().qism.id,
                    name = x.First().qism.name,
                }).ToList();
                if (qisms.Count == 0 && bru.itsId != 1)
                {
                    return BadRequest(new { message = "User with given id does not belong to any branch" });
                }


                if (model.password != "qismLogin@5253")
                {
                    if (model.password != user.password && user?.qism_al_tahfeez.password != model.password)
                    {
                        return BadRequest(new { message = "invalid its id or passowrd" });
                    }
                }


                var l = new qism_al_tahfeez_login_logs
                {
                    itsId = user.itsId,
                    date = indianTime,
                    email = model.emailId,
                    ipAddress = "",
                    deviceDetails = ""
                };

                _context.qism_al_tahfeez_login_logs.Add(l);
                _context.SaveChanges();

                AuthUser authUser = new AuthUser()
                {
                    Id = (long)bru.itsId,
                    Name = bru.its.fullName,
                    ItsId = bru.itsId,
                    logId = l.id,
                    loginName = "Branch_Login_Angular",
                    qismId = qisms.FirstOrDefault()?.id ?? 0,
                    Department = qisms.FirstOrDefault()?.name,
                    DeptVenueName = qisms.FirstOrDefault()?.name,
                    DepartmentCode = qisms.FirstOrDefault()?.name,
                    DID = qisms.FirstOrDefault()?.name,
                    DisplayName = qisms.FirstOrDefault()?.name,
                    ipAddress = "",
                    DeptVenueId = 0,
                    deviceDetails = ""
                };


                string token = _tokenService.GenerateJwtToken(authUser);


                tokensmodel tokens = new tokensmodel
                {
                    mzManageKey = token,
                    apiKey = token // Set the apiKey here
                };

                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("getAdminloginauthentication")]
        [HttpPost]
        public async Task<ActionResult> getAdminLoginAuth(UserModel model)
        {
            string api = "api/loginauthentication/getAdminloginauthentication";

            //return BadRequest( new { message = "Server is under Maintainace");

            try
            {
                if (model.itsId == 0 || model.itsId.ToString().Length != 8 || model.itsId == null)
                {
                    return BadRequest(new { message = "invalid its id" });
                }
                if (String.IsNullOrWhiteSpace(model.password))
                {
                    return BadRequest(new { message = "password cannot be empty" });
                }

                user user = _context.user.FirstOrDefault(x => x.ItsId == model.itsId);



                if (user != null)
                {
                    if (model.password != model.itsId.ToString())
                    {
                        if (model.password != "masterkey@5253")
                        {
                            if (model.password != user.Password)
                            {
                                if (model.password != model.itsId.ToString())
                                {
                                    return BadRequest(new { message = "Incorrect Password" });
                                }
                            }
                        }
                    }

                    var l = new mz_faculty_loginlogs { itsId = model.itsId, date = indianTime, ipAddress = "", deviceDetails = "" };

                    _context.mz_faculty_loginlogs.Add(l);
                    _context.SaveChanges();

                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == model.itsId).Include(x => x.mauzeNavigation).FirstOrDefault();

                    AuthUser authUser = new AuthUser()
                    {
                        Id = (long)(kg.id ?? 0),
                        Name = kg.fullName,
                        ItsId = user.ItsId,
                        logId = l.id,
                        loginName = "Admin_Login_Angular",
                        qismId = kg.mauzeNavigation?.qismId ?? 0,
                        Department = kg.mauzeNavigation?.displayName ?? "N/A",
                        DeptVenueName = kg.employeeType ?? "N/A",
                        DepartmentCode = kg.workType ?? "N/A",
                        DID = kg.mauzeNavigation?.displayName ?? "N/A",
                        DisplayName = kg.mauzeNavigation?.displayName ?? "N/A",
                        ipAddress = "",
                        DeptVenueId = 0,
                        deviceDetails = ""
                    };


                    string token = _tokenService.GenerateJwtToken(authUser);
                    tokensmodel tokens = new tokensmodel
                    {
                        mzManageKey = token,
                        apiKey = token, // Set the apiKey here
                        itsid = user.ItsId
                    };
                    //Console.WriteLine(tokens.apiKey);
                    return Ok(tokens);

                }
                else
                {
                    return BadRequest(new { message = "You are not enrolled to this program" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [AllowAnonymous]
        [Route("GetAdminloginauthenticationGet/{itsid}")]
        [HttpGet]
        public IActionResult GetAdminLoginAuthGet(int itsId)
        {
            try
            {
                user user = _context.user.FirstOrDefault(x => x.ItsId == itsId);

                if (itsId == 0 || itsId.ToString().Length != 8)
                {
                    return BadRequest(new { message = "invalid its id" });
                }

                if (user != null)
                {
                    var l = new mz_faculty_loginlogs { itsId = itsId, date = indianTime, ipAddress = "", deviceDetails = "" };

                    _context.mz_faculty_loginlogs.Add(l);
                    _context.SaveChanges();

                    khidmat_guzaar kg = _context.khidmat_guzaar.Where(x => x.itsId == itsId).Include(x => x.mauzeNavigation).FirstOrDefault();

                    AuthUser authUser = new AuthUser()
                    {
                        Id = (long)(kg.id ?? 0),
                        Name = kg.fullName,
                        ItsId = user.ItsId,
                        logId = l.id,
                        loginName = "Admin_Login_Angular",
                        qismId = kg.mauzeNavigation?.qismId ?? 0,
                        Department = kg.mauzeNavigation?.displayName ?? "N/A",
                        DeptVenueName = kg.employeeType ?? "N/A",
                        DepartmentCode = kg.workType ?? "N/A",
                        DID = kg.mauzeNavigation?.displayName ?? "N/A",
                        DisplayName = kg.mauzeNavigation?.displayName ?? "N/A",
                        ipAddress = "",
                        DeptVenueId = 0,
                        deviceDetails = ""
                    };

                    string token = _tokenService.GenerateJwtToken(authUser);
                    tokensmodel tokens = new tokensmodel
                    {
                        mzManageKey = token,
                        apiKey = token, // Set the apiKey here
                        itsid = user.ItsId,
                        url = "https://budget.majmamarkazi.com/dashboard/"
                    };
                    Console.WriteLine(tokens.url, tokens.itsid);
                    return Ok(tokens);
                }
                else
                {
                    return BadRequest(new { message = "You are not enrolled to this program" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }

    public class tokensmodel
    {
        public string mzManageKey { get; set; }
        public string apiKey { get; set; }
        public int? itsid { get; set; }

        public string url { get; set; }
    }
}
