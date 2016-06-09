using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MyChicken.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

            public System.Data.Entity.DbSet<MyChicken.Models.Product> Products { get; set; }

            public System.Data.Entity.DbSet<MyChicken.Models.Order> Orders { get; set; }

            public System.Data.Entity.DbSet<MyChicken.Models.OrderProduct> OrderProducts { get; set; }

    }




}