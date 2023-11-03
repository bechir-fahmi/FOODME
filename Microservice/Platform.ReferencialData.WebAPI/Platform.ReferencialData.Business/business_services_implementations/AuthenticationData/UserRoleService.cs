using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class UserRoleService : IUserRoleService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly UserManager<UserEntity> _userManager;

        private readonly IUnitOfWork<UserRoleEntity> _unityOfWork;

        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;

        public UserRoleService(RoleManager<RoleEntity> roleManager,
            UserManager<UserEntity> userManager,
            IUnitOfWork<UserRoleEntity> unityOfWork,
            ILogger<RoleService> logger,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unityOfWork = unityOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseDTO> AddUserToRole(string userId, string roleId)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                _logger.LogInformation($"The user {userId} does not exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Error = $"User {userId} does not exist"
                };
            }

            var roleEntity = await _roleManager.FindByIdAsync(roleId);
            if (roleEntity == null)
            {
                _logger.LogInformation($"The role {roleId} does not exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Error = "Role does not exist"
                };
            }
            //var currentUser = _unityOfWork.Repository.Get(userRole => userRole.UserId == userId);
            UserRoleEntity userRoleEntity = new UserRoleEntity
            {
                RoleId = roleId,
                UserId = userId
            };
            //_unityOfWork.Repository.Insert(userRoleEntity);
            var result = await _userManager.AddToRoleAsync(userEntity, roleEntity.Name);
            if (result.Errors.Any()) 
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = result.Errors.ToString(),
                    Message = "an error occured while adding user to role"
                };

            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status201Created,
                Message = "Role added successfully"
            };

        }

        public async Task<IList<RoleDTO>> GetUserRoles(string userId)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);

            var userRoles = await _userManager.GetRolesAsync(userEntity);

            List<RoleEntity> roleEntities = new List<RoleEntity>();

            foreach(var role in userRoles)
            {
                var roleEntity = _roleManager.FindByNameAsync(role).Result;
                roleEntities.Add(roleEntity);
            }

            var roles = _mapper.Map<IList<Role>>(roleEntities);

            var rolesDTO = _mapper.Map<IList<RoleDTO>>(roles);
            return rolesDTO;
        }

        public async Task<ResponseDTO> RemoveUserFromRole(string userId, string roleId)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null)
            {
                _logger.LogInformation($"The user {userId} does not exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Error = "User does not exist"
                };
            }

            var roleEntity = await _roleManager.FindByIdAsync(roleId);
            if (roleEntity == null)
            {
                _logger.LogInformation($"The role {roleId} does not exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Error = "Role does not exist"
                };
            }

            var result = await _userManager.RemoveFromRoleAsync(userEntity, roleEntity.Name);
            if (result.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = $"Success, user has been removed from the role {roleId}"
                };
            }
            else
            {
                _logger.LogInformation($"Unable to remove user {userId} from {roleId}");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = $"Unable to remove user {userId} from {roleId}"
                };
            }

        }
    }
}
