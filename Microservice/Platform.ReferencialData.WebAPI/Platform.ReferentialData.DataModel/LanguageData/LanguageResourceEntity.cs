using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Platform.ReferentialData.DataModel.LanguageData
{
    public class LanguageResourceEntity : ReferentialDataBase
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }=string.Empty;
        [Required]
        [ForeignKey("LanguageResourceSetId")]
        public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; } 
        public Guid LanguageResourceSetId { get; set; }
        public LanguageKey LanguageKey { get; set; }
        public virtual LanguageEntity Language { get; set; }

    }
}
