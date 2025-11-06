using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCOS
{
    public class Vehicle
    {
        [Key, Required,Column("vehicle_id")]
        public int VehicleID { get; set; }

        [Column("vehicle_make")]
        public string Make{ get; set; }

        [Column("vehicle_model")]
        public string Model { get; set; }

        [Column("year_manufactured")]
        public string YearOfManufacture { get; set; }

        [Column("vin_number")]
        public string VIN_Number { get; set; }

        [Column("policy_id"),ForeignKey(nameof(Policy))]
        public int PolicyID { get; set; }

        public Policy Policy { get; set; }
    }
}
