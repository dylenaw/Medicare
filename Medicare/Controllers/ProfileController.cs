using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medicare.Models;

namespace Medicare.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDatabase database = new ApplicationDatabase();

        // GET: Profile
        public ActionResult Index()
        {
            if (!SessionHandler.IsUserSignedIn(Session)) return RedirectToAction("","SignIn");
            if (SessionHandler.IsUserAdmin(Session)) return RedirectToAction("","Dashboard");
            int id = SessionHandler.GetUserId(Session);

            User user = database.Users.Include(u=>u.BloodType).Include(u => u.Specialization).SingleOrDefault(u=>u.Id==id);

            return View("View",user);
        }
        public ActionResult View(int? id)
        {
            if (!SessionHandler.IsUserSignedIn(Session)) return RedirectToAction("", "SignIn");

            if (id == null) id = SessionHandler.GetUserId(Session);

            User user = database.Users.Include(u => u.BloodType).Include(u => u.Specialization).SingleOrDefault(u => u.Id == id);

            return View(user);
        }



        // GET: Profile/Edit/5
        public ActionResult Edit()
        {
            int id = SessionHandler.GetUserId(Session);
  
            if (id == -1) return RedirectToAction("","SignIn");
            if (SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "Dashboard");

            User user = database.Users.Include(u => u.BloodType).Include(u => u.Specialization).SingleOrDefault(u => u.Id == id);

            ViewBag.BloodTypeId = new SelectList(database.BloodTypes, "Id", "Name", user.BloodTypeId);
            ViewBag.SpecializationId = new SelectList(database.Specializations, "Id", "Name", user.SpecializationId);
            return View(user);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save( User user)
        {
            int userId = SessionHandler.GetUserId(Session);
            if (userId == -1) return RedirectToAction("", "SignIn");
            if (SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "Dashboard");

            if (user.Id == userId)
            {
                User dbUser = database.Users.Single(u => u.Id == userId);
                dbUser.BirthDate = user.BirthDate;
                dbUser.Gender = user.Gender;
                dbUser.Address = user.Address;
                dbUser.BloodTypeId = user.BloodTypeId;
                dbUser.SpecializationId = user.SpecializationId;
                database.SaveChanges();
            }

            return RedirectToAction("");
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
