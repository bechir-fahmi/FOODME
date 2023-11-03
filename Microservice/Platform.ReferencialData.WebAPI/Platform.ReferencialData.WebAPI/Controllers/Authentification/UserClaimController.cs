using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.WebAPI.Controllers.Authentification
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserClaimController : ControllerBase
    {
        private readonly IUserClaimService _userClaimService;

        public UserClaimController(IUserClaimService userClaimService)
        {
            _userClaimService = userClaimService;
        }


        [HttpGet]
        public async Task<ResponseDTO> GetUserClaims(string userId)
        {
            var response = await _userClaimService.GetUserClaims(userId);
            return response;

        }

        [HttpPatch]
        [Route("AddClaimsToUser")]
        public async Task<ResponseDTO> AddClaimsToUser(string userId, string claimName, string claimValue)
        {
            var response = await _userClaimService.AddClaimToUser(userId, claimName, claimValue);
            return response;
        }
    }
}
