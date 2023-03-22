using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eUseControl.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "The name field is empty.")]
        [Display(Name = "Username")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "The Username field is not a valid name.")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Name must be more than 4 characters and less than 30 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Email field is empty.")]
        [EmailAddress(ErrorMessage = "The Email Address field is not a valid e-mail address.")]
        [Display(Name = "Email Address")]
        [StringLength(30, ErrorMessage = "Email must be less than 30 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is empty.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password must be more than 5 characters.")]
        public string Password { get; set; }
    }
}