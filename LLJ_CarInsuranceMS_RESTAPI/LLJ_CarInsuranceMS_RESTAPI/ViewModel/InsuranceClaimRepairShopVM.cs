namespace LLJ_CarInsuranceMS_RESTAPI.ViewModel
{
    public class InsuranceClaimRepairShopVM
    {
        public int ClaimId { get; set; }
        public string AccidentDate { get; set; } = null!;
        public string ClaimStatus { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimName { get; set; }

        public string? ShopName { get; set; }
        public string? Location { get; set; }
        public string? ContactInfo { get; set; }
    }
}