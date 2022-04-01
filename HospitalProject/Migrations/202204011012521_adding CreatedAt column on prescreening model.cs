namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingCreatedAtcolumnonprescreeningmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreScreenings", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreScreenings", "CreatedAt");
        }
    }
}
