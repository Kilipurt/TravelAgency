namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Transport", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Hotel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "NumberOfPerson", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "NumberOfPerson");
            DropColumn("dbo.Orders", "Hotel");
            DropColumn("dbo.Orders", "Transport");
        }
    }
}
