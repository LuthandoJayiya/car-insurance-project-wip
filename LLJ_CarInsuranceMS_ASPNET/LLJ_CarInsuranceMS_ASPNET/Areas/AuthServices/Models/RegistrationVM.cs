using System.ComponentModel.DataAnnotations;

namespace LLJ_CarInsuranceMS_ASPNET.Areas.AuthServices.Models
{
    public class RegistrationVM
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? UserName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Email { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Password { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? FullName { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? PhoneNumber { get; set; }

        [StringLength(6, MinimumLength = 6)]
        [Required]
        public string? LicenseNumber { get; set; }


        public string? Role { get; set; }

    }
}