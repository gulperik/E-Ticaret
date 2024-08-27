using Abc.Mvc.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Models
{
    public class DetailsComment
    {
        public IEnumerable<Comment> Comment{ get; set; }
        public Product Product { get; set; }

       
    }
}