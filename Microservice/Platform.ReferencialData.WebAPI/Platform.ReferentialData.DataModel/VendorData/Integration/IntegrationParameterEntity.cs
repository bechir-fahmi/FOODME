using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DataModel;

[Table("IntegrationParameter")]
public class IntegrationParameterEntity
{
    [Key]
    [Required]
    public Guid IntegrationParameterId { get; set; }
    [Required]
    public string Key { get; set; }
    [Required]
    public Matching MatchWithKey { get; set; }
    [Required]
    public Source QueryOrBody { get; set; }
    [Required]
    public ParamsType Type { get; set; }
    public int MatchWithValue { get; set; }
    [Required]
    [ForeignKey("IntegrationMethodId")]
    public Guid MethodId { get; set; }
    public virtual IntegrationMethodEntity Method { get; set; }


} 
