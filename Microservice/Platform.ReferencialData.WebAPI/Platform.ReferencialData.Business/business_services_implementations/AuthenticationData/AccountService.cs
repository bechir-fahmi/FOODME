using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.DataModel.UserData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.Enum;
using Platform.Shared.Images;
using System.Security.Cryptography;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserEntity> _userManager;

        private readonly IAuthManager _authManager;

        private readonly SignInManager<UserEntity> _signInManager;

        private readonly ICheckService _checkService;

        private readonly RoleManager<RoleEntity> _roleManager;


        private readonly ILogger<AccountService> _logger;

        private readonly IMapper _mapper;

        private readonly IPasswordValidator<UserEntity> _passwordValidator;
        private readonly IUnitOfWork<IdentityUserLogin<string>> _userLoginRepository;
        private readonly IUnitOfWork<UserEntity> _userRepository;



        public AccountService(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            RoleManager<RoleEntity> roleManager,
            IUnitOfWork<IdentityUserLogin<string>> userLoginRepository,
            IUnitOfWork<UserEntity> userRepository,
            IAuthManager authManager,
            ICheckService checkService,
            ILogger<AccountService> logger,
            IMapper mapper,
            IPasswordValidator<UserEntity> passwordValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
            _checkService = checkService;
            _roleManager = roleManager;
            _userLoginRepository = userLoginRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _passwordValidator = passwordValidator;
        }

        //Register User application Mobile
        public async Task<ResponseDTO> Register(RegisterDTO userDTO)
        {

            var userEmailExist = _checkService.CheckEmailExist(userDTO.Email);
            if (userEmailExist.Result.StatusCodes != StatusCodes.Status200OK)
            {
                return userEmailExist.Result;
            }

            var userMapped = _mapper.Map<User>(userDTO);
            var user = GenerateBaseData(userMapped);


            var userEntity = _mapper.Map<UserEntity>(user);
            if (userEntity.UserName == null|| userEntity.UserName=="") {
                userEntity.UserName = user.Email;
            }

            if (!string.IsNullOrEmpty(userEntity.Picture))
            {

                var imageURL = ImageHelper.SaveImage(ImageHelper.SaveUrl, userEntity.Picture);
                userEntity.Picture = imageURL;

            }
            var result = await _userManager.CreateAsync(userEntity, userDTO.Password);
            
            if (!result.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Message = result.ToString(),
                    Error = "User could not created"
                };
            }
            if (userEntity.UserType == "client")
            {
                var claimResult = await _userManager.AddToRoleAsync(userEntity, "CLIENT");
                if (!claimResult.Succeeded)
                {
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Message = result.ToString(),
                        Error = "User Role could not be created"
                    };

                }

            }
            
            var tokenConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);

            if (tokenConfirmation != null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status201Created,
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

        public async Task<ResponseDTO> SignInExternalProvider(UserLoginDTO userLoginDTO)
        {
            
            var userLoginExist = _userLoginRepository.Repository.Get(x => x.ProviderKey == userLoginDTO.ProviderKey);
            if (userLoginExist == null)
            {
                var emailExist = _userRepository.Repository.Get(x => x.Email == userLoginDTO.UserName);
                if (emailExist != null)
                {
                    return new ResponseDTO()
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Message = "email already exist"
                    };

                }
                var newUser = new UserEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userLoginDTO.UserName,
                    NormalizedUserName = userLoginDTO.UserName.ToUpper(),
                    Email = userLoginDTO.UserName,
                    NormalizedEmail = userLoginDTO.UserName.ToUpper(),
                    UserType = "client",
                    AuthentificationSource = (AuthentificationSource)Enum.Parse(typeof(AuthentificationSource), userLoginDTO.LoginProvider),
                    CreationTime = DateTime.UtcNow,
                };
                var adduserReult = _userRepository.Repository.InsertAsync(newUser);
                await _userRepository.SaveAsync();
                var claimResult = await _userManager.AddToRoleAsync(newUser, "CLIENT");
                if (!claimResult.Succeeded)
                {
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Message = claimResult.ToString(),
                        Error = "User Role could not be created"
                    };

                }
                UserLogin userLogin = _mapper.Map<UserLogin>(userLoginDTO);
                IdentityUserLogin<string> userLoginEntity = _mapper.Map<IdentityUserLogin<string>>(userLogin);

                userLoginEntity.UserId= newUser.Id;

                var resutlt = _userLoginRepository.Repository.InsertAsync(userLoginEntity);
                _userLoginRepository.Save();
                if (!resutlt.IsCompletedSuccessfully)
                {
                    return new ResponseDTO()
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Error = resutlt.Exception.ToString(),
                        Message = "An error ocurred while registring the user"
                    };
                }

            }
            var UserLogin = _userLoginRepository.Repository.Get(x => x.ProviderKey == userLoginDTO.ProviderKey);
            var userEntity = _userRepository.Repository.Get(x => x.Id == UserLogin.UserId);
            var token = await _authManager.CreateToken(userEntity);
            if (userEntity.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = userEntity.RefreshTokens.FirstOrDefault(t => t.IsActive);
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status202Accepted,
                    Message = "Valid Email",
                    Token = token,
                    RefreshToken = activeRefreshToken.Token,
                    ExpiredOn = activeRefreshToken.ExpiresOn
                };
            }
            var refreshToken = _authManager.GenerateRefreshToken();
            var user= await _userManager.FindByIdAsync(userEntity.Id);
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status202Accepted,
                Message = "Valid Email",
                Token = token,
                RefreshToken = refreshToken.Token,
                ExpiredOn = refreshToken.ExpiresOn
            };
        }

        public async Task<ResponseDTO> Login(LoginDTO userDTO)
        {
            var result = await _authManager.ValidateUser(userDTO);
            return result;
        }

        public async void LogOut(string userId)
        {
            //if (_signInManager.IsSignedIn(userId)) await _signInManager.SignOutAsync();

        }

        public async Task<ResponseDTO> ConfirmEmail(string userEmail, string code)
        {
            if (userEmail == null || code == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Message = "Log email failed",
                    Error = "Log email failed"
                };
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Message = "Invalid Email parametre",
                    Error = "Wrong Email "
                };
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "Thank you for confirming your email",
                    Error = "No error"
                };
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status400BadRequest,
                Message = "Your email is not confirmed, please try again later",
                Error = "Email didn't confirmed"
            };

        }

        public async Task<ResponseDTO> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "This is token confirmation",
                    Token = token
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status401Unauthorized,
                Message = "User not exist",
                Error = "User unauthorized"
            };
        }
        public async Task<ResponseDTO> CheckEmailAndGenerateCode(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var code = GenerateRandomCode();
                user.ResetCode=code;
                user.ResetCodeExpireTime= DateTime.Now.AddHours(1);
                await _userManager.UpdateAsync(user);
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "This is Code confirmation",
                    Token = code
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status401Unauthorized,
                Message = "User not exist",
                Error = "User unauthorized"
            };
        }
        private string GenerateRandomCode()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(2));
        }
        public async Task<ResponseDTO> ConfirmCodeAndGenerateToken(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.ResetCode==code && user.ResetCodeExpireTime>DateTime.Now)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "This is reset token",
                    Token = token
                };
            }
            else if (user == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Message = "User not exist",
                    Error = "User not exist"
                };

            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status401Unauthorized,
                Message = "Reset code invalid or expired",
                Error = "User unauthorized"
            };
        }
        public async Task<ResponseDTO> updatePassword(UpdatePasswordDTO updatePasswordDTO, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Message = "update password failed",
                    Error = "user unaothorized "
                };
            }

            var validPassword = await _passwordValidator.ValidateAsync(_userManager, user, updatePasswordDTO.NewPassword);

            if (validPassword.Errors.Any())
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status406NotAcceptable,
                    Error = "Password not Valid",
                    Message = user.Id
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, updatePasswordDTO.CurrentPassword, updatePasswordDTO.NewPassword);
            if (result.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "password updated with success",
                    Error = "No error"
                };
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status401Unauthorized,
                Message = "Username and password are not correct",
                Error = "Username and password are not correct"
            };

        }
        public async Task<ResponseDTO> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (resetPasswordDTO.Email == null || resetPasswordDTO.ResetToken == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Message = "Log email failed",
                    Error = "Log email failed"
                };
            }
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Message = "Invalid Email parametre",
                    Error = "Wrong Email "
                };
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.ResetToken, resetPasswordDTO.Password);
            if (result.Succeeded)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "Thank you for confirming your Password",
                    Error = "No error"
                };
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status400BadRequest,
                Message = "Your email is not confirmed, please try again later",
                Error = "Email didn't confirmed"
            };

        }

        public async Task<ResponseDTO> RefreshTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Error = "Invalid token"
                };
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Error = "Invalid token"
                };
            }

            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = _authManager.GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            var newToken = await _authManager.CreateToken(user);
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = "Refresh token",
                Token = newToken,
                RefreshToken = newRefreshToken.Token,
                ExpiredOn = newRefreshToken.ExpiresOn

            };
        }


        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return true;
        }

        private User GenerateBaseData(User userBM)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FullName = userBM.FullName,
                Email = userBM.Email,
                Password = userBM.Password,
                PhoneNumber = userBM.PhoneNumber,
                Picture = userBM.Picture,
                Gender = userBM.Gender,
                Age = userBM.Age,
                UserType = userBM.UserType,
                UserAddresses = userBM.UserAddresses,
                TwoFactorEnabled = userBM.TwoFactorEnabled,
                MacAddress = userBM.MacAddress,
                CreationTime = DateTime.UtcNow,
                AuthentificationSource = AuthentificationSource.FoodMeClient,
                Status = Status.isActive,

            };
            return user;
        }
    }
}
