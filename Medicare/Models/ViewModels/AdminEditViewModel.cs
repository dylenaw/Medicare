using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medicare.Models.ViewModels
{
    public class AdminEditViewModel
    {
        public User User { get; set; }

        public List<BloodType> BloodTypes { get; set; }
        public List<Specialization> Specializations { get; set; }
    }
}