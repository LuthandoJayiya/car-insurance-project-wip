using System.Data.Common;
using System.Data.Entity;

namespace POCOS
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }
        public Model1(DbConnection connection) : base(connection, true) { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<InsuranceClaim>().Property(e => e.AccidentDate).IsRequired().HasMaxLength(255);

            modelBuilder.Entity<InsuranceClaim>().Property(e => e.Status).IsRequired().HasMaxLength(128);

            modelBuilder.Entity<PotentialCustomer>().Property(e => e.CustomerName).IsRequired().HasMaxLength(255);

            modelBuilder.Entity<PotentialCustomer>().Property(e => e.CustomerPhone).HasMaxLength(20);

            modelBuilder.Entity<PotentialCustomer>().Property(e => e.CustomerEmail)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<PotentialCustomer>().Property(e => e.CustomerCity)
                .HasMaxLength(100);

            modelBuilder.Entity<PotentialCustomer>().Property(e => e.CustomerCountry)
                .HasMaxLength(50);

            modelBuilder.Entity<Driver>().Property(e => e.AccidentsReported);

            modelBuilder.Entity<Driver>().Property(e => e.DriverFirstName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Driver>().Property(e => e.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Driver>().Property(e => e.ContactInfo)
                .HasMaxLength(255);

            modelBuilder.Entity<Driver>().Property(e => e.RiskProfile)
                .HasMaxLength(50);

            //modelBuilder.Entity<CustomerPolicy>().HasKey(k => new { k.PolicyID, k.CustomerID });
        }

        public virtual DbSet<PotentialCustomer> Customers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<InsuranceClaim> InsuranceClaims { get; set; }
        //public virtual DbSet<InsuranceAgent> InsuranceAgents { get; set; }
        public virtual DbSet<PaymentProcessor> PaymentProcessors { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<RepairShop> RepairShops { get; set; }
        public virtual DbSet<Surveyor> Surveyors { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistorys { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<InsuranceClaimTypes> InsuranceClaimsTypes { get; set; }
        public virtual DbSet<PolicyApplication> PolicyApplications { get; set; }
        //public virtual DbSet<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
