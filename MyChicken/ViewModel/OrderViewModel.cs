using MyChicken.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Display(Name="Date de retrait/livraison")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }
        public double Total { get; set; }
        public ApplicationUser User;
        public List<OrderProductViewModel> Products { get; set; }
    }

    public class OrderProductViewModel
    {
        public Product Product { get; set; }
        [Display(Name="Montant")]
        public double Amount { get; set; }
        [Display(Name = "Quantité")]
        public int Quantity { get; set; }
    }
}