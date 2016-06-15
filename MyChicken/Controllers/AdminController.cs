using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyChicken.Models;
using MyChicken.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyChicken.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : BaseController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateAdminUser()
        {

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            string roleName = "Admin";
            IdentityResult roleResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
                roleResult = RoleManager.Create(new IdentityRole("Superviseur"));
            }

            if (!context.Users.Any(x => x.UserName == "admin"))
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Tel = "0616885074",
                    Email = "adampecory@gmail.com"
                };
                var userResult = UserManager.Create(user, "Azerty1+");
                UserManager.AddToRole(context.Users.First(x => x.UserName == "admin").Id, "Admin");
            }

            if (!context.Products.Any())
            {
                context.Products.Add(new Product { Name = "Poulet 2Kg", Amount = 4000 });
                context.Products.Add(new Product { Name = "Poulet 2Kg", Amount = 5000 });
                context.SaveChanges();
            }
            Trace("User Admin Created", TraceLevel.Info);
            return Content("Creation Admin OK");
        }

        public ActionResult ManageSupervisor()
        {
            var model = new PowerUserViewModel { Rolename="Superviseur"};
            return View(model);
        }

        [HttpPost]
        public ActionResult ManageSupervisor(PowerUserViewModel userPower)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var user = context.Users.FirstOrDefault(x => x.UserName == userPower.Username);
            try 
	        {	        
		        if(user!=null)
                {
                    if (!userPower.Suppression)
                    {
                        UserManager.AddToRole(user.Id,userPower.Rolename);
                        Trace(userPower.Rolename + " role added to" + user.UserName, TraceLevel.Info);
                    }
                    else
                    {
                        UserManager.RemoveFromRole(user.Id, userPower.Rolename);
                        Trace(userPower.Rolename + " role removed from" + user.UserName, TraceLevel.Info);
                    }
                    return RedirectToAction("ManageSupervisor");
                }
                else
                {
                    ModelState.AddModelError("", "Cet utilisateur n'existe pas");
                }
	        }
	        catch (Exception ex)
	        {
		
                ModelState.AddModelError("",ex.Message);
            }
            return View();	        
        }

    }
}