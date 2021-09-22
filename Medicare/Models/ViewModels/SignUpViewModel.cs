using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medicare.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="This field is required")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage ="Enter a valid email address")]
        [Required(ErrorMessage ="This field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(128, ErrorMessage = "Password should contain atleast 8 characters",MinimumLength =8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords doesn't match")]
        public string ConfirmPassword { get; set; }

        [Display(Name="Register as a doctor")]
        public bool IsDoctor { get; set; }
    }
}