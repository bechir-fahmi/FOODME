using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Tracking.DtoModel.UserSearch
{
    public class UserSearchDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? SearchText { get; set; }
        public DateTime SearchTime { get; set; }
        public bool HasResults { get; set; }
    }
}
