using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferentialData.DtoModel.Authentification;
using System.Security.Claims;

namespace Platform.ReferencialData.WebAPI.Controllers.Authentification
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        private readonly IUserRoleService _userRoleService;

        public UserRoleController(
            IUserRoleService userRoleService,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            _userRoleService = userRoleService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("AddUserToRole")]
        public async Task<ResponseDTO> AddUserToRole(string userId, string roleId)
        {

            try
            {
                
                var response = await _userRoleService.AddUserToRole(userId, roleId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddUserToRole)}");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server Error. Please try later, error : {ex}",
                    Error = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IList<RoleDTO>> GetUserRoles(string userId)
        {
            var rolesDTO = await _userRoleService.GetUserRoles(userId);
            return rolesDTO;

        }


        [HttpDelete]
        [Route("RemoveUserFromRole")]
        public async Task<ResponseDTO> RemoveUserFromRole(string userId, string roleId)
        {

            try
            {
                var response = await _userRoleService.RemoveUserFromRole(userId, roleId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddUserToRole)}");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server Error. Please try later, error : {ex}",
                    Error = ex.Message
                };
            }
        }
    }
}
