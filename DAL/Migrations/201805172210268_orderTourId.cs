namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderTourId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Tour_TourId", "dbo.Tours");
            DropIndex("dbo.Orders", new[] { "Tour_TourId" });
            RenameColumn(table: "dbo.Orders", name: "Tour_TourId", newName: "TourId");
            AlterColumn("dbo.Orders", "TourId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "TourId");
            AddForeignKey("dbo.Orders", "TourId", "dbo.Tours", "TourId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "TourId", "dbo.Tours");
            DropIndex("dbo.Orders", new[] { "TourId" });
            AlterColumn("dbo.Orders", "TourId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "TourId", newName: "Tour_TourId");
            CreateIndex("dbo.Orders", "Tour_TourId");
            AddForeignKey("dbo.Orders", "Tour_TourId", "dbo.Tours", "TourId");
        }
    }
}
