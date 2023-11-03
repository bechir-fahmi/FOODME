using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IUserOTPVerificationService
    {
        public void Add(UserOTPVerificationDTO userOTPVerification);
        public void Update(UserOTPVerificationDTO userOTPVerification);
        public UserOTPVerificationDTO GetOTPVerificationByUserPhoneNumber(string phoneNumber);
    }
}
