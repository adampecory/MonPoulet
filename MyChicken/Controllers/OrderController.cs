using MyChicken.Models;
using MyChicken.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyChicken.Controllers
{
    //[Authorize]
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
            //Construction de l'objet OrderProduct
            var tabIndex = form["products.Index"].Split(',');
            var lop = new List<OrderProduct>();
            foreach(var elt in tabIndex)
            {
                var strId = form["products[" + elt + "].Id"];
                long Id = long.Parse(strId);
                var Qte = int.Parse(form["products["+ elt +"].Quantity"]);
                var product = db.Products.Single(x=>x.Id==Id);
                var op = new OrderProduct()
                {
                    Product = product,
                    ProductID = product.Id,
                    Number = Qte,
                    Total = product.Amount * Qte
                };
                lop.Add(op);
            }

            //Construction de l'objet Order
            var model = new Order() { 
                DeliveryDate= DateTime.Now,
                OrderDate=DateTime.Now, 
                Statut=Statut.EN_COURS, 
                //User=db.Users.Select(p=>p.UserName=User.Identity.Name).FirstOrDefault(), 
                TotalAmout=lop.Sum(p=>p.Total), 
                OrderProduct=lop                  
            };

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
