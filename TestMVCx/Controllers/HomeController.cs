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
            var users = db.Users.Include(c => c.Relative);
            return View(users.ToList());
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
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
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}