using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestMVCx.Models;

namespace TestMVCx.Content
{
    public class RelativeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Relative> Relatives { get; set; }
    }
}