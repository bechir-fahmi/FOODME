
using AutoMapper;
using Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.UserAddressData;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.BusinessModel.WorkingTime;
using Platform.FoodMe.Admin.ViewModels.ViewModels.WorkingTime;
using Platform.FoodMe.Admin.ViewModels.ViewModels.KitchenTypeData;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.FoodMe.Admin.ViewModels.ViewModels.SupportServiceData;
using Platform.ReferencialData.BusinessModel.FoodTypeData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.FoodTypeData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.MealData;
using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferencialData.BusinessModel.AgeRangeData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.AgeRangeData;
using DeliveryModeVM = Platform.FoodMe.Admin.ViewModels.ViewModels.DeliveryModeData.DeliveryModeVM;
using Platform.ReferencialData.BusinessModel.DeliveryModeData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.LocationData;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.FoodMe.Admin.ViewModels.ViewModels;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.Brand.Integration;
using Platform.FoodMe.Admin.ViewModels.ViewModelsBrand.Integration;
using Platform.FoodMe.Admin.ViewModels.ViewModels.BrandActionData;
using Platform.Tracking.BusinessModel.BrandAction;

namespace Platform.FoodMe.Admin.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Authentication 
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<User, CreateOrEditUserDto>().ReverseMap();
            CreateMap<LoginVM, User>().ReverseMap();

            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<Response, ResponseVM>().ReverseMap();
            CreateMap<User, ResetPasswordVM>().ReverseMap();

            CreateMap<UserOTPInfoVM, UserOTPInfo>()
                .ForMember(src => src.UserName, opt => opt.MapFrom(src => src.PhoneNumber.ToString()))
                .ReverseMap();

            CreateMap<UserOTPVerificationVM, UserOTPVerification>()
               .ReverseMap();

            #region Role
            CreateMap<CreateRole, CreateRoleVM>().ReverseMap();
            CreateMap<Role, RoleVM>().ReverseMap();

            CreateMap<RoleClaimVM, RoleClaim>().ReverseMap();
            #endregion 
            #endregion

            #region Brand Structure
         //   CreateMap<Api, BrandVM>().ReverseMap();
            #endregion

            CreateMap<UserAddressType, UserAddressTypeVM>().ReverseMap();
            CreateMap<Language, LanguageVM>().ReverseMap();
            CreateMap<LanguageResource, LanguageResourceVM>().ReverseMap();
            CreateMap<DeliveryMode, DeliveryModeVM>().ReverseMap();
            CreateMap<KitchenTypeVM, KitchenType>().ReverseMap();
            CreateMap<FoodTypeVM, FoodType>().ReverseMap();
            CreateMap<MealTypeVM, MealType>().ReverseMap();
            CreateMap<MealTimingVM, MealTiming>().ReverseMap();
            CreateMap<AgeRangeVM, AgeRange>().ReverseMap();
            CreateMap<DeliveryModeVM, DeliveryMode>().ReverseMap();
            CreateMap<CityVM, City>().ReverseMap();
            CreateMap<AreaVM, Area>().ReverseMap();
            CreateMap<RegionVM, Region>().ReverseMap();
            CreateMap<CountryVM, Country>().ReverseMap();
            CreateMap<BrandVM, Brand>().ReverseMap();
            CreateMap<ZoneVM, Zone>().ReverseMap();
            CreateMap<StaticIntegrationVM, StaticIntegration>().ReverseMap();
            CreateMap<DynamicIntegrationVM, DynamicIntegration>().ReverseMap();
            CreateMap<AuthenticationVM, AuthenticationBM>().ReverseMap();





            #region User Address
            CreateMap<UserAddressVM, UserAddress>().ReverseMap();
            CreateMap<UserAddressTypeVM, UserAddressType>().ReverseMap();
            #endregion


            #region Working Time
            CreateMap<ClenderWorkingTime, ClenderWorkingTimeVM>().ReverseMap();
            CreateMap<DayWorkingTime, DayWorkingTimeVM>().ReverseMap();
            CreateMap<ExceptionDayWorkingTime, ExceptionDayWorkingTimeVM>().ReverseMap();
            CreateMap<ExceptionWeekWorkingTime, ExceptionWeekWorkingTimeVM>().ReverseMap();

            #endregion

               

            #region SupportService
            CreateMap<TermsServiceVM, TermsService>().ReverseMap();
            CreateMap<SuportCategoryVM, SuportCategory>().ReverseMap();
            CreateMap<QuestionAnswerVM, QuestionAnswer>().ReverseMap();
            #endregion

            CreateMap<BrandActionVM, BrandAction>().ReverseMap();
        }
    }
}
