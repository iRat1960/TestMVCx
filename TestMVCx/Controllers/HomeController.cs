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