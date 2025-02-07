using BytePress.Shared.Data.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BytePress.Shared.Data;

public class BytePressContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbSet<Domain.Task> Task { get; set; }

    public BytePressContext()
    {
    }

    public BytePressContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public BytePressContext(DbContextOptions<BytePressContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer("Data Source=localhost;Initial Catalog=Configuration;Integrated Security=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUserRole>(userRole =>
        {
            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<ApplicationUserClaim>()
            .HasOne(c => c.User)
            .WithMany(u => u.UserClaims)
            .HasForeignKey(c => c.UserId);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var time = DateTime.Now;
        var name = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "SYSTEM";

        foreach (var entityEntry in ChangeTracker
                     .Entries()
                     .Where(e => e.Entity is BaseEntity &&
                                 e.State is EntityState.Added or EntityState.Modified))
        {
            var entity = (BaseEntity)entityEntry.Entity;

            entity.UpdatedDate = time;

            var t = typeof(BaseEntity);
            var updatedBy = t.GetProperty("UpdatedBy");
            entity.UpdatedBy = name;

            if (entityEntry.State != EntityState.Added)
            {
                continue;
            }

            entity.CreatedDate ??= time;

            var createdBy = t.GetProperty("CreatedBy");
            entity.CreatedBy = name;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
