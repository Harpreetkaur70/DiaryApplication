using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiaryApplicationAPI.Models.User
{
   public class UserTokenEntity :BaseEntity
    {
        public string Token { get; set; }
        public string ExpiryDate { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; }
    }
}
