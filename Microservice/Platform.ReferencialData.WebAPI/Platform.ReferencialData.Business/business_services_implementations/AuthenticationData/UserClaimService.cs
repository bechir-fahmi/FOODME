using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using System.Security.Claims;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class UserClaimService : IUserClaimService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<UserClaimService> _logger;
        private readonly IMapper _mapper;

        public UserClaimService(UserManager<UserEntity> userManager, ILogger<UserClaimService> logger, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<ResponseDTO> GetUserClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogInformation($"The user {userId} does not exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status204NoContent,
                    Error = "User does not exist"
                };
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = userClaims != null ? userClaims.ToString() : "There is no claim to this user"
            };
        }

        public async Task<ResponseDTO> AddClaimToUser(string userId, string claimName, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogInformation($"The user {userId} does not exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status204NoContent,
                    Error = "User does not exist"
                };
            }
            var userClaim = new Claim(claimName, claimValue);

            var result = await _userManager.AddClaimAsync(user, userClaim);

            if (result.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = $"user {userId} has a claim {claimName} added to them"
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status400BadRequest,
                Error = $"Unable to add claim to user {userId}"
            };
        }

        public ResponseDTO RemoveClaimFromUser(string userId, string claimName)
        {
            throw new NotImplementedException();
        }
    }
}
