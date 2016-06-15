using MyChicken.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MyChicken.Services
{
    public class LogService
    {
        public LogViewModel GetLog(DateTime date)
        {





            var log = new LogViewModel();

            var filename = string.Format("~/Logs/{0:yyyyMMdd}.Chicken.log", DateTime.Today);


            var file = new FileStream(HttpContext.Current.Server.MapPath(filename), FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Permet d'éviter un accès concurentiel

            using (StreamReader sr = new StreamReader(file, Encoding.GetEncoding("iso-8859-1"))) //Encodage permettant d'afficher les accents
            {

                log.Date = date;

                while (sr.Peek() >= 0)
                {

                    log.Content.Add(sr.ReadLine()) ;

                }

            }

            return log;

        }
    }
}