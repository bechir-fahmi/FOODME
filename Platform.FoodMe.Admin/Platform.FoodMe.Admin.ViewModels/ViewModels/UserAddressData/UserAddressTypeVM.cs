using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.ReferencialData.BusinessModel;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.UserAddressData
{
    public class UserAddressTypeVM
    {
        #region ID Data
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public virtual LanguageResourceSet LanguageResourceSet { get; set; }

        #endregion
    }
}
