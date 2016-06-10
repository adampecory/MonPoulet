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
                //.Where(x=>x.Statut==Statut.IN_PROGRESS)
                .ToList();
        }

        public List<Order> GetAllByUser(string username)
        {
            return db.Orders
                .Where(x => x.Statut == Statut.IN_PROGRESS && x.User.UserName==username)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }

        public List<Order> GetAll(string username)
        {
            return db.Orders
                .OrderByDescending(x => x.OrderDate>=DateTime.Today)
                .ToList();
        }

        public void MajStatut(long id, string comment, Statut statut)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);
            if (order != null)
            {
                order.Comment = comment;
                order.Statut = statut;
                db.SaveChanges();
            }
        }


        public void Supprimer(long id)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);
            if (order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }



    }
}