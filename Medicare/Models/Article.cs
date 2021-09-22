namespace Medicare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public int UserId { get; set; }
        public System.DateTime Time { get; set; }
    
        public User User { get; set; }
    }
}
