using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamUp.Models
{
    public class Resume
    {
        public Resume()
        {
            Technologies = new List<Technology>();
            Links = new List<Link>();
        }
        [Key]
        public int Id { get; set; }
        public virtual ICollection<Link> Links{ get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string Experience { get; set; }
        public virtual ICollection<Technology> Technologies { get; set; }
        [Required]
        [Display(Name = "Hobbies And Interests")]
        public string HobbiesInterests { get; set; }

    }
}