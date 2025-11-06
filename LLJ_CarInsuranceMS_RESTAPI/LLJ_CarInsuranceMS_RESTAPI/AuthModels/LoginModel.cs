namespace LLJ_CarInsuranceMS_RESTAPI.AuthModels
{
    /// <summary>
    /// This class will be used by the login end point which maps the required  user account to the identity table.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string? Password { get; set; }

    }
}
