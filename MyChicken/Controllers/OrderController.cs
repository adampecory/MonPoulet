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
        OrderService os = new OrderService();
        ProductService ps = new ProductService();
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
                Trace("Order page showed",TraceLevel.Debug);
            }
            catch (Exception ex)
            {
                Trace("Order page show error",TraceLevel.Fatal, ex);
            }
            return View(model);
        }

        // Post: /Order/
        [HttpPost]
        public ActionResult Index(OrderViewModel orderVM, FormCollection form)
        {
            
            Trace("Begin new order saving", TraceLevel.Debug);
            //Construction of product list
            var tabIndex = form["products_Index"].Split(',');
            var lop = new List<OrderProduct>();
            foreach(var elt in tabIndex)
            {
                var strId = form[string.Format("products_{0}_Id",elt)];
                long Id = long.Parse(strId);
                var Qte = int.Parse(form[string.Format("products_{0}_Quantity", elt)]);
                var product = ps.GetbyId(Id);
                var op = new OrderProduct()
                {
                    //Product = product,
                    ProductID = product.Id,
                    Number = Qte,
                    QtyAmount = product.Amount * Qte
                };
                lop.Add(op);
            }

            var model = os.CreateOrder(lop, User.Identity.Name);
            //os.Add(model);

            Trace("End order saving", TraceLevel.Debug);
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
                var cdes = os.GetMyList(User.Identity.Name).Take(10);
                Trace("User last ten order showed", TraceLevel.Info);
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
             Trace("All users order showed", TraceLevel.Info);
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
                    os.MajStatut(id, comment, Statut.ANNULE);
                    break;
                case "valider" :
                    os.MajStatut(id, comment, Statut.VALIDE);
                    break;
                case "livrer":
                    os.MajStatut(id, comment, Statut.LIVRE);
                    break;
                case "supprimer" :
                    os.Delete(id);
                    break;
                default : 
                    break;
            }
            Trace("Order "+id+" statut updated : " + op , TraceLevel.Info);

            return RedirectToAction("List");
        }


    }
}
