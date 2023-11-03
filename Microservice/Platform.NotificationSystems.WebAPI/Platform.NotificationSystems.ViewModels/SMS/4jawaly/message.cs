using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.NotificationSystems.ViewModels.SMS._4jawaly
{
    public class message
    {
        public string text { get; set; }
        public List<string> numbers { get; set; }

    }
    public class Message
    {
        public int inserted_numbers { get; set; }
        public Message2 message { get; set; }
    }

    public class Message2
    {
        public int account_id { get; set; }
        public string job_id { get; set; }
        public string text { get; set; }
        public int sender_id { get; set; }
        public string sender_name { get; set; }
        public string encoding { get; set; }
        public int length { get; set; }
        public int per_message { get; set; }
        public int remaining { get; set; }
        public int messages { get; set; }
        public object send_at { get; set; }
        public object send_at_zone { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public int id { get; set; }
        public Account account { get; set; }
    }
}
