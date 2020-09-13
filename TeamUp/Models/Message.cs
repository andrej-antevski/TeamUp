using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamUp.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser From{ get; set; }
        [Required]
        public string Text { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeSent { get; set; }

    }
}