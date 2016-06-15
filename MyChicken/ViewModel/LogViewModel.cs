using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChicken.ViewModel
{
    public class LogViewModel
    {
        public LogViewModel()
        {
            Content = new List<string>();
        }
        public DateTime Date { get; set; }
        public List<string> Content { get; set; }
    }
}