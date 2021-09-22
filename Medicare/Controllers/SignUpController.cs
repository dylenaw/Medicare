using Medicare.Models;
using Medicare.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Controllers
{
    public class SignUpController : Controller
    {
        ApplicationDatabase database;

        public SignUpController()
        {
            database = new ApplicationDatabase();

        }

        
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp(SignUpViewModel model) {
            User user = database.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user != null)
            {
                return Content("User already exists, sign in instead");
            }
            else
            {
                user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    IsDoctor=model.IsDoctor
                    
                };
                database.Users.Add(user);
                database.SaveChanges();
                return Content("Sign up successful, continue to sign in");
            }

        }
    }
}