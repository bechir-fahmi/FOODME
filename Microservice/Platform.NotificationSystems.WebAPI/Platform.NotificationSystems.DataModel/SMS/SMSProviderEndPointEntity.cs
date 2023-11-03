using System.ComponentModel.DataAnnotations.Schema;
using Platform.Shared.Enum;

namespace Platform.NotificationSystems.DataModel.SMS
{
    [Table("SMSProviderEndPoint")]
    public class SMSProviderEndPointEntity
    {
        public int Id { get; set; }
        public string UrlTemplate { get; set; }
        /*public Shared.Enum.HttpMethod HttpMethod { get; set; }*/
    }
}
