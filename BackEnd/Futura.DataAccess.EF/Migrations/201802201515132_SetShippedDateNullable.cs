namespace Futura.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetShippedDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ShippedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ShippedDate", c => c.DateTime(nullable: false));
        }
    }
}
