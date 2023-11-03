using Platform.ReferencialData.BusinessModel;
using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel;

public class IntegrationParameterDTO
{
    public Guid IntegrationParameterId { get; set; }
    public string Key { get; set; }
    public Matching MatchWithKey { get; set; }
    public Source QueryOrBody { get; set; }
    public ParamsType Type { get; set; }
    public Guid MethodId { get; set; }
    public string MatchWithValue { get; set; } 
    public virtual IntegrationMethodDTO Method { get; set; }
} 
