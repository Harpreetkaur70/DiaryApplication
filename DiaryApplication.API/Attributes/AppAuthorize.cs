using DiaryApplication.Data;
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
    public class AppAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _someFilterParameter;

        public AppAuthorizeAttribute() : base()
        {
            AuthenticationSchemes = "CookieAuthentication";
        }

        public AppAuthorizeAttribute(string someFilterParameter)
        {
            _someFilterParameter = someFilterParameter;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var User = context.HttpContext.User;

            if (!User.Identity.IsAuthenticated)
            {
                return;
            }

            // you can also use registered services
            AppDbContext _context = (AppDbContext)context.HttpContext.RequestServices.GetService(typeof(AppDbContext));
        }
        // throw new NotImplementedException();
    }
    
}
