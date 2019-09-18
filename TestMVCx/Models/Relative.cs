using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TestMVCx.Content;

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

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChildID { get; set; }
        public int ParentID { get; set; }

        [ForeignKey("ParentID"), InverseProperty("Parent")]
        public virtual User ParentUser { get; set; }
        //[ForeignKey("ChildID"), InverseProperty("Children")]
        //public virtual User ChildUser { get; set; }

    }
}