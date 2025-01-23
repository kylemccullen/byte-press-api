using API.Core.DTO;
using API.Core.Interfaces;
using API.Core.Services;

namespace API.Extensions;

public static class ConfigureServicesExtension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddAutoMapper(typeof(BytePressProfile));
    }
}
