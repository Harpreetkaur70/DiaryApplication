using System;
using DiaryApplication.Models;
using DiaryApplication.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DiaryApplication.Web.Areas.Identity.IdentityHostingStartup))]
namespace DiaryApplication.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<DiaryApplicationWebContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("DiaryApplicationWebContextConnection")));

                //services.AddDefaultIdentity<DiaryApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<DiaryApplicationWebContext>();
            });
        }
    }
}