using AutoMapper;
using Microsoft.AspNetCore.Http;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.Business.business_services.NotificationData;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.NotificationData;
using Platform.Shared.Enum;
using Platform.Shared.HttpHelpers;
using Platform.Shared.MicroservicesURLs;

namespace Platform.ReferencialData.Business.business_services_implementations.NotificationData
{
    public class MessageService : IMessageService
    {
        private readonly ICheckService _checkService;
        private readonly IUserOTPVerificationService _userOTPVerificationService;
        private readonly IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO> _helper;
        private readonly IAuthManager _authManager;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;

        public MessageService(ICheckService checkService,
            IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO> helper,
            IUserOTPVerificationService userOTPVerificationService,
            IAuthManager authManager,
            IUserService userService,
            IRoleService roleService,
            IMapper mapper,
            IUserRoleService userRoleService)
        {
            _checkService = checkService;
            _helper = helper;
            _userOTPVerificationService = userOTPVerificationService;
            _authManager = authManager;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
            _userRoleService = userRoleService;
        }
        public async Task<ResponseDTO> AuthenticateUserAndSendVerificationCodeAsync(UserOTPInfo userDTO)
        {
            if (_checkService.checkPhoneNumberValidity(userDTO.PhoneNumber))
            {
                ResponseDTO response = _checkService.checkUserExistByPhoneNumber(userDTO.PhoneNumber);
                if (response.StatusCodes == StatusCodes.Status200OK)
                {
                    return SendVerificationCode(userDTO, response.Message);
                }
                else
                {
                    return new ResponseDTO()
                    {
                        StatusCodes = StatusCodes.Status400BadRequest,
                        Error = $"user doesn't exist"
                    };
                }
            }
            else
            {
                return new ResponseDTO()
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Error = $"Invalid phone number"
                };
            }
        }
        private ResponseDTO SendVerificationCode(UserOTPInfo userDTO, string userId)
        {
            string verificationCode = new Random().Next(1000, 9999).ToString();
            //test account
            if (userDTO.PhoneNumber == "515553891")
            {
                verificationCode = "1234";
                UserOTPVerificationDTO userOTPVerification = new()
                {
                    UserId = userId,
                    PhoneNumber = userDTO.PhoneNumber,
                    OTPVerificationCode = verificationCode,
                };
                AddOrUpdateOTPVerification(userOTPVerification);
                return new ResponseDTO()
                {
                    StatusCodes = StatusCodes.Status200OK,
                    Message = "verification code added succefuly "
                    

                };
            }
                
            //TO DO : define message text in config file
            string OTPMessage = $" Your FoodME Verification Code Is : {verificationCode}";
            SendSMSDTO SendSMSDTO = new()
            {
                PhoneNumber = userDTO.PhoneNumber,
                TextMessage = OTPMessage,
            };
            string endPoint = $"{Microservice.NotifSystems}/SMSProvider/SendSMS";
            ResponseDTO notifSystemResponse = SendToNotifSystemsMicroService(endPoint, SendSMSDTO);
            if (notifSystemResponse.StatusCodes == StatusCodes.Status200OK)
            {
                UserOTPVerificationDTO userOTPVerification = new()
                {
                    UserId = userId,
                    PhoneNumber = userDTO.PhoneNumber,
                    OTPVerificationCode = verificationCode,
                };
                AddOrUpdateOTPVerification(userOTPVerification);
                return notifSystemResponse;
            }
            else
                return notifSystemResponse;
        }
        private void AddOrUpdateOTPVerification(UserOTPVerificationDTO userOTPVerification)
        {
            UserOTPVerificationDTO userOTPVerificationDTO = _userOTPVerificationService.GetOTPVerificationByUserPhoneNumber(userOTPVerification.PhoneNumber);
            if(userOTPVerificationDTO != null)
            {
                userOTPVerificationDTO.OTPVerificationCode = userOTPVerification.OTPVerificationCode;
                _userOTPVerificationService.Update(userOTPVerificationDTO);
            }
            else
            {
                _userOTPVerificationService.Add(userOTPVerification);
            }
        }

        private ResponseDTO SendToNotifSystemsMicroService(string endPoint, SendSMSDTO sendSMSDTO)
        {
            HttpResponseMessage responseMessage = _helper.Create(_mapper, endPoint, sendSMSDTO);
            if (responseMessage.IsSuccessStatusCode)
            {
                return new ResponseDTO()
                {
                    StatusCodes = StatusCodes.Status200OK,
                };
            }
            else
            {
                return new ResponseDTO()
                {
                    StatusCodes = StatusCodes.Status500InternalServerError,
                    Error = "Faild to send OTP"
                };
            }
        }
        public async Task<ResponseDTO> VerifyUserOTPVerificationCodeAsync(OTPVerificationDTO userOTPInfo)
        {
            UserOTPVerificationDTO userOTPVerificationDTO = _userOTPVerificationService.GetOTPVerificationByUserPhoneNumber(userOTPInfo.PhoneNumber);
            if (userOTPVerificationDTO != null)
            {
                if(userOTPVerificationDTO.OTPVerificationCode == userOTPInfo.OTPVerificationCode) 
                {
                    UserDTO userDTO = _userService.GetUserByPhoneNumber(userOTPVerificationDTO.PhoneNumber);
                    if (userDTO != null)
                    {
                        if(userDTO.Status == Status.isInactive)
                        {
                            _userService.Update(userDTO.Id, Status.isActive);
                        }
                        User user = _mapper.Map<User>(userDTO);
                        UserEntity userEntity = _mapper.Map<UserEntity>(user);
                        string accessToken = await _authManager.CreateToken(userEntity);
                        return new ResponseDTO()
                        {
                            StatusCodes = StatusCodes.Status200OK,
                            Token = accessToken
                        };
                    }
                    else
                    {
                        return new ResponseDTO()
                        {
                            StatusCodes = StatusCodes.Status404NotFound,
                            Error = "User does not exist"
                        };
                    }
                }
                else
                {
                    return new ResponseDTO()
                    {
                        StatusCodes = StatusCodes.Status404NotFound,
                        Error = "Invalid Verification Code"
                    };
                }
            }
            else
            {
                return new ResponseDTO()
                {
                    StatusCodes = StatusCodes.Status404NotFound,
                    Error = "User Verification Code does not exist"
                };
            }
        }
    }
}
