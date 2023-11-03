using Platform.ReferentialData.DtoModel.TagData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel;

public class TagLanguageDTO
{
    public int TagId { get; set; }
    public int IdLanguage { get; set; }
    public string value { get; set; }
}
