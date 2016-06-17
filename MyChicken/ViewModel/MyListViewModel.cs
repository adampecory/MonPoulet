using MyChicken.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyChicken.ViewModel
{
    public class MyListViewModel
    {
        public long OrderId { get; set; }
        [DisplayName("Client")]
        public string Username { get; set; }
        [DisplayName("Date commande")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Date livraison")]
        public DateTime DeliveryDate { get; set; }
        public double Total { get; set; }
        public Statut Statut { get; set; }
        public List<string> Details { get; set; }
    }
}