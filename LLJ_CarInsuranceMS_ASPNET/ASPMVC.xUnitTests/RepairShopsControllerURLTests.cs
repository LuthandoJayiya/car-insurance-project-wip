using LLJ_CarInsuranceMS_ASPNET;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ASPMVC.xUnitTests
{
    public class RepairShopsControllerURLTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public RepairShopsControllerURLTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Theory]
        [InlineData("RepairShops/Index")]
        [InlineData("RepairShops/Details/1")]
        [InlineData("RepairShops/Details/2")]
        [InlineData("RepairShops/Details/3")]
        public async void _01Test_All_RepairShops_URL_ReturnsOkResponse(string url)
        {
            //arrange

            //act
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            int statusCode = (int)response.StatusCode;

            //assert
            Assert.NotNull(response);
            Assert.Equal(200, statusCode);
        }

        [Theory]
        [InlineData("TJ Auto Repairs")]
        [InlineData("Nur-Spec Auto")]
        [InlineData("Bryan Autoworxz")]
        public async void _02Test_All_RepairShops_Index_Data_ReturnsContent(string item)
        {
            //arrange

            //act
            var response = await _httpClient.GetAsync("RepairShops/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();

            //assert
            Assert.Contains(item, contentString);
        }

        [Fact]
        public async Task _03Test_RepairShops_Index_ReturnsNonEmptyContent()
        {
            //Arrange
            // Act
            var response = await _httpClient.GetAsync("RepairShops/Index");
            var pageContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(pageContent));
        }

        [Theory]
        [InlineData("RepairShops/Details/100")]
        public async Task _04Test_RepairShops_Details_ReturnsNotFound_NonExistentID(string url)
        {
            //Arrange
            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task _05Test_Index_Page_ContainsRepairShopDetailsLink()
        {

            // Arrange
            //Act
            var response = await _httpClient.GetAsync("RepairShops/Index");
            var pageContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Details", pageContent);
        }
    }
}
