using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.Models
{
    public class Flight
    {
        public int Id { set; get; }
        public string flightCode { set; get; }
        public Airport departure { set; get; }
        public Airport arrival { set; get; }
        public DateTime departureDate { set; get; }
        public DateTime arrivalDate { set; get; }
        public Airline airline { set; get; }
        public int numOfSeat { set; get; }
    }
}
