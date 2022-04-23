namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkPreScreeningToUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PreScreenings", "PatientId", "dbo.Patients");
            DropIndex("dbo.PreScreenings", new[] { "PatientId" });
            AddColumn("dbo.PreScreenings", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.PreScreenings", "PatientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PreScreenings", "PatientId", c => c.Int(nullable: false));
            DropColumn("dbo.PreScreenings", "UserId");
            CreateIndex("dbo.PreScreenings", "PatientId");
            AddForeignKey("dbo.PreScreenings", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
