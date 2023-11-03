using MicroserviceBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferencialData.Business.business_services.LanguageData;
using Platform.ReferencialData.Business.business_services_implementations.Authentication;
using Platform.ReferencialData.Business.business_services_implementations.LanguageData;
using Platform.ReferencialData.Business.Filter;
using Platform.ReferencialData.Business.Helper.IServices;
using Platform.ReferencialData.Business.Helper.Services;
using Platform.ReferencialData.BusinessModel._Mapping;
using Platform.ReferentialData.DataModel.LanguageData;
using Platform.ReferentialData.DataModel.UserData;
using Platform.ReferentialData.DataModel.WorkingTime;
using Platform.ReferentialData.DtoModel._Mapping;
using WatchDog;
using Platform.ReferencialData.Business.Helper.Configuration;
using Platform.ReferentialData.DtoModel.LocationData;
using Platform.ReferencialData.Business.business_services_implementations;
using Platform.ReferencialData.Business.business_services.UserAddressData;
using Platform.Shared.Cache;
using Platform.ReferencialData.Business.business_services_implementations.UserAddressData;
using Platform.ReferencialData.Business.business_services.WorkingTimeData;
using Platform.ReferencialData.Business.business_services_implementations.WorkingTimeData;
using Platform.ReferentialData.DataModel.KitchenTypeData;
using Platform.ReferencialData.Business.business_services.KitchenTypeData;
using Platform.ReferencialData.Business.business_services_implementations.KitchenTypeData;
using Platform.ReferentialData.DataModel.Theme;
using Platform.ReferencialData.Business.business_services.ThemeData;
using Platform.ReferencialData.Business.business_services_implementations.ThemeData;
using Platform.ReferentialData.DataModel.SupportService;
using Platform.ReferencialData.Business.business_services.SupportServiceData;
using Platform.ReferencialData.Business.business_services_implementations.SupportServiceData;
using Platform.ReferencialData.Business.business_services.MealData;
using Platform.ReferencialData.Business.business_services.FoodTypeData;
using Platform.ReferencialData.Business.business_services_implementations.MealData;
using Platform.ReferencialData.Business.business_services_implementations.FoodTypeData;
using Platform.ReferentialData.DataModel.MealData;
using Platform.ReferentialData.DataModel.FoodTypeData;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DataModel.UserAddressData;
using Platform.ReferencialData.Business.business_services.AgeRangeData;
using Platform.ReferencialData.Business.business_services_implementations.AgeRangeData;
using Platform.ReferentialData.DataModel.AgeRangeData;
using DeliveryModeEntity = Platform.ReferentialData.DataModel.DeliveryModeData.DeliveryModeEntity;
using IDeliveryModeService = Platform.ReferencialData.Business.business_services.DeliveryModeData.IDeliveryModeService;
using DeliveryModeService = Platform.ReferencialData.Business.business_services_implementations.DeliveryModeData.DeliveryModeService;
using Platform.ReferencialData.Business.business_services.NotificationData;
using Platform.ReferencialData.Business.business_services_implementations.NotificationData;
using Platform.ReferentialData.DtoModel.NotificationData;
using Platform.Shared.HttpHelpers;
using Platform.ReferentialData.DataModel.LocationData;
using Platform.ReferencialData.Business.business_services.LocationData;
using Platform.ReferencialData.Business.business_services_implementations.LocationData;
using Platform.ReferencialData.Business.business_services;
using Platform.ReferentialData.DataModel;
using Platform.ReferentialData.DataModel.BrandData.IntegrationBrand;
using Platform.ReferentialData.DataModel.BrandData;
using Platform.ReferentialData.EntityFramework;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.ReferentialData.DataModel.OfferData;
using Platform.ReferencialData.Business.business_services.OfferData;
using Platform.ReferencialData.Business.business_services_implementations.OfferData;
using Platform.ReferentialData.DataModel.BrandData.Integration;
using Microsoft.Extensions.Hosting;
using Platform.ReferentialData.DataModel.TagData;
using Platform.ReferentialData.DataModel.QueryData;

var builder = WebApplication.CreateBuilder(args);

// AddGeneralInformations services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );


builder.Services.AddCors();
#region Helper
builder.Services.AddScoped<IHelper<BrandActionSummaryDTO, BrandActionSummaryDTO, BrandActionSummaryDTO>, Helper<BrandActionSummaryDTO, BrandActionSummaryDTO, BrandActionSummaryDTO>>();
#endregion

builder.Services.AddDbContext<ReferentialDataContext>(
    option =>
    {
        option.UseNpgsql(builder.Configuration.GetConnectionString("ReferencialDataDB"));
        option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        option.EnableSensitiveDataLogging();
    }
);


#region caching
builder.Services.AddScoped<ICacheService, CacheService>();
#endregion



#region appSetting

builder.Services.Configure<EmailConfiguration>(
    builder.Configuration.GetSection("EmailConfiguration"));
#endregion

#region Authentication
builder.Services.ConfigurationIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
#endregion

#region Authorization
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});
#endregion

#region IdentityFramewore
builder.Services.Configure<IdentityOptions>(options =>
{
    //options.SignIn.RequireConfirmedPhoneNumber = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDoc();

#region AutoMapper
builder.Services.AddAutoMapper(typeof(ReferentialDataMappersProfile));
builder.Services.AddAutoMapper(typeof(ReferentialDataBusinessMappersProfile));
#endregion

#region DALServices

#region Location Data
builder.Services.AddTransient<IUnitOfWork<CountryEntity>, UnitOfWork<ReferentialDataContext, CountryEntity>>();
builder.Services.AddTransient<IUnitOfWork<RegionEntity>, UnitOfWork<ReferentialDataContext, RegionEntity>>();
builder.Services.AddTransient<IUnitOfWork<CityEntity>, UnitOfWork<ReferentialDataContext, CityEntity>>();
builder.Services.AddTransient<IUnitOfWork<AreaEntity>, UnitOfWork<ReferentialDataContext, AreaEntity>>();
builder.Services.AddTransient<IUnitOfWork<CoordinateEntity>, UnitOfWork<ReferentialDataContext, CoordinateEntity>>();
#endregion
#region Vendor
builder.Services.AddTransient<IUnitOfWork<VendorEntity>, UnitOfWork<ReferentialDataContext, VendorEntity>>();
builder.Services.AddTransient<IUnitOfWork<VendorDeliveryZonesEntity>, UnitOfWork<ReferentialDataContext, VendorDeliveryZonesEntity>>();
builder.Services.AddTransient<IUnitOfWork<StaticIntegrationEntity>, UnitOfWork<ReferentialDataContext, StaticIntegrationEntity>>();
builder.Services.AddTransient<IUnitOfWork<DynamicIntegrationEntity>, UnitOfWork<ReferentialDataContext, DynamicIntegrationEntity>>();
builder.Services.AddTransient<IUnitOfWork<IntegrationMethodEntity>, UnitOfWork<ReferentialDataContext, IntegrationMethodEntity>>();
builder.Services.AddTransient<IUnitOfWork<IntegrationParameterEntity>, UnitOfWork<ReferentialDataContext, IntegrationParameterEntity>>();
builder.Services.AddTransient<IUnitOfWork<AuthenticationEntity>, UnitOfWork<ReferentialDataContext, AuthenticationEntity>>();
#endregion

#region Zone
builder.Services.AddTransient<IUnitOfWork<ZoneEntity>, UnitOfWork<ReferentialDataContext, ZoneEntity>>();
#endregion
#region Restaurant Structure Data
builder.Services.AddTransient<IUnitOfWork<LanguageEntity>, UnitOfWork<ReferentialDataContext, LanguageEntity>>();
builder.Services.AddTransient<IUnitOfWork<VendorEntity>, UnitOfWork<ReferentialDataContext, VendorEntity>>();
builder.Services.AddTransient<IUnitOfWork<LanguageResourceEntity>, UnitOfWork<ReferentialDataContext, LanguageResourceEntity>>();
builder.Services.AddTransient<IUnitOfWork<LanguageResourceSetEntity>, UnitOfWork<ReferentialDataContext, LanguageResourceSetEntity>>();
builder.Services.AddTransient<IUnitOfWork<UserAddressEntity>, UnitOfWork<ReferentialDataContext, UserAddressEntity>>();
builder.Services.AddTransient<IUnitOfWork<UserAddressTypeEntity>, UnitOfWork<ReferentialDataContext, UserAddressTypeEntity>>();
#endregion

#region Authentification
builder.Services.AddTransient<IUnitOfWork<UserEntity>, UnitOfWork<ReferentialDataContext, UserEntity>>();
builder.Services.AddTransient<IUnitOfWork<RoleEntity>, UnitOfWork<ReferentialDataContext, RoleEntity>>();
builder.Services.AddTransient<IUnitOfWork<UserRoleEntity>, UnitOfWork<ReferentialDataContext, UserRoleEntity>>();
builder.Services.AddTransient<IUnitOfWork<RefreshToken>, UnitOfWork<ReferentialDataContext, RefreshToken>>();
builder.Services.AddTransient<IUnitOfWork<IdentityRoleClaim<string>>, UnitOfWork<ReferentialDataContext, IdentityRoleClaim<string>>>();
builder.Services.AddTransient<IUnitOfWork<IdentityUserLogin<string>>, UnitOfWork<ReferentialDataContext, IdentityUserLogin<string>>>();
#endregion

#region Brand Matching
builder.Services.AddTransient<IUnitOfWork<BrandMatchingEntity>, UnitOfWork<ReferentialDataContext, BrandMatchingEntity>>();
builder.Services.AddTransient<IUnitOfWork<QueryEntity>, UnitOfWork<ReferentialDataContext, QueryEntity>>();
#endregion



#region OTP
builder.Services.AddTransient<IUnitOfWork<UserOTPVerificationEntity>, UnitOfWork<ReferentialDataContext, UserOTPVerificationEntity>>();
#endregion

#region Working Time
builder.Services.AddTransient<IUnitOfWork<ClenderWorkingTimeEntity>, UnitOfWork<ReferentialDataContext, ClenderWorkingTimeEntity>>();
builder.Services.AddTransient<IUnitOfWork<DayWorkingTimeEntity>, UnitOfWork<ReferentialDataContext, DayWorkingTimeEntity>>();
builder.Services.AddTransient<IUnitOfWork<ExceptionDayWorkingTimeEntity>, UnitOfWork<ReferentialDataContext, ExceptionDayWorkingTimeEntity>>();
builder.Services.AddTransient<IUnitOfWork<ExceptionWeekWorkingTimeEntity>, UnitOfWork<ReferentialDataContext, ExceptionWeekWorkingTimeEntity>>();

#endregion

#region Kitchen Type
builder.Services.AddTransient<IUnitOfWork<KitchenTypeEntity>, UnitOfWork<ReferentialDataContext, KitchenTypeEntity>>();

#endregion

#region MealType
builder.Services.AddTransient<IUnitOfWork<MealTypeEntity>, UnitOfWork<ReferentialDataContext, MealTypeEntity>>();
#endregion

#region MealTiming
builder.Services.AddTransient<IUnitOfWork<MealTimingEntity>, UnitOfWork<ReferentialDataContext, MealTimingEntity>>();
#endregion

#region FoodType
builder.Services.AddTransient<IUnitOfWork<FoodTypeEntity>, UnitOfWork<ReferentialDataContext, FoodTypeEntity>>();
#endregion

#region AgeRange
builder.Services.AddTransient<IUnitOfWork<AgeRangeEntity>, UnitOfWork<ReferentialDataContext, AgeRangeEntity>>();
#endregion

#region DeliveryMode
builder.Services.AddTransient<IUnitOfWork<DeliveryModeEntity>, UnitOfWork<ReferentialDataContext, DeliveryModeEntity>>();
#endregion
#region Theme
builder.Services.AddTransient<IUnitOfWork<ThemeEntity>, UnitOfWork<ReferentialDataContext, ThemeEntity>>();

#endregion

#region Helpers
builder.Services.AddScoped<IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO>, Helper<SendSMSDTO, SendSMSDTO, SendSMSDTO>>();
#endregion
#region SupportService
builder.Services.AddTransient<IUnitOfWork<TermsServiceEntity>, UnitOfWork<ReferentialDataContext, TermsServiceEntity>>();
builder.Services.AddTransient<IUnitOfWork<SuportCategoryEntity>, UnitOfWork<ReferentialDataContext, SuportCategoryEntity>>();
builder.Services.AddTransient<IUnitOfWork<QuestionAnswerEntity>, UnitOfWork<ReferentialDataContext, QuestionAnswerEntity>>();

#endregion
#region Offer
builder.Services.AddTransient<IUnitOfWork<OfferEntity>, UnitOfWork<ReferentialDataContext, OfferEntity>>();
#endregion

#region Tags
builder.Services.AddTransient<IUnitOfWork<TagLanguageEntity>, UnitOfWork<ReferentialDataContext, TagLanguageEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagFoodTypeEntity>, UnitOfWork<ReferentialDataContext, TagFoodTypeEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagKitchenTypeEntity>, UnitOfWork<ReferentialDataContext, TagKitchenTypeEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagMealTypeEntity>, UnitOfWork<ReferentialDataContext, TagMealTypeEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagMealTimingEntity>, UnitOfWork<ReferentialDataContext, TagMealTimingEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagZoneEntity>, UnitOfWork<ReferentialDataContext, TagZoneEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagRegionEntity>, UnitOfWork<ReferentialDataContext, TagRegionEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagCityEntity>, UnitOfWork<ReferentialDataContext, TagCityEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagOfferEntity>, UnitOfWork<ReferentialDataContext, TagOfferEntity>>();
builder.Services.AddTransient<IUnitOfWork<TagEntity>, UnitOfWork<ReferentialDataContext, TagEntity>>();
#endregion

builder.Services.AddTransient<IUnitOfWork<VendorFoodTypeEntity>, UnitOfWork<ReferentialDataContext, VendorFoodTypeEntity>>();
builder.Services.AddTransient<IUnitOfWork<VendorMealTypeEntity>, UnitOfWork<ReferentialDataContext, VendorMealTypeEntity>>();
builder.Services.AddTransient<IUnitOfWork<VendorMealTimingEntity>, UnitOfWork<ReferentialDataContext, VendorMealTimingEntity>>();
builder.Services.AddTransient<IUnitOfWork<VendorKitchenTypeEntity>, UnitOfWork<ReferentialDataContext, VendorKitchenTypeEntity>>();
builder.Services.AddTransient<IUnitOfWork<VendorDeliveryModeEntity>, UnitOfWork<ReferentialDataContext, VendorDeliveryModeEntity>>();
#endregion

#region BusinessServices
#region Authentication
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserAddressTypeService, UserAddressTypeService>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IRoleClaimService, RoleClaimService>();

builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ICheckService, CheckService>();
builder.Services.AddScoped<ISMSService, SMSService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
#endregion



#region Language
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ILanguageResourceService, LanguageResourceService>();
builder.Services.AddScoped<ILanguageResourceSetService, LanguageResourceSetService>();
#endregion


#region OTP
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUserOTPVerificationService, UserOTPVerificationService>();
#endregion

#region User Address
builder.Services.AddScoped<IUserAddressService, UserAddressService>();
builder.Services.AddScoped<IUserAddressTypeService, UserAddressTypeService>();
#endregion


#region Working Time
builder.Services.AddScoped<IClenderWorkingTimeService, ClenderWorkingTimeService>();
builder.Services.AddScoped<IExceptionWeekWorkingTimeService, ExceptionWeekWorkingTimeService>();
builder.Services.AddScoped<IExceptionDayWorkingTimeService, ExceptionDayWorkingTimeService>();
builder.Services.AddScoped<IDayWorkingTimeService, DayWorkingTimeService>();
#endregion

#region KiTCHEN Type
builder.Services.AddScoped<IKitchenTypeService, KitchenTypeService>();

#endregion

#region MealType
builder.Services.AddScoped<IMealTypeService, MealTypeService>();
#endregion

#region MealTiming
builder.Services.AddScoped<IMealTimingService, MealTimingService>();
#endregion

#region FoodType
builder.Services.AddScoped<IFoodTypeService, FoodTypeService>();
#endregion

#region AgeRange
builder.Services.AddScoped<IAgeRangeService, AgeRangeService>();
#endregion

#region DeliveryMode
builder.Services.AddScoped<IDeliveryModeService, DeliveryModeService>();
#endregion

#region Theme
builder.Services.AddScoped<IThemeService, ThemeService>();

#endregion

#region SupportService
builder.Services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
builder.Services.AddScoped<ISuportCategoryService, SuportCategoryService>();
builder.Services.AddScoped<ITermsServiceService, TermsServiceService>();

#endregion
#region Location Data
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<ICoordinateService, CoordinateService>();
#endregion
#region Vendor
builder.Services.AddScoped<IVendorService, VendorService>();
#endregion


#region Zone
builder.Services.AddScoped<IZoneService, ZoneService>();
#endregion
#region Offer
builder.Services.AddScoped<IOfferService, OfferService>();
#endregion
#endregion



//add cahce service 
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICache, RequestCache>();

var app = builder.Build();


//To enable 'timestamp with time zone' types in PostgreSQL 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI();
//}




app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
    builder.WithExposedHeaders("Strict-Origin-When-Cross-Origin");
});
app.UseAuthentication();
app.UseAuthorization();

 app.MapControllers();

app.Run();
