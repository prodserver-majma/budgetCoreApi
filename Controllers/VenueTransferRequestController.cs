using AutoMapper;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mahadalzahrawebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VenueTransferRequestController : ControllerBase
    {
        private readonly mzdbContext _context;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly HelperService _helperService;
        private readonly NotificationService _notificationService;
        private readonly globalConstants globalConstants;
        private readonly WhatsAppApiService _whatsAppApiService;

        public VenueTransferRequestController(mzdbContext context, TokenService tokenService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
            _helperService = new HelperService();
            _notificationService = new NotificationService();
            globalConstants = new globalConstants();
            _whatsAppApiService = new WhatsAppApiService(context);
        }

        // get all venue transfer approval requests
        [HttpGet]
        public IActionResult Get()
        {
            // get  all venue transfer approval requests
            var venueTransferApprovalRequests = _context.venue_transfer_approval;

            return Ok();
        }
    }
}
