namespace Strek4Mayor.Migrations
{
    using Strek4Mayor.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Strek4Mayor.Models.Strek4MayorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Strek4Mayor.Models.Strek4MayorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            DateTime date1 = new DateTime(2015, 02, 4, 0, 0, 0);
            DateTime date2 = new DateTime(2012, 04, 12, 0, 0, 0);

            QandA example1 = new QandA
            {
                QandAId = 1,
                Title = "Political Stance",
                Body = "Where do you stand on homeless veterans in the Eugene area, and How do you plan on helping the situation?",
                Member = "Guest",
                Answer = "This is something that needs to be addressed",
                Date = date1
            };

            QandA example2 = new QandA
            {
                QandAId = 2,
                Title = "Big Phony",
                Member = "Not Guest",
                Body = "You are not helping or folling anyone!",
                Answer = "This is something that that needs to be hidden",
                Date = date2
            };

            context.QandAs.Add(example1);
            context.QandAs.Add(example2);
        }
    }
}
