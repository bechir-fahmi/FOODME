using ProtoBuf;

namespace Platform.FoodMe.Admin.ViewModels.ViewModels.AddressData
{
    public class DeliveryAddressVM
    {
        #region ID
        public int Id { get; set; }
        #endregion

        #region Order
        public int OrderId { get; set; }
        #endregion
        #region Data
        public double Latitude { get; set; }
       
        public double Longitude { get; set; }
        
        public int AddressId { get; set; }
        
        public string FullAddress { get; set; }
        #endregion
    }
}
