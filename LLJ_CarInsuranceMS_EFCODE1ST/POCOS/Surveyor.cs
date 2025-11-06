using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCOS
{
    public class Surveyor
    {
        [Key, Column("Surveyor_id"), Required]
        public int SurveyorId { get; set; }
        public string Name { get; set; }

        [Column("License_number")]
        public string LicenseNumber { get; set; }

        [Column("Contact_info")]
        public string ContactInfo { get; set; }

        public virtual ICollection<InsuranceClaim> Claims { get; set; }
    }
}
