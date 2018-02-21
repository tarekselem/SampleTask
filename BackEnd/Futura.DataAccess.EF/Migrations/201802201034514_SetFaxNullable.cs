namespace Futura.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetFaxNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Fax", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Fax", c => c.String(nullable: false));
        }
    }
}
