namespace FlightBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        code = c.String(),
                        city_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.city_Id)
                .Index(t => t.city_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        email = c.String(),
                        birthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        flightCode = c.String(),
                        departureDate = c.DateTime(nullable: false),
                        arrivalDate = c.DateTime(nullable: false),
                        numOfSeat = c.Int(nullable: false),
                        airline_Id = c.Int(),
                        arrival_Id = c.Int(),
                        departure_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airlines", t => t.airline_Id)
                .ForeignKey("dbo.Airports", t => t.arrival_Id)
                .ForeignKey("dbo.Airports", t => t.departure_Id)
                .Index(t => t.airline_Id)
                .Index(t => t.arrival_Id)
                .Index(t => t.departure_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        seatNum = c.Int(nullable: false),
                        customer_Id = c.Int(),
                        flight_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.customer_Id)
                .ForeignKey("dbo.Flights", t => t.flight_Id)
                .Index(t => t.customer_Id)
                .Index(t => t.flight_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "flight_Id", "dbo.Flights");
            DropForeignKey("dbo.Reservations", "customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Flights", "departure_Id", "dbo.Airports");
            DropForeignKey("dbo.Flights", "arrival_Id", "dbo.Airports");
            DropForeignKey("dbo.Flights", "airline_Id", "dbo.Airlines");
            DropForeignKey("dbo.Airports", "city_Id", "dbo.Cities");
            DropIndex("dbo.Reservations", new[] { "flight_Id" });
            DropIndex("dbo.Reservations", new[] { "customer_Id" });
            DropIndex("dbo.Flights", new[] { "departure_Id" });
            DropIndex("dbo.Flights", new[] { "arrival_Id" });
            DropIndex("dbo.Flights", new[] { "airline_Id" });
            DropIndex("dbo.Airports", new[] { "city_Id" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Flights");
            DropTable("dbo.Customers");
            DropTable("dbo.Cities");
            DropTable("dbo.Airports");
            DropTable("dbo.Airlines");
        }
    }
}
