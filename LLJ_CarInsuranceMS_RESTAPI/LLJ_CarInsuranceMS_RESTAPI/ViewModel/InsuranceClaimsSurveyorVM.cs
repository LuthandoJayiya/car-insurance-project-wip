namespace LLJ_CarInsuranceMS_RESTAPI.ViewModel
{
    public class InsuranceClaimsSurveyorVM
    {
        public int ClaimId { get; set; }
        public string AccidentDate { get; set; } = null!;
        public string ClaimStatus { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimName { get; set; }

        public string? SurveyorName { get; set; }
        public string? LicenseNumber { get; set; }
        public string? ContactInfo { get; set; }
    }
}
