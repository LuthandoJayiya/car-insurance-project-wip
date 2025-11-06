namespace LLJ_CarInsuranceMS_RESTAPI.ViewModel
{
    public class InsuranceClaimVM
    {
        public int ClaimId { get; set; }
        public string AccidentDate { get; set; } = null!;
        public string ClaimStatus { get; set; } = null!;
        public string? ClaimName { get; set; }
    }
}
