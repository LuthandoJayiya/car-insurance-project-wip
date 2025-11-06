namespace LLJ_CarInsuranceMS_ASPNET.Areas.Admin.Models
{
    public class UserProfileVM
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }

    }
}
