using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.BusinessModel.SMS
{
    public class SMSProvider
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string? Token { get; set; }
        public int Rank { get; set; }
        public int EndpointId { get; set; }
        public SMSProviderEndpoint Endpoint { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string UserAgent { get; set; }
    }
}
