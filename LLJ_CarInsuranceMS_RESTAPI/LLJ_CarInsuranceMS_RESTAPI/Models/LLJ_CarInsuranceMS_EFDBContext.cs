using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class LLJ_CarInsuranceMS_EFDBContext : DbContext
    {
        public LLJ_CarInsuranceMS_EFDBContext()
        {
        }

        public LLJ_CarInsuranceMS_EFDBContext(DbContextOptions<LLJ_CarInsuranceMS_EFDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<InsuranceClaim> InsuranceClaims { get; set; } = null!;
        public virtual DbSet<InsuranceClaimType> InsuranceClaimTypes { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<PaymentProcessor> PaymentProcessors { get; set; } = null!;
        public virtual DbSet<Policy> Policies { get; set; } = null!;
        public virtual DbSet<PolicyApplication> PolicyApplications { get; set; } = null!;
        public virtual DbSet<PotentialCustomer> PotentialCustomers { get; set; } = null!;
        public virtual DbSet<RepairShop> RepairShops { get; set; } = null!;
        public virtual DbSet<Surveyor> Surveyors { get; set; } = null!;
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasIndex(e => e.PolicyId, "IX_policy_id");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.AccidentsReported).HasColumnName("accidents_reported");

                entity.Property(e => e.ContactInfo)
                    .HasMaxLength(255)
                    .HasColumnName("contact_info");

                entity.Property(e => e.DriverFirstName)
                    .HasMaxLength(255)
                    .HasColumnName("driver_first_name");

                entity.Property(e => e.DriverLastName).HasColumnName("driver_last_name");

                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(50)
                    .HasColumnName("license_number");

                entity.Property(e => e.PolicyId).HasColumnName("policy_id");

                entity.Property(e => e.RiskProfile)
                    .HasMaxLength(50)
                    .HasColumnName("risk_profile");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_dbo.Drivers_dbo.Policies_policy_id");
            });

            modelBuilder.Entity<InsuranceClaim>(entity =>
            {
                entity.HasKey(e => e.ClaimId)
                    .HasName("PK_dbo.InsuranceClaims");

                entity.HasIndex(e => e.ClaimTypeId, "IX_claim_type_id");

                entity.HasIndex(e => e.DriverId, "IX_driver_id");

                entity.HasIndex(e => e.ShopId, "IX_shop_id");

                entity.HasIndex(e => e.SurveyorId, "IX_surveyor_id");

                entity.Property(e => e.ClaimId).HasColumnName("claim_id");

                entity.Property(e => e.AccidentDate)
                    .HasMaxLength(255)
                    .HasColumnName("accident_date");

                entity.Property(e => e.ClaimName).HasColumnName("claim_name");

                entity.Property(e => e.ClaimStatus)
                    .HasMaxLength(128)
                    .HasColumnName("claim_status");

                entity.Property(e => e.ClaimTypeId).HasColumnName("claim_type_id");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.SurveyorId).HasColumnName("surveyor_id");

                entity.HasOne(d => d.ClaimType)
                    .WithMany(p => p.InsuranceClaims)
                    .HasForeignKey(d => d.ClaimTypeId)
                    .HasConstraintName("FK_dbo.InsuranceClaims_dbo.InsuranceClaimTypes_claim_type_id");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.InsuranceClaims)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_dbo.InsuranceClaims_dbo.Drivers_driver_id");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.InsuranceClaims)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_dbo.InsuranceClaims_dbo.RepairShops_shop_id");

                entity.HasOne(d => d.Surveyor)
                    .WithMany(p => p.InsuranceClaims)
                    .HasForeignKey(d => d.SurveyorId)
                    .HasConstraintName("FK_dbo.InsuranceClaims_dbo.Surveyors_surveyor_id");
            });

            modelBuilder.Entity<InsuranceClaimType>(entity =>
            {
                entity.HasKey(e => e.ClaimTypeId)
                    .HasName("PK_dbo.InsuranceClaimTypes");

                entity.Property(e => e.ClaimTypeId).HasColumnName("claim_type_id");

                entity.Property(e => e.ClaimTypeDescription).HasColumnName("claim_type_description");

                entity.Property(e => e.ClaimTypeName).HasColumnName("claim_type_name");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<PaymentProcessor>(entity =>
            {
                entity.HasKey(e => e.ProcessorId)
                    .HasName("PK_dbo.PaymentProcessors");

                entity.Property(e => e.ProcessorId).HasColumnName("processor_id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("payment_amount");

                entity.Property(e => e.PaymentGateway).HasColumnName("payment_gateway");

                entity.Property(e => e.ProcessorName).HasColumnName("processor_name");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.PolicyId).HasColumnName("policy_id");

                entity.Property(e => e.Coverage).HasColumnName("coverage");

                entity.Property(e => e.CreationDate).HasColumnName("creation_date");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.PolicyName).HasColumnName("policy_name");

                entity.Property(e => e.PolicyType).HasColumnName("policy_type");

                entity.Property(e => e.Premium).HasColumnName("premium");
            });

            modelBuilder.Entity<PolicyApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PK_dbo.PolicyApplications");

                entity.HasIndex(e => e.CustomerId, "IX_customer_id");

                entity.HasIndex(e => e.PolicyId, "IX_policy_id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.ApplicationStatus).HasColumnName("application_status");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_date");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.PolicyId).HasColumnName("policy_id");

                entity.Property(e => e.RejectedReason).HasColumnName("rejected_reason");

                entity.Property(e => e.ReviewedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reviewed_date");

                entity.Property(e => e.SubmittedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("submitted_date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PolicyApplications)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.PolicyApplications_dbo.PotentialCustomers_customer_id");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PolicyApplications)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_dbo.PolicyApplications_dbo.Policies_policy_id");
            });

            modelBuilder.Entity<PotentialCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK_dbo.PotentialCustomers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerCity)
                    .HasMaxLength(100)
                    .HasColumnName("customer_city");

                entity.Property(e => e.CustomerCountry)
                    .HasMaxLength(50)
                    .HasColumnName("customer_country");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(255)
                    .HasColumnName("customer_email");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(255)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(20)
                    .HasColumnName("customer_phone");

                entity.Property(e => e.IdentintyUsername).HasColumnName("identinty_username");
            });

            modelBuilder.Entity<RepairShop>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK_dbo.RepairShops");

                entity.Property(e => e.ShopId).HasColumnName("Shop_id");

                entity.Property(e => e.ContactInfo).HasColumnName("Contact_info");

                entity.Property(e => e.ShopName).HasColumnName("Shop_name");
            });

            modelBuilder.Entity<Surveyor>(entity =>
            {
                entity.Property(e => e.SurveyorId).HasColumnName("Surveyor_id");

                entity.Property(e => e.ContactInfo).HasColumnName("Contact_info");

                entity.Property(e => e.LicenseNumber).HasColumnName("License_number");
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK_dbo.TransactionHistories");

                entity.HasIndex(e => e.ClaimId, "IX_Claim_id");

                entity.HasIndex(e => e.ProcessorId, "IX_Processor_id");

                entity.Property(e => e.TransactionId).HasColumnName("Transaction_id");

                entity.Property(e => e.ClaimId).HasColumnName("Claim_id");

                entity.Property(e => e.ProcessorId).HasColumnName("Processor_id");

                entity.Property(e => e.TransactionAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Transaction_amount");

                entity.Property(e => e.TransactionDate).HasColumnName("Transaction_date");

                entity.Property(e => e.TransactionName).HasColumnName("Transaction_name");

                entity.Property(e => e.TransactionType).HasColumnName("Transaction_type");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.ClaimId)
                    .HasConstraintName("FK_dbo.TransactionHistories_dbo.InsuranceClaims_Claim_id");

                entity.HasOne(d => d.Processor)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.ProcessorId)
                    .HasConstraintName("FK_dbo.TransactionHistories_dbo.PaymentProcessors_Processor_id");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.PolicyId, "IX_policy_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.PolicyId).HasColumnName("policy_id");

                entity.Property(e => e.VehicleMake).HasColumnName("vehicle_make");

                entity.Property(e => e.VehicleModel).HasColumnName("vehicle_model");

                entity.Property(e => e.VinNumber).HasColumnName("vin_number");

                entity.Property(e => e.YearManufactured).HasColumnName("year_manufactured");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_dbo.Vehicles_dbo.Policies_policy_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
