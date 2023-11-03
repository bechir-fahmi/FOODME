using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DataModel.TagData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel;
[Table("TagLanguage")]
public class TagLanguageEntity
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId { get; set; }
    public int LanguageId { get; set; }
    [Required]
    [ForeignKey("LanguageId")]
    public virtual LanguageEntity Language { get; set; }
    public string value { get; set; }
}
