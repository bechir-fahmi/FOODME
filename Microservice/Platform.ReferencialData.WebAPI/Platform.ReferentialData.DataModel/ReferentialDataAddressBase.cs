namespace Platform.ReferentialData.DataModel
{
    public class ReferentialDataAddressBase : ReferentialDataBase
    {
        public Guid AddressLabelCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
