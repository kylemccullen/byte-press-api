using API.Core.DTO;
using API.Core.Interfaces;
using API.Core.Services;
using API.Extensions;
using BytePress.Shared.Classes;
using BytePress.Shared.Configuration;
using BytePress.Shared.Data;
using Merchants.API.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.ConfigureAppConfiguration(AppContext.BaseDirectory);

var appSettings = new AppSettings();
builder.Configuration.GetSection("AppSettings")
    .Bind(appSettings);

builder.Services.AddSingleton(appSettings);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<BytePressContext>(options =>
{
    options.UseSqlServer(appSettings.ConnectionStrings.BytePress,
        sqlServerOptionsAction => { sqlServerOptionsAction.EnableRetryOnFailure(); });
});

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddAutoMapper(typeof(BytePressProfile));

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (EnvironmentHelper.IsEnvironment(AppEnvironments.Localhost))
{
    await app.MigrateAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
