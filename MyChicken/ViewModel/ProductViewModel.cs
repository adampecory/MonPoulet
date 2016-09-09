using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyChicken.ViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public string Image { get; set; }
        [Display(Name = "Montant")]
        public double Amount { get; set; }
        [Display(Name = "Indisponible")]
        public bool IsArchived { get; set; }
    }
}