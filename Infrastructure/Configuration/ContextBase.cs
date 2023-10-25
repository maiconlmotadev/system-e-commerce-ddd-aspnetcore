using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<UserBuy> UserBuy { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Shopping> Shopping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        private string GetStringConectionConfig()
        {
            string ConStr = "Data Source=WINMACBOOK\\SQLEXPRESS; Initial Catalog=dbecommerceddd; User ID=sa; Password=0000;";
            return ConStr;
        }
    }
}
