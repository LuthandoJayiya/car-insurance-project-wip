//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace POCOS
//{
//    public class InsuranceAgent
//    {
//        [Key, Required, Column("agent_id")]
//        public int AgentID { get; set; }

//        [Column("agent_first_name")]
//        public string AgentFirstName { get; set; }

//        [Column("agent_last_name")]
//        public string AgentLastName { get; set; }

//        [Column("agent_contact_number")]
//        public string AgentContactNumber { get; set; }

//        [Column("agent_email")]
//        public string AgentEmail { get; set; }

//        [Column("location")]
//        public string Location { get; set; }

//        [Column("license_number")]
//        public string LicenseNumber { get; set; }

//        [Column("commission_rate")]
//        public string CommissionRate { get; set; }

//        [Column("customer_feedback")]
//        public string CustomerFeedback { get; set; }

//        public virtual ICollection<Policy> Policies { get; set; }
//        //public virtual ICollection<PotentialCustomer> PotentialCustomer { get; set; }
//    }
//}
