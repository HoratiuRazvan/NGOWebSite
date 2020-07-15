using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta_V0.Models
{
    public class ShowUsersViewModels
    {
        public List<ApplicationUser> Users { get; set; }
        public List<String> Roles { get; set; }
    }
}