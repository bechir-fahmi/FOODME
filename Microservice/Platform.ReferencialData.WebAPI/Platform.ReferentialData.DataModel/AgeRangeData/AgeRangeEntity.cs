using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.AgeRangeData;

[Table("AgeRange")]
public class AgeRangeEntity : ReferentialDataBase
{
    public int Id { get; set; }
    public int MaxAge { get; set; }
    public int MinAge { get; set; }
}
