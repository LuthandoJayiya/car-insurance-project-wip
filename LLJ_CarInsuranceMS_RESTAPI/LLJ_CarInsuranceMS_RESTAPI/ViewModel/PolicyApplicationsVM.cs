namespace LLJ_CarInsuranceMS_RESTAPI.ViewModel
{
    public class PolicyApplicationsVM
    {
        public int ApplicationId { get; set; }
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string? ApplicationStatus { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? RejectedReason { get; set; }
    }
}
