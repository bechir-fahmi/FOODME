using Platform.NotificationSystems.Business.business_services.SMS.Taqnyat;
using Platform.NotificationSystems.BusinessModel.SMS;
using Platform.NotificationSystems.DtoModel.SMS;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.NotificationData;
using Platform.Shared.HttpHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.Business.business_services_implementations.SMS.Taqnyat
{
    public class TaqnyatService : ITaqnyatService
    {
        private readonly IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO> _helper;

        public TaqnyatService(IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO> helper)
        {
            _helper = helper;
        }
        public HttpResponseMessage SendTaqnyat(SMSProviderDTO sMSProvider, SendSMSDTO sendSMSDTO)
        {
            string FullURL = string.Format(sMSProvider.Endpoint.UrlTemplate, sMSProvider.Token, sendSMSDTO.TextMessage, sendSMSDTO.PhoneNumber, sMSProvider.Sender);
            return _helper.ConsumeAPI(FullURL, HttpMethod.Post);
        }
    }
}
