namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelForAppointment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            AddColumn("dbo.Appointments", "UserId", c => c.String(nullable: false));
            DropColumn("dbo.Appointments", "Name");
            DropColumn("dbo.Appointments", "Phone");
            DropColumn("dbo.Appointments", "Email");
            DropColumn("dbo.Appointments", "PatientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "PatientId", c => c.Int(nullable: false));
            AddColumn("dbo.Appointments", "Email", c => c.String());
            AddColumn("dbo.Appointments", "Phone", c => c.String());
            AddColumn("dbo.Appointments", "Name", c => c.String());
            DropColumn("dbo.Appointments", "UserId");
            CreateIndex("dbo.Appointments", "PatientId");
            AddForeignKey("dbo.Appointments", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
