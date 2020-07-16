using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta_V0.Models
{
    public class ShowTeamViewModel
    {
        public TeamModels Team { get; set; }
        public List<MemberModels> Members { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}