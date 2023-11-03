using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DataModel;
using Platform.Shared.Enum;

namespace Platform.ReferencialData.BusinessModel.UserAddressData
{
    public class UserAddressType
    {
        #region ID Data
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }

        public virtual LanguageResourceSet LanguageResourceSet { get; set; }
        public Status Status { get; set; } 

        #endregion
    }
}
