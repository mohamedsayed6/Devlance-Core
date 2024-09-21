using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Models
{
	public class AssignUserToRoleModel
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public string Role { get; set; }
	}
}
