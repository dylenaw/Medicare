using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicare.Models.ViewModels
{
    public class AppointmentCreateViewModel
    {
        public List<Specialization> Specializations { get; set; }
        public List<User> Doctors { get; set; }

        public Appointment Appointment { get; set; }

        public Specialization Specialization { get; set; }
    }
}