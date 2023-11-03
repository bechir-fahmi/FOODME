using AutoMapper;
using Platform.Tracking.BusinessModel.BrandAction;
using Platform.Tracking.DataModel.Offre;
using Platform.Tracking.DataModel.BrandAction;
using Platform.Tracking.DataModel.UserStatus;
using Platform.Tracking.BusinessModel.Offre;
using Platform.Tracking.BusinessModel.GetDeals;
using Platform.Tracking.DataModel.GetDeals;

namespace Platform.Tracking.BusinessModel.Mapping
{
    public class TrackingBusinessMapper : Profile
    {
        public TrackingBusinessMapper() 
        { 
            CreateMap<BrandAction.BrandAction, BrandActionEntity>().ReverseMap();
            CreateMap<BrandActionSummary, BrandActionSummaryEntity>().ReverseMap();
            CreateMap<UserStatus.UserStatus, UserStatusEntity>().ReverseMap();
            CreateMap<OfferAction, OfferActionEntity>().ReverseMap();
            CreateMap<DealAction, DealActionEntity>().ReverseMap();
            CreateMap<AggregatorItem, AggregatorItemEntity>().ReverseMap();
        }
    }

   
}
