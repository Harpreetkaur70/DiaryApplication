using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApplication.ViewModel.Account
{
   public class RegisterInput
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage ="Please enter valid Email Address")]
        public string EmailAddress { get; set; }
 
        [StringLength(15, MinimumLength = 6,ErrorMessage ="Please enter password(6-15)")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessage ="Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid Email")]
        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Phone number must be between 3-10")]
        [Required(ErrorMessage = "Please enter Phone Number")]
        public string PhoneNumber { get; set; }
        

    }
}
