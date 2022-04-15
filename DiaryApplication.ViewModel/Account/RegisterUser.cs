using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApplication.ViewModel.Account
{
    public class RegisterUser
    {
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 6)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }

    }
}
