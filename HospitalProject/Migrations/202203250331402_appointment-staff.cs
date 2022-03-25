namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentstaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "StaffId");
            AddForeignKey("dbo.Appointments", "StaffId", "dbo.Staffs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "StaffId", "dbo.Staffs");
            DropIndex("dbo.Appointments", new[] { "StaffId" });
            DropColumn("dbo.Appointments", "StaffId");
        }
    }
}
