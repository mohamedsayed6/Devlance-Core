using Devlance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Infrastructure.Configurations
{
    public class FreelancerProfileConfigurations : IEntityTypeConfiguration<FreelancerProfile>
    {
        public void Configure(EntityTypeBuilder<FreelancerProfile> builder)
        {

            builder.HasOne(x => x.User)
                   .WithOne()
                   .HasForeignKey<FreelancerProfile>(x => x.UserId);
        }
    }
}
