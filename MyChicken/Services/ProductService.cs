using MyChicken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChicken.Services
{
    public class ProductService
    {
        public Product getbyId(long Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Products.Single(x => x.Id == Id);
            }
            
        }
    }
}