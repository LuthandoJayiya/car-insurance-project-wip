using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POCOS
{
    public class Driver
    {
        [Key, Column("driver_id")]
        public int DriverID { get; set; }

        [Column("accidents_reported")]
        public int AccidentsReported { get; set; }

        [Column("driver_first_name")]
        public string DriverFirstName { get; set; }

        [Column("driver_last_name")]
        public string DriverLastName { get; set; }

        [Column("license_number")]
        public string LicenseNumber { get; set; }

        [Column("contact_info")]
        public string ContactInfo { get; set; }

        [Column("risk_profile")]
        public string RiskProfile { get; set; }

        [ForeignKey(nameof(Policy)), Column("policy_id")]
        public int PolicyID { get; set; }

        public virtual Policy Policy { get; set; }

        public virtual ICollection<InsuranceClaim> Claims { get; set; }


    }
}
