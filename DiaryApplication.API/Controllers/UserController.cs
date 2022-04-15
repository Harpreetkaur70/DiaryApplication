using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using System.Net;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using DiaryApplication.Api.Helpers;
using DiaryApplication.Data;
using DiaryApplicationAPI.Models.User;
using DiaryApplication.ViewModel.Account;

namespace DiaryApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : BaseApiController
    {
        #region fields
        private IConfiguration _config;
        private readonly AppSettings _appSettings;
        private IWebHostEnvironment _env;
        #endregion

        #region constructor
        public UserController(AppDbContext context, IOptions<AppSettings> appSettings, IConfiguration configuration, IWebHostEnvironment env) : base(context)
        {
            _appSettings = appSettings.Value;
            _config = configuration;
            _env = env;
        }
        #endregion


        #region api methods

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            try
            {
               
                using (AppUnitOfWork unitOfWork = new AppUnitOfWork(_context))
                {
                   
                    model.Password = BC.HashPassword(model.Password);
                  
                    UserEntity user = unitOfWork.UserRepository.FindOneReadOnly(m => m.EmailAddress.ToLower() == model.EmailAddress.ToLower());

                    if (user == null)
                    {
                        user = new UserEntity()
                        {
                            EmailAddress = model.EmailAddress,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            PhoneNumber = model.PhoneNumber,
                            PasswordHash = model.Password
                        };

                        unitOfWork.UserRepository.InsertOrUpdate(user);
                        unitOfWork.Commit();

                        var token = TokenHelper.Generate(user.Id, _appSettings.Secret);

                        UserTokenEntity userToken = new UserTokenEntity()
                        {
                            UserId = user.Id,
                            Token = token,
                            ExpiryDate = DateTime.UtcNow.AddYears(1).ToLongDateString(),
                        };

                        unitOfWork.UserTokenRepository.InsertOrUpdate(userToken);
                        unitOfWork.Commit();

                        return ApiResponse(true, "success", new RegisterOutput()
                        {
                            EmailAddress = user.EmailAddress,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Token = token

                        });

                    }
                    return ApiResponse(false, "This email already exists!");
                }
            }
            catch (Exception ex)
            {
                LogError(typeof(UserController), ex);
                return ApiResponse(false, "error", ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInput model)
        {
            try
            {

                using (AppUnitOfWork unitOfWork = new AppUnitOfWork(_context))
                {

                    var allUsers = unitOfWork.UserRepository.FindReadOnly().ToList();
                    UserEntity user = allUsers.Where(f => f.EmailAddress.ToLower() == model.UserName.ToLower()).FirstOrDefault();

                    if (user != null && BC.Verify(model.Password, user.PasswordHash))

                    {
                        var token = TokenHelper.Generate(user.Id, _appSettings.Secret);

                        UserTokenEntity userToken = new UserTokenEntity()
                        {
                            UserId = user.Id,
                            Token = token,
                            ExpiryDate = DateTime.UtcNow.AddYears(1).ToLongDateString(),
                        };

                        unitOfWork.UserTokenRepository.InsertOrUpdate(userToken);
                        unitOfWork.UserRepository.InsertOrUpdate(user);
                        unitOfWork.Commit();

                        return ApiResponse(true, "success", new RegisterOutput()
                        {

                            EmailAddress = user.EmailAddress,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Token = token
                        });
                    }

                    return ApiResponse(false, "Invalid Credentials");
                }
            }


            catch (Exception ex)
            {
                LogError(typeof(UserController), ex);
                return ApiResponse(false, "error",ex);
            }

        }

        #endregion

    }

}
