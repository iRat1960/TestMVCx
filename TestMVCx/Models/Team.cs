using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestMVCx.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Display(Name = "Команда")]
        public string Name { get; set; }
        [Display(Name = "Тренер")]
        public string Coach { get; set; }
        [Display(Name = "Игроки")]
        public ICollection<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }
    }
}