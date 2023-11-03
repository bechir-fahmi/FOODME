using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.Business.Filter;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.Shared.Permissions;
using System.Security.Claims;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IUnitOfWork<UserEntity> _userRepository;
        private readonly IUnitOfWork<UserRoleEntity> _userRoleRepository;
        private readonly IUnitOfWork<RoleEntity> _roleRepository;
        private readonly IUnitOfWork<IdentityRoleClaim<string>> _roleClaimRepository;

        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<RoleEntity> roleManager,
            IUnitOfWork<RoleEntity> roleRepository,
            ILogger<RoleService> logger,
            IMapper mapper,
            IUnitOfWork<UserEntity> userRepository,
            IUnitOfWork<UserRoleEntity> userRoleRepository,
            IUnitOfWork<IdentityRoleClaim<string>> roleClaimRepository)
        { 
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleClaimRepository = roleClaimRepository;
        }



        public async Task<IList<RoleDTO>> GetAllRoles()
        {
            var rolesEntities = await _roleManager.Roles.ToListAsync();
 
            //var roleList = _roleRepository.Repository.GetAll();

            var roles = _mapper.Map<IList<Role>>(rolesEntities);
            foreach (var role in roles)
            {
                var roleEntity = await _roleManager.FindByIdAsync(role.Id);
                var claims = await _roleManager.GetClaimsAsync(roleEntity);
                List<RoleClaim> claimList = new List<RoleClaim>(); ;
                Dictionary<Modules, List<CRUDPermissions>> claimObject = new Dictionary<Modules, List<CRUDPermissions>>();
                foreach (var claim in claims)
                {
                    var claimvalue = claim.Value.Split(".");
                    Modules ActualClaimtype = (Modules)Enum.Parse(typeof(Modules), claimvalue[1], ignoreCase: true);
                    CRUDPermissions ActualClaimvalue = (CRUDPermissions)Enum.Parse(typeof(CRUDPermissions), claimvalue[2], ignoreCase: true);
                    if (!claimObject.ContainsKey(ActualClaimtype))
                    {
                        claimObject[ActualClaimtype] = new List<CRUDPermissions>();
                    }
                    claimObject[ActualClaimtype].Add(ActualClaimvalue);
                }
                foreach (KeyValuePair<Modules, List<CRUDPermissions>> paire in claimObject)
                {
                    RoleClaim claimBM = new RoleClaim()
                    {
                        ClaimType = paire.Key,
                        ClaimValue = paire.Value
                    };
                    claimList.Add(claimBM);
                }

                role.Claims = claimList;
            }

            var roleDtoList = _mapper.Map<IList<RoleDTO>>(roles);
            // _cache.SetData(_cacheKey, brandDtoList, DateTimeOffset.UtcNow.AddDays(1));
            return roleDtoList;

        }

        public async Task<PagedList<RoleDTO>> GetAllRoles(PagedParameters pagedParameters)
        {
            var rolesDTO = await GetAllRoles();

            return PagedList<RoleDTO>.ToGenericPagedList(rolesDTO, pagedParameters);

        }

        public async Task<RoleDTO> GetRole(string id)
        {
            var roleEntity = await _roleManager.FindByIdAsync(id);
            var role = _mapper.Map<Role>(roleEntity);
            var roleDTO = _mapper.Map<RoleDTO>(role);
            return roleDTO;
        }

        public async Task<RoleDTO> GetRoleByName(string name)
        {
            var roleEntity = await _roleManager.FindByNameAsync(name);
            var role = _mapper.Map<Role>(roleEntity);
            var roleDTO = _mapper.Map<RoleDTO>(role);
            return roleDTO;
        }

        public async Task<ResponseDTO> CreateRole(CreateRoleDTO roleDTO, string userId)
        {
            var existedRole = _roleManager.FindByNameAsync(roleDTO.Name).Result;

            if (existedRole != null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = $"Role name already Exist"
                };

            }
            //Unique name of role by group of brand id
            var role = _mapper.Map<CreateRole>(roleDTO);

            RoleEntity roleEntity = new RoleEntity
            {
                Name = roleDTO.Name,
                Status = roleDTO.Status,
                CreatorUserId = userId,
                CreationTime = DateTime.Now,
            };

            var createResult = await _roleManager.CreateAsync(roleEntity);

            if (!createResult.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = $"Error in create the role"
                };
            }

            foreach (RoleClaimDTO claim in roleDTO.Claims)
            {
                List<string> permissions = Permissions.GeneratePermission(claim.ClaimType, claim.ClaimValue);

                foreach (string permission in permissions)
                {
                    var response = await _roleManager.AddClaimAsync(roleEntity, new Claim("Permission", permission));
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

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = $"Role added successfully"
            };

        }

        public async Task<ResponseDTO> UpdateRole(RoleDTO roleDTO)
        {
            var roleEntity = await _roleManager.FindByIdAsync(roleDTO.Id);
            if (roleEntity == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status203NonAuthoritative,
                    Error = $"This role doesn't exist"
                };
            }
            else
            {
                roleEntity.Name = roleDTO.Name;
                roleEntity.Status = roleDTO.Status;
                roleEntity.NormalizedName = roleDTO.Name.Normalize();
            }


            var updateRoleResult = await _roleManager.UpdateAsync(roleEntity);
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
                        var addResult = await _roleManager.AddClaimAsync(roleEntity, new Claim("Permission", permission));
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
            if (!updateRoleResult.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = $"Error in update the role"
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = "This role Updated Successfully"
            };
        }

        public async Task<ResponseDTO> DeleteRole(string id)
        {
            //Delete Role and delete all roleClaims
            //A modifier
            var roleEntity = await _roleManager.FindByIdAsync(id);
            var deleteRoleResult = await _roleManager.DeleteAsync(roleEntity);
            if (!deleteRoleResult.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = "This role doesn't deleted"
                };
            }
            var allClaims = await _roleManager.GetClaimsAsync(roleEntity);
            foreach (var claim in allClaims)
            {
                var deleteClaimResult = await _roleManager.RemoveClaimAsync(roleEntity, claim);
                if (!deleteClaimResult.Succeeded)
                {
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Error = "This role doesn't deleted"
                    };
                }
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status400BadRequest,
                Message = "This role deleted Successfully"
            };

        }


        public List<RoleDTO> GetRolesByUserName(string username)
        {
            var userEntity = _userRepository.Repository.Get(q => q.UserName == username);
            List<RoleDTO> roles = new List<RoleDTO>();

            if (userEntity != null)
            {
                var userRoles = _userRoleRepository.Repository.GetAll(userRole => userRole.UserId == userEntity.Id);
                if (userRoles != null)
                {
                    foreach (UserRoleEntity userRole in userRoles)
                    {
                        RoleEntity roleEntity = _roleRepository.Repository.Get(role => role.Id == userRole.RoleId);
                        var role = _mapper.Map<Role>(roleEntity);
                        var roleDTO = _mapper.Map<RoleDTO>(role);
                        IList<IdentityRoleClaim<string>> claimsEntity = _roleClaimRepository.Repository.GetAll(claims => claims.RoleId == roleEntity.Id);
                        foreach (var claim in claimsEntity)
                        {
                            Modules claimType = (Modules)Enum.Parse(typeof(Modules), claim.ClaimValue.Split('.')[1]);
                            CRUDPermissions claimValue = (CRUDPermissions)Enum.Parse(typeof(CRUDPermissions), claim.ClaimValue.Split(".")[2]);
                            var IsExist = false;
                            foreach (var claimExist in roleDTO.Claims)
                            {
                                if (claimExist.ClaimType == claimType)
                                {
                                    claimExist.ClaimValue.Add(claimValue);
                                    IsExist = true;
                                }
                            }
                            if (!IsExist)
                            {
                                roleDTO.Claims.Add(new RoleClaimDTO
                                {
                                    ClaimType = claimType,
                                    ClaimValue = new List<CRUDPermissions> { claimValue }
                                });
                            }

                        }
                        roles.Add(roleDTO);

                    }
                }
            }


            return roles;
        }


    }
}
