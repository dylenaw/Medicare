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
    public class PatientsController : Controller
    {
        ApplicationDatabase database = new ApplicationDatabase();

        // GET: Patients
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(int? id)
        {
            if (!id.HasValue)
            {
                id = 0;
            }

            User patient = database.Users.Include(u=>u.BloodType).Where(d => !d.IsDoctor && !d.IsAdmin && d.Id == id).SingleOrDefault();
            return View(patient);
        }

        public ActionResult AdminEdit(int? id)
        {
            if (!id.HasValue)
            {
                id = 0;
            }

            User user = database.Users.Include(d => d.BloodType).Where(d => !d.IsDoctor && d.Id == id).SingleOrDefault();
            List<BloodType> bloodTypes = database.BloodTypes.ToList();
            return View(
                new AdminEditViewModel
                {
                    User=user,
                    BloodTypes = bloodTypes
                }
                );
        }

        public ActionResult Save(User user)
        {
            User dbPatient = database.Users.Single(d => d.Id == user.Id);
            if (SessionHandler.IsUserAdmin(Session))
            {
                dbPatient.Address = user.Address;
                dbPatient.BloodTypeId = user.BloodTypeId;
                dbPatient.Gender = user.Gender;
                database.SaveChanges();


                return RedirectToAction("patients", "admin");

            }
            database.SaveChanges();
            return RedirectToAction("", "dashboard");
        }
        public ActionResult Delete(int? id)
        {
            if (SessionHandler.IsUserAdmin(Session))
            {
                User user = database.Users.SingleOrDefault(d => d.Id == id);
                if (user != null)
                {
                    database.Users.Remove(user);
                    database.SaveChanges();
                }
                return RedirectToAction("patients", "admin");


            }
            return RedirectToAction("", "dashboard");
        }




    }
}