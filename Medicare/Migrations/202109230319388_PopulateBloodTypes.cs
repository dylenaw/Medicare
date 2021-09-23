namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBloodTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO BloodTypes(Name) VALUES('O +')");
            Sql("INSERT INTO BloodTypes(Name) VALUES('O -')");
            Sql("INSERT INTO BloodTypes(Name) VALUES('A +')");
            Sql("INSERT INTO BloodTypes(Name) VALUES('A -')");
            Sql("INSERT INTO BloodTypes(Name) VALUES('B +')");
            Sql("INSERT INTO BloodTypes(Name) VALUES('B -')");
            Sql("INSERT INTO BloodTypes(Name) VALUES('AB +')");
            Sql("INSERT INTO BloodTypes(Name) VALUES('AB -')");
        }
        
        public override void Down()
        {
        }
    }
}
