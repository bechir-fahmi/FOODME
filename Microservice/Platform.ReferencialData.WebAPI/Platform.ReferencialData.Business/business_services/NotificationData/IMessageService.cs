using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferentialData.DtoModel.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business.business_services.NotificationData
{
    public interface IMessageService
    {
        public Task<ResponseDTO> AuthenticateUserAndSendVerificationCodeAsync(UserOTPInfo userDTO);
        public Task<ResponseDTO> VerifyUserOTPVerificationCodeAsync(OTPVerificationDTO userOTPInfo);
    }
}
