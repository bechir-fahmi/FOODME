
namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class UserOTPVerificationDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual UserDTO User { get; set; }
        public string OTPVerificationCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
