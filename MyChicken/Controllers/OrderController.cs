using MyChicken.Models;
using MyChicken.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MyChicken.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Order/
        public ActionResult Index()
        {
            var model = new OrderViewModel();
            List<Product> products = db.Products.ToList();
            foreach ( var p in products)
            {
                model.Products.Add(new OrderProductViewModel { Product = p, Amount=0, Quantity=0 });
            }
            return View(model);
        }

        // Post: /Order/
        [HttpPost]
        public ActionResult Index(OrderViewModel orderVM, FormCollection form)
        {
            //Construction of product list
            var tabIndex = form["products_Index"].Split(',');
            var lop = new List<OrderProduct>();
            foreach(var elt in tabIndex)
            {
                var strId = form[string.Format("products_{0}_Id",elt)];
                long Id = long.Parse(strId);
                var Qte = int.Parse(form[string.Format("products_{0}_Quantity", elt)]);
                var product = db.Products.Single(x=>x.Id==Id);
                var op = new OrderProduct()
                {
                    Product = product,
                    ProductID = product.Id,
                    Number = Qte,
                    QtyAmount = product.Amount * Qte
                };
                lop.Add(op);
            }

            //Construction of Order
            var model = new Order() { 
                OrderDate=DateTime.Now, 
                DeliveryDate = DateTime.Today.AddDays(1),
                Statut=Statut.IN_PROGRESS, 
                //User=db.Users.Select(p=>p.UserName=User.Identity.Name).FirstOrDefault(), 
                TotalAmount=lop.Sum(p=>p.QtyAmount), 
                OrderProduct=lop                  
            };

            //Get Users
            if (User.Identity.IsAuthenticated)
            {
                
                var user = db.Users.First(x => x.UserName == User.Identity.Name);
                model.User = user;
                model.UserID = User.Identity.GetUserId();
            }

            db.Orders.Add(model);
            db.SaveChanges();

            return View("Summary", model);
        }

        public ActionResult Summary(Order order)
        {
            return View(order);
        }

        //
        // GET: /Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Order/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
