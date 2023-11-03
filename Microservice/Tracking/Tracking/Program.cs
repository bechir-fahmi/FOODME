using Microsoft.EntityFrameworkCore;
using Platform.ReferencialData.GenericRepository;
using Platform.Shared.Cache;
using Platform.Tracking.Business.Business_Service;
using Platform.Tracking.Business.Business_Service_Implementation;
using Platform.Tracking.BusinessModel.Mapping;
using Platform.Tracking.DataModel.BrandAction;
using Platform.Tracking.DataModel.GetDeals;
using Platform.Tracking.DataModel.Offre;
using Platform.Tracking.DataModel.UserSearch;
using Platform.Tracking.DataModel.UserStatus;
using Platform.Tracking.DtoModel.Mapping;
using Platform.Tracking.EntityFramework;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TrackingContext>(
    option =>
    {
        option.UseNpgsql(builder.Configuration.GetConnectionString("TrackingDB"));
        option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        option.EnableSensitiveDataLogging();
    }
);

#region AutoMapper
builder.Services.AddAutoMapper(typeof(TrackingDtoMapper));
builder.Services.AddAutoMapper(typeof(TrackingBusinessMapper));
#endregion

#region DALService
builder.Services.AddTransient<IUnitOfWork<BrandActionEntity>, UnitOfWork<TrackingContext, BrandActionEntity>>();
builder.Services.AddTransient<IUnitOfWork<BrandActionSummaryEntity>, UnitOfWork<TrackingContext, BrandActionSummaryEntity>>();
builder.Services.AddTransient<IUnitOfWork<BrandActionSummaryView>, UnitOfWork<TrackingContext, BrandActionSummaryView>>();
builder.Services.AddTransient<IUnitOfWork<UserStatusEntity>, UnitOfWork<TrackingContext, UserStatusEntity>>();
builder.Services.AddTransient<IUnitOfWork<UserSearchEntity>, UnitOfWork<TrackingContext, UserSearchEntity>>();
builder.Services.AddTransient<IUnitOfWork<OfferActionEntity>, UnitOfWork<TrackingContext, OfferActionEntity>>();
builder.Services.AddTransient<IUnitOfWork<DealActionEntity>, UnitOfWork<TrackingContext, DealActionEntity>>();
#endregion


#region BusinessService
builder.Services.AddScoped<IBrandActionService, BrandActionService>();
builder.Services.AddScoped<IBrandActionSummaryService, BrandActionSummaryService>();
builder.Services.AddScoped<IUserStatusService, UserStatusService>();
builder.Services.AddScoped<IUserSearchService, UserSearchService>();
builder.Services.AddScoped<IOfferActionService, OfferActionService>();
builder.Services.AddScoped<IDealAction, DealActionService>();
#endregion

#region caching
builder.Services.AddScoped<ICacheService, CacheService>();
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
