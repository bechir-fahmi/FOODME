using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;

namespace Platform.FoodMe.AdminAPI.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOTPVerificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHelper<UserOTPInfoDTO, UserOTPInfo, UserOTPInfoVM> _userHelper;
        private readonly IHelper<UserOTPVerificationDTO, UserOTPVerification, UserOTPVerificationVM> _oTPVerificationHelper;
        public UserOTPVerificationController(IHelper<UserOTPInfoDTO, UserOTPInfo, UserOTPInfoVM> userHelper,
            IHelper<UserOTPVerificationDTO, UserOTPVerification, UserOTPVerificationVM> oTPVerificationHelper,
            IMapper mapper)
        {
            _userHelper = userHelper;
            _oTPVerificationHelper = oTPVerificationHelper;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SendOTP")]
        public IActionResult SendOTPMessage([FromBody] UserOTPInfoVM userVM)
        {
            string endPoint = $"{Microservice.RefData}/Message/SendVerificationCode";
            HttpResponseMessage response = _userHelper.Create(_mapper, endPoint, userVM);
            if (response.IsSuccessStatusCode)
            {
                return Ok(response);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return StatusCode(404, response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return StatusCode(500, response.Content.ReadAsStringAsync().Result);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("VerifyOTP")]
        public IActionResult VerifyOTPVerificationCode([FromBody] UserOTPVerificationVM userOTPVerificationVM)
        {
            string endPoint = $"{Microservice.RefData}/Message/VerifyOTP";
            HttpResponseMessage response = _oTPVerificationHelper.Create(_mapper, endPoint, userOTPVerificationVM);
            if (response.IsSuccessStatusCode)
            {
                response.ReasonPhrase = response.Content.ReadAsStringAsync().Result;
                return Ok(response);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return StatusCode(404, response.Content.ReadAsStringAsync().Result);
            }
            {
                return StatusCode(500, response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
