using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class Strek4MayorContext : IdentityDbContext<User>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Strek4MayorContext() : base("name=Strek4MayorContext")
        {
        }

        public System.Data.Entity.DbSet<Strek4Mayor.Models.QandA> QandAs { get; set; }
        public System.Data.Entity.DbSet<Strek4Mayor.Models.Donation> Donations { get; set; }
        public System.Data.Entity.DbSet<Strek4Mayor.Models.Employment> Employments { get; set; }
        public System.Data.Entity.DbSet<Strek4Mayor.Models.Donor> Donors { get; set; }
        public System.Data.Entity.DbSet<Strek4Mayor.Models.Person> Persons { get; set; }
        public System.Data.Entity.DbSet<Strek4Mayor.Models.Event> Events { get; set; }
        public System.Data.Entity.DbSet<Strek4Mayor.Models.NewsArticle> NewsArticles { get; set; }
        public System.Data.Entity.DbSet<Strek4Mayor.Models.volunteer> volunteers { get; set; }
    }
}
