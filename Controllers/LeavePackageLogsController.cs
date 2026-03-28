using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings.Leave;
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
    public class LeavePackageLogsController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly HelperService _helperService;

        public LeavePackageLogsController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _helperService = new HelperService(context);
        }

        // GET: api/LeaveLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mzlm_leave_package_logs_dto>> Getmzlm_leave_package_logs(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            if (_context.mzlm_leave_logs == null)
            {
                return NotFound();
            }

            var mzlm_leave_log = _context.mzlm_leave_package_logs.Where(x => x.id == id).Include(x => x.createdByNavigation).Include(x => x.stage).FirstOrDefault();

            mzlm_leave_package_logs_dto mzlmAplicationLogsDto = _mapper.Map<mzlm_leave_package_logs_dto>(mzlm_leave_log);

            if (mzlm_leave_log == null)
            {
                return NotFound();
            }

            return mzlmAplicationLogsDto;
        }

        [HttpGet("packageId/{id}")]
        public async Task<ActionResult<List<mzlm_leave_package_logs_dto>>> Getmzlm_leave_package_logs_by_application(int id, [FromQuery] string? stageIds = null)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_package_logs == null)
            {
                return NotFound();
            }
            var mzlm_leave_log = _context.mzlm_leave_package_logs.Where(x => x.packageId == id).Include(x => x.createdByNavigation).Include(x => x.stage).ToList();

            List<mzlm_leave_package_logs_dto> mzlmAplicationLogsDto = _mapper.Map<List<mzlm_leave_package_logs_dto>>(mzlm_leave_log);

            if (stageIds != null)
            {
                List<int> stageList = _helperService.parseIds(stageIds);
                mzlmAplicationLogsDto = mzlmAplicationLogsDto.Where(x => stageList.Any(y => y == x.stageId)).ToList();
            }

            if (mzlm_leave_log == null)
            {
                return NotFound();
            }

            return mzlmAplicationLogsDto;
        }

        private bool mzlm_leave_package_logsExists(int id)
        {
            return (_context.mzlm_leave_package_logs?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
