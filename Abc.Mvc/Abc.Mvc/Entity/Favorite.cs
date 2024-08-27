using Abc.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Entity
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        public int productId { get; set; }
        public string UserId { get; set; }

        // Navigation Properties
        public virtual Product Products { get; set; }
        //public virtual Register Registers { get; set; }

        public Favorite()
        {
        }
    }
}
