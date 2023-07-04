using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<UserBuy> UserBuy { get; set; }
        public DbSet<IdentityUser> IdentityUser { get; set; }

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
            builder.Entity<IdentityUser>().ToTable("AspNetUser").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        private string GetStringConectionConfig()
        {
            string ConStr = "Data Source=MAC-WIN\\SQLSERVER2022; Initial Catalog=dbecommerceddd; User ID=sa; Password=0000;";
            return ConStr;
        }
    }
}
