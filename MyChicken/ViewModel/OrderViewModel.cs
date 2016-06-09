using MyChicken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChicken.ViewModel
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Products = new List<OrderProductViewModel>();
        }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double Total { get; set; }
        public ApplicationUser User;
        public List<OrderProductViewModel> Products { get; set; }
    }

    public class OrderProductViewModel
    {
        public Product Product { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
    }
}