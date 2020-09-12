using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamUp.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "What is displayed, eg. YouTube")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "What is reffering to, eg. https://www.youtube.com/")]
        public string URL { get; set; }
    }
}