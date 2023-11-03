using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IAuthManager
    {
        Task<ResponseDTO> ValidateUser(LoginDTO userDTO);

        Task<string> CreateToken(UserEntity user);
        RefreshToken GenerateRefreshToken();
    }
}
