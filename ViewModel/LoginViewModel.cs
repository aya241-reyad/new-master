using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Miar.ViewModel
{
    public class LoginViewModel
    {
        public string Key { get; set; }

        [Required]
        [Display(Name ="E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Compare("Password",
            ErrorMessage = "Password not the same")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]

        [Display(Name = "Confirm Password")]
        public string Confirm_Password { get; set; }

    }
}
