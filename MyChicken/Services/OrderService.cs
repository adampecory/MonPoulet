using MyChicken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChicken.Services
{
    public class OrderService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Order> GetAll()
        {
            return db.Orders
                .Where(x=>x.Statut==Statut.IN_PROGRESS)
                .ToList();
        }

        public List<Order> GetAllByUser(string username)
        {
            return db.Orders
                .Where(x => x.Statut == Statut.IN_PROGRESS && x.User.UserName==username)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }
    }
}