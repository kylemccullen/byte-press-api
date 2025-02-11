using API.Extensions;
using BytePress.Shared.Classes;
using BytePress.Shared.Configuration;
using BytePress.Shared.Data;
using BytePress.Shared.Data.Domain.Identity;
using EasyAuth.Overrides;
using Microsoft.AspNetCore.Identity;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        configurePolicy =>
        {
            configurePolicy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.ConfigureServices();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddEntityFrameworkStores<BytePressContext>();

var app = builder.Build();

app.UseMiddleware<UnauthorizedAccessExceptionMiddleware>();

app.MapIdentityApiFilterable(new IdentityApiEndpointRouteBuilderOptions());

if (EnvironmentHelper.IsEnvironment(AppEnvironments.Localhost))
{
    await app.MigrateAsync();
    await app.SeedRolesAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
