namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDeliveryType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.Int(nullable: false));
        }
    }
}
