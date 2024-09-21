using Devlance.Domain.Models;
using Devlance.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Infrastructure.DbContext
{
    public class DevlanceContext : IdentityDbContext<ApplicationUser>
    {

        public DevlanceContext(DbContextOptions<DevlanceContext> option) : base(option)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FreelancerProfileConfigurations());

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "freelancer",
                    NormalizedName = "FREELANCER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "client",
                    NormalizedName = "CLIENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }
	
        //Tables

		public DbSet<FreelancerProfile> FreelancerProfiles { get; set; }
	
	}
}

