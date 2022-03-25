namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staffspeacialization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "SpecializationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Staffs", "SpecializationId");
            AddForeignKey("dbo.Staffs", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Staffs", new[] { "SpecializationId" });
            DropColumn("dbo.Staffs", "SpecializationId");
        }
    }
}
