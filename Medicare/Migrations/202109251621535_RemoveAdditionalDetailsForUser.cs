namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAdditionalDetailsForUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "AdditionalDetails");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AdditionalDetails", c => c.String());
        }
    }
}
