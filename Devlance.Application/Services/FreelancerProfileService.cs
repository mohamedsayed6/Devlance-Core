using Devlance.Domain.Interfaces.Repositories;
using Devlance.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Application.Services
{
    public class FreelancerProfileService: IFreelancerProfileService
    {
        private readonly IFreelancerProfileRepository _freelancerProfileRepository;

        public FreelancerProfileService(IFreelancerProfileRepository freelancerProfileRepository)
        {
            _freelancerProfileRepository = freelancerProfileRepository;
            
        }





    }
}
