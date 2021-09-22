namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertAdminUser : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO USERS(Name,Email,Password,IsAdmin) VALUES ('System Administrator','admin@medicare.com','Pass4@123','true')");
              
        }
        
        public override void Down()
        {
        }
    }
}
