using LLJ_CarInsuranceMS_RESTAPI.Controllers;
using LLJ_CarInsuranceMS_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class InsuranceClaimTypesControllerTests
    {
        private Object _CarInsuranceContext;
        private InsuranceClaimTypesController _controllerUnderTest;
        private List<InsuranceClaimType> _claimTypeList;
        InsuranceClaimType _claimType;

        [SetUp]
        public void Setup()
        {
            _CarInsuranceContext = InMemoryContext.GeneratedData();
            _controllerUnderTest = new InsuranceClaimTypesController((LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext);
            _claimTypeList = new List<InsuranceClaimType>();
            _claimType = new InsuranceClaimType()
            {
                ClaimTypeId = 1,
                ClaimTypeName = "Test",
                ClaimTypeDescription = "Testing Testing Testing"
            };

        }

        [TearDown]
        public void Teardown()
        {
            _CarInsuranceContext = null;
            _claimTypeList = null;
            _claimType = null;
            _controllerUnderTest = null;
        }

        [Test]
        public async Task _001Test_GetAllInsuranceClaimTypes_ReturnsaListwithValidCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);


            //Act            
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_localContext.InsuranceClaimTypes, Is.Not.Null);
                Assert.That(_controllerUnderTest.GetInsuranceClaimTypes(), Is.Not.Null);
                Assert.That(_localContext.InsuranceClaimTypes.Count(), Is.EqualTo(5));
            });

        }

        [Test]
        public async Task _002Test_GetClaimTypeById_ReturnsWithCorrectType()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);


            await _controllerUnderTest.PostInsuranceClaimType(_claimType);
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());

            //Act            
            var actionResult = await _controllerUnderTest.GetInsuranceClaimType(1);

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<ActionResult<InsuranceClaimType>>());
        }

        [Test]
        public async Task _003Test_PostInsuranceClaimTypes_AddedSuccessfullyAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);

            //Act            
            var actionResult = await _controllerUnderTest.PostInsuranceClaimType(_claimType);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(_localContext.InsuranceClaimTypes.Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task _004Test_DeleteInsuranceClaimType_DeleteSuccessfullyReturnsWithCorrectTypeAndNull_WhenFindingDeletedID()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);

            InsuranceClaimType _claimType2 = new InsuranceClaimType()
            {
                ClaimTypeId = 2,
                ClaimTypeName = "Test",
                ClaimTypeDescription = "Testing Testing Testing"
            };

            //Act
            var actionResult = await _controllerUnderTest.PostInsuranceClaimType(_claimType);
            await _controllerUnderTest.PostInsuranceClaimType(_claimType);
            _claimTypeList = await _localContext.InsuranceClaimTypes.ToListAsync();

            var actionResultDeleted = await _controllerUnderTest.DeleteInsuranceClaimType(_claimType2.ClaimTypeId);

            //Assert
            Assert.That(actionResultDeleted, Is.Not.Null);
            Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<InsuranceClaimType>>());

            var deleted = await _localContext.InsuranceClaimTypes.FindAsync(_claimType2.ClaimTypeId);
            Assert.That(deleted, Is.Null);

        }

        [Test]
        public async Task _005Test_DeleteInsuranceClaimType_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);

            InsuranceClaimType _claimType2 = new InsuranceClaimType()
            {
                ClaimTypeId = 2,
                ClaimTypeName = "Test",
                ClaimTypeDescription = "Testing Testing Testing"
            };

            InsuranceClaimType _claimType3 = new InsuranceClaimType()
            {
                ClaimTypeId = 3,
                ClaimTypeName = "Test",
                ClaimTypeDescription = "Testing Testing Testing"
            };

            //Act
            await _controllerUnderTest.PostInsuranceClaimType(_claimType);
            await _controllerUnderTest.PostInsuranceClaimType(_claimType2);
            await _controllerUnderTest.PostInsuranceClaimType(_claimType3);

            var actionResultDeleted = await _controllerUnderTest.DeleteInsuranceClaimType(_claimType2.ClaimTypeId);

            //Assert
            Assert.NotNull(actionResultDeleted);
            Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<InsuranceClaimType>>());
            Assert.That(_localContext.InsuranceClaimTypes.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task _006Test_PutInsuranceClaimType_ValidUpdate_ReturnsOk()
        {
            // Arrange
            int claimTypeId = 1;
            var _claimType3 = new InsuranceClaimType()
            {
                ClaimTypeId = claimTypeId,
                ClaimTypeName = "Updated",
                ClaimTypeDescription = "Testing Testing Testing"
            };

            // Act
            var result = await _controllerUnderTest.PutInsuranceClaimType(claimTypeId, _claimType3);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }

        [Test]
        public async Task _007Test_PutInsuranceClaimType_InvalidUpdate_ReturnsBadRequest()
        {
            // Arrange
            int claimTypeId = 1;
            var _claimType3 = new InsuranceClaimType()
            {
                ClaimTypeId = 3,
                ClaimTypeName = "Updated",
                ClaimTypeDescription = "Testing Testing Testing"
            };


            // Act
            var result = await _controllerUnderTest.PutInsuranceClaimType(claimTypeId, _claimType3);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task _008Test_GetInsuranceClaimTypes_ReturnsListOfClaimTypes()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);


            await _controllerUnderTest.PostInsuranceClaimType(_claimType);
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());

            //Act            
            var actionResult = await _controllerUnderTest.GetInsuranceClaimType(1);
            var all = await _controllerUnderTest.GetInsuranceClaimTypes();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(all, Is.Not.Null);
                Assert.That(actionResult, Is.InstanceOf<ActionResult<InsuranceClaimType>>());
            });

        }

        [Test]
        public async Task _009Test_GetAllClaimTypes_ReturnsWithCorrectTypeAndCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);


            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());
            await _controllerUnderTest.PostInsuranceClaimType(new InsuranceClaimType());

            //Act            
            var actionResult = await _controllerUnderTest.GetInsuranceClaimTypes();

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<ActionResult<IEnumerable<InsuranceClaimType>>>());
            var value = actionResult.Value;
            Assert.That(value.Count(), Is.EqualTo(5));
        }

        [Test]
        public async Task _010Test_GetInsuranceClaimTypes_DatabaseError_ReturnsInternalServerError()
        {
            // Arrange
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimTypesController(_localContext);

            _controllerUnderTest = new InsuranceClaimTypesController(null);

            // Act
            var result = await _controllerUnderTest.GetInsuranceClaimTypes();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<InsuranceClaimType>>>());
            Assert.That(result.Result, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = (StatusCodeResult)result.Result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }
    }
}
