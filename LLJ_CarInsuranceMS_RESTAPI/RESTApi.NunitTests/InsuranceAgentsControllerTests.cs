//using LLJ_CarInsuranceMS_RESTAPI.Controllers;
//using LLJ_CarInsuranceMS_RESTAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace RESTApi.NunitTests
//{
//    public class InsuranceAgentsControllerTests
//    {
//        private Object _CarInsuranceContext;
//        private InsuranceAgentsController _controllerUnderTest;
//        private List<InsuranceAgent> _agentList;
//        InsuranceAgent _agent;

//        [SetUp]
//        public void Setup()
//        {
//            _CarInsuranceContext = InMemoryContext.GeneratedData();
//            _controllerUnderTest = new InsuranceAgentsController((LLJ_CarInsuranceMS_EFDBContext) _CarInsuranceContext);
//            _agentList = new List<InsuranceAgent>();
//            _agent = new InsuranceAgent()
//            {
//                AgentFirstName = "First",
//                AgentLastName = "Last",
//                AgentContactNumber = "123654789",
//                AgentEmail =  "FirstL@example.com",
//                Location = "Anywhere",
//                LicenseNumber = "test345",
//                CommissionRate = "6.00",
//                CustomerFeedback = "Excellent"
//            };

//        }

//        [TearDown]
//        public void Teardown()
//        {
//            _CarInsuranceContext = null;
//            _agentList = null;
//            _agent = null;
//            _controllerUnderTest = null;
//        }

//        [Test]
//        public async Task _001Test_GetAllAgents_ReturnsaListwithValidCount()
//        {
//            //Arrange 
//            var _localClaimContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localClaimContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localClaimContext);


//            //Act            
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
            
//            //Assert
//            Assert.Multiple(() =>
//            {
//                Assert.That(_localClaimContext.InsuranceAgents, Is.Not.Null);
//                Assert.That(_controllerUnderTest.GetInsuranceAgents(), Is.Not.Null);
//                Assert.That(_localClaimContext.InsuranceAgents.Count(), Is.EqualTo(5));
//            });
            
//        }

//        [Test]
//        public async Task _002Test_GetAgentById_ReturnsWithCorrectType()
//        {
//            //Arrange 
//            var _localAgentContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localAgentContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localAgentContext);


//            await _controllerUnderTest.PostInsuranceAgent(_agent);
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());

//            //Act            
//            var actionResult = await _controllerUnderTest.GetInsuranceAgent(1);

//            //Assert
//            Assert.NotNull(actionResult);
//            Assert.That(actionResult, Is.InstanceOf<ActionResult<InsuranceAgent>>());
//        }

//        [Test]
//        public async Task _003Test_PostInsuranceAgents_AddedSuccessfullyAndShowsInContextCount()
//        {
//            //Arrange 
//            var _localAgentContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localAgentContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localAgentContext);

//            //Act            
//            var actionResult = await _controllerUnderTest.PostInsuranceAgent(_agent);

//            //Assert
//            Assert.Multiple(() =>
//            {
//                Assert.That(actionResult, Is.Not.Null);
//                Assert.That(_localAgentContext.InsuranceAgents.Count(), Is.EqualTo(1));
//            });
//        }

//        [Test]
//        public async Task _004Test_DeleteInsuranceAgents_DeleteSuccessfullyReturnsWithCorrectTypeAndNull_WhenFindingDeletedID()
//        {
//            //Arrange 
//            var _localAgentContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localAgentContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localAgentContext);

//            InsuranceAgent _agent2 = new InsuranceAgent()
//            {
//                AgentId = 2,
//                AgentFirstName = "Second",
//                AgentLastName = "Second Last",
//                AgentContactNumber = "123654789",
//                AgentEmail = "FirstL@example.com",
//                Location = "Anywhere",
//                LicenseNumber = "test345",
//                CommissionRate = "6.00",
//                CustomerFeedback = "Excellent"
//            };

//            //Act
//            var actionResult = await _controllerUnderTest.PostInsuranceAgent(_agent2);
//            _agentList = await _localAgentContext.InsuranceAgents.ToListAsync();

//            var actionResultDeleted = await _controllerUnderTest.DeleteInsuranceAgent(_agent2.AgentId);

//            //Assert
//            Assert.That(actionResultDeleted, Is.Not.Null);
//            Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<InsuranceAgent>>());

//            var deletedAgent = await _localAgentContext.InsuranceAgents.FindAsync(_agent2.AgentId);
//            Assert.That(deletedAgent, Is.Null);

//        }

//        [Test]
//        public async Task _005Test_DeleteInsuranceAgent_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
//        {
//            //Arrange 
//            var _localAgentContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localAgentContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localAgentContext);
//            InsuranceAgent _agent1 = new InsuranceAgent()
//            {
//                AgentId = 2,
//                AgentFirstName = "Second",
//                AgentLastName = "Second Last",
//                AgentContactNumber = "123654789",
//                AgentEmail = "FirstL@example.com",
//                Location = "Anywhere",
//                LicenseNumber = "test345",
//                CommissionRate = "6.00",
//                CustomerFeedback = "Excellent"
//            };
//            InsuranceAgent _agent2 = new InsuranceAgent()
//            {
//                AgentId = 3,
//                AgentFirstName = "Second",
//                AgentLastName = "Second Last",
//                AgentContactNumber = "123654789",
//                AgentEmail = "FirstL@example.com",
//                Location = "Anywhere",
//                LicenseNumber = "test345",
//                CommissionRate = "6.00",
//                CustomerFeedback = "Excellent"
//            };

//            //Act
//            await _controllerUnderTest.PostInsuranceAgent(_agent);
//            await _controllerUnderTest.PostInsuranceAgent(_agent1);
//            await _controllerUnderTest.PostInsuranceAgent(_agent2);

//            var actionResultDeleted = await _controllerUnderTest.DeleteInsuranceAgent(_agent2.AgentId);

//            //Assert
//            Assert.NotNull(actionResultDeleted);
//            Assert.That(actionResultDeleted, Is.InstanceOf<ActionResult<InsuranceAgent>>());
//            Assert.That(_localAgentContext.InsuranceAgents.Count(), Is.EqualTo(2));
//        }

//        [Test]
//        public async Task _006Test_PutInsuranceAgent_ValidUpdate_ReturnsOk()
//        {
//            // Arrange
//            int agentId = 1;
//            var updatedAgent = new InsuranceAgent()
//            {
//                AgentId = agentId,
//                AgentFirstName = "UpdatedFirstName",
//                AgentLastName = "UpdatedLastName",
//                AgentContactNumber = "123654789",
//                AgentEmail = "FirstL@example.com",
//                Location = "Anywhere",
//                LicenseNumber = "test345",
//                CommissionRate = "6.00",
//                CustomerFeedback = "Excellent"
//            };

//            // Act
//            var result = await _controllerUnderTest.PutInsuranceAgent(agentId, updatedAgent);

//            // Assert
//            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            
//        }

//        [Test]
//        public async Task _007Test_PutInsuranceAgent_InvalidUpdate_ReturnsBadRequest()
//        {
//            // Arrange
//            int agentId = 1;
//            var updatedAgent = new InsuranceAgent()
//            {
//                AgentId = 2, 
//                AgentFirstName = "UpdatedFirstName",
//                AgentLastName = "UpdatedLastName",
//                AgentContactNumber = "123654789",
//                AgentEmail = "FirstL@example.com",
//                Location = "Anywhere",
//                LicenseNumber = "test345",
//                CommissionRate = "6.00",
//                CustomerFeedback = "Excellent"
//            };

//            // Act
//            var result = await _controllerUnderTest.PutInsuranceAgent(agentId, updatedAgent);

//            // Assert
//            Assert.That(result, Is.InstanceOf<BadRequestResult>());
//        }

//        [Test]
//        public async Task _008Test_GetInsuranceAgents_ReturnsListOfAgents()
//        {
//            //Arrange 
//            var _localAgentContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localAgentContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localAgentContext);


//            await _controllerUnderTest.PostInsuranceAgent(_agent);
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());

//            //Act            
//            var actionResult = await _controllerUnderTest.GetInsuranceAgent(1);
//            var allAgents = await _controllerUnderTest.GetInsuranceAgents();


//            //Assert
//            Assert.Multiple(() =>
//            {
//                Assert.That(actionResult, Is.Not.Null);
//                Assert.That(allAgents, Is.Not.Null);
//                Assert.That(actionResult, Is.InstanceOf<ActionResult<InsuranceAgent>>());
//            });

//        }

//        [Test]
//        public async Task _009Test_GetAllClaims_ReturnsWithCorrectTypeAndCount()
//        {
//            //Arrange 
//            var _localAgentContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localAgentContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localAgentContext);


//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());
//            await _controllerUnderTest.PostInsuranceAgent(new InsuranceAgent());

//            //Act            
//            var actionResult = await _controllerUnderTest.GetInsuranceAgents();

//            //Assert
//            Assert.That(actionResult, Is.Not.Null);
//            Assert.That(actionResult, Is.InstanceOf<ActionResult<IEnumerable<InsuranceAgent>>>());
//            var value = actionResult.Value;
//            Assert.That(value.Count(), Is.EqualTo(5));
//        }

//        [Test]
//        public async Task _010Test_GetInsuranceAgents_DatabaseError_ReturnsInternalServerError()
//        {
//            // Arrange
//            var _localAgentContext = (LLJ_CarInsuranceMS_EFDBContext)_CarInsuranceContext;
//            _localAgentContext.Database.EnsureDeleted();
//            _controllerUnderTest = new InsuranceAgentsController(_localAgentContext);

//            _controllerUnderTest = new InsuranceAgentsController(null);

//            // Act
//            var result = await _controllerUnderTest.GetInsuranceAgents();

//            // Assert
//            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<InsuranceAgent>>>());
//            Assert.That(result.Result, Is.InstanceOf<StatusCodeResult>());
//            var statusCodeResult = (StatusCodeResult)result.Result;
//            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
//        }
//    }
//}
