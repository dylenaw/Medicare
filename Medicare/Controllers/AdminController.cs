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
    public class AdminController : Controller
    {
        ApplicationDatabase database = new ApplicationDatabase();

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Doctors(string search)
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "dashboard");

            IQueryable<User> query = database.Users.Include(d => d.Specialization).Where(d => d.IsDoctor);

            if (search != null)
            {
                query = query.Where(d => d.Name.Contains(search) || d.Email.Contains(search));
            }

            List<User> doctors = query.ToList();

            return View(doctors);
        }
        public ActionResult DoctorAuth(string search)
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "dashboard");

            IQueryable<User> query = database.Users.Include(d => d.Specialization).Where(d => d.IsDoctor && d.DoctorRegistration==null);

            if (search != null)
            {
                query = query.Where(d => d.Name.Contains(search) || d.Email.Contains(search));
            }

            List<User> doctors = query.ToList();
            List<Specialization> specializations = database.Specializations.ToList();

            return View(
                new AdminDoctorAuthViewModel { 
                    Doctors=doctors,
                    Specializations=specializations,
                }
                );
        }




        public ActionResult Patients(string search)
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "dashboard");

            IQueryable<User> query = database.Users.Include(u=>u.BloodType).Where(d => !d.IsDoctor && !d.IsAdmin);

            if (search != null)
            {
                query = query.Where(d => d.Name.Contains(search) || d.Email.Contains(search));
            }

            List<User> patients = query.ToList();

            return View(patients);
        }
    }
}