using Devlance.Domain.Interfaces.Repositories;
using Devlance.Domain.Models;
using Devlance.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Infrastructure.Repositories
{
    public class FreelancerProfileRepository : GenericRepository<FreelancerProfile, long>, IFreelancerProfileRepository
    {
        public FreelancerProfileRepository(DevlanceContext dbContext) : base(dbContext)
        {
        }
    }
}
