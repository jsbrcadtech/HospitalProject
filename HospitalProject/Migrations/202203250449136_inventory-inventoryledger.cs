namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventoryinventoryledger : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryLedgers", "InventoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.InventoryLedgers", "InventoryId");
            AddForeignKey("dbo.InventoryLedgers", "InventoryId", "dbo.Inventories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryLedgers", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.InventoryLedgers", new[] { "InventoryId" });
            DropColumn("dbo.InventoryLedgers", "InventoryId");
        }
    }
}
