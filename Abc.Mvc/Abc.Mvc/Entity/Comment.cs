using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Entity
{
    public class Comment
    {

        [Key]
        public int Id { get; set; }  // Primary Key

        public string UserId { get; set; }



        public string UserComment { get; set; }

        public int ProductId { get; set; }

        public Comment()
        {
        }



    }
}