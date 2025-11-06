using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LLJ_CarInsuranceMS_RESTAPI.AuthModels
{
    /// <summary>
    /// Represents the database context for authentication-related entities.
    /// </summary>
    public class AuthenticationContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public AuthenticationContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Gets or sets the DbSet of application users.
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Overrides the default behavior when the model is created.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roleId_1 = Guid.NewGuid().ToString();
            var userId_1 = Guid.NewGuid().ToString();

            var roleId_2 = Guid.NewGuid().ToString();
            var userId_2 = Guid.NewGuid().ToString();

            var roleId_3 = Guid.NewGuid().ToString();
            var userId_3 = Guid.NewGuid().ToString();

            var roleId_4 = Guid.NewGuid().ToString();
            var userId_4 = Guid.NewGuid().ToString();

            var roleId_5 = Guid.NewGuid().ToString();
            var userId_5 = Guid.NewGuid().ToString();

            var roleId_6 = Guid.NewGuid().ToString();
            var userId_6 = Guid.NewGuid().ToString();

            #region "Seed Data"
            builder.Entity<IdentityRole>().HasData(
                new { Id = roleId_1, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new { Id = roleId_2, Name = "InsuranceAgent", NormalizedName = "INSURANCEAGENT" },
                new { Id = roleId_3, Name = "ClaimSurveyor", NormalizedName = "CLAIMSURVEYOR" },
                new { Id = roleId_4, Name = "Driver", NormalizedName = "DRIVER" },
                new { Id = roleId_5, Name = "PotentialCustomer", NormalizedName = "POTENTIALCUSTOMER" },
                new { Id = roleId_6, Name = "RepairShop", NormalizedName = "REPAIRSHOP" }
                );

            //create Administrator user
            var AdminUser = new ApplicationUser
            {
                Id = userId_1,
                Email = "Admin_1@cims.gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "025-897-6314",
                PhoneNumberConfirmed = true,
                FullName = "CarInsuranceMSAdmin",
                LicenseNumber = "CA1207",
                UserName = "HighLordCIMSAdmin",
                NormalizedUserName = "HIGHLORDCIMSADMIN"
            };

            //set user password
            PasswordHasher<ApplicationUser> adminph = new PasswordHasher<ApplicationUser>();
            AdminUser.PasswordHash = adminph.HashPassword(AdminUser, "@#Secret1234");

            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = userId_1
            });

            //create Insurance Agent user
            var InsuranceAgentUser = new ApplicationUser
            {
                Id = userId_2,
                Email = "Insurance_1Agent@cims.gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "026-548-9752",
                PhoneNumberConfirmed = true,
                FullName = "Neil",
                LicenseNumber = "GP1234",
                UserName = "AgentNeil",
                NormalizedUserName = "AGENTNEIL"
            };

            //set user password
            PasswordHasher<ApplicationUser> agentph = new PasswordHasher<ApplicationUser>();
            InsuranceAgentUser.PasswordHash = agentph.HashPassword(InsuranceAgentUser, "AgentNeil@#1234");

            //seed user
            builder.Entity<ApplicationUser>().HasData(InsuranceAgentUser);

            //set user role to Insurance Agent
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = userId_2
            });

            //create Claim Surveyor user
            var ClaimSurveyorUser = new ApplicationUser
            {
                Id = userId_3,
                Email = "Surveyor_1@cims.gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "212-797-2569",
                PhoneNumberConfirmed = true,
                FullName = "Lani",
                LicenseNumber = "KL1617",
                UserName = "SLani",
                NormalizedUserName = "SLANI"
            };

            //set user password
            PasswordHasher<ApplicationUser> surveyorph = new PasswordHasher<ApplicationUser>();
            ClaimSurveyorUser.PasswordHash = surveyorph.HashPassword(ClaimSurveyorUser, "LaniS4321@#!");

            //seed user
            builder.Entity<ApplicationUser>().HasData(ClaimSurveyorUser);

            //set user role to Claim Surveyor
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_3
            });

            //create Driver user
            var DriverUser = new ApplicationUser
            {
                Id = userId_4,
                Email = "Driver_1@cims.gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "086-765-4548",
                PhoneNumberConfirmed = true,
                FullName = "Seth",
                LicenseNumber = "SA8976",
                UserName = "DSeth",
                NormalizedUserName = "DSETH"
            };

            //set user password
            PasswordHasher<ApplicationUser> driverph = new PasswordHasher<ApplicationUser>();
            DriverUser.PasswordHash = driverph.HashPassword(DriverUser, "@123*#DSeth");

            //seed user
            builder.Entity<ApplicationUser>().HasData(DriverUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_4,
                UserId = userId_4
            });

            //create Potential customer user
            var CustomerUser = new ApplicationUser
            {
                Id = userId_5,
                Email = "john123@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "027-816-3587",
                PhoneNumberConfirmed = true,
                FullName = "John",
                LicenseNumber = "WK1647",
                UserName = "CJohn",
                NormalizedUserName = "CJOHN"
            };

            //set user password
            PasswordHasher<ApplicationUser> custph = new PasswordHasher<ApplicationUser>();
            CustomerUser.PasswordHash = custph.HashPassword(CustomerUser, "CJohn@#1987");

            //seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_5,
                UserId = userId_5
            });

            //create Repair Shop user
            var RepairShopUser = new ApplicationUser
            {
                Id = userId_6,
                Email = "tjauto@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "057-643-4654",
                PhoneNumberConfirmed = true,
                FullName = "TJ Auto Repairs",
                LicenseNumber = "TJ5655",
                UserName = "TJRepairs",
                NormalizedUserName = "TJREPAIRS"
            };

            //set user password
            PasswordHasher<ApplicationUser> shopph = new PasswordHasher<ApplicationUser>();
            RepairShopUser.PasswordHash = shopph.HashPassword(RepairShopUser, "TJauto@#1987");

            //seed user
            builder.Entity<ApplicationUser>().HasData(RepairShopUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_6,
                UserId = userId_6
            });

            #endregion
        }
    }
}
