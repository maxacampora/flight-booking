using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.Models
{
    public class Airport
    {
        public int Id { set; get; }
        public string name { set; get; }
        public string code { set; get; }
        public virtual City city { set; get; }
    }
}
