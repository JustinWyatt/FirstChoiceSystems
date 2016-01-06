namespace FirstChoiceSystems.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FirstChoiceSystems.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirstChoiceSystems.Models.ApplicationDbContext context)
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
            var userStore = new UserStore<BusinessUser>(context);
            var userManager = new UserManager<BusinessUser>(userStore);

            if (!(context.Users.Any(u => u.UserName == "justin@test.com")))
            {
                var userToInsert = new BusinessUser { UserName = "justin@test.com", PhoneNumber = "0797697898" };
                userManager.Create(userToInsert, "Password@123");
            }

            if (!(context.Users.Any(u => u.UserName == "daniel@test.com")))
            {
                var userToInsert = new BusinessUser { UserName = "daniel@test.com", PhoneNumber = "0797697898" };
                userManager.Create(userToInsert, "Password@123");
            }
        }
    }
}
