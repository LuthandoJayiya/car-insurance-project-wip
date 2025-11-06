using LLJ_CarInsuranceMS_ASPNET;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;


namespace ASPMVC.xUnitTests
{
    public class PoliciesControllerURLTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public PoliciesControllerURLTests( WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Theory]
        [InlineData("Policies/Index")]
        [InlineData("Policies/Details/1")]
        [InlineData("Policies/Details/2")]
        [InlineData("Policies/Details/3")]
        public async void _01Test_All_Policies_URL_ReturnsOkResponse(string url)
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
        [InlineData("ABC123")]
        [InlineData("KJU098")]
        [InlineData("FTR345")]
        public async void _02Test_All_Policies_Index_Data_ReturnsContent(string item)
        { 
            //arrange
            
            //act
            var response = await _httpClient.GetAsync("Policies/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();

            //assert
            Assert.Contains(item, contentString);
        }

        [Fact]
        public async Task _03Test_Policies_Index_ReturnsNonEmptyContent()
        {
            //Arrange
            // Act
            var response = await _httpClient.GetAsync("Policies/Index");
            var pageContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(pageContent));
        }

        [Theory]
        [InlineData("Policies/Details/100")]
        public async Task _04Test_Policy_Details_ReturnsNotFound_NonExistentID(string url)
        {
            //Arrange
            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task _05Test_Index_Page_ContainsPolicyDetailsLink()
        {

            // Arrange
            //Act
            var response = await _httpClient.GetAsync("Policies/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            
            // Assert
            Assert.Contains("Details", pageContent);
        }
    }
}
