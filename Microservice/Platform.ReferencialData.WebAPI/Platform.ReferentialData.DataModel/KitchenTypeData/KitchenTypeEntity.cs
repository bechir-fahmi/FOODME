using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.KitchenTypeData
{
    public class KitchenTypeEntity: ReferentialDataBase
    {
        public int Id { get; set; }
  
        public Guid NameLabelCode { get; set; }

        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }
        [ForeignKey("LanguageResourceSetId")]
        public Guid LanguageResourceSetId { get; set; }
        public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
        public virtual List<TagKitchenTypeEntity> Tags { get; set; }

    }
}
