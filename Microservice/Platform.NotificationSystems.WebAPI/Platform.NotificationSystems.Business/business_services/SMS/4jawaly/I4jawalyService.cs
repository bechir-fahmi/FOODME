using Platform.NotificationSystems.DtoModel.SMS;
using Platform.NotificationSystems.ViewModels.SMS._4jawaly;
using Platform.ReferentialData.DtoModel.Authentification;

namespace Platform.NotificationSystems.Business.business_services.SMS._4jawaly
{
    public interface I4jawalyService
    {
        public Task<ResponseDTO> Send4jawaly(SMSProviderDTO sMSProvider, List<message> messages);
    }
}
