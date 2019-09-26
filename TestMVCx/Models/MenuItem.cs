using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestMVCx.Content;

namespace TestMVCx.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Url { get; set; }
        public int? Order { get; set; }
        public int? ParentId { get; set; }
        public MenuItem Parent { get; set; }

        public ICollection<MenuItem> Children { get; set; }

        public MenuItem()
        {
            Children = new List<MenuItem>();
        }
    }

    public class AppDbInitializer : DropCreateDatabaseAlways<RelativeContext>
    {
        protected override void Seed(RelativeContext db)
        {
            var menuItems = new List<MenuItem>
            {
                new MenuItem{Id=1, Header = "Главная", Url = "/Home/Index", Order = 1},
                new MenuItem{Id=2, Header = "Задания", Url = "#", Order = 2},
                new MenuItem{Id=3, Header = "О программе", Url = "/Home/About", Order = 3},
                new MenuItem{Id=4, Header = "Контакты", Url = "/Home/Contact", Order = 4},
                new MenuItem{Id=5, Header = "Меню второго уровня 1", Url = "#", Order = 1, ParentId = 2},
                new MenuItem{Id=6, Header = "Загрузка данных", Url = "/Home/LoadFile", Order = 2, ParentId = 2},
                new MenuItem{Id=7, Header = "Фильтрация данных", Url = "#", Order = 3, ParentId = 2},
                new MenuItem{Id=8, Header = "Меню третьго уровня 1", Url = "#",  Order = 1, ParentId = 5},
                new MenuItem{Id=9, Header = "Меню третьго уровня 2", Url = "#", Order = 2, ParentId = 5},
                new MenuItem{Id=10, Header = "Меню третьго уровня 3", Url = "#", Order = 3, ParentId = 5}
            };
            db.MenuItems.AddRange(menuItems);
            db.SaveChanges();

            var team = new List<Team>
            {
                new Team{Id=1, Name = "Реал", Coach = "Анчелотти"},
                new Team{Id=2, Name = "Барселона", Coach = "Мартино"},
                new Team{Id=3, Name = "Бавария", Coach = "Гуардиола"},
                new Team{Id=4, Name = "Боруссия", Coach = "Клопп"}
            };
            db.Teams.AddRange(team);
            db.SaveChanges();

            var player = new List<Player>
            {
                new Player{Id=1, Name = "Месси", Age = 26, Position = "Нападающий", TeamId = 2},
                new Player{Id=2, Name = "Роналду", Age = 29, Position = "Нападающий", TeamId = 1},
                new Player{Id=3, Name = "Бейл", Age = 24, Position = "Полузащитник", TeamId = 1},
                new Player{Id=4, Name = "Неймар", Age = 22, Position = "Нападающий", TeamId = 2},
                new Player{Id=5, Name = "Иньеста", Age = 29, Position = "Полузащитник", TeamId = 2},
                new Player{Id=6, Name = "Рибери", Age = 30, Position = "Полузащитник", TeamId = 3}
            };
            db.Players.AddRange(player);
            db.SaveChanges();

            
        }
    }
}