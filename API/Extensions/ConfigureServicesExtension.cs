using API.Core.Classes;
using API.Core.DTO;
using API.Core.Interfaces;
using API.Core.Services;
using API.Core.Services.Email;
using BytePress.Shared.Data.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace API.Extensions;

public static class ConfigureServicesExtension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IEmailSender<ApplicationUser>, MessageEmailService>();
        builder.Services.AddScoped<IEmailSender, EmailSenderService>();

        builder.Services.AddScoped<RazorRenderer>();

        builder.Services.AddAutoMapper(typeof(BytePressProfile));
    }
}
