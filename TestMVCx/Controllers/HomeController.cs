using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMVCx.Content;
using System.Data.Entity;
using System.IO;
using System.Text;
using TestMVCx.Models;
using System.Data.Entity.Validation;

namespace TestMVCx.Controllers
{
    public class HomeController : Controller
    {
        RelativeContext db = new RelativeContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Иерархическое меню.";
            List<MenuItem> menuItems = db.MenuItems.ToList();
            return View(menuItems);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = "Добавление нового игрока";
            SelectList positions = new SelectList(new List<string>()
                {
                    "Нападающий",
                    "Полузащитник",
                    "Защитник",
                    "Вратарь"
                });
            ViewBag.Positions = positions;
            SelectList teams = new SelectList(db.Teams, "Id", "Name");
            ViewBag.Teams = teams;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Player player)
        {
            db.Players.Add(player);
            db.SaveChanges();
            return RedirectToAction("FilterData");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Внесение изменений по игроку";
            Player player = db.Players.Find(id);
            if (player != null)
            {
                SelectList positions = new SelectList(new List<string>()
                {
                    "Нападающий",
                    "Полузащитник",
                    "Защитник",
                    "Вратарь"
                }, player.Position);
                ViewBag.Positions = positions;
                SelectList teams = new SelectList(db.Teams, "Id", "Name", player.TeamId);
                ViewBag.Teams = teams;
                return View(player);
            }
            return RedirectToAction("FilterData");
        }
        [HttpPost]
        public ActionResult Edit(Player player)
        {
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FilterData");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamName = db.Teams.Where(p => p.Id == player.TeamId).FirstOrDefault().Name;
            return View(player);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("FilterData");
        }
        public ActionResult FilterData(int? team, string position)
        {
            ViewBag.Message = "Каталог игроков.";
            IQueryable<Player> players = db.Players.Include(o => o.Team);
            if (team != null && team != 0)
            {
                players = players.Where(p => p.TeamId == team);
            }
            if (!String.IsNullOrEmpty(position) && !position.Equals("Все"))
            {
                players = players.Where(p => p.Position == position);
            }
            List<Team> teams = db.Teams.ToList();
            teams.Insert(0, new Team { Name = "Все", Id = 0 });

            PlayersListViewModel plvm = new PlayersListViewModel
            {
                Players = players.ToList(),
                Teams = new SelectList(teams, "Id", "Name"),
                Positions = new SelectList(new List<string>()
                {
                    "Все",
                    "Нападающий",
                    "Полузащитник",
                    "Защитник",
                    "Вратарь"
                })
            };
            return View(plvm);
        }

        public ActionResult LoadFile()
        {
            ViewBag.Message = "Выберите файл для загрузки:";
            var users = db.Users.Include(c => c.Relative);
            return View(users.ToList());
        }
        [HttpPost]
        public ActionResult Upload(string importButton, string clearButton, HttpPostedFileBase uploadFile)
        {
            if (importButton != null)
            {
                if (uploadFile != null)
                {
                    int ct = db.Users.Count();
                    if (ct == 0)
                    {
                        string path = uploadFile.FileName;
                        string s;
                        int i = 0;
                        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("Windows-1251"));
                        while (!sr.EndOfStream)
                        {
                            while ((s = sr.ReadLine()) != null)
                            {
                                if (i > 0)
                                {
                                    User u = new User(s.Split(';'));
                                    db.Users.Add(u);
                                    if (u.ParentID != 0)
                                    {
                                        db.Relatives.Add(u.Relative);
                                    }

                                    try
                                    {
                                        db.SaveChanges();
                                    }
                                    catch (DbEntityValidationException ex)
                                    {
                                        foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                                        {
                                            Response.Write("Object: " + validationError.Entry.Entity.ToString());
                                            Response.Write("");
                                            foreach (DbValidationError err in validationError.ValidationErrors)
                                            {
                                                Response.Write(err.ErrorMessage + "");
                                            }
                                        }
                                    }
                                }
                                i++;
                            }
                        }
                    }
                }
            }
            if (clearButton != null)
            {
                int ct = db.Users.Count();
                if (ct > 0)
                {
                    for (int i = 1; i <= ct; i++)
                    {
                        User u = db.Users
                            .Include(c => c.Relative)
                            .FirstOrDefault(c => c.ID == i);
                        if (u != null)
                        {
                            u.Relative = null;
                            db.Entry(u).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    var list = db.Relatives.ToList();
                    foreach (Relative rel in list)
                    {
                        db.Entry(rel).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                    var lu = db.Users.ToList();
                    foreach (User u in lu)
                    {
                        db.Entry(u).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания приложения.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Данные для связи.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}