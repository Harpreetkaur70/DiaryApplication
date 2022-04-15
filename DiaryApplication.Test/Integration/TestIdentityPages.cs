using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DiaryApplication.Test.Integration
{
    [TestClass]
    public class TestIdentityPages : IClassFixture<WebApplicationFactory<DiaryApplication.Web.Startup>>
    {
        private readonly WebApplicationFactory<DiaryApplication.Web.Startup> _factory;
        public TestIdentityPages(WebApplicationFactory<DiaryApplication.Web.Startup> factory)
        {
            _factory = factory;
        }
        #region positive Test cases
        [Theory]
        [InlineData("/")]
        [InlineData("/Identity/Account/Login")]
        [InlineData("/Identity/Account/Register")]
        [InlineData("/Identity/Account/ForgotPassword")]
        [InlineData("/Identity/Account/ResendEmailConfirmation")]
        [TestMethod]
        public async Task TestRenderPages(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equals("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

        }
        #endregion
        #region Negative Test Cases
        public async Task GetNegativeAllPostAsync(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 404
            Assert.Equals(404, response.StatusCode);
                //response.Content.Headers.ContentType.ToString());
        }
        #endregion
    }
}
