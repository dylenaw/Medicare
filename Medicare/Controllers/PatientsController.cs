using Medicare.Models;
using Medicare.Models.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web.Helpers;
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

            User patient = database.Users.Include(u => u.BloodType).Where(d => !d.IsDoctor && !d.IsAdmin && d.Id == id).SingleOrDefault();
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
                    User = user,
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

        public ActionResult ViewPdf()
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "Dashboard");
            List<User> patients = database.Users.Include(u => u.BloodType).Where(a => !a.IsDoctor && !a.IsAdmin).ToList();

            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("    " + "Name");
            dataTable.Columns.Add("    " + "Gender");
            dataTable.Columns.Add("    " + "Email Address");
            dataTable.Columns.Add("    " + "Date of birth");
            dataTable.Columns.Add("    " + "Blood type");

            foreach (User patient in patients)
            {
                string bloodtype = patient.BloodType == null ? "Not updated" : patient.BloodType.Name;
                string dob = patient.BirthDate == null ? "Not updated" : patient.BirthDate.Value.ToString("yyyy MMM dd");
                dataTable.Rows.Add("    " + patient.Name, "    " + patient.Gender, "    " + patient.Email, "    " + dob, "    " + bloodtype);
            }
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page, new PointF(10, 10));
            document.Save("Output.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            document.Close(true);
            return View();
        }

        
    }
}