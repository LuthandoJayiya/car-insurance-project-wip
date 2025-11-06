using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOS
{
    public class Policy
    {
        [Key, Required, Column("policy_id")]
        public int PolicyID { get; set; }

        [Column("policy_type")]
        public string PolicyType { get; set; }

        [Column("policy_name")]
        public string PolicyName { get; set; }

        [Column("coverage")]
        public string Coverage { get; set; }

        [Column("premium")]
        public double Premium { get; set; }

        [Column("expiration_date")]
        public string ExpirationDate { get; set; }

        [Column("creation_date")]
        public string CreationDate { get; set; }

        //[ForeignKey(nameof(Agent)), Required, Column("agent_id")]
        //public int AgentID { get; set; }

        //navigation properties
        //public virtual InsuranceAgent Agent { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<PolicyApplication> PolicyApplications { get; set; }

       // public virtual ICollection<CustomerPolicy> CustomerPolicies { get; set; }

    }
}
