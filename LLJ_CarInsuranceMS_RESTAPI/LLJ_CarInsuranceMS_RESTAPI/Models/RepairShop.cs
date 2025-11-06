using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class RepairShop
    {
        public RepairShop()
        {
            InsuranceClaims = new HashSet<InsuranceClaim>();
        }

        public int ShopId { get; set; }
        public string? ShopName { get; set; }
        public string? Location { get; set; }
        public string? ContactInfo { get; set; }

        public virtual ICollection<InsuranceClaim> InsuranceClaims { get; set; }
    }
}
