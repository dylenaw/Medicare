using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medicare.Models
{
    public class Appointment
    {

        public int Id { get; set; }
        public int DoctorId { get; set; }

        public User Doctor { get; set; }
        public int? PatientId { get; set; }

        public User Patient { get; set; }
        public string Reason { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime DateTime { get; set; }

    }
}