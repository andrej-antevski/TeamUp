using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamUp.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateSent { get; set; }
        public virtual ApplicationUser From { get; set; }
        public virtual Team To { get; set; }
        public virtual RolesNeeded ForRole { get; set; }
        [Required]
        public string Description { get; set; }


    }
}