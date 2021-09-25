using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicare.Models.ViewModels
{
    public class AppointmentsViewModel
    {
        public bool ShowPast { get; set; }
        public bool ShowPresent { get; set; }
        public bool ShowFuture { get; set; }

        public string Search { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }

        
    }
}