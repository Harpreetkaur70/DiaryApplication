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
    public class TestDeletePostAsync
    {
        private readonly IDiaryService _diaryService;
        public TestDeletePostAsync()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("TestDataBase"));
            services.AddTransient<IDiaryService, DiaryService>();
            var serviceProvider = services.BuildServiceProvider();
            _diaryService = serviceProvider.GetService<IDiaryService>();
        }
        #region positive test cases
        [TestMethod]
        public async Task TestPositiveDeletePostAsync()
        {
            //Arrange
            var expected = true;
            //Act
            var result = await _diaryService.DeletePostAsync(new DiaryPostEntity { Id = 1, Title = "MyDiaryApp", Content = "sfgh", ImageUrl = "", CreatedDate = DateTime.Now, UserId = "" });
            //Assert
            Assert.AreEqual(expected, result);
        }
        #endregion
        #region negative Test cases
        public async Task TestNegitiveDeletePOstAsync()
        {
            //Arrangei
            var expected = false;
            //Act
            var result = await _diaryService.DeletePostAsync(new DiaryPostEntity { Id = 1, Title = "MyDiaryApp", Content = "dghj", ImageUrl = "", CreatedDate = DateTime.Now, UserId = "" });
            //Assert
            Assert.AreEqual(expected, result);
        }
        #endregion
    }
}

