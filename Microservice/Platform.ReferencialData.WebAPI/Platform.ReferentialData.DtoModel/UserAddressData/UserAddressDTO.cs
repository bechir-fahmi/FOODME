using Platform.Shared.Enum;

namespace Platform.ReferentialData.DtoModel.UserAddressData
{
    public class UserAddressDTO
    {
        #region ID Data
        public int Id { get; set; }
        public string UserId { get; set; }
        public int UserAddressTypeId { get; set; }

        #endregion

        #region Data
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullAddress { get; set; }
        public AddressType AddressType { get; set; }
        public Status Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion
    }
}
