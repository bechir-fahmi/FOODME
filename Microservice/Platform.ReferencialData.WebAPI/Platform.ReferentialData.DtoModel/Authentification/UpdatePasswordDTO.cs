using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel.Authentification
{
    public class UpdatePasswordDTO
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
