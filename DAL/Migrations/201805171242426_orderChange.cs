namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tours", "Id", "dbo.Orders");
            DropIndex("dbo.Tours", new[] { "Id" });
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Tours");
            AlterColumn("dbo.Orders", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Tours", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "Id");
            AddPrimaryKey("dbo.Tours", "Id");
            CreateIndex("dbo.Orders", "Id");
            AddForeignKey("dbo.Orders", "Id", "dbo.Tours", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Id", "dbo.Tours");
            DropIndex("dbo.Orders", new[] { "Id" });
            DropPrimaryKey("dbo.Tours");
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Tours", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tours", "Id");
            AddPrimaryKey("dbo.Orders", "Id");
            CreateIndex("dbo.Tours", "Id");
            AddForeignKey("dbo.Tours", "Id", "dbo.Orders", "Id");
        }
    }
}
