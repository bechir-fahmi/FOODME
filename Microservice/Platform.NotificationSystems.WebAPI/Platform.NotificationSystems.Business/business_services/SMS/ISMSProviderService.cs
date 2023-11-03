using Platform.NotificationSystems.DtoModel.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.Business.business_services.SMS
{
    public interface ISMSProviderService
    {
        SMSProviderDTO GetByRank(int rank);
        public void Add(SMSProviderDTO smsProviderDTO);
    }
}
