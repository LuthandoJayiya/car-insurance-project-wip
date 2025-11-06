using Microsoft.AspNetCore.Identity;

namespace LLJ_CarInsuranceMS_RESTAPI.AuthModels
{
    public class UserAccountViewModel
    {
        public UserAccountViewModel()
        {
        }

        public UserAccountViewModel(IdentityUser aus, List<string> userRoles)
        {
            UserName = aus.UserName;
            Email = aus.Email;
            RolesHeld = userRoles;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> RolesHeld { get; set; }

    }

}
