namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prescreeningpatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreScreenings", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.PreScreenings", "PatientId");
            AddForeignKey("dbo.PreScreenings", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PreScreenings", "PatientId", "dbo.Patients");
            DropIndex("dbo.PreScreenings", new[] { "PatientId" });
            DropColumn("dbo.PreScreenings", "PatientId");
        }
    }
}
