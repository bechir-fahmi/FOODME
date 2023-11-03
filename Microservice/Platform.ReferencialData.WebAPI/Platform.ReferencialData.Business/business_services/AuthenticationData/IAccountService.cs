using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IAccountService
    {
        Task<ResponseDTO> Register(RegisterDTO userDTO);
        Task<ResponseDTO> SignInExternalProvider(UserLoginDTO userLoginDTO);

        Task<ResponseDTO> Login(LoginDTO userDTO);

        void LogOut(string userId);
        Task<ResponseDTO> updatePassword(UpdatePasswordDTO updatePasswordDTO,string id);

        Task<ResponseDTO> ForgetPassword(string email);
        Task<ResponseDTO> CheckEmailAndGenerateCode(string email);
        Task<ResponseDTO> ConfirmCodeAndGenerateToken(string email, string code);
        Task<ResponseDTO> ResetPassword(ResetPasswordDTO resetPasswordDTO);

        Task<ResponseDTO> ConfirmEmail(string userEmail, string code);

        Task<ResponseDTO> RefreshTokenAsync(string token);

        Task<bool> RevokeTokenAsync(string token);
    }
}
