namespace LLJ_CarInsuranceMS_RESTAPI.AuthModels
{
    /// <summary>
    /// This class will be used by the registration end point which maps the required  user account details to the identity tables
    /// </summary>
    public class ApplicationUserModel
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the license number of the user.
        /// </summary>
        public string? LicenseNumber { get; set; }
    }
}
