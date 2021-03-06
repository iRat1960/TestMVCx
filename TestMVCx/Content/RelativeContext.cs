﻿using System;
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
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        //db.Database.ExecuteSqlCommand("ALTER TABLE dbo.Players ADD CONSTRAINT Players_Teams FOREIGN KEY (TeamId) REFERENCES dbo.Teams (Id) ON DELETE SET NULL");

    }
}