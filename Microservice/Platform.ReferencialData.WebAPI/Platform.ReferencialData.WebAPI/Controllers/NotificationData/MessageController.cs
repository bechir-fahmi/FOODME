using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.ReferencialData.Business.business_services.NotificationData;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.WebAPI.Controllers.NotificationData
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService,
            ILogger<MessageController> logge, IMapper mapper)
        {
            _messageService = messageService;
            _logger = logge;
            _mapper = mapper;
        }
        [Route("SendVerificationCode")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendVerificationCodeAsync([FromBody] UserOTPInfo userOTPInfo)
        {
            try
            {
               ResponseDTO response = await _messageService.AuthenticateUserAndSendVerificationCodeAsync(userOTPInfo);
                if(response.StatusCodes == StatusCodes.Status200OK)
                {
                    return Ok(response);
                }
                else if(response.StatusCodes == StatusCodes.Status404NotFound)
                {
                    return NotFound(response.Error);
                }
                else
                {
                    return StatusCode(500, response.Error);
                }             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(SendVerificationCodeAsync)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }

        [Route("VerifyOTP")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VerifyOTPAsync([FromBody] OTPVerificationDTO userOTPInfo)
        {
            try
            {
               ResponseDTO response = await _messageService.VerifyUserOTPVerificationCodeAsync(userOTPInfo);
                if(response.StatusCodes == StatusCodes.Status200OK)
                {
                    return Ok(response);
                }
                else if(response.StatusCodes == StatusCodes.Status404NotFound)
                {
                    return NotFound(response.Error);
                }
                else
                {
                    return StatusCode(500, response.Error);
                }             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(VerifyOTPAsync)}");
                return StatusCode(500, $"Internal server Error. Please try later, error : {ex}");
            }
        }
    }
}
