﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Entity
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dataConnection")
        {
          
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Sepet> Sepet { get; set; }
        public System.Data.Entity.DbSet<Abc.Mvc.Models.ProductModel> ProductModels { get; set; }
    }
}