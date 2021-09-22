using Medicare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Controllers
{
    public class DashboardController : Controller
    {
        ApplicationDatabase database = new ApplicationDatabase();

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            if (!SessionHandler.IsUserSignedIn(Session)) {return RedirectToAction("index", "signIn"); }

            return View();
        }
       
    }


}