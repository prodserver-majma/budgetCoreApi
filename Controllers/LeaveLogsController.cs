using AutoMapper;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Mappings;
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
    public class LeaveLogsController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly IMapper _mapper;
        private readonly HelperService _helperService;
        private readonly TokenService _tokenService;

        public LeaveLogsController(mzdbContext context, IMapper mapper, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _helperService = new HelperService();
            _tokenService = tokenService;
        }

        // GET: api/LeaveLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mzlm_leave_logs_dto>> Getmzlm_leave_logs(int id)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);
            if (_context.mzlm_leave_logs == null)
            {
                return NotFound();
            }

            var mzlm_leave_log = _context.mzlm_leave_logs.Where(x => x.id == id).Include(x => x.createdByNavigation).Include(x => x.stage).FirstOrDefault();

            mzlm_leave_logs_dto mzlmAplicationLogsDto = _mapper.Map<mzlm_leave_logs_dto>(mzlm_leave_log);

            if (mzlm_leave_log == null)
            {
                return NotFound();
            }

            return mzlmAplicationLogsDto;
        }

        [HttpGet("applicationId/{id}")]
        public async Task<ActionResult<List<mzlm_leave_logs_dto>>> Getmzlm_leave_logs_by_application(int id, [FromQuery] string? stageIds = null)
        {
            string token = _tokenService.ExtractTokenFromRequest(HttpContext);
            AuthUser authUser = _tokenService.GetAuthUserFromToken(token);

            if (_context.mzlm_leave_logs == null)
            {
                return NotFound();
            }
            var mzlm_leave_log = _context.mzlm_leave_logs.Where(x => x.leaveId == id).Include(x => x.createdByNavigation).Include(x => x.stage).ToList();

            List<mzlm_leave_logs_dto> mzlmAplicationLogsDto = _mapper.Map<List<mzlm_leave_logs_dto>>(mzlm_leave_log);


            if (mzlm_leave_log == null)
            {
                return NotFound();
            }
            if (stageIds != null)
            {
                List<int> stageList = _helperService.parseIds(stageIds);
                mzlmAplicationLogsDto = mzlmAplicationLogsDto.Where(x => stageList.Any(y => y == x.stageId)).ToList();
            }

            return mzlmAplicationLogsDto;
        }

        private bool mzlm_leave_logsExists(int id)
        {
            return (_context.mzlm_leave_logs?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
