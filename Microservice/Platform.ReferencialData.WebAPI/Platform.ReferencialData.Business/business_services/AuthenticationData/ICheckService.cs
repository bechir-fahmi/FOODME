using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface ICheckService
    {
        Task<ResponseDTO> CheckEmailExist(string email);

        Task<ResponseDTO> checkUsernameExist(string userName);

        Task<ResponseDTO> checkPhoneNumberExist(string phoneNumber);

        public ResponseDTO checkUserExistByPhoneNumber(string phoneNumber);
        bool checkPhoneNumberValidity(string phoneNumber);
    }
}
