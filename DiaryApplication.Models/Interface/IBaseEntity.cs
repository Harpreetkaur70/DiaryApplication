using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplication.Models.Interface
{
   public interface IBaseEntity : IIdentifier, IAuditInfo
    {
    }
}
