using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface IUserClaimService
    {
        Task<ResponseDTO> GetUserClaims(string userId);

        Task<ResponseDTO> AddClaimToUser(string userId, string claimName, string claimValue);

        ResponseDTO RemoveClaimFromUser(string userId, string claimName);


    }
}
