using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using System.Text.RegularExpressions;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class CheckService : ICheckService
    {
        private readonly UserManager<UserEntity> _userManager;

        private readonly IUnitOfWork<UserEntity> _unitOfWork;

        private readonly ILogger<CheckService> _logger;

        private readonly IMapper _mapper;

       // public const string PHONE_NUMBER_PATTERN = @"^(\+216)?[0-9]{8}$";
        public const string PHONE_NUMBER_PATTERN = @"^(\+966)?5[0-9]{8}$";

        public CheckService(UserManager<UserEntity> userManager, ILogger<CheckService> logger, IMapper mapper, IUnitOfWork<UserEntity> unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseDTO> CheckEmailExist(string email)
        {
            var userEmailExist = await _userManager.FindByEmailAsync(email);
            if (userEmailExist != null)
            {
                _logger.LogInformation($"Email {email} is already exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status302Found,
                    Message = "Error",
                    Error = $"Email {email} is already exist"
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = $"Email {email} does not exist"
            };
        }

        public async Task<ResponseDTO> checkPhoneNumberExist(string phoneNumber)
        {
            var user = await _unitOfWork.Repository.GetAsync(q => q.PhoneNumber == phoneNumber);
            if (user != null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status302Found,
                    Message = "Error",
                    Error = $"Phone Number {phoneNumber} is already exist"
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = $"Phone Number {phoneNumber} does not exist"
            };
        }

        public async Task<ResponseDTO> checkUsernameExist(string userName)
        {
            var userNameExist = await _userManager.FindByNameAsync(userName);
            if (userNameExist != null)
            {
                _logger.LogInformation($"UserName {userName} is already exist");
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status302Found,
                    Message = "Error",
                    Error = $"UserName {userName} is already exist"
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = $"Email {userName} does not exist"
            };
        }

        public ResponseDTO checkUserExistByPhoneNumber(string phoneNumber)
        {
            var user = _unitOfWork.Repository.Get(q => q.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Message = "Error",
                    Error = $"User with Phone Number {phoneNumber} does not exist"
                };
            }
            return new ResponseDTO
            {
                StatusCodes = StatusCodes.Status200OK,
                Message = user.Id
            };
        }

        public bool checkPhoneNumberValidity(string phoneNumber)
        {
            bool isPhoneNumberValid = Regex.IsMatch(phoneNumber, PHONE_NUMBER_PATTERN);
            return isPhoneNumberValid;
        }
    }
}
