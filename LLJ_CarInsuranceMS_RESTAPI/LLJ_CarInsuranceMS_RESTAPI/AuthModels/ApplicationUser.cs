using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LLJ_CarInsuranceMS_RESTAPI.AuthModels
{
    /// <summary>
    /// Represents an application user, derived from IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the license number of the user.
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string? LicenseNumber { get; set; }
    }

}
