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
        public Relative() { }

        public Relative(int[] i)
        {
            ChildID = i[0];
            ParentID = i[1];
        }
        [Key]
        public int ChildID { get; set; }
        public int ParentID { get; set; }
        [Required]
        public User User { get; set; }
    }
}