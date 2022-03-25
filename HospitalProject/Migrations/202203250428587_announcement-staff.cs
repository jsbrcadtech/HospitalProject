namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class announcementstaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.Announcements", "StaffId");
            AddForeignKey("dbo.Announcements", "StaffId", "dbo.Staffs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Announcements", "StaffId", "dbo.Staffs");
            DropIndex("dbo.Announcements", new[] { "StaffId" });
            DropColumn("dbo.Announcements", "StaffId");
        }
    }
}
