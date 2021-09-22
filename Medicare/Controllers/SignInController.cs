using Medicare.Models;
using Medicare.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Controllers
{
    public class SignInController : Controller
    {
        ApplicationDatabase database;

        public SignInController()
        {
            database = new ApplicationDatabase();
        }

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        // GET: Login
        public ActionResult Index()
        {
            if (SessionHandler.IsUserSignedIn(Session)) return RedirectToAction("", "dashboard");

            return View();
        }

        public ActionResult SignIn(SignInViewModel model)
        {


            if (model == null)
            {
                return RedirectToAction("","SignIn");
            }
            else
            {
                User user = database.Users.SingleOrDefault(u => u.Email == model.Email);
                if (user != null && model.Password == user.Password)
                {
                    SessionHandler.SetUser(Session, user);


                    return RedirectToAction("", "dashboard");
                    //Show invalid username or password
                    //Sign up to continue
                }
                else
                {
                    return RedirectToAction("","SignIn");

                }
            }
        }

        public ActionResult SignOut()
        {
            SessionHandler.Abandon(Session);
            return RedirectToAction("Index", "Home");
        }
    }


}