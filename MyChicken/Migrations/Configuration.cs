namespace MyChicken.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyChicken.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyChicken.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MyChicken.Models.ApplicationDbContext";
        }

        protected override void Seed(MyChicken.Models.ApplicationDbContext context)
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

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            string roleName = "Admin";
            IdentityResult roleResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
                roleResult = RoleManager.Create(new IdentityRole("Superviseur"));
            }

            if (context.Users.Any(x=>x.UserName=="admin"))
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Tel = "0616885074",
                    Email = "adampecory@gmail.com"
                };
                var userResult = UserManager.Create(user, "Azerty1+");
                UserManager.AddToRole(context.Users.First(x=>x.UserName=="admin").Id,"Admin");
            }

        }
    }
}
