using FX.FP.AuthenticationCenter.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FX.FP.AuthenticationCenter.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<APIKeyInfo> APIKeyInfos { get; set; }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<APIKeyInfo>().ToTable("T_APIKeyInfo");

            base.OnModelCreating(builder);
        }
    }
}
