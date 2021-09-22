namespace Medicare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(),
                        Reason = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        BirthDate = c.DateTime(),
                        Gender = c.String(),
                        Address = c.String(),
                        BloodTypeId = c.Int(),
                        DoctorRegistration = c.String(),
                        SpecializationId = c.Int(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BloodTypes", t => t.BloodTypeId)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId)
                .Index(t => t.BloodTypeId)
                .Index(t => t.SpecializationId);
            
            CreateTable(
                "dbo.BloodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsRhPositive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ImageLink = c.String(),
                        UserId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Users");
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.Users");
            DropForeignKey("dbo.Users", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Users", "BloodTypeId", "dbo.BloodTypes");
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "SpecializationId" });
            DropIndex("dbo.Users", new[] { "BloodTypeId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            DropTable("dbo.Articles");
            DropTable("dbo.Specializations");
            DropTable("dbo.BloodTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Appointments");
        }
    }
}
