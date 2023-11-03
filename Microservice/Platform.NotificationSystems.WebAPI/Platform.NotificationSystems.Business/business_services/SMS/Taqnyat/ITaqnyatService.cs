using Platform.NotificationSystems.DtoModel.SMS;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.NotificationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.Business.business_services.SMS.Taqnyat
{
    public interface ITaqnyatService
    {
        public HttpResponseMessage SendTaqnyat(SMSProviderDTO sMSProvider, SendSMSDTO sendSMSDTO);
    }
}
