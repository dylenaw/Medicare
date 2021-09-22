using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Models.ViewModels
{
    public class AppointmentController : Controller
    {
        ApplicationDatabase database = new ApplicationDatabase();

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        // GET: Appointment
        public ActionResult Index()
        {
            
            return View();
        }      
        public ActionResult Create(int? id)
        {
            IQueryable<User> users = database.Users.Where(u => u.DoctorRegistration != null);
            if (id.HasValue)
            {
                users = users.Where(u => u.SpecializationId == id);
            }
            AppointmentCreateViewModel model = new AppointmentCreateViewModel
            {
                Doctors = users.ToList(),
                Specializations = database.Specializations.ToList()
            };
            return View(model);
        }

        public ActionResult Save(Appointment appointment)
        {
            database.Appointments.Add(appointment);
            database.SaveChanges();


            return RedirectToAction("","appointment");
        }

    }
}