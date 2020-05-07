using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", 
                ErrorMessage = "Wachtwoord en bevestiging matchen niet.")]
        public string ConfirmPassword { get; set; }
    }
}
