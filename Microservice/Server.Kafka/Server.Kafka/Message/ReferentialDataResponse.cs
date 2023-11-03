
using Platform.ReferentialData.DtoModel;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferentialData.DtoModel.UserAddressData;
using ProtoBuf;

namespace Server.Kafka.Message
{
    [ProtoContract()]
    public class ReferentialDataResponse
    {
        #region Vendor Data
        [ProtoMember(5)]
        public VendorDTO Vendor { get; set; }

        [ProtoMember(6)]
        public string BrandName { get; set; }

        [ProtoMember(7)]
        public string BrandArName { get; set; }

        [ProtoMember(8)]
        public string CustomerAddress { get; set; }

        [ProtoMember(9)]
        public double CustomerAddressLatitude { get; set; }

        [ProtoMember(10)]
        public double CustomerAddressLongitude { get; set; }

       // [ProtoMember(11)]
       // public POSBrandDTO POSBrand { get; set; }
        #endregion

        #region Delivery Company
       /* [ProtoMember(12)]
        public List<DeliveryCompanyDto> DeliveryCompanies { get; set; }*/
        #endregion

        #region User
        [ProtoMember(13)]
        public UserDTOInfo User { get; set; }

        [ProtoMember(14)]
        public UserAddressDTO UserAddress { get; set; }
        #endregion

        #region Menu Template
        [ProtoMember(15)]
        public int MenuTemplateId { get; set; }
        #endregion

        #region Area
        [ProtoMember(16)]
        public AreaDTO Area { get; set; }
        #endregion
    }
}
