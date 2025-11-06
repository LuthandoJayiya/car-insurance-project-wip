using LLJ_CarInsuranceMS_RESTAPI.Controllers;
using LLJ_CarInsuranceMS_RESTAPI.Models;
using LLJ_CarInsuranceMS_RESTAPI.Repository;
using LLJ_CarInsuranceMS_RESTAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework.Internal;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class InsuranceClaimsControllerTests
    {
        private Object _CarInsuranceContext;
        private InsuranceClaimsController _controllerUnderTest;
        InsuranceClaimVM _claimVM;
        private List<InsuranceClaim> _claimList;
        InsuranceClaim _claim;
        private Mock<InsuranceClaimsRepo> _repoMock;
        private InsuranceClaimsController _controller;
        private Mock<LLJ_CarInsuranceMS_EFDBContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            _CarInsuranceContext = InMemoryContext.GeneratedData();
            _claimList = new List<InsuranceClaim>();
            _repoMock = new Mock<InsuranceClaimsRepo>();
            _contextMock = new Mock<LLJ_CarInsuranceMS_EFDBContext>();
            _controller = new InsuranceClaimsController(_contextMock.Object);

            _claimVM = new InsuranceClaimVM()
            {
                AccidentDate = DateTime.Now.ToString(),
                ClaimName = "Test",
                ClaimStatus = "Testing",
                ClaimId =1
                

            };
            _claim = new InsuranceClaim()
            {
                ClaimName = _claimVM.ClaimName,
                ClaimStatus = _claimVM.ClaimStatus,
                ClaimId = _claimVM.ClaimId,
                AccidentDate = DateTime.Now.ToString(),
            };

        }

        [TearDown]
        public void Teardown()
        {
            _CarInsuranceContext = null;
            _claimVM = null;
            _claim = null;
            _controllerUnderTest = null;
            _claimList = null;
            _repoMock = null;
            _controller = null;
            _contextMock = null;
        }

        [Test]
        public async Task _001Test_GetAllClaims_ReturnsaListwithValidCount()
        {
            //Arrange 
            var _localClaimContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localClaimContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localClaimContext);


            //Act            
            await _controllerUnderTest.CreateInsuranceClaim(_claimVM);

            _claimList = await _localClaimContext.InsuranceClaims.ToListAsync();
            var claims = _controllerUnderTest.GetInsuranceClaims();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_claimList, Is.Not.Null);
                Assert.That(claims, Is.Not.Null);
                Assert.That(_claimList, Has.Count.EqualTo(1));
            }); 
        }

        [Test]
        public async Task _002Test_GetClaimById_ReturnsWithCorrectType()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localContext);

            var _claim2 = new InsuranceClaimVM()
            {
                AccidentDate = DateTime.Now.ToString(),
                ClaimName = "Testing",
                ClaimStatus = "Testing",
                ClaimId = 2
            };

            await _controllerUnderTest.CreateInsuranceClaim(_claimVM);
            await _controllerUnderTest.CreateInsuranceClaim(_claim2);

            //Act            
            var actionResult = await _controllerUnderTest.GetInsuranceClaim(1);

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<ActionResult<InsuranceClaim>>());
        }

        [Test]
        public async Task _003Test_PostInsuranceClaim_AddedSuccessfullyAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localContext);

            //Act            
            var actionResult = await _controllerUnderTest.CreateInsuranceClaim(_claimVM);
            

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(_localContext.InsuranceClaims.Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task _004Test_DeleteInsuranceClaim_DeleteSuccessfullyReturnsWithCorrectTypeAndNull_WhenFindingDeletedID()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localContext);

            var _claim2 = new InsuranceClaimVM()
            {
                AccidentDate = DateTime.Now.ToString(),
                ClaimName = "Testing",
                ClaimStatus = "Testing",
                ClaimId = 2
            };

            //Act
            var actionResult = await _controllerUnderTest.CreateInsuranceClaim(_claim2);
            _claimList = await _localContext.InsuranceClaims.ToListAsync();

            var actionResultDeleted = await _controllerUnderTest.DeleteInsuranceClaim(_claim2.ClaimId);

            //Assert
            Assert.That(actionResultDeleted, Is.Not.Null);
            Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<InsuranceClaim>>());

            var deletedClaim = await _localContext.InsuranceClaims.FindAsync(_claim2.ClaimId);
            Assert.That(deletedClaim, Is.Null);

        }

        [Test]
        public async Task _005Test_DeleteInsuranceClaim_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localContext);

            var _claim2 = new InsuranceClaimVM()
            {
                AccidentDate = DateTime.Now.ToString(),
                ClaimName = "Testing",
                ClaimStatus = "Testing",
                ClaimId = 2
            };

            var _claim3 = new InsuranceClaimVM()
            {
                AccidentDate = DateTime.Now.ToString(),
                ClaimName = "Testing",
                ClaimStatus = "Testing",
                ClaimId = 3
            };

            //Act
            await _controllerUnderTest.CreateInsuranceClaim(_claimVM);
            await _controllerUnderTest.CreateInsuranceClaim(_claim2);
            await _controllerUnderTest.CreateInsuranceClaim(_claim3);

            var actionResultDeleted = await _controllerUnderTest.DeleteInsuranceClaim(_claim2.ClaimId);

            //Assert
            Assert.That(actionResultDeleted, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<InsuranceClaim>>());
                Assert.That(_localContext.InsuranceClaims.Count(), Is.EqualTo(2));
            });
        }

        [Test]
        public async Task _006Test_PutInsuranceAgent_InvalidUpdate_ReturnsBadRequest()
        {
            // Arrange
            int claimId = 1;
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localContext);

            var updatedClaim = new InsuranceClaim()
            {
                AccidentDate = DateTime.Now.ToString(),
                ClaimName = "Updated",
                ClaimStatus = "Testing",
                ClaimId = 2
            };

            // Act
            var result = await _controllerUnderTest.PutInsuranceClaim(claimId, updatedClaim);
           

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task _007Test_GetInsuranceClaims_ReturnsListOfClaims()
        {
            //Arrange 
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localContext);

            var claim2 = new InsuranceClaimVM()
            {
                AccidentDate = DateTime.Now.ToString(),
                ClaimName = "Updated",
                ClaimStatus = "Testing",
                ClaimId = 2
            };


            await _controllerUnderTest.CreateInsuranceClaim(_claimVM);
            await _controllerUnderTest.CreateInsuranceClaim(claim2);

            //Act            
            var actionResult = await _controllerUnderTest.GetInsuranceClaim(1);
            var allAgents = await _controllerUnderTest.GetInsuranceClaims();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.Not.Null);
                Assert.That(allAgents, Is.Not.Null);
                Assert.That(actionResult, Is.InstanceOf<ActionResult<InsuranceClaim>>());
            });

        }

        [Test]
        public async Task _008Test_GetInsuranceClaims_DatabaseError_ReturnsInternalServerError()
        {
            // Arrange
            var _localContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
            _localContext.Database.EnsureDeleted();
            _controllerUnderTest = new InsuranceClaimsController(_localContext);
            LLJ_CarInsuranceMS_EFDBContext ?tempContext = null;

            _controllerUnderTest = new InsuranceClaimsController(tempContext);

            // Act
            var result = await _controllerUnderTest.GetInsuranceClaims();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<InsuranceClaim>>>());
            Assert.That(result.Result, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = (StatusCodeResult)result.Result;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
        }
    }
}
