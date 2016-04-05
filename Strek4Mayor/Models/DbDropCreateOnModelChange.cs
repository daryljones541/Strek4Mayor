using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class DbDropCreateOnModelChange : System.Data.Entity.DropCreateDatabaseAlways<Strek4MayorContext>
    {
        protected UserManager<User> userManager = new UserManager<User>(
                new UserStore<User>(new Strek4MayorContext()));

        protected override void Seed(Strek4MayorContext context)
        {
            base.Seed(context);          
        }
    }
}