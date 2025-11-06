using System.ComponentModel.DataAnnotations;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.AuthServices.Models
{
    public class CurrentUserVM
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
