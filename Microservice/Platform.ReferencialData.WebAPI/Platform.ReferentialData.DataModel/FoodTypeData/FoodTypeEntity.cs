using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.FoodTypeData
{
    [Table("FoodType")]
    public class FoodTypeEntity : ReferentialDataBase
    {
        public int Id { get; set; }
        public Guid NameLabelCode { get; set; }
        public Guid ImageLabelCode { get; set; }
        public string Name { get; set; }
        [ForeignKey("LanguageResourceSetId")]
        public Guid LanguageResourceSetId { get; set; }
        public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
        public virtual List<TagFoodTypeEntity> Tags { get; set; }

    }
}