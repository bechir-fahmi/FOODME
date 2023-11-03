using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.Tracking.DataModel.BrandAction
{
    public class BrandActionEntity 
    {
        public int Id { get; set; }
        public Guid BrandModelId { get; set; }
        public Guid UserId { get; set; }
        public DateTime TimeOfAction { get; set; }
        public TypeOfAction TypeOfAction { get; set; }
        public string? BrandName { get; set; }
    }
}
