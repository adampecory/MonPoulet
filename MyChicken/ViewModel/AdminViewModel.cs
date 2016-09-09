using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyChicken.ViewModel
{
    public class PowerUserViewModel
    {
        [Display(Name = "Nom utilisateur")]
        public string Username { get; set; }
        [Display(Name = "Role")]
        public string Rolename { get; set; }
        public bool Suppression { get; set; }

    }
}