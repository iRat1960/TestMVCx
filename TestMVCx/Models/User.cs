using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestMVCx.Models
{
    public class User
    {
        public int ID { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime DateBirth { get; set; }
        [Display(Name = "Пол")]
        public bool Gender { get; set; }
        [Display(Name = "Имя родителя")]
        public int ParentID { get; set; }

        public Relative Relative { get; set; }
    }
}