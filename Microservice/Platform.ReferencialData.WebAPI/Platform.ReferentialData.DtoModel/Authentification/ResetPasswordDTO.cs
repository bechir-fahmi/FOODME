namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ResetToken { get; set; }
    }
}
