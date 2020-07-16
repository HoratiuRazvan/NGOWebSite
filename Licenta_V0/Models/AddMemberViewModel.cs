using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta_V0.Models
{
    public class AddMemberViewModel
    {
        public MemberModels Member { get; set; }
        public List<ApplicationUser> Users { get; set; }

    }
}