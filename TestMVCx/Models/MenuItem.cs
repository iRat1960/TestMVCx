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
        }
    }
}