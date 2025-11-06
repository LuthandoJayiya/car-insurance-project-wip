using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOS
{
    public class CustomerPolicy
    {
        [Key, Required, Column("customer_id")]
        public int CustomerID { get; set; }

        [Key, Required, Column("policy_id")]
        public int PolicyID { get; set; }
    }
}
