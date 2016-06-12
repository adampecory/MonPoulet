using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyChicken.Controllers
{
    [HandleError]  
    public class BaseController : Controller
    {

        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Trace(string msg, TraceLevel level, Exception ex=null)
        {
            var user = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonyme";
            var message = user + " - " + msg;
            switch(level)
            {
                case TraceLevel.Debug:
                    Log.Debug(message);
                    break;
                case TraceLevel.Info:
                    Log.Info(message);
                    break;
                case TraceLevel.Fatal:
                    Log.Fatal(message, ex);
                    break;
            }
        }

    }

    public enum TraceLevel
    {
        Debug,
        Info,
        Fatal,
    }
}