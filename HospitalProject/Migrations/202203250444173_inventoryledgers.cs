namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventoryledgers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Delta = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventoryLedgers");
        }
    }
}
