using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace TeamUp.Models
{
    public class Team
    {
        public Team()
        {
            Chat = new List<Message>();
            RolesNeeded = new List<RolesNeeded>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public virtual ApplicationUser Admin { get; set; }
        public virtual ICollection<Message> Chat { get; set; }
        public virtual ICollection<RolesNeeded> RolesNeeded{ get; set; }
    }
}