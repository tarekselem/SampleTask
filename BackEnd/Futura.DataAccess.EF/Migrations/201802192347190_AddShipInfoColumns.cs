namespace Futura.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShipInfoColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ShipName", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "ShipAddress", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "ShipCity", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "ShipRegion", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "ShipPostalCode", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "ShipCountry", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShipCountry");
            DropColumn("dbo.Orders", "ShipPostalCode");
            DropColumn("dbo.Orders", "ShipRegion");
            DropColumn("dbo.Orders", "ShipCity");
            DropColumn("dbo.Orders", "ShipAddress");
            DropColumn("dbo.Orders", "ShipName");
        }
    }
}
