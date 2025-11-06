namespace POCOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

 

    internal sealed class Configuration : DbMigrationsConfiguration<POCOS.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(POCOS.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            #region AgentSeed
            //context.InsuranceAgents.AddOrUpdate
            //(
            //    new InsuranceAgent()
            //    {
            //        AgentID = 1,
            //        AgentFirstName = "Neil",
            //        AgentLastName = "Tyson",
            //        AgentEmail = "NtysoN@example.com",
            //        AgentContactNumber = "152-3651-95863",
            //        Location = "Cape Town",
            //        LicenseNumber = "AB1234",
            //        CommissionRate = "5",
            //        CustomerFeedback = "Great Agent give him 5 stars"
            //    },
            //    new InsuranceAgent()
            //    {
            //        AgentID = 2,
            //        AgentFirstName = "Trish",
            //        AgentLastName = "Waynes",
            //        AgentEmail = "WaynesT@example.com",
            //        AgentContactNumber = "152-8591-95863",
            //        Location = "Cape Town",
            //        LicenseNumber = "KL3417",
            //        CommissionRate = "6",
            //        CustomerFeedback = "Great Agent give her 5 stars"
            //    },
            //    new InsuranceAgent()
            //    {
            //        AgentID = 3,
            //        AgentFirstName = "Thabo",
            //        AgentLastName = "Mdluli",
            //        AgentEmail = "MdluliT@example.com",
            //        AgentContactNumber = "152-781-95863",
            //        Location = "Cape Town",
            //        LicenseNumber = "GH5613",
            //        CommissionRate = "5",
            //        CustomerFeedback = "Great Agent give him 5 stars"
            //    },
            //    new InsuranceAgent()
            //    {
            //        AgentID = 4,
            //        AgentFirstName = "Wandile",
            //        AgentLastName = "Mncube",
            //        AgentEmail = "MncubeW@example.com",
            //        AgentContactNumber = "152-789-95863",
            //        Location = "Cape Town",
            //        LicenseNumber = "LP7821",
            //        CommissionRate = "5",
            //        CustomerFeedback = "Great Agent give him 5 stars"
            //    },
            //    new InsuranceAgent()
            //    {
            //        AgentID = 5,
            //        AgentFirstName = "Kate",
            //        AgentLastName = "Williams",
            //        AgentEmail = "WilliamsK@example.com",
            //        AgentContactNumber = "789-789-95863",
            //        Location = "Cape Town",
            //        LicenseNumber = "GH8793",
            //        CommissionRate = "5",
            //        CustomerFeedback = "Great Agent give her 5 stars"
            //    });
            #endregion

            #region CustomerSeed
            context.Customers.AddOrUpdate(
              new PotentialCustomer()
              {
                  CustomerID = 1,
                  //AgentID = 1,
                  CustomerName = "John",
                  CustomerPhone = "0278163587",
                  CustomerEmail = "john@hotmail.com",
                  CustomerCity = "Welkom",
                  CustomerCountry = "South Africa"

              },
              new PotentialCustomer()
              {
                  CustomerID = 2,
                  //AgentID = 2,
                  CustomerName = "James",
                  CustomerPhone = "027868987",
                  CustomerEmail = "james@hotmail.com",
                  CustomerCity = "Leeds",
                  CustomerCountry = "England"
              },
              new PotentialCustomer()
              {
                  CustomerID = 3,
                  //AgentID = 2,
                  CustomerName = "Jimmy",
                  CustomerPhone = "0278343587",
                  CustomerEmail = "jimmy@hotmail.com",
                  CustomerCity = "Cape Town",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 4,
                 // AgentID = 4,
                  CustomerName = "Isaac",
                  CustomerPhone = "0278163456",
                  CustomerEmail = "isaac@hotmail.com",
                  CustomerCity = "Maseru",
                  CustomerCountry = "Lesotho"
              },
              new PotentialCustomer()
              {
                  CustomerID = 5,
                  //AgentID = 3,
                  CustomerName = "Sarah",
                  CustomerPhone = "0278163450",
                  CustomerEmail = "sarah@hotmail.com",
                  CustomerCity = "Maputo",
                  CustomerCountry = "Mozambique"
              },
              new PotentialCustomer()
              {
                  CustomerID = 6,
                  //AgentID = 5,
                  CustomerName = "Libo",
                  CustomerPhone = "0278163987",
                  CustomerEmail = "libo@hotmail.com",
                  CustomerCity = "Kimberly",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 7,
                  //AgentID = 3,
                  CustomerName = "Sands",
                  CustomerPhone = "0278163389",
                  CustomerEmail = "sands@hotmail.com",
                  CustomerCity = "George",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 8,
                  //AgentID = 4,
                  CustomerName = "Honest",
                  CustomerPhone = "0278163469",
                  CustomerEmail = "honest@hotmail.com",
                  CustomerCity = "Stellenbosch",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 9,
                  //AgentID = 5,
                  CustomerName = "Litha",
                  CustomerPhone = "0278163899",
                  CustomerEmail = "litha@hotmail.com",
                  CustomerCity = "East London",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 10,
                  //AgentID = 5,
                  CustomerName = "Lethu",
                  CustomerPhone = "0278163693",
                  CustomerEmail = "lethu@hotmail.com",
                  CustomerCity = "Pretoria",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 11,
                  //AgentID = 4,
                  CustomerName = "Lisa",
                  CustomerPhone = "0278163158",
                  CustomerEmail = "lisa@hotmail.com",
                  CustomerCity = "Gqeberha",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 12,
                  //AgentID = 3,
                  CustomerName = "Jude",
                  CustomerPhone = "0278163145",
                  CustomerEmail = "jude@hotmail.com",
                  CustomerCity = "Bloemfontein",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 13,
                  //AgentID = 3,
                  CustomerName = "Clark",
                  CustomerPhone = "0278163568",
                  CustomerEmail = "clark@hotmail.com",
                  CustomerCity = "Johannesburg",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 14,
                  //AgentID = 1,
                  CustomerName = "Gabriel",
                  CustomerPhone = "0278163589",
                  CustomerEmail = "gabriel@hotmail.com",
                  CustomerCity = "Durban",
                  CustomerCountry = "South Africa"
              },
              new PotentialCustomer()
              {
                  CustomerID = 15,
                  //AgentID = 1,
                  CustomerName = "Paul",
                  CustomerPhone = "0278163961",
                  CustomerEmail = "paul@hotmail.com",
                  CustomerCity = "Polokwane",
                  CustomerCountry = "South Africa"
              });

            #endregion

            #region RepairShopSeed
            context.RepairShops.AddOrUpdate(r => r.ShopID,
                new RepairShop()
                {
                    ShopID =
                    1,
                    ShopName = "TJ Auto Repairs",
                    Location = "Cape Town",
                    ContactInfo = "tj@example.com"
                },
                new RepairShop()
                {
                    ShopID = 2,
                    ShopName = "Nur-Spec Auto",
                    Location = "Cape Town",
                    ContactInfo = "nurspec@example.com"
                },
                new RepairShop()
                {
                    ShopID = 3,
                    ShopName = "Bryan Autoworxz",
                    Location = "Cape Town",
                    ContactInfo = "bauto@example.com"
                });
            #endregion

            #region ServeyorSeed
            context.Surveyors.AddOrUpdate(
                new Surveyor()
                {
                    SurveyorId = 1,
                    Name = "Lani",
                    LicenseNumber = "KL1617",
                    ContactInfo = " 212 - 797 - 2569"
                },
                new Surveyor()
                {
                    SurveyorId = 2,
                    Name = "Charline",
                    LicenseNumber = "QR2223",
                    ContactInfo = "683 - 440 - 2684"
                },
                new Surveyor()
                {
                    SurveyorId = 3,
                    Name = "Sammy",
                    LicenseNumber = "ST2425",
                    ContactInfo = "902 - 426 - 0976"
                },
                new Surveyor()
                {
                    SurveyorId = 4,
                    Name = "Costa",
                    LicenseNumber = "QRT223",
                    ContactInfo = "121 - 151 - 5654"
                });
            #endregion

            #region ProcrssorSeed
            context.PaymentProcessors.AddOrUpdate(p => p.ProcessorID,
                new PaymentProcessor()
                {
                    ProcessorID = 1,
                    ProcessorName = "Edeline",
                    Email = "ebriant0@foxnews.com",
                    PaymentGateway = "WePay",
                    ProcessorAmount = 9345.54M
                },
                new PaymentProcessor()
                {
                    ProcessorID = 2,
                    ProcessorName = "Guthry",
                    Email = "greyes1@gravatar.com",
                    PaymentGateway = "Stripe",
                    ProcessorAmount = 8555.52M
                },
                new PaymentProcessor()
                {
                    ProcessorID = 3,
                    ProcessorName = "Netti",
                    Email = "ncovey2@reuters.com",
                    PaymentGateway = "Authorize.Net",
                    ProcessorAmount = 4563.57M
                },
                new PaymentProcessor()
                {
                    ProcessorID = 4,
                    ProcessorName = "Ariela",
                    Email = "afridlington3@arizona.edu",
                    PaymentGateway = "Venmo",
                    ProcessorAmount = 634.77M
                });
            #endregion

            #region PoliciesSeed
            context.Policies.AddOrUpdate(
            new Policy()
            {
                PolicyID = 1,
                PolicyName = "ABC123",
                //AgentID = 1,
                PolicyType = "Fully Comprehensive",
                Coverage = "Accidents, Theft & hijacking, vandalism, fire & explosion, damage to someone else's property, weather damage",
                Premium = 700,
                CreationDate = DateTime.Now.ToString(),
                ExpirationDate = DateTime.Now.AddYears(5).ToString()
            },
                new Policy()
                {
                    PolicyID = 2,
                    PolicyName = "KJU098",
                    //AgentID = 2,
                    PolicyType = "Third Party Fire & Theft",
                    Coverage = "Theft & hijacking, damage to someone else's property",
                    Premium = 400,
                    CreationDate = DateTime.Now.ToString(),
                    ExpirationDate = DateTime.Now.AddYears(5).ToString()
                },
                new Policy()
                {
                    PolicyID = 3,
                    PolicyName = "FTR345",
                    //AgentID = 3,
                    PolicyType = "Third Party Only",
                    Coverage = "Weather damages",
                    Premium = 250,
                    CreationDate = DateTime.Now.ToString(),
                    ExpirationDate = DateTime.Now.AddYears(5).ToString()
                });
            #endregion

            #region DriverSeed
            context.Drivers.AddOrUpdate(
            new Driver()
            {
                DriverID = 1,
                DriverFirstName = "Seth",
                DriverLastName = "Britten",
                LicenseNumber = "S89761ZA",
                ContactInfo = "seth@example.com",
                RiskProfile = "Minor injury",
                AccidentsReported = 3,
                PolicyID = 1
            },
            new Driver()
            {
                DriverID = 2,
                DriverFirstName = "Keith",
                DriverLastName = "Stark",
                LicenseNumber = "K88762ZA",
                ContactInfo = "keith@example.com",
                RiskProfile = "Moderate injury",
                AccidentsReported = 8,
                PolicyID = 2
            },
            new Driver()
            {
                DriverID = 3,
                DriverFirstName = "Blessing",
                DriverLastName = "Mthunzi",
                LicenseNumber = "B69763ZA",
                ContactInfo = "blessing@example.com",
                RiskProfile = "Minor injury",
                AccidentsReported = 5,
                PolicyID = 1
            },
            new Driver()
            {
                DriverID = 4,
                DriverFirstName = "Chico",
                DriverLastName = "Thwala",
                LicenseNumber = "C84564ZA",
                ContactInfo = "chico@example.com",
                RiskProfile = "Serious injury",
                AccidentsReported = 6,
                PolicyID = 2
            },
            new Driver()
            {
                DriverID = 5,
                DriverFirstName = "Lincoln",
                DriverLastName = "Abrahams",
                LicenseNumber = "L23465ZA",
                ContactInfo = "lincoln@example.com",
                RiskProfile = "Moderate injury",
                AccidentsReported = 7,
                PolicyID = 3
            });
            #endregion

            #region ClaimTypesSeed
            context.InsuranceClaimsTypes.AddOrUpdate(
                    new InsuranceClaimTypes()
                    {
                        ClaimTypeId = 1,
                        ClaimTypeName = "Collision",
                        ClaimTypeDescription = "Damage to your vehicle caused by a collision with another object."
                    },
                    new InsuranceClaimTypes()
                    {
                        ClaimTypeId = 2,
                        ClaimTypeName = "Comprehensive",
                        ClaimTypeDescription = "Damage to your vehicle caused by events other than collision (theft, vandalism, fire, weather events, etc.)."
                    },
                    new InsuranceClaimTypes()
                    {
                        ClaimTypeId = 3,
                        ClaimTypeName = "Liability",
                        ClaimTypeDescription = "Covers your legal responsibility for property damage or bodily injury caused to others while operating your vehicle."
                    },
                    new InsuranceClaimTypes()
                    {
                        ClaimTypeId = 4,
                        ClaimTypeName = "Medical Payments",
                        ClaimTypeDescription = "Covers medical expenses for you and your passengers, regardless of who is at fault in an accident."
                    },
                    new InsuranceClaimTypes()
                    {
                        ClaimTypeId = 5,
                        ClaimTypeName = "Uninsured/Underinsured Motorist (UM/UIM)",
                        ClaimTypeDescription = "Covers your injuries and vehicle damage if the driver who caused the accident is uninsured or underinsured."
                    }
            );

            #endregion

            #region ClaimSeed
            context.InsuranceClaims.AddOrUpdate(
                new InsuranceClaim()
                {
                    ClaimID = 1,
                    ClaimName = "DEF456",
                    AccidentDate = DateTime.Now.ToString(),
                    Status = "In progress",
                    DriverID = 1,
                    ClaimTypeId = 1,
                    SurveyorId = 1,
                    ShopID = 1
                },
                new InsuranceClaim()
                {
                    ClaimID = 2,
                    ClaimName = "DHY546",
                    AccidentDate = DateTime.Now.ToString(),
                    Status = "Success",
                    ClaimTypeId = 2,
                    DriverID = 2,
                    SurveyorId = 2,
                    ShopID = 2
                },
                new InsuranceClaim()
                {
                    ClaimID = 3,
                    ClaimName = "HYT768",
                    AccidentDate = DateTime.Now.ToString(),
                    Status = "Pending",
                    ClaimTypeId = 3,
                    DriverID = 1,
                    SurveyorId = 3,
                    ShopID = 3
                });
            #endregion

            #region VehicleSeed
            context.Vehicles.AddOrUpdate(
                new Vehicle()
                {
                    VehicleID = 1,
                    Make = "Honda",
                    Model = "Civic",
                    YearOfManufacture = "2018",
                    VIN_Number = "JHMEY1J20JK123456",
                    PolicyID = 1,
                },

                new Vehicle()
                {
                    VehicleID = 2,
                    Make = "Toyota",
                    Model = "Camry",
                    YearOfManufacture = "2022",
                    VIN_Number = "JTDBE2GK0LK654321",
                    PolicyID = 2,
                },
                new Vehicle()
                {
                    VehicleID = 3,
                    Make = "Ford",
                    Model = "F-150",
                    YearOfManufacture = "2020",
                    VIN_Number = "1FTPW8A8XLE100000",
                    PolicyID = 1,
                });
            #endregion

            #region TransactionSeed
            context.TransactionHistorys.AddOrUpdate(
                new TransactionHistory()
                {
                    TransactionID = 1,
                    TransactionName = "Premium Payment",
                    TransactionDate = DateTime.Now.ToString(),
                    TransactionType = "Purchase",
                    TransactionAmount = 500,
                    ClaimID = 1,
                    ProcessorID = 1
                },
                new TransactionHistory()
                {
                    TransactionID = 2,
                    TransactionName = "Monthly Payment",
                    TransactionDate = DateTime.Now.ToString(),
                    TransactionType = "Payment",
                    TransactionAmount = 250,
                    ClaimID = 2,
                    ProcessorID = 2
                },
                new TransactionHistory()
                {
                    TransactionID = 3,
                    TransactionName = "Premium Payment",
                    TransactionDate = DateTime.Now.ToString(),
                    TransactionType = "Purchase",
                    TransactionAmount = 500,
                    ClaimID = 1,
                    ProcessorID = 3
                },
                new TransactionHistory()
                {
                    TransactionID = 4,
                    TransactionName = "Transfer Of Fees",
                    TransactionDate = DateTime.Now.ToString(),
                    TransactionType = "Transfer",
                    TransactionAmount = 300,
                    ClaimID = 2,
                    ProcessorID = 4
                },
                new TransactionHistory()
                {
                    TransactionID = 5,
                    TransactionName = "Premium Payment",
                    TransactionDate = DateTime.Now.ToString(),
                    TransactionType = "Purchase",
                    TransactionAmount = 500,
                    ClaimID = 2,
                    ProcessorID = 4
                });
            #endregion

            context.PolicyApplications.AddOrUpdate(
                new PolicyApplication()
                {
                    ApplicationId = 1,
                    CustomerId = 1,
                    PolicyId = 1,
                    Status = "Pending",
                    SubmittedDate = DateTime.Now,
                    ReviewedDate = DateTime.Now,
                    ApprovedDate = DateTime.Now.AddDays(30),
                    RejectedReason = "Pending"
                });
        }
    }
}
