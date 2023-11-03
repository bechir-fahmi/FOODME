using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Platform.GenericRepository;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.EntityFramework;
using Platform.Shared.Enum;
using Platform.Shared.Images;
using Platform.Shared.Permissions;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class UserService : IUserService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IPasswordValidator<UserEntity> _passwordValidator;

        private readonly ICheckService _checkService;

        private readonly IUnitOfWork<UserEntity> _userRepository;
        private readonly IUnitOfWork<UserRoleEntity> _userRoleRepository;
        private readonly IUnitOfWork<RoleEntity> _roleRepository;
        private readonly IUnitOfWork<IdentityRoleClaim<string>> _roleClaimRepository;
        private readonly IUnitOfWork<VendorEntity> _vendorRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        private readonly ReferentialDataContext _context;

        public UserService(RoleManager<RoleEntity> roleManager,
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            ICheckService checkService,
            IUnitOfWork<UserEntity> userRepository,
            IUnitOfWork<VendorEntity> vendorRepository,
            ILogger<UserService> logger,
            IMapper mapper,
            ReferentialDataContext context,
            IPasswordValidator<UserEntity> passwordValidator,
            IUnitOfWork<UserRoleEntity> userRoleRepository,
            IUnitOfWork<RoleEntity> roleRepository,
            IUnitOfWork<IdentityRoleClaim<string>> roleClaimRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _vendorRepository = vendorRepository;
            _signInManager = signInManager;

            _checkService = checkService;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _passwordValidator = passwordValidator;
            _userRoleRepository = userRoleRepository;
            _roleRepository= roleRepository;
            _roleClaimRepository= roleClaimRepository;

        }
        public List<UserDTOInfo> GetAllUsersAsync()
        {
            IList<UserEntity> usersEntity = _userRepository.Repository.GetAll();
            var users = _mapper.Map<IList<User>>(usersEntity);
            var usersDTO = _mapper.Map<IList<UserDTOInfo>>(users);
            foreach(var userDTO in usersDTO)
            {
                
                    var vendorEntity = _vendorRepository.Repository.Get(x => x.VendorId.ToString() == userDTO.AssignedTo);
                    if (vendorEntity != null)
                    {
                        userDTO.AssignedTo = vendorEntity.Name;
                    }
            }

            return (List<UserDTOInfo>)usersDTO;
        }

        public List<UserDTOInfo> GetUsersByUserTypeAsync(string userType)
        {
            IList<UserEntity> usersEntity = (IList<UserEntity>)_userRepository.Repository.GetAll().Where(x=>x.UserType == userType).ToList();
            var users = _mapper.Map<IList<User>>(usersEntity);
            var usersDTO = _mapper.Map<IList<UserDTOInfo>>(users);

            return (List<UserDTOInfo>)usersDTO;
        }

        public PagedList<UserDTOInfo> GetAllUsersAsync(PagedParameters pagedParameters)
        {
            var usersDTOList = GetAllUsersAsync();

            return PagedList<UserDTOInfo>.ToGenericPagedList(usersDTOList, pagedParameters);
        }

        public PagedList<UserDTOInfo> GetUsersByUserTypeAsync(PagedParameters pagedParameters, string userType)
        {
            var usersDTOList = GetUsersByUserTypeAsync(userType);

            return PagedList<UserDTOInfo>.ToGenericPagedList(usersDTOList, pagedParameters);
        }

        public UserDTOInfo GetUserAsync(string id)
        {
            var userEntity = _userRepository.Repository.Get(q => q.Id == id);
            var user = _mapper.Map<User>(userEntity);
            var userDTO = _mapper.Map<UserDTOInfo>(user);

            if (userEntity != null)
            {
                var userRoles = _userRoleRepository.Repository.GetAll(userRole => userRole.UserId == id);
                if(userRoles != null)
                {
                    foreach(UserRoleEntity userRole in userRoles)
                    {
                        RoleEntity roleEntity = _roleRepository.Repository.Get(role => role.Id == userRole.RoleId);
                        var role = _mapper.Map<Role>(roleEntity);
                        var roleDTO = _mapper.Map<RoleDTO>(role);
                        IList<IdentityRoleClaim<string>> claimsEntity = _roleClaimRepository.Repository.GetAll(claims => claims.RoleId == roleEntity.Id);
                        foreach(var claim in claimsEntity)
                        {
                            Modules claimType = (Modules)Enum.Parse(typeof(Modules), claim.ClaimValue.Split('.')[1]);
                            CRUDPermissions claimValue = (CRUDPermissions)Enum.Parse(typeof(CRUDPermissions), claim.ClaimValue.Split(".")[2]);
                            var IsExist = false;
                            foreach (var claimExist in roleDTO.Claims)
                            {
                                if(claimExist.ClaimType == claimType)
                                {
                                    claimExist.ClaimValue.Add(claimValue);
                                    IsExist= true;
                                }
                            }
                            if(!IsExist)
                            {
                                roleDTO.Claims.Add(new RoleClaimDTO
                                {
                                    ClaimType = claimType,
                                    ClaimValue = new List<CRUDPermissions> { claimValue }
                                });
                            }
                            
                        } 
                        userDTO.Roles.Add(roleDTO);

                    }
                }
            }
            

            return userDTO;
        }

        public async Task<ResponseDTO> AddUser(UserDTO userDTO)
        {
            //check if the email exist 
            var userExist = await _checkService.CheckEmailExist(userDTO.Email);
            if (userExist.StatusCodes == StatusCodes.Status302Found)
            {
                return userExist;
            }
            
            //AddGeneralInformations User 
            var user = _mapper.Map<User>(userDTO);
            var userEntity = _mapper.Map<UserEntity>(user);
            userEntity.Id = Guid.NewGuid().ToString();
            userEntity.UserName = userEntity.Email;


            //check role exist in group of brand
            //  var GroupBrand = _mapper.Map<BrandGroup>(userDTO.BrandGroupId);
            // var GroupBrandEntity = _mapper.Map<BrandGroupEntity>(GroupBrand);
            var listRolesUser = new List<UserRoleEntity>();
            foreach (var roleId in userDTO.Roles)
            {
                if (roleId.Name != "CLIENT")
                {
                    if (!_context.Roles.Any(userRole => userRole.Id == roleId.Id))
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status404NotFound,
                            Error = "Not Save User,Role unfound"
                        };
                    }
                }
            }

/*            if (!_context.Api.Any(x => x.Id == GroupBrand.Id))
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Error = "Not Save User,Api unfound"
                };
            }*/

            var validPassword = await _passwordValidator.ValidateAsync(_userManager, userEntity, user.Password);

            if (validPassword.Errors.Any())
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status406NotAcceptable,
                    Error = "Password not Valid",
                    Message = userEntity.Id
                };
            }
            var createUserResult = await _userManager.CreateAsync(userEntity);

            var addPassword = await _userManager.AddPasswordAsync(userEntity, user.Password);

            listRolesUser = userDTO.Roles.Select(x => new UserRoleEntity()
            {
                UserId = userEntity.Id,
                RoleId = x.Id
            }).ToList();

            _context.UserRoles.AddRange(listRolesUser);
            _userRepository.Save();

            if (!createUserResult.Succeeded)
            {
                string errorMessage = "";
                foreach (var error in createUserResult.Errors)
                {
                    errorMessage += error.Description + "\n";
                };
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Error = $"Error while creating user, reasons : {errorMessage}",
                };
            }


            var tokenConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);

            if (tokenConfirmation != null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "User has Added successfully",
                    Error = "No error",
                    Token = tokenConfirmation
                };
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status401Unauthorized,
                Message = "No Token",
                Error = "Error in generate Token"
            };
        }

        public async Task<ResponseDTO> AddUserByAdmin(UserDTO userDTO)
        {
            //check if the email exist 
            var userExist = await _checkService.CheckEmailExist(userDTO.Email);
            if (userExist.StatusCodes == StatusCodes.Status302Found)
            {
                return userExist;
            }
            var UserNameExist = await _checkService.checkUsernameExist(userDTO.UserName);
            if (UserNameExist.StatusCodes == StatusCodes.Status302Found)
            {
                return UserNameExist;
            }
            var vendorId="";
            if ((userDTO.UserType == "brand" || userDTO.UserType=="aggregator") && userDTO.AssignedTo!=null)
            {
                var vendorExist = _vendorRepository.Repository.Get(x => x.Name == userDTO.AssignedTo);
                if (vendorExist == null)
                {
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        Error = "An error occured while saving user, brand doesn't exist"
                    };
                }
                else
                {
                    vendorId = vendorExist.VendorId.ToString();
                }
            }
             if (userDTO.Status == (Status)9)
            {
                var allUsers = GetAllUsersAsync();
                var maxRequestCode = 0;
                foreach (var User in allUsers)
                {
                    int RequestCode = 0;
                    if (User.RequestCode != null && User.RequestCode!="")
                    {
                        RequestCode = int.Parse(User.RequestCode);
                    }

                    if (RequestCode > maxRequestCode)
                    {
                        maxRequestCode = RequestCode;
                    }
                }
                maxRequestCode++;
                userDTO.RequestCode = maxRequestCode.ToString();
                userDTO.RequestTime = DateTime.UtcNow;
            }
            else
            {
                userDTO.RequestCode ="";
            }

            //AddGeneralInformations User 
            var user = _mapper.Map<User>(userDTO);
            user.FullName = userDTO.FullName;
            var userEntity = _mapper.Map<UserEntity>(user);
            userEntity.Id = Guid.NewGuid().ToString();
            userEntity.AssignedTo = vendorId;


            //check role exist in group of brand
            //  var GroupBrand = _mapper.Map<BrandGroup>(userDTO.BrandGroupId);
            // var GroupBrandEntity = _mapper.Map<BrandGroupEntity>(GroupBrand);
            var listRolesUser = new List<UserRoleEntity>();
            foreach (var roleId in userDTO.Roles)
            {
                if (roleId.Name != "CLIENT")
                {
                    if (!_context.Roles.Any(userRole => userRole.Id == roleId.Id))
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status404NotFound,
                            Error = "Not Save User,Role unfound"
                        };
                    }
                }
            }

            /*            if (!_context.Api.Any(x => x.Id == GroupBrand.Id))
                        {
                            return new ResponseDTO
                            {
                                StatusCodes = StatusCodes.Status404NotFound,
                                Error = "Not Save User,Api unfound"
                            };
                        }*/

            var validPassword = await _passwordValidator.ValidateAsync(_userManager, userEntity, user.Password);

            if (validPassword.Errors.Any())
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status406NotAcceptable,
                    Error = "Password not Valid",
                    Message = userEntity.Id
                };
            }
            if (!string.IsNullOrEmpty(userEntity.Picture))
            {

                var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, userEntity.Picture);
                userEntity.Picture = imageURL;

            }
            var createUserResult = await _userManager.CreateAsync(userEntity);
            if (userEntity.UserType == "client")
            {
                var claimResult = await _userManager.AddToRoleAsync(userEntity, "CLIENT");
                if (!claimResult.Succeeded)
                {
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Message = claimResult.ToString(),
                        Error = "User Role could not be created"
                    };

                }

            }
            var addPassword = await _userManager.AddPasswordAsync(userEntity, user.Password);

            listRolesUser = userDTO.Roles.Select(x => new UserRoleEntity()
            {
                UserId = userEntity.Id,
                RoleId = x.Id
            }).ToList();

            _context.UserRoles.AddRange(listRolesUser);
            _userRepository.Save();

            if (!createUserResult.Succeeded)
            {
                string errorMessage = "";
                foreach (var error in createUserResult.Errors)
                {
                    errorMessage += error.Description + "\n";
                };
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Error = $"Error while creating user, reasons : {errorMessage}",
                };
            }


            var tokenConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);

            if (tokenConfirmation != null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "User has Added successfully",
                    Error = "No error",
                    Token = tokenConfirmation
                };
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status401Unauthorized,
                Message = "No Token",
                Error = "Error in generate Token"
            };
        }

        public async Task<ResponseDTO> Update(UserDTO userDTO)
        {

            UserEntity userEntityExist = _userRepository.Repository.Get(user => user.Id == userDTO.Id);
            
            if (userEntityExist != null)
            {
                var AncientEmail = userEntityExist.Email;
                if (userEntityExist.Email != userDTO.Email)
                {
                    var EmailExist = _userRepository.Repository.Get(user => user.Email == userDTO.Email && user.Id != userDTO.Id);
                    if (EmailExist != null)
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status400BadRequest,
                            Error = $"Email {userDTO.Email} already exist",
                            Message = ""
                        };

                    }


                }
                if (!string.IsNullOrEmpty(userEntityExist.Picture))
                {
                    ImageHelper.DeleteImage(ImageHelper.SaveUrl, userEntityExist.Picture);
                }
                if (!string.IsNullOrEmpty(userDTO.Picture))
                {

                    var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, userDTO.Picture);
                    userDTO.Picture = imageURL;

                }
                userEntityExist.Email = userDTO.Email;
                userEntityExist.NormalizedEmail = userDTO.Email.ToUpper();
                userEntityExist.FullName = userDTO.FullName;
                userEntityExist.UserName = userDTO.Email;
                userEntityExist.NormalizedUserName = userDTO.Email.ToUpper();
                userEntityExist.Picture = userDTO.Picture;
                userEntityExist.PhoneNumber = userDTO.PhoneNumber;
                userEntityExist.Location = userDTO.Location;
                userEntityExist.Status = userDTO.Status;
                userEntityExist.Gender = userDTO.Gender;
                userEntityExist.Age = userDTO.Age;
                userEntityExist.UserType = userDTO.UserType;
                userEntityExist.AssignedTo = userDTO.AssignedTo;
                userEntityExist.LastModificationTime = DateTime.Now;
                userEntityExist.LastModifierUserId = userDTO.LastModifierUserId;


                _userRepository.Repository.Update(userEntityExist);
                _userRepository.Save();
                if (AncientEmail != userEntityExist.Email)
                {
                    var tokenConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(userEntityExist);
                    userEntityExist.EmailConfirmed = false;
                    _userRepository.Repository.Update(userEntityExist);
                    _userRepository.Save();
                    if (tokenConfirmation != null)
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status200OK,
                            Message = "User updated successfully",
                            Error = "No error",
                            Token = tokenConfirmation
                        };
                    }
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status401Unauthorized,
                        Message = "No Token",
                        Error = "Error in generate Token"
                    };

                }
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Error = "User updated successfully",
                    Message = userDTO.Id
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status404NotFound,
                Error = "User Not Found",
                Message = $"user Id: {userDTO.Id}"
            };
        }
        public async Task<ResponseDTO> UpdateUserByAdmin(UserDTO userDTO)
        {

            UserEntity userEntityExist = _userRepository.Repository.Get(user => user.Id == userDTO.Id);

            if (userEntityExist != null)
            {
                var AncientEmail = userEntityExist.Email;
                if (userEntityExist.Email != userDTO.Email)
                {
                    var EmailExist = _userRepository.Repository.Get(user => user.Email == userDTO.Email && user.Id != userDTO.Id);
                    if (EmailExist != null)
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status400BadRequest,
                            Error = $"Email {userDTO.Email} already exist",
                            Message = ""
                        };

                    }
                }

                UserEntity UserNameExist = null;
                if (userEntityExist.UserName != userDTO.UserName && userDTO.UserName!="")
                {
                    UserNameExist = _userRepository.Repository.Get(user => user.UserName == userDTO.UserName && user.Id != userDTO.Id);
                    if (UserNameExist != null)
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status400BadRequest,
                            Error = $"UserName {userDTO.UserName} already exist",
                            Message = ""
                        };

                    }
                }
                var vendorId = "";
                if ((userDTO.UserType == "brand" || userDTO.UserType == "aggregator") && userDTO.AssignedTo != null)
                {
                    var vendorExist = _vendorRepository.Repository.Get(x => x.Name == userDTO.AssignedTo);
                    if (vendorExist == null)
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status404NotFound,
                            Error = "An error occured while saving user, brand doesn't exist"
                        };
                    }
                    else
                    {
                        vendorId = vendorExist.VendorId.ToString();
                    }
                }
                if (!string.IsNullOrEmpty(userEntityExist.Picture))
                {
                    ImageHelper.DeleteImage(ImageHelper.SaveUrl, userEntityExist.Picture);
                }
                if (!string.IsNullOrEmpty(userDTO.Picture))
                {

                    var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, userDTO.Picture);
                    userDTO.Picture = imageURL;

                }
                userEntityExist.Email = userDTO.Email;
                userEntityExist.NormalizedEmail = userDTO.Email.ToUpper();
                userEntityExist.FullName = userDTO.FullName;
                userEntityExist.UserName = userDTO.UserName;
                userEntityExist.NormalizedUserName = userDTO.UserName.ToUpper();
                userEntityExist.Picture = userDTO.Picture;
                userEntityExist.PhoneNumber = userDTO.PhoneNumber;
                userEntityExist.Location = userDTO.Location;
                userEntityExist.Status = userDTO.Status;
                userEntityExist.Gender = userDTO.Gender;
                userEntityExist.Age = userDTO.Age;
                userEntityExist.UserType = userDTO.UserType;
                userEntityExist.AssignedTo = vendorId;
                userEntityExist.LastModificationTime = DateTime.Now;
                userEntityExist.LastModifierUserId = userDTO.LastModifierUserId;

                // Remove user Role Ancient
                List<UserRoleEntity> userRoleEntities = (List<UserRoleEntity>)_userRoleRepository.Repository.GetAll(userRole => userRole.UserId == userDTO.Id);
                _context.UserRoles.RemoveRange(userRoleEntities);
                _context.SaveChanges();
                var listRolesUser = userDTO.Roles.Select(x => new UserRoleEntity()
                {
                    UserId = userDTO.Id,
                    RoleId = x.Id
                }).ToList();
                //Add Updates Roles
                _context.UserRoles.AddRange(listRolesUser);
                _context.SaveChanges();

                _userRepository.Repository.Update(userEntityExist);
                _userRepository.Save();
                if (AncientEmail != userEntityExist.Email)
                {
                    var tokenConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(userEntityExist);
                    userEntityExist.EmailConfirmed = false;
                    _userRepository.Repository.Update(userEntityExist);
                    _userRepository.Save();
                    if (tokenConfirmation != null)
                    {
                        return new ResponseDTO
                        {
                            StatusCodes = StatusCodes.Status200OK,
                            Message = "User updated successfully",
                            Error = "No error",
                            Token = tokenConfirmation
                        };
                    }
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status401Unauthorized,
                        Message = "No Token",
                        Error = "Error in generate Token"
                    };

                }
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Error = "User updated successfully",
                    Message = userDTO.Id
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status404NotFound,
                Error = "User Not Found",
                Message = $"user Id: {userDTO.Id}"
            };
        }
        public void Update(string id, Status status)
        {
            UserEntity userEntity = _userRepository.Repository.Get(user => user.Id == id);
            if (userEntity != null)
            {
                userEntity.Status = status;
                if (status == (Status)7)
                {
                    userEntity.ApprovalTime = DateTime.UtcNow;
                }
                if (status == (Status)8)
                {
                    userEntity.RejectTime = DateTime.UtcNow;
                }
                if (status == (Status)1)
                {
                    userEntity.DeletionTime = DateTime.UtcNow;
                }
                _userRepository.Repository.Update(userEntity);
                _userRepository.Save();
            }
        }

        public async Task Delete(string userId)
        {
            UserEntity userEntity = await _userManager.FindByIdAsync(userId);
            
            userEntity.Status = Shared.Enum.Status.isDeleted;
            _userRepository.Repository.Update(userEntity);
            _userRepository.Save();
        }

        public UserDTO GetUserByPhoneNumber(string phoneNumber)
        {
            var userEntity = _userRepository.Repository.Get(q => q.PhoneNumber == phoneNumber);
            var user = _mapper.Map<User>(userEntity);
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public UserDTO GetUser(string id)
        {
            var userEntity = _userRepository.Repository.Get(q => q.Id == id);
            var user = _mapper.Map<User>(userEntity);
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }
        public async Task<IList<UserDTOInfo>> GetAllUsersWithGenderAsync()
        {
            IList<UserEntity> usersEntity = _userRepository.Repository.GetAll().Where(u => u.Gender != null).ToList();
            var userBMList = _mapper.Map<IList<User>>(usersEntity);
            var userDTOList = _mapper.Map<IList<UserDTOInfo>>(userBMList);


            return userDTOList;
        }

        /********************************/
        public async Task<IList<UserDTO>> GetCustomerLastweekAsync()
        {
            IList<UserEntity> usersEntity = _userRepository.Repository.GetAll().Where(u => u.Gender != null).ToList();
            var userBMList = _mapper.Map<IList<User>>(usersEntity);
            var userDTOList = _mapper.Map<IList<UserDTOInfo>>(userBMList);
            var userDTO = _mapper.Map<IList<UserDTO>>(userBMList);



            return userDTO;
        }

        /********************************/
        public async Task<IList<UserDTO>> GetCustomerLastDayAsync()
        {
            IList<UserEntity> usersEntity = _userRepository.Repository.GetAll().Where(u => u.Gender != null).ToList();
            var userBMList = _mapper.Map<IList<User>>(usersEntity);
            var userDTOList = _mapper.Map<IList<UserDTOInfo>>(userBMList);
            var userDTO = _mapper.Map<IList<UserDTO>>(userBMList);



            return userDTO;
        }
    }
}
