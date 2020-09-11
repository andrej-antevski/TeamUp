using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamUp.Models
{
    public class Technology
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,10)]
        [Display(Name = "Rate your knowledge 1-10")]
        public float Grade { get; set; }
        public string Comment { get; set; }
    }
}