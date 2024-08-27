using Abc.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Entity
{
    public class Sepet
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int ProductId { get; set; }
        //public virtual ProductModel ProductModel { get; set; }

        public  virtual Product Product { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}