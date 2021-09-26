namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalDetailsForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AdditionalDetails", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AdditionalDetails");
        }
    }
}
