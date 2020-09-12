using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamUp.Models
{
    public class AddTeamRole
    {
        public int TeamId { get; set; }
        public RolesNeeded RolesNeeded { get; set; }
    }
}