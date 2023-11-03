using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.UserData
{
    [Table("UserOTPVerification")]
    public class UserOTPVerificationEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public string OTPVerificationCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
