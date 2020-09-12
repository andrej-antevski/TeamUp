using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamUp.Models
{
    public class Team
    {
        public Team()
        {
            Chat = new List<Message>();
            RolesNeeded = new List<RolesNeeded>();
            Members = new List<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Team Name")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }
        [Display(Name = "Team Admin")]
        public virtual ApplicationUser Admin { get; set; }
        public virtual ICollection<Message> Chat { get; set; }
        [Display(Name = "Role(s) Needed")]
        public virtual ICollection<RolesNeeded> RolesNeeded{ get; set; }
        [Display(Name = "Team Members")]
        public virtual ICollection<ApplicationUser> Members { get; set; }
    }
}