using Medicare.Models;
using Medicare.Models.ViewModels;
using Microsoft.Ajax.Utilities;
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

            IQueryable<User> query = database.Users.Include(d => d.Specialization).Where(d => d.IsDoctor && d.DoctorRegistration == null);

            if (search != null)
            {
                query = query.Where(d => d.Name.Contains(search) || d.Email.Contains(search));
            }

            List<User> doctors = query.ToList();
            List<Specialization> specializations = database.Specializations.ToList();

            return View(
                new AdminDoctorAuthViewModel
                {
                    Doctors = doctors,
                    Specializations = specializations,
                }
                );
        }




        public ActionResult Patients(string search)
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "dashboard");

            IQueryable<User> query = database.Users.Include(u => u.BloodType).Where(d => !d.IsDoctor && !d.IsAdmin);

            if (search != null)
            {
                query = query.Where(d => d.Name.Contains(search) || d.Email.Contains(search));
            }

            List<User> patients = query.ToList();

            return View(patients);
        }


        public ActionResult Appointments2(AppointmentsViewModel model)
        {
            return Content(string.Format("{0} {1} {2}", model.ShowFuture, model.ShowPresent, model.ShowPast));
        }

        public ActionResult Appointments(AppointmentsViewModel model)
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "dashboard");

            if (model == null ||(!model.ShowPast && !model.ShowPresent && !model.ShowFuture))  { model.ShowFuture = true;model.ShowPast = true;model.ShowPresent = true ;};

            IQueryable<Appointment> query = database.Appointments.Include(a => a.Doctor).Include(a => a.Patient);


            query = query.Where(
                a => (model.ShowPresent && a.DateTime == DateTime.Today) ||
                (model.ShowFuture && a.DateTime > DateTime.Today) ||
                    (model.ShowPast && a.DateTime < DateTime.Today)
                );


            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                query = query.Where(a => a.Doctor.Name.Contains(model.Search) || a.Doctor.Email.Contains(model.Search) || a.Patient.Name.Contains(model.Search) || a.Patient.Email.Contains(model.Search));
            }
            model.Appointments = query.ToList();

            return View(model);
        }


    }

}