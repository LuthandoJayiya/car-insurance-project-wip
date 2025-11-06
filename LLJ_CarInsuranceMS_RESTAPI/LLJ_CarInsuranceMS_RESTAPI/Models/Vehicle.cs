using System;
using System.Collections.Generic;

namespace LLJ_CarInsuranceMS_RESTAPI.Models
{
    public partial class Vehicle
    {
        public int VehicleId { get; set; }
        public string? VehicleMake { get; set; }
        public string? VehicleModel { get; set; }
        public string? YearManufactured { get; set; }
        public string? VinNumber { get; set; }
        public int PolicyId { get; set; }

        public virtual Policy Policy { get; set; } = null!;
    }
}
