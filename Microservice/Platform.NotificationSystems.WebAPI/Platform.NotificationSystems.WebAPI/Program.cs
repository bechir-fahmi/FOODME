using Microsoft.EntityFrameworkCore;
using Platform.NotificationSystems.Business.business_services.SMS;
using Platform.NotificationSystems.Business.business_services.SMS._4jawaly;
using Platform.NotificationSystems.Business.business_services.SMS.Taqnyat;
using Platform.NotificationSystems.Business.business_services_implementations.SMS;
using Platform.NotificationSystems.Business.business_services_implementations.SMS.Taqnyat;
using Platform.NotificationSystems.BusinessModel._Mapping;
using Platform.NotificationSystems.DataModel.SMS;
using Platform.NotificationSystems.DtoModel._Mapping;
using Platform.NotificationSystems.EntityFramework;
using Platform.ReferencialData.GenericRepository;
using Platform.ReferentialData.DtoModel.NotificationData;
using Platform.Shared.HttpHelpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotificationSystemsContext>(
    option =>
    {
        option.UseNpgsql(builder.Configuration.GetConnectionString("NotificationSystemsDB"));
    }
    );

#region AutoMapper
builder.Services.AddAutoMapper(typeof(BusinessEntityMappersProfile));
builder.Services.AddAutoMapper(typeof(DTOBusinessMappersProfile));
#endregion

#region DALServices
builder.Services.AddTransient<IUnitOfWork<SMSProviderEntity>, UnitOfWork<NotificationSystemsContext, SMSProviderEntity>>();
builder.Services.AddTransient<IUnitOfWork<SMSProviderEndPointEntity>, UnitOfWork<NotificationSystemsContext, SMSProviderEndPointEntity>>();
#endregion

#region BusinessServices
builder.Services.AddScoped<ISMSProviderService, SMSProviderService>();
builder.Services.AddScoped<ISMSService, SMSService>();
builder.Services.AddScoped<I4jawalyService, _4jawalyService>();
builder.Services.AddScoped<ITaqnyatService, TaqnyatService>();
#endregion

#region Helpers
builder.Services.AddScoped<IHelper<SendSMSDTO, SendSMSDTO, SendSMSDTO>, Helper<SendSMSDTO, SendSMSDTO, SendSMSDTO>>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
