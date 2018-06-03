namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tourId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Id", "dbo.Tours");
            DropPrimaryKey("dbo.Tours");
            AddColumn("dbo.Tours", "TourId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tours", "TourId");
            AddForeignKey("dbo.Orders", "Id", "dbo.Tours", "TourId");
            DropColumn("dbo.Tours", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Orders", "Id", "dbo.Tours");
            DropPrimaryKey("dbo.Tours");
            DropColumn("dbo.Tours", "TourId");
            AddPrimaryKey("dbo.Tours", "Id");
            AddForeignKey("dbo.Orders", "Id", "dbo.Tours", "Id");
        }
    }
}
