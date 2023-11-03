using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.SupportService
{
    public class SuportCategoryEntity: ReferentialDataBase
    {
        public int Id { get; set; }

        public  HelpSupportType HelpSupportType { get; set; }

        public Guid NameLabelCode { get; set; }

        public Guid Image { get; set; }
    }
}
