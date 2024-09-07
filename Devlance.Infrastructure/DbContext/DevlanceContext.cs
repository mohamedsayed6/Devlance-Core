using Devlance.Domain.Models;
using Devlance.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public DbSet<FreelancerProfile> FreelancerProfiles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FreelancerProfileConfigurations());
        }
    }
}
