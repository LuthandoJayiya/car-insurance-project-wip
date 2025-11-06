namespace LLJ_CarInsuranceMS_RESTAPI.ViewModel
{
    public class InsuranceClaimsDriverVM
    {
        public int ClaimId { get; set; }
        public string AccidentDate { get; set; } = null!;
        public string ClaimStatus { get; set; } = null!;
        //public string? ClaimType { get; set; }
        public string? ClaimName { get; set; }

        public int AccidentsReported { get; set; }
        public string DriverName { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? RiskProfile { get; set; }
    }
}
