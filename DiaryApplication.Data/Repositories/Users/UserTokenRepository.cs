using DiaryApplicationAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApplication.Data.Repositories.Users
{
   public class UserTokenRepository : BaseRepository<UserTokenEntity, AppDbContext>
    {
        #region constructor
        public UserTokenRepository(AppDbContext context) : base(context)
        { 
        }
        #endregion
    }
}
