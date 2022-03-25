namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parkingspotstaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkingSpots", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.ParkingSpots", "StaffId");
            AddForeignKey("dbo.ParkingSpots", "StaffId", "dbo.Staffs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingSpots", "StaffId", "dbo.Staffs");
            DropIndex("dbo.ParkingSpots", new[] { "StaffId" });
            DropColumn("dbo.ParkingSpots", "StaffId");
        }
    }
}
