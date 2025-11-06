using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POCOS
{
    public class PotentialCustomer
    {
        [Key, Required, Column("customer_id")]
        public int CustomerID { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("customer_phone")]
        public string CustomerPhone { get; set; }

        [Column("customer_email")]
        public string CustomerEmail { get; set; }

        [Column("customer_city")]
        public string CustomerCity { get; set; }

        [Column("customer_country")]
        public string CustomerCountry { get; set; }

        [Column("identinty_username")]
        public string IdentityUsername
        {
            get { return CustomerName; }
            set { _ = CustomerName; }
        }


        //[ForeignKey(nameof(InsuranceAgent)),Column("agent_id")]
        //public int AgentID { get; set; }

        //public InsuranceAgent InsuranceAgent { get; set; }

        public virtual ICollection<PolicyApplication> PolicyApplications { get; set; }
        //public virtual ICollection<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
