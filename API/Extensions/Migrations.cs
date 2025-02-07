using BytePress.Shared.Classes;
using BytePress.Shared.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationRole = BytePress.Shared.Data.Domain.ApplicationRole;

namespace API.Extensions;

public static class Migrations
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BytePressContext>();

        await context.Database.MigrateAsync();
    }

    public static async Task SeedRolesAsync(this WebApplication app)
    {
        var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        foreach (var roleName in Roles.List.Select(r => r.Name))
        {
            if (await roleManager.RoleExistsAsync(roleName))
                continue;

            await roleManager.CreateAsync(new ApplicationRole(roleName));
        }
    }
}
