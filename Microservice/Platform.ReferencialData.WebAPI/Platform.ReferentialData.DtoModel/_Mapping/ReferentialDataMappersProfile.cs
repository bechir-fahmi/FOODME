
using AutoMapper;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.BusinessModel.WorkingTime;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.ThemeData;
using Platform.ReferencialData.BusinessModel.ThemeData;
using Platform.ReferentialData.DtoModel.SupportService;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.ReferentialData.DtoModel.MealData;
using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferencialData.BusinessModel.FoodTypeData;
using Platform.ReferentialData.DtoModel.FoodTypeData;
using Platform.ReferencialData.BusinessModel.AgeRangeData;
using Platform.ReferentialData.DtoModel.AgeRangeData;
using Platform.ReferencialData.BusinessModel.DeliveryModeData;
using Platform.ReferentialData.DtoModel.DeliveryModeData;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData.BusinessModel.BrandData;
using Platform.ReferentialData.DtoModel.BrandData;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.ReferencialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DtoModel.OfferData;
using Platform.ReferencialData.BusinessModel.OfferData;
using Platform.ReferencialData.BusinessModel.BrandData.Integration;
using Platform.ReferentialData.DtoModel.BrandData.Integration;
using Platform.ReferencialData.BusinessModel.TagData;
using Platform.ReferentialData.DtoModel.TagData;
using Platform.ReferentialData.DtoModel.QueryData;
using Platform.ReferencialData.BusinessModel.QueryData;

namespace Platform.ReferentialData.DtoModel._Mapping
{
    public class ReferentialDataMappersProfile : Profile
    {
        public ReferentialDataMappersProfile()
        {
            #region Location Data
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Area, AreaDTO>().ReverseMap();
            CreateMap<Coordinate, CoordinateDTO>().ReverseMap();

            #endregion

            #region User Mapping
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
                .ReverseMap();
            CreateMap<User, UserDTOInfo>().ReverseMap();


            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<UserClaim, UserClaimDTO>().ReverseMap();
            CreateMap<UserToken, UserTokenDTO>().ReverseMap();
            CreateMap<RoleClaim, RoleClaimDTO>().ReverseMap();
            CreateMap<UserLogin, UserLoginDTO>().ReverseMap();

            #region User Address
            CreateMap<UserAddress, UserAddressDTO>().ReverseMap();
            CreateMap<UserAddressType, UserAddressTypeDTO>().ReverseMap();
            #endregion

            #region Role
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<CreateRole, CreateRoleDTO>().ReverseMap();
            #endregion

            CreateMap<User, LoginDTO>()
                .ForMember(dest =>
                        dest.EmailOrUserName,
                        opt => opt.MapFrom(src => src.Email));

            CreateMap<User, UserDTOInfo>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();

            CreateMap<UserOTPInfo, UserOTPInfoDTO>().ReverseMap();
            #endregion

            #region Vendor Structure
            CreateMap<Vendor, VendorDTO>().ReverseMap();
            CreateMap<VendorDeliveryMode, VendorDeliveryModeDTO>().ReverseMap();

            #endregion

            #region Language
            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<LanguageResource, LanguageResourceDTO>().ReverseMap();
            CreateMap<LanguageResourceSet, LanguageResourceSetDTO>().ReverseMap();
            #endregion

   

      


            CreateMap<Response, ResponseDTO>().ReverseMap();



            CreateMap<UserOTPVerification, UserOTPVerificationDTO>().ReverseMap();

            #region working Time
            CreateMap<ClenderWorkingTimeDTO, ClenderWorkingTime>().ReverseMap();
            CreateMap<DayWorkingTimeDTO, DayWorkingTime>().ReverseMap();
            CreateMap<ExceptionDayWorkingTimeDTO, ExceptionDayWorkingTime>().ReverseMap();
            CreateMap<ExceptionWeekWorkingTimeDTO, ExceptionWeekWorkingTime>().ReverseMap();
            #endregion

            #region Kitchen Type
            CreateMap<KitchenType, KitchenTypeDTO>().ReverseMap();
            #endregion
            #region FoodType
            CreateMap<FoodType, FoodTypeDTO>().ReverseMap();
            #endregion
            #region MeaType
            CreateMap<MealType, MealTypeDTO>().ReverseMap();
            #endregion
            #region Offer
            CreateMap<Offer, OfferDTO>().ReverseMap();
            #endregion

            #region MeaTiming
            CreateMap<MealTiming, MealTimingDTO>().ReverseMap();
            #endregion
           
            #region AgeRange
            CreateMap<AgeRange, AgeRangeDTO>().ReverseMap();
            #endregion

            #region DeliveryMode
            CreateMap<DeliveryMode, DeliveryModeDTO>().ReverseMap();
            #endregion
            CreateMap<ThemeDTO, Theme>().ReverseMap();

            #region SupportService
            CreateMap<QuestionAnswer, QuestionAnswerDTO>().ReverseMap();
            CreateMap<SuportCategory, SuportCategoryDTO>().ReverseMap();
            CreateMap<TermsService, TermsServiceDTO>().ReverseMap();

            #endregion
            #region Brand
            CreateMap<Vendor, VendorDTO>().ReverseMap();
            CreateMap<VendorDeliveryMode, VendorDeliveryModeDTO>().ReverseMap();
            CreateMap<VendorFoodType, VendorFoodTypeDTO>().ReverseMap();
            CreateMap<VendorKitchenType, VendorKitchenTypeDTO>().ReverseMap();
            CreateMap<VendorMealTiming, VendorMealTimingDTO>().ReverseMap();
            CreateMap<VendorMealType, VendorMealTypeDTO>().ReverseMap();
            CreateMap<VendorDeliveryZone, VendorDeliveryZoneDTO>().ReverseMap();
            CreateMap<StaticIntegration, StaticIntegrationDTO>().ReverseMap();
            CreateMap<DynamicIntegration, DynamicIntegrationDTO>().ReverseMap();
            CreateMap<Zone, ZoneDTO>().ReverseMap();
            CreateMap<IntegrationMethod, IntegrationMethodDTO>().ReverseMap();
            CreateMap<IntegrationParameter, IntegrationParameterDTO>().ReverseMap();
            CreateMap<AuthenticationBM, AuthenticationDTO>().ReverseMap();
            #endregion

            #region Tags
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<TagVendor, TagVendorDTO>().ReverseMap();
            CreateMap<TagKitchenType, TagKitchenTypeDTO>().ReverseMap();
            CreateMap<TagFoodType, TagFoodTypeDTO>().ReverseMap();
            CreateMap<TagMealType, TagMealTypeDTO>().ReverseMap();
            CreateMap<TagMealTiming, TagMealTimingDTO>().ReverseMap();
            CreateMap<TagCity, TagCityDTO>().ReverseMap();
            CreateMap<TagRegion, TagRegionDTO>().ReverseMap();
            CreateMap<TagZone, TagZoneDTO>().ReverseMap();
            CreateMap<TagLanguage, TagLanguageDTO>().ReverseMap();
            CreateMap<TagOffer, TagOfferDTO>().ReverseMap();
            #endregion

            #region Vendor GeneralInformation
            CreateMap<Vendor,VendorGeneralInformationDTO>().ReverseMap();
            CreateMap<Vendor, VendorDeliveryModeDTO>().ReverseMap();
            #endregion
            #region Brand Matching
            CreateMap<BrandMatching, BrandMatchingDTO>().ReverseMap();
            CreateMap<Query, QueryDTO>().ReverseMap();
            #endregion 
        }
    }
}
