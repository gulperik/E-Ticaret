using Abc.Mvc.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Identity
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {

        public IdentityDataContext() : base("dataConnection")
        {
            
        }






    }
}