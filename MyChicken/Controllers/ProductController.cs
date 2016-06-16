using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyChicken.Models;
using MyChicken.Services;
using AutoMapper;
using MyChicken.ViewModel;

namespace MyChicken.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductController : BaseController
    {
        ProductService ps = new ProductService();

        // GET: /Product/
        public ActionResult Index()
        {
            var data = ps.GetAll();
            List<ProductViewModel> model = new List<ProductViewModel>();
            data.ForEach(x=>model.Add(Tools.Mapping<Product, ProductViewModel>.DefautMapping(x)));
            return View(model);
        }

        // GET: /Product/Details/5
        public ActionResult Details(long id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ps.GetbyId(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Image,Amount,IsArchived")] ProductViewModel product)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Product>());
            //var mapper = config.CreateMapper();
            //Product model = mapper.Map<Product>(product);
            var model = Tools.Mapping<ProductViewModel, Product>.DefautMapping(product);

            if (ModelState.IsValid)
            {
                ps.Add(model);
                Trace("New product created : " + product.Name, TraceLevel.Info);
                return RedirectToAction("Index");
            }

            return View(product);
        }



        // GET: /Product/Edit/5
        public ActionResult Edit(long id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ps.GetbyId(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Image,Amount,IsArchived")] Product product)
        {
            if (ModelState.IsValid)
            {
                ps.Update(product);
                Trace("Product updated : " + product.Name, TraceLevel.Info);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(long id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ps.GetbyId(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ps.Delete(id);
            Trace("Product deleted : " + id, TraceLevel.Info);
            return RedirectToAction("Index");
        }
    }
}
