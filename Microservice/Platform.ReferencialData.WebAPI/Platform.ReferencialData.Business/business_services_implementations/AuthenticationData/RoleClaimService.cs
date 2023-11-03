using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.Business.Filter;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using System.Security.Claims;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class RoleClaimService : IRoleClaimService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly ILogger<RoleClaimService> _logger;
        private readonly IMapper _mapper;

        public RoleClaimService(RoleManager<RoleEntity> roleManager, ILogger<RoleClaimService> logger, IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IList<Claim>> GetAllClaimsFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var allClaims = await _roleManager.GetClaimsAsync(role);

            return allClaims;
        }

        public async Task<ResponseDTO> AddClaimsToRole(string roleId, IList<RoleClaimDTO> claims)
        {
            var roleEntity = await _roleManager.FindByIdAsync(roleId);
            var allClaims = await _roleManager.GetClaimsAsync(roleEntity);

            foreach (RoleClaimDTO claim in claims)
            {
                List<string> permissions = Permissions.GeneratePermission(claim.ClaimType, claim.ClaimValue);

                foreach (string permission in permissions)
                {
                    if (!allClaims.Any(c => c.Type == claim.ClaimType.ToString() && c.Value == permission))
                    {
                        var response = await _roleManager.AddClaimAsync(roleEntity, new Claim("Pemission", permission));
                        if (!response.Succeeded)
                        {
                            return new ResponseDTO
                            {
                                StatusCodes = StatusCodes.Status400BadRequest,
                                Error = "Error in add claim to role"
                            };
                        }
                    }
                }

            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = "All Claims are added to the role"
            };
        }

        //TODO
        public async Task<ResponseDTO> UpdateClaimsInRole(RoleDTO roleDTO)
        {
            var roleEntity = await _roleManager.FindByIdAsync(roleDTO.Id);
            var allClaims = await _roleManager.GetClaimsAsync(roleEntity);
            foreach (Claim claim in allClaims)
            {
                var deleteResult = await _roleManager.RemoveClaimAsync(roleEntity, claim);
                if (!deleteResult.Succeeded)
                {
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Message = "Error",
                        Error = "Error in delete permission"
                    };
                }
            }

            foreach (RoleClaimDTO claim in roleDTO.Claims)
            {
                List<string> permissions = Permissions.GeneratePermission(claim.ClaimType, claim.ClaimValue);

                foreach (string permission in permissions)
                {
                    if (!allClaims.Any(c => c.Type == claim.ClaimType.ToString() && c.Value == permission))
                    {
                        var addResult = await _roleManager.AddClaimAsync(roleEntity, new Claim(claim.ClaimType.ToString(), permission));
                        if (!addResult.Succeeded)
                        {
                            return new ResponseDTO
                            {
                                StatusCodes = StatusCodes.Status400BadRequest,
                                Error = "Error in update permission in the role"
                            };
                        }
                    }
                }

            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = "All permissions are updated in the role"
            };
        }

        public async Task<ResponseDTO> DeleteClaimsFromRole(RoleDTO roleDTO)
        {
            var roleEntity = await _roleManager.FindByIdAsync(roleDTO.Id);
            foreach (RoleClaimDTO claim in roleDTO.Claims)
            {
                List<string> permissions = Permissions.GeneratePermission(claim.ClaimType, claim.ClaimValue);
                foreach (string permission in permissions)
                {
                    var deleteResult = await _roleManager.RemoveClaimAsync(roleEntity, new Claim(claim.ClaimType.ToString(), permission));
                    if (!deleteResult.Succeeded)
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status400BadRequest,
                            Message = "Error",
                            Error = "Error in delete permission"
                        };
                    }

                }
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = "Deleting permissions successfully",
                Error = "No Error"
            };
        }
    }
}