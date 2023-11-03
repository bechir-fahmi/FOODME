
using Platform.FoodMe.Admin.Mapping;
using Platform.FoodMe.Admin.ViewModels.ViewModels.AuthenticationData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.UserAddressData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.LanguageData;
using Platform.ReferencialData.BusinessModel.Authentification;
using Platform.ReferencialData.BusinessModel.UserAddressData;
using Platform.ReferencialData.BusinessModel.WorkingTime;
using Platform.ReferentialData.DtoModel._Mapping;
using Platform.ReferentialData.DtoModel.Authentification;
using Platform.ReferentialData.DtoModel.UserAddressData;
using Platform.ReferentialData.DtoModel.WorkingTimeData;
using Platform.ReferentialData.EntityFramework;
using Platform.Shared.HttpHelpers;
using Platform.FoodMe.Admin.ViewModels.ViewModels.WorkingTime;
using Platform.ReferentialData.DtoModel.KitchenTypeData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.KitchenTypeData;
using Platform.ReferencialData.BusinessModel.KitchenTypeData;
using Platform.ReferentialData.DtoModel.SupportService;
using Platform.ReferencialData.BusinessModel.SupportService;
using Platform.FoodMe.Admin.ViewModels.ViewModels.SupportServiceData;
using Platform.ReferentialData.DtoModel.LanguageData;
using Platform.ReferentialData.BusinessModel.LanguageData;
using Platform.ReferentialData.DtoModel;
using Platform.ReferencialData.BusinessModel;
using Platform.ReferencialData;
using Platform.FoodMe.Admin.ViewModels.ViewModels.Brand.Integration;
using Platform.Tracking.DtoModel.BrandAction;
using Platform.Tracking.BusinessModel.BrandAction;
using Platform.FoodMe.Admin.ViewModels.ViewModels.BrandActionData;
using Platform.Tracking.DtoModel.Mapping;

var builder = WebApplication.CreateBuilder(args);

// AddGeneralInformations services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();

#region Mappers
builder.Services.AddAutoMapper(typeof(ReferentialDataMappersProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(TrackingDtoMapper));
#endregion

#region Helpers
//builder.Services.AddScoped<IHelper<AreaDTO, Area, AreaVM>, Helper<AreaDTO, Area, AreaVM>>();
//builder.Services.AddScoped<IHelper<CountryDTO, Country, CountryVM>, Helper<CountryDTO, Country, CountryVM>>();
//builder.Services.AddScoped<IHelper<RegionDTO, Region, RegionVM>, Helper<RegionDTO, Region, RegionVM>>();
//builder.Services.AddScoped<IHelper<CityDTO, City, CityVM>, Helper<CityDTO, City, CityVM>>();
builder.Services.AddScoped<IHelper<LanguageDTO, Language, LanguageVM>, Helper<LanguageDTO, Language, LanguageVM>>();
builder.Services.AddScoped<IHelper<LanguageResourceDTO, LanguageResource, LanguageResourceVM>, Helper<LanguageResourceDTO, LanguageResource, LanguageResourceVM>>();

builder.Services.AddScoped<IHelper<UserAddressDTO, UserAddress, UserAddressVM>, Helper<UserAddressDTO, UserAddress, UserAddressVM>>();
builder.Services.AddScoped<IHelper<UserAddressTypeDTO, UserAddressType, UserAddressTypeVM>, Helper<UserAddressTypeDTO, UserAddressType, UserAddressTypeVM>>();
builder.Services.AddScoped<IHelper<BrandDeliveryModeDTO, BrandDeliveryMode, BrandDeliveryModeVM>, Helper<BrandDeliveryModeDTO, BrandDeliveryMode, BrandDeliveryModeVM>>();
builder.Services.AddScoped<IHelper<KitchenTypeDTO, KitchenType, KitchenTypeVM>, Helper<KitchenTypeDTO, KitchenType, KitchenTypeVM>>();


builder.Services.AddScoped<IHelper<BrandActionDTO, BrandAction, BrandActionVM>, Helper<BrandActionDTO, BrandAction, BrandActionVM>>();




#region Authentication
builder.Services.AddScoped<IHelper<UserDTO, User, CreateOrEditUserDto>, Helper<UserDTO, User, CreateOrEditUserDto>>();
builder.Services.AddScoped<IHelper<UserDTO, User, UserVM>, Helper<UserDTO, User, UserVM>>();
builder.Services.AddScoped<IHelper<UserAddressTypeDTO, UserAddressType, UserAddressTypeVM>, Helper<UserAddressTypeDTO, UserAddressType, UserAddressTypeVM>>();
builder.Services.AddScoped<IHelper<UserOTPInfoDTO, UserOTPInfo, UserOTPInfoVM>, Helper<UserOTPInfoDTO, UserOTPInfo, UserOTPInfoVM>>();
builder.Services.AddScoped<IHelper<UserOTPVerificationDTO, UserOTPVerification, UserOTPVerificationVM>, Helper<UserOTPVerificationDTO, UserOTPVerification, UserOTPVerificationVM>>();
#region Role
builder.Services.AddScoped<IHelper<RoleDTO, Role, RoleVM>, Helper<RoleDTO, Role, RoleVM>>();
builder.Services.AddScoped<IHelper<CreateRoleDTO, CreateRole, CreateRoleVM>, Helper<CreateRoleDTO, CreateRole, CreateRoleVM>>();
#endregion
#endregion

#region Restaurant Data

#region Brand Structure
builder.Services.AddScoped<IHelper<BrandDTO, Brand, BrandVM>, Helper<BrandDTO, Brand, BrandVM>>();

#endregion

#endregion



#region SupportService

builder.Services.AddScoped<IHelper<TermsServiceDTO, TermsService, TermsServiceVM>, Helper<TermsServiceDTO, TermsService, TermsServiceVM>>();
builder.Services.AddScoped<IHelper<SuportCategoryDTO, SuportCategory, SuportCategoryVM>, Helper<SuportCategoryDTO, SuportCategory, SuportCategoryVM>>();
builder.Services.AddScoped<IHelper<QuestionAnswerDTO, QuestionAnswer, QuestionAnswerVM>, Helper<QuestionAnswerDTO, QuestionAnswer, QuestionAnswerVM>>();

#endregion

#endregion

builder.Services.AddAuthentication();
builder.Services.ConfigureJWT(builder.Configuration);
//add cores
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//enable cores
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
