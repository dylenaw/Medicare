namespace Medicare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BloodType
    {

    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRhPositive { get; set; }
    
    }
}
