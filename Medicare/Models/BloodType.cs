namespace Medicare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BloodType
    {

    
        public int Id { get; set; }

        [Display(Name="Bloodtype")]
        public string Name { get; set; }
    
    }
}
