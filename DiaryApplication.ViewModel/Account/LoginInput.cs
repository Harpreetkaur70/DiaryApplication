using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApplication.ViewModel.Account
{
   public class LoginInput
   {
        [Required(ErrorMessage = "Please enter Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please enter Password")]
        public string Password { get; set; }

   }
}
