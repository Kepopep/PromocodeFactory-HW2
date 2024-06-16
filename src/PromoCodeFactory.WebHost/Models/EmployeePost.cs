using PromoCodeFactory.Core.Domain.Administration;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models
{
    public class EmployeePost
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<RoleItemPost> Roles { get; set; }
    }
}
