using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Models
{
    public class FreelancerProfile:BaseEntity<long>
    {
        public string UserId { get; set; }

        public decimal HourRate { get; set; }

        public int AverageResponseTime { get; set; }

        public ApplicationUser User { get; set; }

    }
}
