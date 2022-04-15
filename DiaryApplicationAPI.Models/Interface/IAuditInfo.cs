using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplicationAPI.Models.Interface
{
   public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
