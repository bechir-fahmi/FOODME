using Platform.ReferentialData.DtoModel.NotificationData;

namespace Platform.NotificationSystems.Business.business_services.SMS
{
    public interface ISMSService
    {
        Task<HttpResponseMessage> SendSMS(SendSMSDTO sendSMSDTO);
    }
}
