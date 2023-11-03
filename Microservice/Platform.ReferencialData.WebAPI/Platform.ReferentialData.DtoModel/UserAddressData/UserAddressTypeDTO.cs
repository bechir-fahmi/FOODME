using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.UserAddressData
{
    public class UserAddressTypeDTO
    {
        #region ID Data
        public int Id { get; set; }

        public Guid NameLabelCode { get; set; }
        public virtual LanguageResourceSetDTO LanguageResourceSet { get; set; }
        public Status Status { get; set; }
        #endregion
    }
}
