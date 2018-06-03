namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHotel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelReservations",
                c => new
                    {
                        HotelReservationId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        numberOfPersons = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Hotel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.HotelReservationId)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id)
                .Index(t => t.Hotel_Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelReservations", "Hotel_Id", "dbo.Hotels");
            DropIndex("dbo.HotelReservations", new[] { "Hotel_Id" });
            DropTable("dbo.Hotels");
            DropTable("dbo.HotelReservations");
        }
    }
}
