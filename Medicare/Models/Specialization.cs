
namespace Medicare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Specialization
    {
    
        public int Id { get; set; }

        [Display(Name="Specialization")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
