using Medicare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Medicare.Controllers
{
    public class ChartsController : Controller
    {
        ApplicationDatabase database = new ApplicationDatabase();

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }


        public ActionResult Index()
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("","Dashboard");
            return View();
        }

        // GET: Charts
        public ActionResult PatientBloodTypes()
        {


            List<string> bloodTypesInDb = (from bloods in database.BloodTypes select bloods.Name).ToList();
            
            List<string> bloodTypes =new List<string>();
            List<int> bloodCount = new List<int>();

            foreach (string bloodType in bloodTypesInDb)
            {
                int count= (from patient in database.Users.Include(u => u.BloodType) where patient.BloodType.Name == bloodType select patient.Id).Count();

                if (count != 0)
                {
                    bloodTypes.Add(bloodType);                bloodCount.Add(count);

                }

            }
            Chart chart = new Chart(width: 600, height: 400,theme: ChartTheme.Blue)
        .AddTitle("Patients Blood Types")
        .AddSeries(
        name: "Patient",
        xValue: bloodTypes,
        yValues: bloodCount,chartType:"Pie");


            return File(chart.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult DoctorCategories()
        {


            List<string> categories = (from c in database.Specializations select c.Name).ToList();
            List<int> categoryCounts = new List<int>();

            foreach (string category in categories)
            {
                categoryCounts.Add((from doctor in database.Users.Include(u => u.Specialization) where doctor.Specialization.Name == category select doctor.Id).Count());
            }
            Chart chart = new Chart(width: 600, height: 400,theme:ChartTheme.Blue)
        .AddTitle("Doctor categories")
        .AddSeries(
        name: "Patient",
        xValue: categories,
        yValues: categoryCounts);


            return File(chart.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult UserTypes()
        {

            int patientCount = database.Users.Where(u => !u.IsDoctor && !u.IsAdmin).Count();
            int authDoctorCount = database.Users.Where(u => u.IsDoctor && u.DoctorRegistration != null).Count();
            int unauthDoctorCount = database.Users.Where(u => u.IsDoctor && u.DoctorRegistration == null).Count();


            Chart chart = new Chart(width: 600, height: 400,theme:ChartTheme.Blue)
        .AddTitle("User types")
        .AddSeries(
        name: "User types",
        xValue: new string[] { "Patients", "Authorized doctors", "Unauthorized doctors" },
        yValues: new int[] { patientCount, authDoctorCount, unauthDoctorCount });


            return File(chart.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Appointments()
        {

            int pastAppointments = database.Appointments.Where(a=>a.DateTime<DateTime.Today).Count();
            int todayAppointments = database.Appointments.Where(a=>a.DateTime==DateTime.Today).Count();
            int futureAppointments = database.Appointments.Where(a=>a.DateTime>DateTime.Today).Count();
    

            Chart chart = new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
        .AddTitle("Appointments")
        .AddSeries(
        name: "Appointments",
        xValue: new string[] { "Past", "Today", "Future" },
        yValues: new int[] { pastAppointments, todayAppointments, futureAppointments });


            return File(chart.ToWebImage().GetBytes(), "image/jpeg");
        }
    }
}