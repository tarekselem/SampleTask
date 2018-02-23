namespace Futura.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropPrimaryKey("dbo.Customers");
            AddColumn("dbo.Customers", "CustomerId", c => c.String());
            AlterColumn("dbo.Customers", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Orders", "CustomerId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Customers", "Id");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Customers", "CustomerId");
            AddPrimaryKey("dbo.Customers", "Id");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id");
        }
    }
}
