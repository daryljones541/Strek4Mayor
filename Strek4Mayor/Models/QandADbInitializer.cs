using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Strek4Mayor.Models
{
    public class QandADbInitializer : DropCreateDatabaseAlways<Strek4MayorContext>
    {
        protected override void Seed(Strek4MayorContext context)
        {
        }
    }
}