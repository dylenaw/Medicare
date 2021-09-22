
namespace Medicare.Models
{
    using System;
    using System.Collections.Generic;
    
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
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int? BloodTypeId { get; set; }
        public string DoctorRegistration { get; set; }
        public int? SpecializationId { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsDoctor { get; set; }

        public  BloodType BloodType { get; set; }
        public  Specialization Specialization { get; set; }
    }
}
