using Microsoft.Extensions.Options;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.Business.Helper;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Platform.ReferencialData.Business.business_services_implementations.Authentication
{
    public class SMSService : ISMSService
    {
        private readonly TwilioHelper _twilioHelper;

        public SMSService(IOptions<TwilioHelper> twilioHelper)
        {
            _twilioHelper = twilioHelper.Value;
        }
        public MessageResource SendSMS(string phoneNumber, string bodySMS)
        {
            TwilioClient.Init(_twilioHelper.AccountSID, _twilioHelper.AuthToken);

            var result = MessageResource.Create(
            body: bodySMS,
            from: new Twilio.Types.PhoneNumber(_twilioHelper.TwilioPhoneNumber),
            to: new Twilio.Types.PhoneNumber(phoneNumber)
            );


            return result;



        }
    }
}
