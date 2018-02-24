namespace Futura.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetCustomerIdRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerId", c => c.String());
        }
    }
}
