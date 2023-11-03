using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.DataModel.SMS
{
    [Table("SMSProvider")]
    public class SMSProviderEntity
    {
        public int Id { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public int Rank { get; set; }
        public int EndpointId { get; set; }
        public SMSProviderEndPointEntity Endpoint { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string UserAgent { get; set; }
    }
}
