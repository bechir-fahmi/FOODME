using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DtoModel;

public class LanguageResourceSetDTO
{
    public Guid LanguageResourceSetId { get; set; }
    public virtual List<LanguageResourceDTO> LanguageRessource { get; set; }
}
