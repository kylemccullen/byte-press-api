using Microsoft.EntityFrameworkCore;

namespace BytePress.Shared.Data;

public class BytePressContext : DbContext
{
    public DbSet<Domain.Task> Task { get; set; }

    public BytePressContext()
    {
    }

    public BytePressContext(DbContextOptions<BytePressContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer("Data Source=localhost;Initial Catalog=Configuration;Integrated Security=True;");
        }
    }
}
