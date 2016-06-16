using MyChicken.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyChicken.Services
{
    public class ProductService
    {
        public Product GetbyId(long id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Products.Find(id);
            }            
        }

        public Product GetbyName(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Products.FirstOrDefault(x=>x.Name==name);
            }
        }

        public List<Product> GetAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Products.ToList();
            }    
        }

        public void Add(Product p)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Products.Add(p);
                db.SaveChanges();
            }    
        }

        public void Update(Product product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();                
            }
        }

    }
}