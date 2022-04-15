using DiaryApplicationAPI.Models;
using DiaryApplicationAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApplication.Data.Repositories.Users
{
    public class UserRepository : BaseRepository<UserEntity, AppDbContext>
    {
        private readonly AppDbContext _context;



        #region constructor
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        #endregion

    }
}
