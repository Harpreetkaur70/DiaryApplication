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
    public class TestGetPostById
    {
        private readonly IDiaryService _diaryService;
        public TestGetPostById()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("TestDataBase"));
            services.AddTransient<IDiaryService, DiaryService>();
            var serviceProvider = services.BuildServiceProvider();
            _diaryService = serviceProvider.GetService<IDiaryService>();
        }
        #region positive test cases

        [TestMethod]
        public async Task TestPositiveGetPostById()
        {
            string userId = "1";
            int id = 1;
            //Arrange
            DiaryPostEntity expected = null;
            //Act
            var result =await _diaryService.GetPostById(id,userId);
            //Assert
            Assert.AreEqual(expected, result);
        }
        #endregion

        #region negative test cases
        [TestMethod]
        public async Task TestNegativeGetPostById()
        {
            string userId = "1";
            int id = 1;
            //Arrange
            DiaryPostEntity expected = new DiaryPostEntity();
            //Act
            var result = await _diaryService.GetPostById(id, userId);
            //Assert
            Assert.AreNotEqual(expected, result);
        }
        #endregion
    }
}
