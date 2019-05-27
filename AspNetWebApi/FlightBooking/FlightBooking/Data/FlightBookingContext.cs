using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FlightBooking.Models;


namespace FlightBooking.Data
{
    public class FlightBookingContext : DbContext
    {
        public FlightBookingContext() : base("ConnectionString")
        {
        }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}