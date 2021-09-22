namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSpecializations : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Specializations(Name,Description) VALUES('General Physician', 'For your general health queries')");
            Sql("INSERT INTO Specializations(Name,Description) VALUES('Cardiologist','For heart related matters')");
            Sql("INSERT INTO Specializations(Name,Description) VALUES('Dentist','For all your oral needs')");
            Sql("INSERT INTO Specializations(Name,Description) VALUES('Obstetrician','For womans reproductive health and pregnancy')");
            Sql("INSERT INTO Specializations(Name,Description) VALUES('Ophthalmologist','To protect your vision')");
            Sql("INSERT INTO Specializations(Name,Description) VALUES('Orthopedic surgeon','For your muscle and bone health')");
            Sql("INSERT INTO Specializations(Name,Description) VALUES('Pediatrician','For your child')");
            Sql("INSERT INTO Specializations(Name,Description) VALUES('Psychiatrist','For your mental health')");
        }
        
        public override void Down()
        {
        }
    }
}
