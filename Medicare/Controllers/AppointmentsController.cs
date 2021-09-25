using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Models.ViewModels
{
    public class AppointmentsController : Controller
    {
        readonly ApplicationDatabase database = new ApplicationDatabase();

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        // GET: Appointment

        public ActionResult Index(AppointmentsViewModel model)
        {
            if (!SessionHandler.IsUserSignedIn(Session)) return RedirectToAction("", "signIn");

            if (model == null || (!model.ShowPast && !model.ShowPresent && !model.ShowFuture)) { model.ShowFuture = true; model.ShowPast = false; model.ShowPresent = true; };

            int userId = SessionHandler.GetUserId(Session);

            IQueryable<Appointment> query = database.Appointments.Include(a => a.Doctor).Include(a => a.Doctor.Specialization).Where(a => a.PatientId == userId);


            query = query.Where(
                a => (model.ShowPresent && a.DateTime == DateTime.Today) ||
                (model.ShowFuture && a.DateTime > DateTime.Today) ||
                    (model.ShowPast && a.DateTime < DateTime.Today)
                );

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                query = query.Where(a => a.Reason.Contains(model.Search) || a.Doctor.Name.Contains(model.Search) || a.Doctor.Email.Contains(model.Search) || a.Doctor.Specialization.Name.Contains(model.Search));
            }
            model.Appointments = query.ToList();

            return View(model);

        }
        public ActionResult Mine(AppointmentsViewModel model)
        {
            if (!SessionHandler.IsUserDoctor(Session)) return RedirectToAction("", "dashboard");

            if (model == null || (!model.ShowPast && !model.ShowPresent && !model.ShowFuture)) { model.ShowFuture = true; model.ShowPast = true; model.ShowPresent = true; };

            int userId = SessionHandler.GetUserId(Session);

            IQueryable<Appointment> query = database.Appointments.Include(a => a.Patient).Where(a => a.DoctorId == userId);


            query = query.Where(
                a => (model.ShowPresent && a.DateTime == DateTime.Today) ||
                (model.ShowFuture && a.DateTime > DateTime.Today) ||
                    (model.ShowPast && a.DateTime < DateTime.Today)
                );

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                query = query.Where(a => a.Reason.Contains(model.Search) || a.Patient.Name.Contains(model.Search) || a.Patient.Email.Contains(model.Search));
            }
            model.Appointments = query.ToList();

            return View(model);

        }
        public ActionResult Create(int? id)
        {
            if (!SessionHandler.IsUserSignedIn(Session)) return RedirectToAction("", "signIn");
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
            appointment.PatientId = SessionHandler.GetUserId(Session);
            database.Appointments.Add(appointment);
            database.SaveChanges();


            return RedirectToAction("", "appointments");
        }


        public ActionResult Delete(int? id)
        {
            if (SessionHandler.IsUserAdmin(Session))
            {
                Appointment appointment = database.Appointments.SingleOrDefault(d => d.Id == id);
                if (appointment != null)
                {
                    database.Appointments.Remove(appointment);
                    database.SaveChanges();
                }
                return RedirectToAction("appointments", "admin");


            }
            return RedirectToAction("", "dashboard");
        }



        public ActionResult View(int? id)
        {
            int userId = SessionHandler.GetUserId(Session);
            if (userId == -1) return RedirectToAction("","SignIn");
            if (id.HasValue)
            {
                Appointment appointment = database.Appointments.Include(a => a.Doctor).Include(a => a.Patient).SingleOrDefault(a => a.Id == id.Value);
                if (SessionHandler.IsUserAdmin(Session) || userId == appointment.DoctorId || userId == appointment.PatientId)
                {
                    return View(appointment);
                }
            }
            if (SessionHandler.IsUserDoctor(Session))
            {
                return RedirectToAction("Mine", "Appointments");
            }
            else
            {
                return RedirectToAction("", "Appointments");
            }
        }
        public ActionResult ViewPdf(int? id)
        {
            if (!id.HasValue) return RedirectToAction("", "appointments");
            Appointment appointment = database.Appointments.Include(a => a.Doctor).Include(a => a.Doctor.Specialization).Include(a => a.Patient).Single(a => a.Id == id.Value);

            string data =
                "Appointment Slip\n" +
                "Your Appointment ID : " + id + "\n" +
                "Appointment date: " + appointment.DateTime.ToString("yyyy MMM dd") + "\n" +
                "Name: " + appointment.Patient.Name + "\n" +
                "Email: " + appointment.Patient.Email + "\n" +
                "Reason for the Appointment: " + appointment.Reason + "\n" +
                "Doctor's Name: " + appointment.Doctor.Name + "\n" +
                "Doctor's Email: " + appointment.Doctor.Email + "\n" +
                "Doctor category: " + appointment.Doctor.Specialization.Name + "\n" +

                "\n\n\n\n\n\n Auto Generated by Medicare";



            string dataid = appointment.Patient.Name + id + ".pdf";
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            graphics.DrawString(data, font, PdfBrushes.Black, new PointF(0, 0));
            document.Save(dataid, HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            return View();
        }









    }
}