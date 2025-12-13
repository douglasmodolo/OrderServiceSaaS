using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OS.Domain.Interfaces;
using OS.Domain.Entities;
using System.Reflection;

namespace OS.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly ITenantContext _tenantContext;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ITenantContext tenantContext)
            : base(options)
        {
            _tenantContext = tenantContext;
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyTenantFilter(modelBuilder);
        }

        private void ApplyTenantFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IMustHaveTenant).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(ApplicationDbContext)
                        .GetMethod(nameof(ApplyTenantFilterToEntity), BindingFlags.NonPublic | BindingFlags.Static)?
                        .MakeGenericMethod(entityType.ClrType);

                    method?.Invoke(null, new object[] { modelBuilder, _tenantContext });
                }
            }
        }

        private static void ApplyTenantFilterToEntity<T>(ModelBuilder modelBuilder, ITenantContext tenantContext)
            where T : class, IMustHaveTenant
        {
            modelBuilder.Entity<T>().HasQueryFilter(
                e => e.TenantId == tenantContext.TenantId
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var baseEntity = (BaseEntity)entityEntry.Entity;
                baseEntity.LastModified = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    baseEntity.CreatedAt = DateTime.UtcNow;

                    if (baseEntity is IMustHaveTenant multiTenantEntity && _tenantContext.TenantId.HasValue)
                    {
                        multiTenantEntity.TenantId = _tenantContext.TenantId.Value;
                    }
                }
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}