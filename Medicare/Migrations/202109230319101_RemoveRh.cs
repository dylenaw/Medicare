namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRh : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BloodTypes", "IsRhPositive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BloodTypes", "IsRhPositive", c => c.Boolean(nullable: false));
        }
    }
}
