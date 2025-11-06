namespace LLJ_CarInsuranceMS_RESTAPI.ViewModel
{
    public class PolicyVM
    {
        public int PolicyId { get; set; }
        public string? PolicyType { get; set; }
        public string? PolicyName { get; set; }
        public string? Coverage { get; set; }
        public double Premium { get; set; }
        public string? ExpirationDate { get; set; }
        public string? CreationDate { get; set; }
    }
}
