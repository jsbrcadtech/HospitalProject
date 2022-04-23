namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePreScreeningModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreScreenings", "Vaccinated", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreScreenings", "LastVaccinationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PreScreenings", "Cough", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreScreenings", "SoreThroat", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreScreenings", "FeverOrChills", c => c.Boolean(nullable: false));
            AddColumn("dbo.PreScreenings", "ShortnessOfBreath", c => c.Boolean(nullable: false));
            DropColumn("dbo.PreScreenings", "VaccinationStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PreScreenings", "VaccinationStatus", c => c.Int(nullable: false));
            DropColumn("dbo.PreScreenings", "ShortnessOfBreath");
            DropColumn("dbo.PreScreenings", "FeverOrChills");
            DropColumn("dbo.PreScreenings", "SoreThroat");
            DropColumn("dbo.PreScreenings", "Cough");
            DropColumn("dbo.PreScreenings", "LastVaccinationDate");
            DropColumn("dbo.PreScreenings", "Vaccinated");
        }
    }
}
