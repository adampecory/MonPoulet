using MyChicken.Models;
using MyChicken.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyChicken.Services;
using log4net;

namespace MyChicken.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Order/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new OrderViewModel { DeliveryDate = DateTime.Today.AddDays(1) };
            try
            {
                List<Product> products = db.Products.ToList();
                foreach ( var p in products)
                {
                    model.Products.Add(new OrderProductViewModel { Product = p, Amount=0, Quantity=0 });
                }
                Trace("Affichage page Commande OK",TraceLevel.Debug);
            }
            catch (Exception ex)
            {
                Trace("Erreur affichage page Commande",TraceLevel.Fatal, ex);
            }
            return View(model);
        }

        // Post: /Order/
        [HttpPost]
        public ActionResult Index(OrderViewModel orderVM, FormCollection form)
        {
            Trace("Debut enregistrement commande", TraceLevel.Debug);
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

            Trace("Fin Enregistrement Commande", TraceLevel.Debug);
            return View("Summary", model);
        }

        public ActionResult Summary(Order order)
        {
            return View(order);
        }
 
        // GET: /Order/MyList
        public ActionResult MyList()
        {
            try
            {
                OrderService os = new OrderService();
                var cdes = os.GetAllByUser(User.Identity.Name).Take(5);
                return View(cdes);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            
            
            }
        }
        
        [Authorize(Roles="Admin,superviseur")]
        public ActionResult List(string username, string deliverydate)
        {
             OrderService os = new OrderService();
             var cdes = os.GetAll();
             if (!string.IsNullOrEmpty(username))
                 cdes = cdes.Where(x => x.User.UserName == username).ToList();
             if (!string.IsNullOrEmpty(deliverydate))
                 cdes = cdes.Where(x => x.DeliveryDate.ToShortDateString() == deliverydate).ToList();
             return View(cdes);            
        }

        // GET: /Order/Delete/5
        [Authorize(Roles = "Admin,superviseur")]
        public ActionResult Operation(long id, string op)
        {
            var model = db.Orders.FirstOrDefault(x => x.Id == id);
            ViewData["op"] = op;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,superviseur")]
        public ActionResult Operation(long id, string op, string comment)
        {
            OrderService os = new OrderService();
            var model = db.Orders.FirstOrDefault(x => x.Id == id);
            var a = ViewData["op"];
            switch(op)
            { 
                case "annuler" :
                    os.MajStatut(id, comment, Statut.CANCELLED);
                    break;
                case "valider" :
                    os.MajStatut(id, comment, Statut.VALIDATED);
                    break;
                case "livrer":
                    os.MajStatut(id, comment, Statut.DELIVERED);
                    break;
                case "supprimer" :
                    os.Supprimer(id);
                    break;
                default : 
                    break;
            }
            return RedirectToAction("List");
        }


    }
}
