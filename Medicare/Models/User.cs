
namespace Medicare.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public User()
        {
            IsAdmin = false;
        }
      
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name="Date of birth")]
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        [Display(Name = "Bloodtype")]
        public int? BloodTypeId { get; set; }

        [Display(Name = "Doctor registration number")]

        public string DoctorRegistration { get; set; }

        [Display(Name = "Specialization")]

        public int? SpecializationId { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsDoctor { get; set; }

        public  BloodType BloodType { get; set; }
        public  Specialization Specialization { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);

        }
    }
}
