using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Platform.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.QueryData;
[Table("Query")]
public class QueryEntity
{
    [Key]
    public Guid IdQuery { get; set; }
    public Guid IdAggregator { get; set; }
    public Guid IdDynamicIntegration { get; set; }
    public MethodType Method { get; set; }
    public string Api { get; set; }
    public string Content { get; set; }
        
}
