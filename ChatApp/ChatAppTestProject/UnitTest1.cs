using ChatApp.Pages;
using ChatApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Xunit;

namespace ChatAppTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
        [Theory]
        [InlineData(signInManager)]              
        public async Task OnPostAsync_Login_WhenModelStateIsInvalid(SignInManager<IdentityUser> signInManager)
        {

            var pageModel = new LoginModel(signInManager)
            {

            };
          
            pageModel.ModelState.AddModelError("Message.Text", "The Text field is required.");

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }

    }
}