using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferencialData.BusinessModel.QueryData;

public class Query
{
    public Guid IdQuery { get; set; }
    public Guid IdAggregator { get; set; }
    public Guid IdDynamicIntegration { get; set; }
    public MethodType Method { get; set; }
    public string Api { get; set; }
    public string Content { get; set; }
}
