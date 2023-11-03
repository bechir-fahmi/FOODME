using Twilio.Rest.Api.V2010.Account;

namespace Platform.ReferencialData.Business.business_services.Authentication
{
    public interface ISMSService
    {
        MessageResource SendSMS(string phoneNumber, string bodySMS);
    }
}
