namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIsDoctorField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsDoctor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsDoctor");
        }
    }
}
