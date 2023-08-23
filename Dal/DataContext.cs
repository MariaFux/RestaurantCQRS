using Dal.Configurations;
using Domain.Aggregates.RecipeAggregate;
using Domain.Aggregates.UserProfileAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserProfileConfig());
            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
            builder.ApplyConfiguration(new MenuConfig());
            builder.ApplyConfiguration(new RecipeConfig());
        }
    }
}
