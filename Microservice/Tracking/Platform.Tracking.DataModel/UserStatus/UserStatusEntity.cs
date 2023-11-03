using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DataModel.UserStatus
{
    public class UserStatusEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DateLogin { get; set; }
        public DateTime? DateLogout { get; set; }
        public bool Status { get; set; }

    }
}
