using AutoMapper;
using Platform.Tracking.BusinessModel.BrandAction;
using Platform.Tracking.BusinessModel.Offre;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.DtoModel.Offre;
using Platform.Tracking.DtoModel.UserStatus;

namespace Platform.Tracking.DtoModel.Mapping
{
    public class TrackingDtoMapper : Profile
    {
        public TrackingDtoMapper() 
        {
            CreateMap<BrandActionDTO, Platform.Tracking.BusinessModel.BrandAction.BrandAction>().ReverseMap();
            CreateMap<BrandActionSummaryDTO, BrandActionSummary>().ReverseMap();
            CreateMap<UserStatusDTO, Platform.Tracking.BusinessModel.UserStatus.UserStatus>().ReverseMap();
            CreateMap<OfferActionDTO, OfferAction>().ReverseMap();
        }
    }
}
