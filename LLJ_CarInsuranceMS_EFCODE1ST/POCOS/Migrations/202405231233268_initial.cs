namespace POCOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PotentialCustomers",
                c => new
                    {
                        customer_id = c.Int(nullable: false, identity: true),
                        customer_name = c.String(nullable: false, maxLength: 255),
                        customer_phone = c.String(maxLength: 20),
                        customer_email = c.String(nullable: false, maxLength: 255),
                        customer_city = c.String(maxLength: 100),
                        customer_country = c.String(maxLength: 50),
                        identinty_username = c.String(),
                    })
                .PrimaryKey(t => t.customer_id);
            
            CreateTable(
                "dbo.PolicyApplications",
                c => new
                    {
                        application_id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        policy_id = c.Int(nullable: false),
                        application_status = c.String(),
                        submitted_date = c.DateTime(),
                        reviewed_date = c.DateTime(),
                        approved_date = c.DateTime(),
                        rejected_reason = c.String(),
                    })
                .PrimaryKey(t => t.application_id)
                .ForeignKey("dbo.PotentialCustomers", t => t.customer_id, cascadeDelete: true)
                .ForeignKey("dbo.Policies", t => t.policy_id, cascadeDelete: true)
                .Index(t => t.customer_id)
                .Index(t => t.policy_id);
            
            CreateTable(
                "dbo.Policies",
                c => new
                    {
                        policy_id = c.Int(nullable: false, identity: true),
                        policy_type = c.String(),
                        policy_name = c.String(),
                        coverage = c.String(),
                        premium = c.Double(nullable: false),
                        expiration_date = c.String(),
                        creation_date = c.String(),
                    })
                .PrimaryKey(t => t.policy_id);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        driver_id = c.Int(nullable: false, identity: true),
                        accidents_reported = c.Int(nullable: false),
                        driver_first_name = c.String(nullable: false, maxLength: 255),
                        driver_last_name = c.String(),
                        license_number = c.String(nullable: false, maxLength: 50),
                        contact_info = c.String(maxLength: 255),
                        risk_profile = c.String(maxLength: 50),
                        policy_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.driver_id)
                .ForeignKey("dbo.Policies", t => t.policy_id, cascadeDelete: true)
                .Index(t => t.policy_id);
            
            CreateTable(
                "dbo.InsuranceClaims",
                c => new
                    {
                        claim_id = c.Int(nullable: false, identity: true),
                        accident_date = c.String(nullable: false, maxLength: 255),
                        claim_status = c.String(nullable: false, maxLength: 128),
                        claim_name = c.String(),
                        driver_id = c.Int(nullable: false),
                        claim_type_id = c.Int(nullable: false),
                        surveyor_id = c.Int(nullable: false),
                        shop_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.claim_id)
                .ForeignKey("dbo.InsuranceClaimTypes", t => t.claim_type_id, cascadeDelete: true)
                .ForeignKey("dbo.Drivers", t => t.driver_id, cascadeDelete: true)
                .ForeignKey("dbo.RepairShops", t => t.shop_id, cascadeDelete: true)
                .ForeignKey("dbo.Surveyors", t => t.surveyor_id, cascadeDelete: true)
                .Index(t => t.driver_id)
                .Index(t => t.claim_type_id)
                .Index(t => t.surveyor_id)
                .Index(t => t.shop_id);
            
            CreateTable(
                "dbo.InsuranceClaimTypes",
                c => new
                    {
                        claim_type_id = c.Int(nullable: false, identity: true),
                        claim_type_name = c.String(),
                        claim_type_description = c.String(),
                    })
                .PrimaryKey(t => t.claim_type_id);
            
            CreateTable(
                "dbo.RepairShops",
                c => new
                    {
                        Shop_id = c.Int(nullable: false, identity: true),
                        Shop_name = c.String(),
                        Location = c.String(),
                        Contact_info = c.String(),
                    })
                .PrimaryKey(t => t.Shop_id);
            
            CreateTable(
                "dbo.Surveyors",
                c => new
                    {
                        Surveyor_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        License_number = c.String(),
                        Contact_info = c.String(),
                    })
                .PrimaryKey(t => t.Surveyor_id);
            
            CreateTable(
                "dbo.TransactionHistories",
                c => new
                    {
                        Transaction_id = c.Int(nullable: false, identity: true),
                        Transaction_name = c.String(),
                        Transaction_date = c.String(),
                        Transaction_type = c.String(),
                        Transaction_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Claim_id = c.Int(nullable: false),
                        Processor_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Transaction_id)
                .ForeignKey("dbo.InsuranceClaims", t => t.Claim_id, cascadeDelete: true)
                .ForeignKey("dbo.PaymentProcessors", t => t.Processor_id, cascadeDelete: true)
                .Index(t => t.Claim_id)
                .Index(t => t.Processor_id);
            
            CreateTable(
                "dbo.PaymentProcessors",
                c => new
                    {
                        processor_id = c.Int(nullable: false, identity: true),
                        processor_name = c.String(),
                        email = c.String(),
                        payment_gateway = c.String(),
                        payment_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.processor_id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        vehicle_id = c.Int(nullable: false, identity: true),
                        vehicle_make = c.String(),
                        vehicle_model = c.String(),
                        year_manufactured = c.String(),
                        vin_number = c.String(),
                        policy_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.vehicle_id)
                .ForeignKey("dbo.Policies", t => t.policy_id, cascadeDelete: true)
                .Index(t => t.policy_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicyApplications", "policy_id", "dbo.Policies");
            DropForeignKey("dbo.Vehicles", "policy_id", "dbo.Policies");
            DropForeignKey("dbo.Drivers", "policy_id", "dbo.Policies");
            DropForeignKey("dbo.TransactionHistories", "Processor_id", "dbo.PaymentProcessors");
            DropForeignKey("dbo.TransactionHistories", "Claim_id", "dbo.InsuranceClaims");
            DropForeignKey("dbo.InsuranceClaims", "surveyor_id", "dbo.Surveyors");
            DropForeignKey("dbo.InsuranceClaims", "shop_id", "dbo.RepairShops");
            DropForeignKey("dbo.InsuranceClaims", "driver_id", "dbo.Drivers");
            DropForeignKey("dbo.InsuranceClaims", "claim_type_id", "dbo.InsuranceClaimTypes");
            DropForeignKey("dbo.PolicyApplications", "customer_id", "dbo.PotentialCustomers");
            DropIndex("dbo.Vehicles", new[] { "policy_id" });
            DropIndex("dbo.TransactionHistories", new[] { "Processor_id" });
            DropIndex("dbo.TransactionHistories", new[] { "Claim_id" });
            DropIndex("dbo.InsuranceClaims", new[] { "shop_id" });
            DropIndex("dbo.InsuranceClaims", new[] { "surveyor_id" });
            DropIndex("dbo.InsuranceClaims", new[] { "claim_type_id" });
            DropIndex("dbo.InsuranceClaims", new[] { "driver_id" });
            DropIndex("dbo.Drivers", new[] { "policy_id" });
            DropIndex("dbo.PolicyApplications", new[] { "policy_id" });
            DropIndex("dbo.PolicyApplications", new[] { "customer_id" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.PaymentProcessors");
            DropTable("dbo.TransactionHistories");
            DropTable("dbo.Surveyors");
            DropTable("dbo.RepairShops");
            DropTable("dbo.InsuranceClaimTypes");
            DropTable("dbo.InsuranceClaims");
            DropTable("dbo.Drivers");
            DropTable("dbo.Policies");
            DropTable("dbo.PolicyApplications");
            DropTable("dbo.PotentialCustomers");
        }
    }
}
