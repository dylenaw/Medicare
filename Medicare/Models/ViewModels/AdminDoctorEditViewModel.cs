using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medicare.Models.ViewModels
{
    public class AdminDoctorEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Required]
        public string DoctorRegistration { get; set; }

        [Required]
        public int? SpecializationId { get; set; }

        public List<Specialization> Specializations { get; set; }
    }
}