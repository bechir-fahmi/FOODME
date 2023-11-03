using AutoMapper;
using Platform.NotificationSystems.BusinessModel.SMS;
using Platform.NotificationSystems.DtoModel.SMS;

namespace Platform.NotificationSystems.DtoModel._Mapping
{
    public class DTOBusinessMappersProfile : Profile
    {
        public DTOBusinessMappersProfile()
        {
            CreateMap<SMSProviderDTO, SMSProvider>().ReverseMap();
            CreateMap<SMSProviderEndpointDTO, SMSProviderEndpoint>().ReverseMap();
        }
    }
}
