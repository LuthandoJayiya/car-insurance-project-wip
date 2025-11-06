using LLJ_CarInsuranceMS_ASPNET;
using LLJ_CarInsuranceMS_ASPNET.Controllers;
using LLJ_CarInsuranceMS_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPMVC.xUnitTests
{
    public class HomeControllerTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationfactory;
        private readonly Mock<ILogger<HomeController>> _mockLogger;

        public HomeControllerTests()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
        }

        [Fact]
        public void _01Test_HomeController_Index_ReturnsViewResults()
        {
            //arrange
            var controller = new HomeController(_mockLogger.Object);

            //act
            var result = controller.Index();

            //assert
            var vewResults = Assert.IsType<ViewResult>(result);
            Assert.Null(vewResults.ViewName);
        }

        [Fact]
        public void _02Test_HomeController_Privacy_ReturnsViewResults()
        {
            //arrange
            var controller = new HomeController(_mockLogger.Object);

            //act
            var result = controller.Privacy();

            //assert
            var vewResults = Assert.IsType<ViewResult>(result);
            Assert.Null(vewResults.ViewName);
        }

        [Fact]
        public void _03Test_HomeController_Contact_ReturnsViewResults()
        {
            //arrange
            var controller = new HomeController(_mockLogger.Object);

            //act
            var result = controller.Contact();

            //assert
            var vewResults = Assert.IsType<ViewResult>(result);
            Assert.Null(vewResults.ViewName);
        }
    }
}
