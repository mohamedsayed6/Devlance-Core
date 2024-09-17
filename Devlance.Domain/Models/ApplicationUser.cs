using Devlance.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Models
{
    public  class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
		[MaxLength(50)]
		public string LastName { get; set; }
        
        [MaxLength(250)]
        public string NationalIdPhotoName { get; set; }
        
        [MaxLength(250)]
        public string UserPhotoName { get; set; }

        [MaxLength(2000)]
        public string Bio { get; set; }
        
        [Required]
        public GenderEnum Gender { get; set; }
        
        [Required]
		[DataType(DataType.Date, ErrorMessage = "The BirthDate must be in the past and a valid date.")]
        public DateTime BirthDate { get; set; } // instead of Age
        //public string Address { get; set; }

        [CreditCard]
        public string CardNumber { get; set; } // Ensure you mask or tokenize card numbers in a real system
		
        [Range(1, 12, ErrorMessage = "ExpirationMonth must be between 1 and 12.")]
		public int ExpirationMonth { get; set; } // Store month as an integer

		[Range(2024, 2100, ErrorMessage = "Expiration Year must be between 2024 and 2100.")]
		public int ExpirationYear { get; set; }  // Store year as an integer

        [MaxLength(100)]
        public string CardHolderName { get; set; }

		[RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV must be 3 or 4 digits.")]
		public string Cvv { get; set; } // Mask/Encrypt in real systems
        public bool IsActive { get; set; }

        [Required]
        public AccountTypeEnum AccountType { get; set; }
    }
}
