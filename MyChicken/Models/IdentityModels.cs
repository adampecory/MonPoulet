﻿using FluentValidation.Attributes;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MyChicken.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    [Validator(typeof(ApplicationUserValidator))]
    public class ApplicationUser : IdentityUser
    {

        //public string Tel { get; set; }
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

    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Amount { get; set; }
        public bool IsArchived { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }

    public class OrderProduct
    {
        public long Id { get; set; }
        public long ProductID { get; set; }
        public virtual Product Product { get; set; }
        public long OrderID { get; set; }
        public virtual Order Order { get; set; }
        public int Number { get; set; }
        public double Total { get; set; }
    }

    public class Order
    {
        public Order()
        {
            OrderProduct = new List<OrderProduct>();
        }
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double TotalAmout { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public long UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Statut Statut { get; set; }
    }

    public enum Statut
    {
        EN_COURS = 1,
        LIVREE,
        ANNULEE
    }
}