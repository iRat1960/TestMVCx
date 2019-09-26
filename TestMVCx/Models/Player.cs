using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestMVCx.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Display(Name = "Имя игрока")]
        public string Name { get; set; }
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        [Display(Name = "Позиция на поле")]
        public string Position { get; set; }
        
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}