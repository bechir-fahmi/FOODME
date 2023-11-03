using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.NotificationSystems.Business.business_services.SMS;
using Platform.NotificationSystems.DtoModel.SMS;
using Platform.ReferentialData.DtoModel.NotificationData;

namespace Platform.NotificationSystems.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSProviderController : ControllerBase
    {
        private readonly ISMSService _smsService;
        private readonly ISMSProviderService _smsProviderService;
        private readonly ILogger<SMSProviderController> _logger;

        public SMSProviderController(ISMSService smsService, ILogger<SMSProviderController> logger
            , ISMSProviderService smsProviderService)
        {
            _smsService = smsService;
            _logger = logger;
            _smsProviderService = smsProviderService;
        }

        [Route("SendSMS")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendSMSAsync([FromBody] SendSMSDTO sendSMSDTO)
        {
            try
            {
                HttpResponseMessage response = await _smsService.SendSMS(sendSMSDTO);
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    throw new Exception(message: $"SMS not send " +
                        $"\n StatusCode : {response.StatusCode} " +
                        $"\n Reason : {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(SendSMSAsync)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }
        
        [Route("AddSMSProvider")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddSMSProvider([FromBody] SMSProviderDTO sMSProviderDTO)
        {
            try
            {
                _smsProviderService.Add(sMSProviderDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddSMSProvider)}");
                return StatusCode(500, "Internal server Error. Please try later");
            }

        }

    }
}
