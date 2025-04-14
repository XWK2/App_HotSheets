using System.Threading.Tasks;
using Denso.ShippingInstructions.Models.TokenAuth;
using Denso.ShippingInstructions.Web.Controllers;
using Shouldly;
using Xunit;

namespace Denso.ShippingInstructions.Web.Tests.Controllers
{
    public class HomeController_Tests: ShippingInstructionsWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}