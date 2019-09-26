using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestMVCx.Content;

namespace TestMVCx.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime DateBirth { get; set; }
        [Display(Name = "Пол")]
        public bool Gender { get; set; }
        [Display(Name = "Имя родителя")]
        public int ParentID { get; set; }
        public Relative Relative { get; set; }

        public virtual ICollection<Relative> Parent { get; set; }

        public User() { }

        public User(string[] str)
        {
            ID = Convert.ToInt32(str[0]);
            Name = str[1].ToString().Trim();
            DateBirth = DateTime.Parse(str[2].ToString());
            Gender = str[3].ToString().ToUpper().Contains('М') | str[3].ToString().ToUpper().Contains('M') & !str[3].ToString().ToUpper().Contains('F');
            string sid = str[4].ToString();
            ParentID = sid.Length == 0 ? 0 : Convert.ToInt32(sid);
            if (ParentID == 0)
                Relative = null;
            else
                Relative = new Relative(new int[] { ID, ParentID });
        }
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<RelativeContext>
    {

        protected override void Seed(RelativeContext context)
        {
            int ct = context.Users.Count();
            if (ct > 0)
            {
                string q = "delete from [Relatives]";
                context.Database.ExecuteSqlCommand(q, new object[] { "" });
                q = "delete from [Users]";
                context.Database.ExecuteSqlCommand(q, new object[] { "" });
                q = " DBCC CHECKIDENT('Users',RESEED,0)";
                context.Database.ExecuteSqlCommand(q, new object[] { "" });
            }
            //base.Seed(context);
        }

    }
}