using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCOS
{
    public class InsuranceClaimTypes
    {
        [Key,Required,Column("claim_type_id")]
        public int ClaimTypeId { get; set; }

        [Column("claim_type_name")]
        public string ClaimTypeName { get; set; }

        [Column("claim_type_description")]
        public string ClaimTypeDescription { get; set; }

        public virtual ICollection<InsuranceClaim> InsuranceClaims { get; set; }
    }
}
