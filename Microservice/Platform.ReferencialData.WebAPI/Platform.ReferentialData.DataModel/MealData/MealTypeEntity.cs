using Platform.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platform.ReferentialData.DataModel.MealData;

public class MealTypeEntity : ReferentialDataBase
{
    public int Id { get; set; }

    public Guid NameLabelCode { get; set; }

    public Guid ImageLabelCode { get; set; }
    public string Name { get; set; }
    [ForeignKey("LanguageResourceSetId")]
    public Guid LanguageResourceSetId { get; set; }
    public virtual LanguageResourceSetEntity LanguageResourceSet { get; set; }
    public virtual List<TagMealTypeEntity> Tags { get; set; }
}