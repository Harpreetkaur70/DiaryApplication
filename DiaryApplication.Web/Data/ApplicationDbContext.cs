using DiaryApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplication.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<DiaryApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DiaryPostEntity> DiaryPosts { get; set; }
    }
}
