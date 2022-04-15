using DiaryApplication.Data;
using DiaryApplication.Data.Repositories;
using DiaryApplication.Data.Repositories.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplication.Data
{
    public class AppUnitOfWork : BaseUow
    {
        #region Constructor
        public AppUnitOfWork(AppDbContext context) : base(context) { }

        public AppUnitOfWork(AppDbContext context, IConfiguration config) : base(context) { _config = config; }

        #endregion

        #region Users

        private UserRepository _userRepository;
        IConfiguration _config;
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(Context);
                }

                return _userRepository;
            }
        }

        private UserTokenRepository _userTokenRepository;

        public UserTokenRepository UserTokenRepository
        {
            get
            {
                if (_userTokenRepository == null)
                {
                    _userTokenRepository = new UserTokenRepository(Context);
                }

                return _userTokenRepository;
            }
        }

        #endregion
    }
}



