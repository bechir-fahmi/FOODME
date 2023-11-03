using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.DtoModel.SMS
{
    public class SMSProviderDTO
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string? Token { get; set; }
        public int Rank { get; set; }
        public int EndpointId { get; set; }
        public SMSProviderEndpointDTO Endpoint { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string UserAgent { get; set; }
    }
}
