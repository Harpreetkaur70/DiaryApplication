using DiaryApplication.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApplication.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _someFilterParameter;

        public ApiAuthorizeAttribute() : base()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }

        public ApiAuthorizeAttribute(string someFilterParameter)
        {
            _someFilterParameter = someFilterParameter;
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // throw new NotImplementedException();

            var User = context.HttpContext.User;
            if(User.Identity.IsAuthenticated)
            {
                return;
            }

            AppDbContext _context = (AppDbContext)context.HttpContext.RequestServices.GetService(typeof(AppDbContext));

            //AppUnitOfWork unitOfWork = new AppUnitOfWork(_context);
            //if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            //{
            //    var user = unitOfWork.UserRepository.FindOneReadOnly(m => m.Id == Convert.ToInt32(User.Identity.Name) && m.UserStatus == UserStatus.Active);

            //    if (user == null)
            //    {
            //        context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
            //        return;
            //    }
            //}
        }
    }
}
