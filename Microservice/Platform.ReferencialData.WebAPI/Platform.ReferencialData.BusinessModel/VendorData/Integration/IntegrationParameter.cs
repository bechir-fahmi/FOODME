using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel;

public class IntegrationParameter
{
    public Guid IntegrationParameterId { get; set; }
    public string Key { get; set; }
    public Matching MatchWithKey { get; set; }
    public Source QueryOrBody { get; set; }
    public ParamsType Type { get; set; }
    public Guid MethodId { get; set; }
    public int MatchWithValue { get; set; }
    public virtual IntegrationMethod Method { get; set; }    
} 
