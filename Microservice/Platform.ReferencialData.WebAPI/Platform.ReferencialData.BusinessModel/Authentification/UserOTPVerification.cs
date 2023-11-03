namespace Platform.ReferencialData.BusinessModel.Authentification
{
    public class UserOTPVerification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string OTPVerificationCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
