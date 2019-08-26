using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestMVCx.Models
{
    public class Relative
    {
        [Key]
        public int ChildID { get; set; }
        public int ParentID { get; set; }
        [Required]
        public User User { get; set; }
    }
}