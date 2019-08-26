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
                if (ct > 0)
                {
                    string q = "delete from [Relatives]";
                    db.Database.ExecuteSqlCommand(q, new object[] { "" });
                    q = "delete from [Users]";
                    db.Database.ExecuteSqlCommand(q, new object[] { "" });
                    q = " DBCC CHECKIDENT('Users',RESEED,0)";
                    db.Database.ExecuteSqlCommand(q, new object[] { "" });
                }
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
                            string[] str = s.Split(';');
                            int id = Convert.ToInt32(str[0]);
                            string name = str[1].ToString().Trim();
                            DateTime dt = DateTime.Parse(str[2].ToString());
                            bool fl = str[3].ToString().ToUpper().Contains('М') | str[3].ToString().ToUpper().Contains('M') & !str[3].ToString().ToUpper().Contains('F');
                            string sid = str[4].ToString();
                            int pid = sid.Length == 0 ? 0 : Convert.ToInt32(sid);
                            Relative r;
                            User u = new User { ID = id, Name = name, DateBirth = dt, Gender = fl, ParentID = pid, Relative = null };
                            db.Users.Add(u);
                            if (pid != 0)
                            {
                                r = new Relative { ChildID = pid != 0 ? id : 0, ParentID = pid, User = u };
                                db.Relatives.Add(r);
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