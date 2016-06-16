using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChicken.ViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Amount { get; set; }
        public bool IsArchived { get; set; }
    }
}