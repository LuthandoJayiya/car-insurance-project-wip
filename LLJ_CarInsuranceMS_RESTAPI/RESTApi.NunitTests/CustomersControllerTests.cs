using LLJ_CarInsuranceMS_RESTAPI.Controllers;
using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.Repository;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private Object _CarInsuranceContext;
        private CustomersController _controllerUnderTest;
        private List<PotentialCustomer> _customerList;
        private PotentialCustomerVM _potentialCustomerVM;
        PotentialCustomer _customer;

        [SetUp]
        public void Setup()
        {
            _CarInsuranceContext = InMemoryContext.GeneratedData();
            _controllerUnderTest = new CustomersController((LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext);
            _customerList = [];

            _potentialCustomerVM = new PotentialCustomerVM()
            {
                CustomerId = 1,
                CustomerName = "Testing",
                CustomerPhone = "0987654321",
                CustomerEmail = "example@testing.com",
                CustomerCity = "Test",
                CustomerCountry = "Testing"
            };
        }

        [TearDown]
        public void Teardown()
        {
            _CarInsuranceContext = null;
            _potentialCustomerVM = null;
            _customer = null;
            _controllerUnderTest = null;
            _customerList = null;
        }

        [Test]
        public async Task _001Test_GetAllCustomers_ReturnsaListwithValidCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);


            //Act            
            await _controllerUnderTest.PostCustomers(_potentialCustomerVM);

            _customerList = await _localContext.PotentialCustomers.ToListAsync();
            var customers = _controllerUnderTest.GetCustomers();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_customerList, Is.Not.Null);
                Assert.That(customers, Is.Not.Null);
                Assert.That(_customerList, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public async Task _002Test_GetCustomerById_ReturnsWithCorrectType()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);

            var _cust2 = new PotentialCustomerVM()
            {
                CustomerId = 2,
                CustomerName = "Testing",
                CustomerPhone = "0987654321",
                CustomerEmail = "example@testing.com",
                CustomerCity = "Test",
                CustomerCountry = "Testing"
            };

            await _controllerUnderTest.PostCustomers(_potentialCustomerVM);
            await _controllerUnderTest.PostCustomers(_cust2);

            //Act            
            var actionResult = await _controllerUnderTest.GetCustomer(_potentialCustomerVM.CustomerId);

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<ActionResult<PotentialCustomer>>());
        }

        [Test]
        public async Task _003Test_PostCustomer_AddedSuccessfullyAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);

            //Act            
            var actionResult = await _controllerUnderTest.PostCustomers(_potentialCustomerVM);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(_localContext.PotentialCustomers.Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task _004Test_DeleteCustomer_DeleteSuccessfullyReturnsWithCorrectTypeAndNull_WhenFindingDeletedID()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);

            var _cust2 = new PotentialCustomerVM()
            {
                CustomerId = 2,
                CustomerName = "Testing",
                CustomerPhone = "0987654321",
                CustomerEmail = "example@testing.com",
                CustomerCity = "Test",
                CustomerCountry = "Testing"
            };

            //Act
            var actionResult = await _controllerUnderTest.PostCustomers(_cust2);
            _customerList = await _localContext.PotentialCustomers.ToListAsync();

            var actionResultDeleted = await _controllerUnderTest.DeleteCustomers(_cust2.CustomerId);

            //Assert
            Assert.That(actionResultDeleted, Is.Not.Null);
            Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<PotentialCustomer>>());

            var deletedCust = await _localContext.PotentialCustomers.FindAsync(_cust2.CustomerId);
            Assert.That(deletedCust, Is.Null);

        }

        [Test]
        public async Task _005Test_DeleteCustomer_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);

            var _cust2 = new PotentialCustomerVM()
            {
                CustomerId = 2,
                CustomerName = "Testing",
                CustomerPhone = "0987654321",
                CustomerEmail = "example@testing.com",
                CustomerCity = "Test",
                CustomerCountry = "Testing"
            };

            var _cust3 = new PotentialCustomerVM()
            {
                CustomerId = 3,
                CustomerName = "Testing",
                CustomerPhone = "0987654321",
                CustomerEmail = "example@testing.com",
                CustomerCity = "Test",
                CustomerCountry = "Testing"
            };

            //Act
            await _controllerUnderTest.PostCustomers(_potentialCustomerVM);
            await _controllerUnderTest.PostCustomers(_cust2);
            await _controllerUnderTest.PostCustomers(_cust3);

            var actionResultDeleted = await _controllerUnderTest.DeleteCustomers(_cust2.CustomerId);

            //Assert
            Assert.That(actionResultDeleted, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<PotentialCustomer>>());
                Assert.That(_localContext.PotentialCustomers.Count(), Is.EqualTo(2));
            });
        }

        [Test]
        public async Task _006Test_PutCustomer_InvalidUpdate_ReturnsBadRequest()
        {
            // Arrange
            int custId = 1;
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);

            var updatedcust = new PotentialCustomerVM()
            {
                CustomerId = 3,
                CustomerName = "Update",
                CustomerPhone = "0987654321",
                CustomerEmail = "example@testing.com",
                CustomerCity = "Updated",
                CustomerCountry = "Updating"
            };

            // Act
            var result = await _controllerUnderTest.PutCustomer(custId, updatedcust);


            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task _007Test_GetCustomers_ReturnsListOfClaims()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);


            var updatedcust = new PotentialCustomerVM()
            {
                CustomerId = 3,
                CustomerName = "Update",
                CustomerPhone = "0987654321",
                CustomerEmail = "example@testing.com",
                CustomerCity = "Updated",
                CustomerCountry = "Updating"
            };


            await _controllerUnderTest.PostCustomers(_potentialCustomerVM);
            await _controllerUnderTest.PostCustomers(updatedcust);

            //Act            
            var actionResult = await _controllerUnderTest.GetCustomer(1);
            var allAgents = await _controllerUnderTest.GetCustomers();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(allAgents, Is.Not.Null);
                Assert.That(actionResult, Is.InstanceOf<ActionResult<PotentialCustomer>>());
            });

        }

        [Test]
        public async Task _008Test_GetCustomers_DatabaseError_ReturnsInternalServerError()
        {
            // Arrange
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new CustomersController(_localContext);
            LLJ_CarInsuranceMS_EFDBContext? tempContext = null;

            _controllerUnderTest = new CustomersController(tempContext);

            // Act
            var result = await _controllerUnderTest.GetCustomers();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<PotentialCustomer>>>());
            Assert.That(result.Result, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = (StatusCodeResult)result.Result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }
    }
}
