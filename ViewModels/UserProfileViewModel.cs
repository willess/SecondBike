using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
