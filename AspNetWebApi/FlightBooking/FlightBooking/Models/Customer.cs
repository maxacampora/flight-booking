using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.Models
{
    public class Customer
    {
        public int Id { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        public DateTime birthDate { set; get; }
    }
}
