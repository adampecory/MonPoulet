using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyChicken.Models
{
    public class Product
    {
        public long Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public string Image { get; set; }
        [Display(Name="Montant")]
        public double Amount { get; set; }
        [Display(Name="Masquer")]
        public bool IsArchived { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }

    public class OrderProduct
    {
        public long Id { get; set; }
        public long ProductID { get; set; }
        public virtual Product Product { get; set; }
        public long OrderID { get; set; }
        public virtual Order Order { get; set; }
        [Display(Name = "Nombre")]
        public int Number { get; set; }
        [Display(Name = "Montant Total")]
        public double QtyAmount { get; set; }
    }

    public class Order
    {
        public Order()
        {
            OrderProduct = new List<OrderProduct>();
        }
        public long Id { get; set; }
        [Display(Name = "Date commande")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Date livraison")]
        public DateTime DeliveryDate { get; set; }
        [Display(Name = "Montant total")]
        public double TotalAmount { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Statut Statut { get; set; }
        [Display(Name = "Commentaire")]
        public string Comment { get; set; }
    }

    public enum Statut
    {
        EN_COURS = 1,
        VALIDE,
        LIVRE,
        ANNULE
    }
}