namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Id", "dbo.Tours");
            DropIndex("dbo.Orders", new[] { "Id" });
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "Tour_TourId", c => c.Int());
            AlterColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "Id");
            CreateIndex("dbo.Orders", "Tour_TourId");
            AddForeignKey("dbo.Orders", "Tour_TourId", "dbo.Tours", "TourId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Tour_TourId", "dbo.Tours");
            DropIndex("dbo.Orders", new[] { "Tour_TourId" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Tour_TourId");
            AddPrimaryKey("dbo.Orders", "Id");
            CreateIndex("dbo.Orders", "Id");
            AddForeignKey("dbo.Orders", "Id", "dbo.Tours", "TourId");
        }
    }
}
