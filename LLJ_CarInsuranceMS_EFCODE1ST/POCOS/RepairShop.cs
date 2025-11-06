using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOS
{
    public class RepairShop
    {
        [Key, Required, Column("Shop_id")]
        public int ShopID { get; set; }

        [Column("Shop_name")]
        public string ShopName { get; set; }
        public string Location { get; set; }

        [Column("Contact_info")]
        public string ContactInfo { get; set; }

        public virtual ICollection<InsuranceClaim> Claims { get; set; }
    }
}
