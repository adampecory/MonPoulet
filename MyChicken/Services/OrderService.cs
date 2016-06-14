using MyChicken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChicken.Services
{
    public class OrderService
    {

        public List<Order> GetAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Orders
                    //.Where(x=>x.Statut==Statut.IN_PROGRESS)
                    .ToList();

            }
        }

        public List<Order> GetAllByUser(string username)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
            return db.Orders
                .Where(x => x.Statut == Statut.IN_PROGRESS && x.User.UserName==username)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
            }
        }

        public List<Order> GetAll(string username)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Orders
                    .OrderByDescending(x => x.OrderDate >= DateTime.Today)
                    .ToList();
            }
        }

        public void MajStatut(long id, string comment, Statut statut)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == id);
                if (order != null)
                {
                    order.Comment = comment;
                    order.Statut = statut;
                    db.SaveChanges();
                }
            }
        }


        public void Delete(long id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == id);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            }
        }

        public void Add(Order order)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public Order CreateOrder(List<OrderProduct> lop,string Username="")
        {
            //Construction of Order
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var model = new Order()
                {
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Today.AddDays(1),
                    Statut = Statut.IN_PROGRESS,
                    User = db.Users.First(x => x.UserName == Username),
                    UserID = db.Users.First(x => x.UserName == Username).Id,
                    TotalAmount = lop.Sum(p => p.QtyAmount),
                    OrderProduct = lop
                };

                db.Orders.Add(model);
                db.SaveChanges();
                SendMail(model);
                return model;
            }
        }


        private void SendMail(Order order)
        {
            EmailService es = new EmailService();
            var recipients = System.Configuration.ConfigurationManager.AppSettings["recipients"].Split(';');
            var mail = new Email
            {
                Recipient = recipients.ToList(),
                Subject = "Nouvelle commande",
                Content = string.Format("Bonjour,{0} Une nouvelle commande a été effectuée.{0} Retouvez la dans votre espace dédié !{0} ", Environment.NewLine)
            };
            es.Sendmail(mail);
        }



    }
}