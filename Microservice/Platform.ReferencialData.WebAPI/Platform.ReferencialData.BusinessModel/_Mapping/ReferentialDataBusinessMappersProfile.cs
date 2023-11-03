
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.BusinessModel.WorkingTime;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferentialData.DataModel.KitchenTypeData;
using Platform.ReferencialData.BusinessModel.ThemeData;
using Platform.ReferentialData.DataModel.Theme;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.ReferentialData.DataModel.SupportService;
using Platform.ReferencialData.BusinessModel.FoodTypeData;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferencialData.BusinessModel.MealData;
using Platform.ReferentialData.DataModel.MealData;
using Platform.ReferencialData.BusinessModel.AgeRangeData;
using Platform.ReferentialData.DataModel.AgeRangeData;
using Platform.ReferentialData.DataModel.DeliveryModeData;
using Platform.ReferencialData.BusinessModel.DeliveryModeData;
using Platform.ReferencialData.BusinessModel.LocationData;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferentialData.DataModel.BrandData.IntegrationBrand;
using Platform.ReferentialData.DataModel;
using Platform.ReferencialData.BusinessModel.BrandData;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferencialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DataModel.OfferData;
using Platform.ReferencialData.BusinessModel.OfferData;
using Platform.ReferencialData.BusinessModel.BrandData.Integration;
using Platform.ReferentialData.DataModel.BrandData.Integration;
using Platform.ReferencialData.BusinessModel.TagData;
using Platform.ReferentialData.DataModel.TagData;
using Platform.ReferentialData.DataModel.QueryData;
using Platform.ReferencialData.BusinessModel.QueryData;

namespace Platform.ReferencialData.BusinessModel._Mapping
{
    public class ReferentialDataBusinessMappersProfile : Profile
    {
        public ReferentialDataBusinessMappersProfile()
        {
            #region LocationData

            CreateMap<CountryEntity, Country>().ReverseMap();
            CreateMap<RegionEntity, Region>().ReverseMap();
            CreateMap<CityEntity, City>().ReverseMap();
            CreateMap<AreaEntity, Area>().ReverseMap();
            CreateMap<CoordinateEntity, Coordinate>().ReverseMap();

            #endregion


            #region Authentication
            CreateMap<User, UserEntity> ()
                .ReverseMap();

            CreateMap<UserRoleEntity, UserRole>().ReverseMap();
            CreateMap<IdentityUserToken<string>, UserToken>().ReverseMap();
            CreateMap<IdentityUserClaim<string>, UserClaim>().ReverseMap();
            CreateMap<IdentityUserLogin<string>, UserLogin>().ReverseMap();
            CreateMap<IdentityRoleClaim<string>, RoleClaim>().ReverseMap();

            #region User Address Data
            CreateMap<UserAddress, UserAddressEntity>().ReverseMap();
            CreateMap<UserAddressType, UserAddressTypeEntity>().ReverseMap();
            #endregion

            #region Role
            CreateMap<RoleEntity, Role>().ReverseMap();
            CreateMap<RoleEntity, CreateRole>().ReverseMap();
            #endregion
            #region Claim
            CreateMap<IdentityRoleClaim<string>, RoleClaim>().ReverseMap();
            #endregion
            #endregion

            CreateMap<VendorEntity, Vendor>().ReverseMap();
            CreateMap<VendorDeliveryModeEntity, VendorDeliveryMode>().ReverseMap();


            
            CreateMap<Language, LanguageEntity>().ReverseMap();
            CreateMap<UserAddressTypeEntity, UserAddressType>().ReverseMap();
            CreateMap<LanguageResourceEntity, LanguageResource>().ReverseMap();
            CreateMap<LanguageResourceSetEntity, LanguageResourceSet>().ReverseMap();
            CreateMap<UserOTPVerificationEntity, UserOTPVerification>().ReverseMap();

            #region Working Time           
            CreateMap<ClenderWorkingTimeEntity, ClenderWorkingTime>().ReverseMap();
            CreateMap<DayWorkingTimeEntity, DayWorkingTime>().ReverseMap();
            CreateMap<ExceptionDayWorkingTimeEntity, ExceptionDayWorkingTime>().ReverseMap();
            CreateMap<ExceptionWeekWorkingTimeEntity, ExceptionWeekWorkingTime>().ReverseMap();
            #endregion
            #region Kitchen Type
            CreateMap<KitchenType, KitchenTypeEntity>().ReverseMap();

            #endregion

            #region FoodType
            CreateMap<FoodType, FoodTypeEntity>().ReverseMap();
            #endregion

            #region MealType
            CreateMap<MealType, MealTypeEntity>().ReverseMap();
            #endregion

            #region Offer
            CreateMap<Offer, OfferEntity>().ReverseMap();
            #endregion

            #region MealTiming
            CreateMap<MealTiming, MealTimingEntity>().ReverseMap();
            #endregion

            #region AgeRange
            CreateMap<AgeRange, AgeRangeEntity>().ReverseMap();
            #endregion

            #region DeliveryMode
            CreateMap<DeliveryMode, DeliveryModeEntity>().ReverseMap();
            #endregion


            CreateMap<Theme, ThemeEntity>().ReverseMap();


            #region SupportService
            CreateMap<QuestionAnswer, QuestionAnswerEntity>()
            .ForMember(dest => dest.SuportCategoryId, opt => opt.MapFrom(src => src.SuportCategoryId))
            .ReverseMap();
            CreateMap<SuportCategory, SuportCategoryEntity>().ReverseMap();
            CreateMap<TermsService, TermsServiceEntity>().ReverseMap();
            #endregion
            #region Vendor
            CreateMap<VendorEntity, Vendor>().ReverseMap();
            CreateMap<VendorDeliveryModeEntity, VendorDeliveryMode>().ReverseMap();
            CreateMap<VendorFoodTypeEntity, VendorFoodType>().ReverseMap();
            CreateMap<VendorKitchenTypeEntity, VendorKitchenType>().ReverseMap();
            CreateMap<VendorMealTimingEntity, VendorMealTiming>().ReverseMap();
            CreateMap<VendorMealTypeEntity, VendorMealType>().ReverseMap();
            CreateMap<ZoneEntity, Zone>().ReverseMap();
            CreateMap<VendorDeliveryZonesEntity, VendorDeliveryZone>().ReverseMap();
            CreateMap<StaticIntegrationEntity, StaticIntegration>().ReverseMap();
            CreateMap<DynamicIntegrationEntity, DynamicIntegration>().ReverseMap();
            CreateMap<IntegrationMethodEntity, IntegrationMethod>().ReverseMap();
            CreateMap<IntegrationParameterEntity, IntegrationParameter>().ReverseMap();
            CreateMap<AuthenticationEntity, AuthenticationBM>().ReverseMap();
            #endregion
          
            #region Tags
            CreateMap<Tag, TagEntity>().ReverseMap();
            CreateMap<TagVendor, TagVendorEntity>().ReverseMap();
            CreateMap<TagFoodType, TagFoodTypeEntity>().ReverseMap();
            CreateMap<TagMealType, TagMealTypeEntity>().ReverseMap();
            CreateMap<TagMealTiming, TagMealTimingEntity>().ReverseMap();
            CreateMap<TagKitchenType, TagKitchenTypeEntity>().ReverseMap();
            CreateMap<TagCity, TagCityEntity>().ReverseMap();
            CreateMap<TagRegion, TagRegionEntity>().ReverseMap();
            CreateMap<TagZone, TagZoneEntity>().ReverseMap();
            CreateMap<TagLanguage, TagLanguageEntity>().ReverseMap();
            CreateMap<TagOffer, TagOfferEntity>().ReverseMap();
            #endregion

            #region Brand Matching
            CreateMap<Query, QueryEntity>().ReverseMap();
            CreateMap<BrandMatching, BrandMatchingEntity>().ReverseMap();
            #endregion
        }
    }
}
