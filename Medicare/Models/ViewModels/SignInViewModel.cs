using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medicare.Models.ViewModels
{
    public class SignInViewModel
    {

        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [StringLength(128, ErrorMessage = "Password should contain atleast 8 characters", MinimumLength = 8)]
        public string Password { get; set; }

    }
}