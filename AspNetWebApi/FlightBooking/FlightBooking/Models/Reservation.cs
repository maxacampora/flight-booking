using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.Models
{
    public class Reservation
    {
        public int Id { set; get; }
        public Customer customer { set; get; }
        public Flight flight { set; get; }
        public int seatNum { set; get; }
    }
}
