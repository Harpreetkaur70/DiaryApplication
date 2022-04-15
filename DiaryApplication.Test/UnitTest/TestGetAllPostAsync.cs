using DiaryApplication.Models;
using DiaryApplication.Web.Data;
using DiaryApplication.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApplication.Test.UnitTest
{
    [TestClass]
    public class TestGetAllPostAsync
    {
        //private readonly UserManager<DiaryApplicationUser> _userManager;
        private readonly IDiaryService _diaryService;

        public   TestGetAllPostAsync()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("TestDataBase"));
            services.AddTransient<IDiaryService,DiaryService >();
            var serviceProvider = services.BuildServiceProvider();
            _diaryService = serviceProvider.GetService<IDiaryService>();
        }
        #region positive test cases

        [TestMethod]
        public async Task TestPositiveGetAllPostAsync()
        {
            string userId = "1";

            //Arrange
            var expected = 0;
            //Act
            var result = await _diaryService.GetAllPostsAsync(userId);
            //Assert
            Assert.AreEqual(expected, result.Count());
        }
        #endregion

        #region negative test cases
        [TestMethod]
        public async Task TestNegitiveGetAllPostAsync()
        {
            string userId = "1";

            //Arrange
            var expected = 5;
            //Act
            var result = await _diaryService.GetAllPostsAsync(userId);
            //Assert
            Assert.AreNotEqual(expected, result.Count());

        }
        #endregion
    }
}
