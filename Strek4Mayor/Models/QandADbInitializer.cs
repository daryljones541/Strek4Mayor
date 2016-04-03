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
            DateTime date1 = new DateTime(2015, 02, 4, 0, 0, 0);
            DateTime date2 = new DateTime(2012, 04, 12, 0, 0, 0);

            QandA example1 = new QandA
            {
                QandAId = 1,
                Title = "Political Stance",
                Body = "Where do you stand on homeless veterans in the Eugene area, and How do you plan on helping the situation?",
                Answer = "This is something that needs to be addressed",
                Date = date1
            };

             QandA example2 = new QandA
            {
                QandAId = 2,
                Title = "Big Phony",
                Body = "You are not helping or folling anyone!",
                Answer = "This is something that that needs to be hidden",
                Date = date2
            };

            context.QandAs.Add(example1);
            context.QandAs.Add(example2);
            base.Seed(context);
        }
    }
}