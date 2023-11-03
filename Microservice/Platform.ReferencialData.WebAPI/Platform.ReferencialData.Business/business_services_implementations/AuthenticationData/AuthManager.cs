using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.Business.business_services_implementations.AuthenticationData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.Shared.Enum;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class AuthManager : IAuthManager
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IUnitOfWork<UserEntity> _userRepository;
        private readonly IUnitOfWork<RefreshToken> _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private UserEntity _user;

        public AuthManager(
            SignInManager<UserEntity> signInManager,
            RoleManager<RoleEntity> roleManager,
            UserManager<UserEntity> userManager,
            IUnitOfWork<UserEntity> userRepository,
            IUnitOfWork<RefreshToken> refreshTokenRepository,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        public async Task<string> CreateToken(UserEntity user)
        {
            SigningCredentials signingCredentials = GetSigningCredentials();
            List<Claim> claims = await GetClaims(user);
            JwtSecurityToken tokenOptions = GenerateTokenOption(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOption(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            DateTime expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                jwtSettings.GetSection("LifeTime").Value));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("ValidIssuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            return token;
        }

        private async Task<List<Claim>> GetClaims(UserEntity user)
        {
            var rolesUser = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id)
            };

            var userClaim = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaim);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }



        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(1),
                CreatedOn = DateTime.UtcNow
            };

        }



        public async Task<ResponseDTO> ValidateUser(LoginDTO userDTO)
        {

           
            _user = _userRepository.Repository.Get(userEntity =>
                        (userEntity.NormalizedEmail == userDTO.EmailOrUserName.ToUpper()||
                        userEntity.NormalizedUserName==userDTO.EmailOrUserName.ToUpper()) &&
                        userEntity.EmailConfirmed == true &&
                        (userEntity.Status == Status.isActive || userEntity.Status == Status.isUpdated)
                        );
            await UserBasicService.ProcessC4User(_configuration, userDTO.EmailOrUserName);
            if (_user == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Message = "Wrong Email or UserName or email not confirmed",
                    Error = "User Unauthorize"
                };
            }

            var checkPassword = await _signInManager.PasswordSignInAsync(_user, userDTO.Password, true, true);
            if (checkPassword.Succeeded)
            {
                var token = await CreateToken(_user);
                if (_user.RefreshTokens.Any(t => t.IsActive))
                {
                    var activeRefreshToken = _user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                    return new ResponseDTO
                    {
                        StatusCodes = StatusCodes.Status202Accepted,
                        Message = "Valid Email and Password",
                        Token = token,
                        RefreshToken = activeRefreshToken.Token,
                        ExpiredOn = activeRefreshToken.ExpiresOn
                    };
                }
                var refreshToken = GenerateRefreshToken();
                var userEntity = await _userManager.FindByIdAsync(_user.Id);
                userEntity.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(userEntity);

                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status202Accepted,
                    Message = "Valid Email and Password",
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    ExpiredOn = refreshToken.ExpiresOn
                };
            }
            if (checkPassword.IsLockedOut)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status401Unauthorized,
                    Message = "Account Locked out",
                    Error = "User Unauthorize"
                };
            }

            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status401Unauthorized,
                Message = "Wrong Password",
                Error = "User Unauthorize"
            };
        }
    }
}
