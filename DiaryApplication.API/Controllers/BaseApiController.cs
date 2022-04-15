using DiaryApplication.Data;
using DiaryApplicationAPI.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected AppDbContext _context;
        public BaseApiController(AppDbContext context)
        {
            _context = context;
        }

        protected UserEntity GetAuthorizedUser(AppUnitOfWork unitOfWork)
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                return unitOfWork.UserRepository.FindOneReadOnly(m => m.Id == Convert.ToInt32(User.Identity.Name));
            }
            return null;
        }

        protected UserEntity GetAuthorizedUser()
        {
            using (AppUnitOfWork unitOfWork = new AppUnitOfWork(_context))
            {
                return GetAuthorizedUser(unitOfWork);
            }
        }

        protected void LogError(Type type, Exception ex)
        {
            //Logger.GetLogger(type).Error(ex.Message, ex);
        }

        protected IActionResult ApiResponse(bool status, string message, object result = null)
        {
            if (result != null)
            {
                return Ok(new { Status = status, Message = message, Result = result });
            }
            else
            {
                return Ok(new { Status = status, Message = message });
            }
        }

    }
}
