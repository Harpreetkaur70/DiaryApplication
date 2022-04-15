using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplication.Models.Interface
{
   public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
