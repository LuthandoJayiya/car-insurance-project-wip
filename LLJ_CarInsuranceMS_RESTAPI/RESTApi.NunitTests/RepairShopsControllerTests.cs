using LLJ_CarInsuranceMS_RESTAPI.Controllers;
using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class RepairShopsControllerTests
    {
        private Object _CarInsuranceContext;
        private RepairShopsController _controllerUnderTest;
        private List<RepairShop> _shopList;
        //private PotentialCustomerVM _potentialCustomerVM;
        RepairShop _shop;

        [SetUp]
        public void Setup()
        {
            _CarInsuranceContext = InMemoryContext.GeneratedData();
            _controllerUnderTest = new RepairShopsController((LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext);
            _shopList = [];

            _shop = new RepairShop()
            {
                ShopId = 1,
                ShopName = "Testing",
                Location = "Test",
                ContactInfo = "test@example.com"
            };
        }

        [TearDown]
        public void Teardown()
        {
            _CarInsuranceContext = null;
            _controllerUnderTest = null;
            _shopList = null;
            _shop = null;
        }

        [Test]
        public async Task _001Test_GetAllRepairShops_ReturnsaListwithValidCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);


            //Act            
            await _controllerUnderTest.PostRepairShop(_shop);

            _shopList = await _localContext.RepairShops.ToListAsync();
            var customers = _controllerUnderTest.GetRepairShops();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_shopList, Is.Not.Null);
                Assert.That(customers, Is.Not.Null);
                Assert.That(_shopList, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public async Task _002Test_GetRepairShopById_ReturnsWithCorrectType()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);

            var _shop1 = new RepairShop()
            {
                ShopId = 2,
                ShopName = "Testing",
                Location = "Test",
                ContactInfo = "test@example.com"
            };

            await _controllerUnderTest.PostRepairShop(_shop);
            await _controllerUnderTest.PostRepairShop(_shop1);

            //Act            
            var actionResult = await _controllerUnderTest.GetRepairShop(_shop1.ShopId);

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<ActionResult<RepairShop>>());
        }

        [Test]
        public async Task _003Test_PostRepairShop_AddedSuccessfullyAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);

            //Act            
            var actionResult = await _controllerUnderTest.PostRepairShop(_shop);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(_localContext.RepairShops.Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task _004Test_DeleteRepairShop_DeleteSuccessfullyReturnsWithCorrectTypeAndNull_WhenFindingDeletedID()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);

            var _shop1 = new RepairShop()
            {
                ShopId = 2,
                ShopName = "Testing",
                Location = "Test",
                ContactInfo = "test@example.com"
            };

            //Act
            var actionResult = await _controllerUnderTest.PostRepairShop(_shop1);
            _shopList = await _localContext.RepairShops.ToListAsync();

            var actionResultDeleted = await _controllerUnderTest.DeleteRepairShop(_shop1.ShopId);

            //Assert
            Assert.That(actionResultDeleted, Is.Not.Null);
            Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<RepairShop>>());

            var deletedShop = await _localContext.RepairShops.FindAsync(_shop1.ShopId);
            Assert.That(deletedShop, Is.Null);

        }

        [Test]
        public async Task _005Test_DeleteRepairShop_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);

            var _shop1 = new RepairShop()
            {
                ShopId = 2,
                ShopName = "Testing",
                Location = "Test",
                ContactInfo = "test@example.com"
            };

            var _shop2 = new RepairShop()
            {
                ShopId = 3,
                ShopName = "TestingTesting",
                Location = "TestTest",
                ContactInfo = "testtest@example.com"
            };

            //Act
            await _controllerUnderTest.PostRepairShop(_shop);
            await _controllerUnderTest.PostRepairShop(_shop1);
            await _controllerUnderTest.PostRepairShop(_shop2);

            var actionResultDeleted = await _controllerUnderTest.DeleteRepairShop(_shop2.ShopId);

            //Assert
            Assert.That(actionResultDeleted, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<RepairShop>>());
                Assert.That(_localContext.RepairShops.Count(), Is.EqualTo(2));
            });
        }

        [Test]
        public async Task _006Test_PutRepairShop_InvalidUpdate_ReturnsBadRequest()
        {
            // Arrange
            int custId = 1;
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);

            var _shop1 = new RepairShop()
            {
                ShopId = 2,
                ShopName = "Testing",
                Location = "Test",
                ContactInfo = "test@example.com"
            };

            // Act
            var result = await _controllerUnderTest.PutRepairShop(custId, _shop1);


            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task _007Test_GetCustomers_ReturnsListOfClaims()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);


            var _shop1 = new RepairShop()
            {
                ShopId = 2,
                ShopName = "Testing",
                Location = "Test",
                ContactInfo = "test@example.com"
            };


            await _controllerUnderTest.PostRepairShop(_shop);
            await _controllerUnderTest.PostRepairShop(_shop1);

            //Act            
            var actionResult = await _controllerUnderTest.GetRepairShop(1);
            var allRShops = await _controllerUnderTest.GetRepairShops();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(allRShops, Is.Not.Null);
                Assert.That(actionResult, Is.InstanceOf<ActionResult<RepairShop>>());
            });

        }

        [Test]
        public async Task _008Test_GetRepairShops_DatabaseError_ReturnsInternalServerError()
        {
            // Arrange
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new RepairShopsController(_localContext);
            LLJ_CarInsuranceMS_EFDBContext? tempContext = null;

            _controllerUnderTest = new RepairShopsController(tempContext);

            // Act
            var result = await _controllerUnderTest.GetRepairShops();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<RepairShop>>>());
            Assert.That(result.Result, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = (StatusCodeResult)result.Result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }
    }
}
