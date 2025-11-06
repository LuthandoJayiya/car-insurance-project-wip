namespace LLJ_CarInsuranceMS_RESTAPI.AuthModels
{
    /// <summary>
    /// This class is used by the Middleware to manage JWT.  Actual values will be stored in the appsettings.json
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the JWT secret.
        /// </summary>
        public string? JWT_Secret { get; set; }

        /// <summary>
        /// Gets or sets the name of the JWT cookie.
        /// </summary>
        public string? JwtCookieName { get; set; }

        /// <summary>
        /// Gets or sets the signing key for JWT.
        /// </summary>
        public string? SigningKey { get; set; }

        /// <summary>
        /// Gets or sets the expiry time for JWT in minutes.
        /// </summary>
        public string? ExpiryInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the URL for the site using JWT.
        /// </summary>
        public string? JWT_Site_URL { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        public string? Client_URL { get; set; }
    }
}
