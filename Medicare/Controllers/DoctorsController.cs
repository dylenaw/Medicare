﻿using Medicare.Models;
using Medicare.Models.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicare.Controllers
{
    public class DoctorsController : Controller
    {
        ApplicationDatabase database = new ApplicationDatabase();

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        // GET: Doctors
        public ActionResult Index()
        {
            //if(!SessionHandler.IsUserAdmin(Session))
            return View();
        }



        public ActionResult View(int? id)
        {
            if (!id.HasValue)
            {
                id = 0;
            }

            User doctor = database.Users.Include(d => d.Specialization).Where(d => d.IsDoctor && d.Id == id).SingleOrDefault();
            return View(doctor);
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
                return RedirectToAction("doctors", "admin");


            }
            return RedirectToAction("","dashboard");
        }


        public ActionResult AdminEdit(int? id)
        {
            if (!id.HasValue)
            {
                id = 0;
            }

            User doctor = database.Users.Include(d => d.Specialization).Where(d => d.IsDoctor && d.Id == id).SingleOrDefault();
            List<Specialization> specializations = database.Specializations.ToList();
            return View(
                new AdminEditViewModel
                {
                    User=doctor,
                    Specializations = specializations
                }
                );
        }
        public ActionResult Edit(int? id)
        {
            return View();
        }
        public ActionResult Save(User user)
        {
            User dbDoctor = database.Users.Single(d => d.Id == user.Id);
            if (SessionHandler.IsUserAdmin(Session))
            {
                dbDoctor.DoctorRegistration = user.DoctorRegistration;
                dbDoctor.SpecializationId = user.SpecializationId;
                database.SaveChanges();


                return RedirectToAction("doctors", "admin");

            }
            database.SaveChanges();
            return RedirectToAction("", "dashboard");
        }
        public ActionResult AuthSave(User doctor)
        {
            User dbDoctor = database.Users.Single(d => d.Id == doctor.Id);
            if (SessionHandler.IsUserAdmin(Session))
            {
                dbDoctor.DoctorRegistration = doctor.DoctorRegistration;
                dbDoctor.SpecializationId = doctor.SpecializationId;
                database.SaveChanges();


                return RedirectToAction("doctorAuth", "admin");

            }
            database.SaveChanges();
            return RedirectToAction("", "dashboard");
        }
        public ActionResult ViewPdf()
        {
            if (!SessionHandler.IsUserAdmin(Session)) return RedirectToAction("", "Dashboard");



            List<User> doctors = database.Users.Include(u => u.Specialization).Where(a => a.IsDoctor).ToList();

            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("    " + "Registration ID");
            dataTable.Columns.Add("    " + "Name");
            dataTable.Columns.Add("    " + "Gender");
            dataTable.Columns.Add("    " + "Email Address");
            dataTable.Columns.Add("    " + "Category");
            dataTable.Columns.Add("    " + "Status");
            foreach (User doctor in doctors)
            {
                dataTable.Rows.Add("    " + doctor.DoctorRegistration, "    " + doctor.Name, "    " + doctor.Gender, "    " + doctor.Email, "    " + doctor.Specialization.Name, "    " + doctor.DoctorRegistration == null ? "Unauthorized" : "Authorized");
            }
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page, new PointF(10, 10));
            document.Save("Output.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            document.Close(true);
            return View();
        }
    }
}