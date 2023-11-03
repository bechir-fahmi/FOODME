using Platform.NotificationSystems.Business.business_services.SMS;
using Platform.NotificationSystems.Business.business_services.SMS._4jawaly;
using Platform.NotificationSystems.Business.business_services.SMS.Taqnyat;
using Platform.NotificationSystems.DtoModel.SMS;
using Platform.NotificationSystems.ViewModels.SMS._4jawaly;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.NotificationData;
using Platform.Shared.HttpHelpers;

namespace Platform.NotificationSystems.Business.business_services_implementations.SMS
{
    public class SMSService : ISMSService
    {
        private readonly ISMSProviderService _sMSProviderService;
        private readonly IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO> _helper;
        private readonly I4jawalyService _jawalyService;
        private readonly ITaqnyatService _taqnyatService;

        public SMSService(ISMSProviderService sMSProviderService, 
            IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO> helper,
            I4jawalyService jawalyService,
            ITaqnyatService taqnyatService)
        {

            _sMSProviderService = sMSProviderService;
            _helper = helper;
            _jawalyService = jawalyService;
            _taqnyatService = taqnyatService;
        }
        /// <summary>
        /// send SMS on 4jawaly sms provider
        /// if not successful use taqnyat 
        /// </summary>
        /// <param name="sendSMSDTO"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendSMS(SendSMSDTO sendSMSDTO)
        {
            SMSProviderDTO sMSProvider = _sMSProviderService.GetByRank(1);
            List<string> numbers = new ()
            {
                sendSMSDTO.PhoneNumber
            };

            List<message> messages = new()
            {
                new message ()
                {
                numbers = numbers,
                text = sendSMSDTO.TextMessage
                }
            };

            ResponseDTO response = await _jawalyService.Send4jawaly(sMSProvider, messages);
            if(response.StatusCodes == 200)
            {
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ReasonPhrase = response.Message
                };
            }
            else
            {
                SMSProviderDTO sMSProvider2 = _sMSProviderService.GetByRank(2);
                return _taqnyatService.SendTaqnyat(sMSProvider2, sendSMSDTO);
                
            }
        }
    }
}
