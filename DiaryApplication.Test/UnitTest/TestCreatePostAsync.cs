using DiaryApplication.Models;
using DiaryApplication.Web.Data;
using DiaryApplication.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApplication.Test.UnitTest
{
    [TestClass]
    public class TestCreatePostAsync
    {
        private readonly IDiaryService _diaryService;
        public TestCreatePostAsync()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("TestDataBase"));
            services.AddTransient<IDiaryService, DiaryService>();
            var serviceProvider = services.BuildServiceProvider();
            _diaryService = serviceProvider.GetService<IDiaryService>();
        }
        #region positive test cases

        [TestMethod]
        public async Task TestCreatPostAsync()
        {
            //Arrange
            var expected = true;
            //Act
            var result = await _diaryService.CreatePostAsync(new DiaryPostEntity { Title = "My Diary", Content = "abc", ImageUrl = "", CreatedDate = DateTime.Now, UserId = "" });
            //Assert
            Assert.AreEqual(expected, result);
        }

        #endregion
        #region negative Test Cases
        [TestMethod]
        public void TestNoCreatePostAsync()
        {
            //Arrange
            var expected = false;
            //Act
            var result = _diaryService.CreatePostAsync(new DiaryPostEntity { Title = null, Content = "abc", ImageUrl = "", CreatedDate = DateTime.Now, UserId = "" });
            //Assert
            Assert.AreNotEqual(expected, result);
        }
        #endregion

    }
}
