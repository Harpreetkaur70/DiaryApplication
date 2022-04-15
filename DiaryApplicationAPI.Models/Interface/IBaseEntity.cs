using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplicationAPI.Models.Interface
{
   public interface IBaseEntity : IIdentifier, IAuditInfo
    {
    }
}
