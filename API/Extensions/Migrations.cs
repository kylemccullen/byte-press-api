using BytePress.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class Migrations
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BytePressContext>();

        await context.Database.MigrateAsync();
    }
}
