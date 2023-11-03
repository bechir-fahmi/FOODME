using Platform.ReferentialData.DataModel.LanguageData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel;

public class LanguageResourceSetEntity : ReferentialDataBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid LanguageResourceSetId { get; set; }
    public virtual  List<LanguageResourceEntity> LanguageRessource { get; set; }
}
