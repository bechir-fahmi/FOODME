using Hangfire;
using Hangfire.PostgreSql;
using JobScheduling.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add authorize service
builder.Services.Configure<AuthorizationOptions>(opt => opt.AddPolicy("adminOnly", policy =>
{
    policy.RequireAuthenticatedUser();

    //add the admin roles
    //policy.RequireRole();
}));

builder.Services.AddHangfire(x => x.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

var app = builder.Build();

#region Language Config
var supportedCultures = new[]
{
    new CultureInfo("en-EG"),
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-EG"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});
#endregion
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
        
app.UseAuthorization();




#region Hangfire 



app.UseHangfireDashboard("/Dyno", new DashboardOptions
{
    DashboardTitle = "Dyno",
    // ==>no one can not edit in timing in platform
    //IsReadOnlyFunc = (DashboardContext context) => true,

    // ==>get only by admin 
    //Authorization = new IDashboardAuthorizationFilter[]
    //{
    //    new HangFireAuthorizationFilter("adminOnly")
    //}
});

var jobschedul = new JobSchedulingController();
RecurringJob.AddOrUpdate(() => jobschedul.updateBrandActionSummary(), Cron.Hourly());

#endregion





app.MapControllers();

app.Run();
