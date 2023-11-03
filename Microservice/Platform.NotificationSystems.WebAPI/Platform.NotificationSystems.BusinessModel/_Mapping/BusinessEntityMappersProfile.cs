using AutoMapper;
using Platform.NotificationSystems.BusinessModel.SMS;
using Platform.NotificationSystems.DataModel.SMS;

namespace Platform.NotificationSystems.BusinessModel._Mapping
{
    public class BusinessEntityMappersProfile : Profile
    {
        public BusinessEntityMappersProfile() 
        {
            CreateMap<SMSProviderEntity, SMSProvider>()
                .ReverseMap();
            CreateMap<SMSProviderEndPointEntity, SMSProviderEndpoint>()
                .ReverseMap();

        }
    }
}
