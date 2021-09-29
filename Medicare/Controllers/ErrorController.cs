using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HandleError]
        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult ServerError()
        {
            return View();
        }


        public ActionResult Index()
        {
            int i = 0;
            int x = 5 / i;
            return View();
        }
    }
}