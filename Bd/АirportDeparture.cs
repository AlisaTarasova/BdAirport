using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class АirportDeparture
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        /*[InverseProperty("АirportDeparture")]
        public List<Flight> АirportDepartures { get; set; } = new List<Flight>();
        [InverseProperty("АirportDestination")]
        public List<Flight> АirportDestinations { get; set; } = new List<Flight>();*/
    }

    public class АirportDepartureCreate
    {
        public string City { get; set; }
        public string Address { get; set; }
    }
}
