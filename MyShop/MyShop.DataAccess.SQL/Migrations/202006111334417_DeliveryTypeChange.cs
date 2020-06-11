namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeliveryTypeChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "DeliveryType", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "PaymentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.String());
            AlterColumn("dbo.Orders", "DeliveryType", c => c.Int(nullable: false));
        }
    }
}
